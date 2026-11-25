using System.Globalization;
using Application.Common;
using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class Kzko0010Service : IKzko0010Service
{
    private const int LockRecKbn = 1;
    private const int DepartmentRecKb = 4;

    private readonly IKmLockRepository _lockRepo;
    private readonly IKmMeisRepository _meisRepo;
    private readonly IKmSyofRepository _syofRepo;
    private readonly IKmZaikoRepository _zaikoRepo;

    public Kzko0010Service(
        IKmLockRepository lockRepo,
        IKmMeisRepository meisRepo,
        IKmSyofRepository syofRepo,
        IKmZaikoRepository zaikoRepo)
    {
        _lockRepo = lockRepo;
        _meisRepo = meisRepo;
        _syofRepo = syofRepo;
        _zaikoRepo = zaikoRepo;
    }

    public void CheckDatabaseConnection()
    {
        _lockRepo.TestConnection();
    }

    public Kzko0010InitContextDto GetInitContext(DateTime? now = null)
    {
        DateTime current = now ?? DateTime.Now;
        long? keiYm = _lockRepo.GetKeiYm(LockRecKbn);
        if (!keiYm.HasValue)
        {
            throw new InvalidOperationException("ロックマスタ(KRECKBN=1)にデータが存在しません。");
        }

        long svKeiYm = keiYm.Value;
        string svKeiYmStr = svKeiYm.ToString(CultureInfo.InvariantCulture);

        // VB6:
        // If Mid$(svKEIYM, 5, 2) = "12" Then svKEIYM = svKEIYM + 89 Else svKEIYM = svKEIYM + 1
        if (svKeiYmStr.Length >= 6 && svKeiYmStr.Substring(4, 2) == "12")
        {
            svKeiYm += 89;
        }
        else
        {
            svKeiYm += 1;
        }

        long svKeiYmd = svKeiYm * 100L + 1L;
        string nextYmStr = svKeiYm.ToString("000000", CultureInfo.InvariantCulture);

        return new Kzko0010InitContextDto(
            SvKeiYm: svKeiYm,
            SvKeiYmd: svKeiYmd,
            DefaultYearText: nextYmStr[..4],
            DefaultMonthText: nextYmStr.Substring(4, 2),
            DefaultDayText: "31",
            SystemDateText: current.ToString("yy/MM/dd", CultureInfo.InvariantCulture));
    }

    public string? GetDepartmentName(int bmnCd)
    {
        return _meisRepo.GetShortNameRaw(DepartmentRecKb, bmnCd);
    }

    public Kzko0010ProductLookupResultDto LookupProduct(long kshncd)
    {
        KmSyof? entity = _syofRepo.GetByCode(kshncd);
        if (entity is null || entity.DDate.HasValue)
        {
            return new Kzko0010ProductLookupResultDto(Kzko0010ProductLookupStatus.NotFound, string.Empty);
        }

        if (entity.ZkKb == 0)
        {
            return new Kzko0010ProductLookupResultDto(Kzko0010ProductLookupStatus.NotInventoryTarget, string.Empty);
        }

        return new Kzko0010ProductLookupResultDto(
            Kzko0010ProductLookupStatus.Found,
            LegacyTextHelper.NormalizeString(entity.ShnNameS));
    }

    public IReadOnlyList<Kzko0010TargetProductDto> GetTargetProductsByDepartment(int bmCd)
    {
        IReadOnlyList<KmSyof> rows = _syofRepo.GetActiveStockTargetsByDepartment(bmCd);
        return rows.Select(r => new Kzko0010TargetProductDto(r.KShnCd, r.InSu, r.ZkSu, r.Boka)).ToList();
    }

    public IReadOnlyList<Kzko0010TargetProductDto> GetTargetProductsByCode(long kshncd)
    {
        IReadOnlyList<KmSyof> rows = _syofRepo.GetActiveStockTargetByCode(kshncd);
        return rows.Select(r => new Kzko0010TargetProductDto(r.KShnCd, r.InSu, r.ZkSu, r.Boka)).ToList();
    }

    public void ExecuteZaikoUpdate(
        int year,
        int month,
        int day,
        IReadOnlyList<Kzko0010TargetProductDto> products,
        Action<int>? onItemProcessed = null)
    {
        // VB6 cmdSTR_Click 範囲日付設定
        long fromYmd;
        if (day < 31)
        {
            if (month == 1)
            {
                fromYmd = (year - 1) * 10000L + 1200L + day + 1L;
            }
            else
            {
                fromYmd = year * 10000L + (month - 1) * 100L + day + 1L;
            }
        }
        else
        {
            fromYmd = year * 10000L + month * 100L;
        }

        long toYmd = year * 10000L + month * 100L + day;

        // VB6 upd_zaiko 当月・前月算出
        long dwTouYmd = year * 100L + month;
        long dwZenYmd = month == 1
            ? (year - 1) * 100L + 12L
            : dwTouYmd - 1L;

        // Step 1: 論理削除・在庫対象外商品の当月在庫マスタを削除 (del_KMZAIKO)
        try
        {
            _zaikoRepo.DeleteInvalidProductsForMonth(dwTouYmd);
        }
        catch (Exception ex)
        {
            throw new Kzko0010SqlContextException("在庫マスタ削除", $" {dwTouYmd}", ex);
        }

        DateTime today = DateTime.Today;

        // Step 2: 対象商品ごとに在庫マスタを登録/更新
        for (int i = 0; i < products.Count; i++)
        {
            Kzko0010TargetProductDto product = products[i];
            long kshncd = product.KShnCd;
            string strKey = kshncd.ToString("00000000", CultureInfo.InvariantCulture);

            // 前月在庫数量・簿価の設定 (get_zaiko(dwZENYMD, ...))
            (decimal TgmtSuu, long TgmstKin)? prevMonthEnd;
            try
            {
                prevMonthEnd = _zaikoRepo.GetEndingBalance(kshncd, dwZenYmd);
            }
            catch (Exception ex)
            {
                throw new Kzko0010SqlContextException("在庫ﾏｽﾀ", strKey, ex);
            }

            decimal dfZnmtSuu;
            long dwZnmtKin;
            if (!prevMonthEnd.HasValue)
            {
                dfZnmtSuu = product.ZkSu;
                dwZnmtKin = Convert.ToInt64(Math.Round(product.Boka, MidpointRounding.ToEven));
            }
            else
            {
                dfZnmtSuu = prevMonthEnd.Value.TgmtSuu;
                dwZnmtKin = prevMonthEnd.Value.TgmstKin;
            }

            // 当月入庫数量・簿価の設定 (get_siirej)
            (decimal SumSirSu, long SumSirKin)? siire;
            try
            {
                siire = _zaikoRepo.GetMonthlyPurchaseTotal(kshncd, fromYmd, toYmd);
            }
            catch (Exception ex)
            {
                throw new Kzko0010SqlContextException("仕入ｼﾞｬｰﾅﾙ", strKey, ex);
            }

            decimal dfTgnySuu = siire?.SumSirSu ?? 0m;
            long dwTgnyKin = siire?.SumSirKin ?? 0L;

            // 当月調整数量・簿価の設定 (get_zaicyoj)
            (decimal SumChoSuu, long SumChoKin)? zaicyo;
            try
            {
                zaicyo = _zaikoRepo.GetMonthlyAdjustmentTotal(kshncd, fromYmd, toYmd);
            }
            catch (Exception ex)
            {
                throw new Kzko0010SqlContextException("在庫調整ｼﾞｬｰﾅﾙ", strKey, ex);
            }

            decimal dfTgcsSuu = zaicyo?.SumChoSuu ?? 0m;
            long dwTgcsKin = zaicyo?.SumChoKin ?? 0L;

            // 当月調整数量・簿価2 (増加分 CHOKU IN 21, 22, 23: get_zaicyoj2)
            (decimal SumChoSuu, long SumChoKin)? zaicyo2;
            try
            {
                zaicyo2 = _zaikoRepo.GetMonthlyPositiveAdjustmentTotal(kshncd, fromYmd, toYmd);
            }
            catch (Exception ex)
            {
                throw new Kzko0010SqlContextException("在庫調整ｼﾞｬｰﾅﾙ", strKey, ex);
            }

            decimal dfTgcsSuu2 = zaicyo2?.SumChoSuu ?? 0m;
            long dwTgcsKin2 = zaicyo2?.SumChoKin ?? 0L;

            // 当月出庫数量・簿価の設定 (get_uriagej)
            decimal? uriSu;
            try
            {
                uriSu = _zaikoRepo.GetMonthlySalesQuantity(kshncd, fromYmd, toYmd);
            }
            catch (Exception ex)
            {
                throw new Kzko0010SqlContextException("売上ｼﾞｬｰﾅﾙ", strKey, ex);
            }

            decimal dfTgsySuu = 0m;
            long dwTgsyKin = 0L;
            if (uriSu.HasValue)
            {
                dfTgsySuu = uriSu.Value;
                dwTgsyKin = CalculateSalesBookValue(
                    suu: (double)dfTgsySuu,
                    dfZnmtSuu: (double)dfZnmtSuu,
                    dwZnmtKin: dwZnmtKin,
                    dfTgnySuu: (double)dfTgnySuu,
                    dwTgnyKin: dwTgnyKin,
                    dfTgcsSuu2: (double)dfTgcsSuu2,
                    dwTgcsKin2: dwTgcsKin2);
            }

            // 当月末在庫数量・簿価の設定
            decimal dfTgmtSuu = dfZnmtSuu + dfTgnySuu - dfTgsySuu + dfTgcsSuu;
            long dwTgmstKin = dwZnmtKin + dwTgnyKin - dwTgsyKin + dwTgcsKin;

            // 当月在庫マスタ存在確認 (get_zaiko(dwTOUYMD, ...))
            (decimal TgmtSuu, long TgmstKin)? existing;
            try
            {
                existing = _zaikoRepo.GetEndingBalance(kshncd, dwTouYmd);
            }
            catch (Exception ex)
            {
                throw new Kzko0010SqlContextException("在庫ﾏｽﾀ", strKey, ex);
            }

            var entity = new KmZaiko
            {
                TDate = today,
                KDate = today,
                DDate = null,
                KzkDay = dwTouYmd,
                KShnCd = kshncd,
                ZnmtSuu = dfZnmtSuu,
                ZnmtKin = dwZnmtKin,
                TgnySuu = dfTgnySuu,
                TgnyKin = dwTgnyKin,
                TgsySuu = dfTgsySuu,
                TgsyKin = dwTgsyKin,
                TgcsSuu = dfTgcsSuu,
                TgcsKin = dwTgcsKin,
                TgmtSuu = dfTgmtSuu,
                TgmstKin = dwTgmstKin
            };

            if (!existing.HasValue)
            {
                try
                {
                    _zaikoRepo.Insert(entity);
                }
                catch (Exception ex)
                {
                    throw new Kzko0010SqlContextException("在庫ﾏｽﾀ", "INSERT", ex);
                }
            }
            else
            {
                try
                {
                    _zaikoRepo.Update(entity);
                }
                catch (Exception ex)
                {
                    throw new Kzko0010SqlContextException("在庫ﾏｽﾀ", "UPDATE", ex);
                }
            }

            onItemProcessed?.Invoke(i + 1);
        }
    }

    /// <summary>
    /// VB6 KZKO0010 get_uriagej 内の当月出庫簿価計算式 (切り捨て対応)
    /// (当月出庫数量 / (前月末在庫数量 + 当月入庫数量 [+ 当月調整増加数量])) * (前月末在庫簿価 + 当月入庫簿価 [+ 当月調整増加簿価])
    /// </summary>
    private static long CalculateSalesBookValue(
        double suu,
        double dfZnmtSuu,
        long dwZnmtKin,
        double dfTgnySuu,
        long dwTgnyKin,
        double dfTgcsSuu2,
        long dwTgcsKin2)
    {
        if (dfTgcsSuu2 > 0 || dwTgcsKin2 > 0)
        {
            double denom = dfZnmtSuu + dfTgnySuu + dfTgcsSuu2;
            if (denom == 0)
            {
                return 0L;
            }

            double kinWk = (suu / denom) * (dwZnmtKin + dwTgnyKin + dwTgcsKin2);
            return (long)Math.Truncate(kinWk);
        }
        else
        {
            double denom = dfZnmtSuu + dfTgnySuu;
            if (denom == 0)
            {
                return 0L;
            }

            double kinWk = (suu / denom) * (dwZnmtKin + dwTgnyKin);
            return (long)Math.Truncate(kinWk);
        }
    }
}

