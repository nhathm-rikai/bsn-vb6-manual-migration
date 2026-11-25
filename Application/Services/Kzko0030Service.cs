using System.Globalization;
using Application.Common;
using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class Kzko0030Service : IKzko0030Service
{
    private const int MakerRecKb = 9;

    private readonly IKmZaikoRepository _zaikoRepo;
    private readonly IKmSyofRepository _syofRepo;
    private readonly IKmBnriRepository _bnriRepo;
    private readonly IKmMeisRepository _meisRepo;

    public Kzko0030Service(
        IKmZaikoRepository zaikoRepo,
        IKmSyofRepository syofRepo,
        IKmBnriRepository bnriRepo,
        IKmMeisRepository meisRepo)
    {
        _zaikoRepo = zaikoRepo;
        _syofRepo = syofRepo;
        _bnriRepo = bnriRepo;
        _meisRepo = meisRepo;
    }

    public Kzko0030DateContextDto GetDateContext(DateTime? now = null)
    {
        DateTime current = now ?? DateTime.Now;
        var firstOfCurrentMonth = new DateTime(current.Year, current.Month, 1);
        var firstOfPrevMonth = firstOfCurrentMonth.AddMonths(-1);
        var firstOfFiveMonthsAgo = firstOfCurrentMonth.AddMonths(-5);

        long g0Yymm = firstOfCurrentMonth.Year * 100L + firstOfCurrentMonth.Month;
        long g1Yymm = firstOfPrevMonth.Year * 100L + firstOfPrevMonth.Month;
        long g6Yymm = firstOfFiveMonthsAgo.Year * 100L + firstOfFiveMonthsAgo.Month;
        long g36Yymm = g0Yymm - 300L;
        long fromYmd = g0Yymm * 100L + 1L;
        long toYmd = g0Yymm * 100L + 31L;

        string g6Text = g6Yymm.ToString("000000", CultureInfo.InvariantCulture);
        string defaultYearText = g6Text[..4];
        string defaultMonthText = g6Text.Substring(4, 2);
        string systemDateText = current.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        return new Kzko0030DateContextDto(
            G0Yymm: g0Yymm,
            G1Yymm: g1Yymm,
            G6Yymm: g6Yymm,
            G36Yymm: g36Yymm,
            FromYmd: fromYmd,
            ToYmd: toYmd,
            DefaultYearText: defaultYearText,
            DefaultMonthText: defaultMonthText,
            SystemDateText: systemDateText);
    }

    public string? GetActiveProductShortName(long kshncd)
    {
        KmSyof? entity = _syofRepo.GetByCode(kshncd);
        if (entity is null || entity.DDate.HasValue)
        {
            return null;
        }

        return LegacyTextHelper.NormalizeString(entity.ShnNameS);
    }

    public IReadOnlyList<ZaikoMeisaiDto> SearchZaikoHistory(
        long kshncd,
        long startYymm,
        Kzko0030DateContextDto dateContext)
    {
        // 1. get_zaiko(GSYYMM): KMZAIKO から KZKDAY >= startYymm かつ KSHNCD = kshncd を取得
        IReadOnlyList<KmZaiko> historyRows = _zaikoRepo.GetHistoryFromMonth(kshncd, startYymm);
        if (historyRows.Count == 0)
        {
            return Array.Empty<ZaikoMeisaiDto>();
        }

        var list = new List<ZaikoMeisaiDto>(historyRows.Count + 1);
        int idx = 1;
        foreach (KmZaiko row in historyRows)
        {
            list.Add(new ZaikoMeisaiDto(
                KzkDay: row.KzkDay,
                ZnmtSuu: row.ZnmtSuu,
                ZnmtKin: row.ZnmtKin,
                TgnySuu: row.TgnySuu,
                TgnyKin: row.TgnyKin,
                TgsySuu: row.TgsySuu,
                TgsyKin: row.TgsyKin,
                TgcsSuu: row.TgcsSuu,
                TgcsKin: row.TgcsKin,
                TgmtSuu: row.TgmtSuu,
                TgmtKin: row.TgmstKin,
                Idx: idx++));
        }

        // 2. meisai_set: 当月(G0YYMM)情報の設定
        // 前月(G1YYMM)の在庫数量・簿価を取得
        (decimal TgmtSuu, long TgmstKin)? prevMonthEnd =
            _zaikoRepo.GetEndingBalance(kshncd, dateContext.G1Yymm);

        if (prevMonthEnd.HasValue)
        {
            decimal dfTgmSuu = prevMonthEnd.Value.TgmtSuu;
            long dwTgmKin = prevMonthEnd.Value.TgmstKin;

            // 当月入庫数量・簿価 (get_siirej)
            (decimal SumSirSu, long SumSirKin)? siire =
                _zaikoRepo.GetMonthlyPurchaseTotal(kshncd, dateContext.FromYmd, dateContext.ToYmd);
            decimal dfSiiSuu = siire?.SumSirSu ?? 0m;
            long dwSiiKin = siire?.SumSirKin ?? 0L;

            // 当月調整数量・簿価 (get_zaicyoj)
            (decimal SumChoSuu, long SumChoKin)? zaicyo =
                _zaikoRepo.GetMonthlyAdjustmentTotal(kshncd, dateContext.FromYmd, dateContext.ToYmd);
            decimal dfCyoSuu = zaicyo?.SumChoSuu ?? 0m;
            long dwCyoKin = zaicyo?.SumChoKin ?? 0L;

            // 当月調整数量・簿価2 (増加分 CHOKU IN 21, 22, 23: get_zaicyoj2)
            (decimal SumChoSuu, long SumChoKin)? zaicyo2 =
                _zaikoRepo.GetMonthlyPositiveAdjustmentTotal(kshncd, dateContext.FromYmd, dateContext.ToYmd);
            decimal dfCyoSuu2 = zaicyo2?.SumChoSuu ?? 0m;
            long dwCyoKin2 = zaicyo2?.SumChoKin ?? 0L;

            // 当月出庫数量・簿価 (get_uriagej)
            decimal? uriSu = _zaikoRepo.GetMonthlySalesQuantity(kshncd, dateContext.FromYmd, dateContext.ToYmd);
            decimal dfUriSuu = 0m;
            long dwUriKin = 0L;

            if (uriSu.HasValue)
            {
                dfUriSuu = uriSu.Value;
                dwUriKin = CalculateSalesBookValue(
                    suu: (double)dfUriSuu,
                    dfTgmSuu: (double)dfTgmSuu,
                    dwTgmKin: dwTgmKin,
                    dfSiiSuu: (double)dfSiiSuu,
                    dwSiiKin: dwSiiKin,
                    dfCyoSuu2: (double)dfCyoSuu2,
                    dwCyoKin2: dwCyoKin2);
            }

            // 当月末在庫数量・簿価の設定
            decimal tgmtSuu = dfTgmSuu + dfSiiSuu + dfCyoSuu - dfUriSuu;
            long tgmtKin = dwTgmKin + dwSiiKin + dwCyoKin - dwUriKin;

            list.Add(new ZaikoMeisaiDto(
                KzkDay: dateContext.G0Yymm,
                ZnmtSuu: dfTgmSuu,
                ZnmtKin: dwTgmKin,
                TgnySuu: dfSiiSuu,
                TgnyKin: dwSiiKin,
                TgsySuu: dfUriSuu,
                TgsyKin: dwUriKin,
                TgcsSuu: dfCyoSuu,
                TgcsKin: dwCyoKin,
                TgmtSuu: tgmtSuu,
                TgmtKin: tgmtKin,
                Idx: idx));
        }

        return list;
    }

    /// <summary>
    /// VB6 get_uriagej 内の当月出庫簿価計算式 (切り捨て対応)
    /// (当月出庫数量 / (前月末在庫数量 + 当月入庫数量 [+ 当月調整増加数量])) * (前月末在庫簿価 + 当月入庫簿価 [+ 当月調整増加簿価])
    /// </summary>
    private static long CalculateSalesBookValue(
        double suu,
        double dfTgmSuu,
        long dwTgmKin,
        double dfSiiSuu,
        long dwSiiKin,
        double dfCyoSuu2,
        long dwCyoKin2)
    {
        if (dfCyoSuu2 > 0 || dwCyoKin2 > 0)
        {
            double denom = dfTgmSuu + dfSiiSuu + dfCyoSuu2;
            if (denom == 0)
            {
                return 0L;
            }

            double kinWk = (suu / denom) * (dwTgmKin + dwSiiKin + dwCyoKin2);
            return (long)Math.Truncate(kinWk);
        }
        else
        {
            double denom = dfTgmSuu + dfSiiSuu;
            if (denom == 0)
            {
                return 0L;
            }

            double kinWk = (suu / denom) * (dwTgmKin + dwSiiKin);
            return (long)Math.Truncate(kinWk);
        }
    }

    // =========================================================================
    // SYOKEN (商品コード検索ダイアログ) 用メソッド
    // =========================================================================

    public IReadOnlyList<SyokenCategoryItemDto> GetMajorCategories()
    {
        IReadOnlyList<KmBnri> rows = _bnriRepo.GetActiveMajorCategories();
        return rows.Select(r =>
        {
            string codeText = r.KDaiCd.ToString("00", CultureInfo.InvariantCulture);
            string name = LegacyTextHelper.NormalizeString(r.DtName);
            return new SyokenCategoryItemDto(
                Code: r.KDaiCd,
                Name: name,
                DisplayText: $"{codeText}  {name}");
        }).ToList();
    }

    public IReadOnlyList<SyokenCategoryItemDto> GetMinorCategories(int daiCd)
    {
        IReadOnlyList<KmBnri> rows = _bnriRepo.GetActiveMinorCategories(daiCd);
        return rows.Select(r =>
        {
            string codeText = r.KTyuCd.ToString("00", CultureInfo.InvariantCulture);
            string name = LegacyTextHelper.NormalizeString(r.DtName);
            return new SyokenCategoryItemDto(
                Code: r.KTyuCd,
                Name: name,
                DisplayText: $"{codeText}  {name}");
        }).ToList();
    }

    public IReadOnlyList<MakerLookupItem> GetMakersByMajorCategory(int daiCd)
    {
        int minCode = daiCd * 100;
        int maxCode = daiCd * 100 + 99;
        IReadOnlyList<KmMeis> rows = _meisRepo.GetActiveByRange(MakerRecKb, minCode, maxCode);
        return rows.Select(r =>
        {
            string shortName = LegacyTextHelper.NormalizeString(r.MeisyoS);
            return new MakerLookupItem
            {
                Code = r.KMeisCd,
                FullName = shortName,
                ShortName = shortName
            };
        }).ToList();
    }

    public IReadOnlyList<SyokenProductItemDto> SearchProductsByCategory(int daiCd, int tyuCd, int? mekaCd)
    {
        IReadOnlyList<KmSyof> rows = _syofRepo.SearchActiveByCategory(daiCd, tyuCd, mekaCd);
        return MapProductItems(rows);
    }

    public IReadOnlyList<SyokenProductItemDto> SearchProductsByName(
        string keyword,
        SyokenNameMatchMode matchMode,
        bool fuzzySearch,
        int? daiCd,
        int? tyuCd,
        int? mekaCd)
    {
        IReadOnlyList<KmSyof> rows = _syofRepo.SearchActiveByName(
            keyword ?? string.Empty,
            (int)matchMode,
            fuzzySearch,
            daiCd,
            tyuCd,
            mekaCd);
        return MapProductItems(rows);
    }

    private static IReadOnlyList<SyokenProductItemDto> MapProductItems(IReadOnlyList<KmSyof> rows)
    {
        return rows.Select(r =>
        {
            string codeText = r.KShnCd.ToString("00000000", CultureInfo.InvariantCulture);
            string nameKn = LegacyTextHelper.NormalizeString(r.ShnNameKn);
            return new SyokenProductItemDto(
                KShnCd: r.KShnCd,
                CodeText: codeText,
                ShnNameKn: nameKn,
                DisplayText: $"{codeText}  {nameKn}");
        }).ToList();
    }
}
