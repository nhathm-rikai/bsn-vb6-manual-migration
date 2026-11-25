using System.Globalization;
using Application.Common;
using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class Kzko0020Service : IKzko0020Service
{
    private const int MakerRecKb = 9;

    private readonly IKmLockRepository _lockRepo;
    private readonly IKtZjnlRepository _zjnlRepo;
    private readonly IKmTantRepository _tantRepo;
    private readonly IKmSyofRepository _syofRepo;
    private readonly IKmBnriRepository _bnriRepo;
    private readonly IKmMeisRepository _meisRepo;

    public Kzko0020Service(
        IKmLockRepository lockRepo,
        IKtZjnlRepository zjnlRepo,
        IKmTantRepository tantRepo,
        IKmSyofRepository syofRepo,
        IKmBnriRepository bnriRepo,
        IKmMeisRepository meisRepo)
    {
        _lockRepo = lockRepo;
        _zjnlRepo = zjnlRepo;
        _tantRepo = tantRepo;
        _syofRepo = syofRepo;
        _bnriRepo = bnriRepo;
        _meisRepo = meisRepo;
    }

    public void CheckDatabaseConnection()
    {
        _lockRepo.TestConnection();
    }

    public Kzko0020InitContextDto GetInitContext(DateTime? now = null)
    {
        DateTime sysNow = now ?? DateTime.Now;
        long? keiYmOpt = _lockRepo.GetKeiYm(1);
        if (!keiYmOpt.HasValue)
        {
            throw new InvalidOperationException("ロックマスタ(KRECKBN=1)にデータが存在しません。");
        }

        long svKeiYm = keiYmOpt.Value;
        long svKeiYmd = svKeiYm * 100L + 99L;

        string ymStr = svKeiYm.ToString("000000", CultureInfo.InvariantCulture);
        if (ymStr.Substring(4, 2) == "12")
        {
            svKeiYm += 89L;
        }
        else
        {
            svKeiYm += 1L;
        }

        string nextYmStr = svKeiYm.ToString("000000", CultureInfo.InvariantCulture);
        string glbCyy = nextYmStr[..4];
        string glbCmm = nextYmStr.Substring(4, 2);
        string glbCdd = glbCmm switch
        {
            "01" or "03" or "05" or "07" or "08" or "10" or "12" => "31",
            "04" or "06" or "09" or "11" => "30",
            "02" => "28",
            _ => string.Empty
        };

        return new Kzko0020InitContextDto(
            SvKeiYm: svKeiYm,
            SvKeiYmd: svKeiYmd,
            GlbCyy: glbCyy,
            GlbCmm: glbCmm,
            GlbCdd: glbCdd,
            SystemDateText: sysNow.ToString("yy/MM/dd", CultureInfo.InvariantCulture));
    }

    public string? GetActiveTantShortName(int ktantCd)
    {
        KmTant? row = _tantRepo.GetByCode(ktantCd);
        if (row is null || row.DDate.HasValue)
        {
            return null;
        }

        return LegacyTextHelper.NormalizeString(row.TantNameS);
    }

    public IReadOnlyList<TantLookupItem> GetTantLookupList()
    {
        IReadOnlyList<KmTant> rows = _tantRepo.GetAllActive();
        return rows.Select(r =>
        {
            string codeText = r.KTantCd.ToString("0000", CultureInfo.InvariantCulture);
            string nameKj = LegacyTextHelper.NormalizeString(r.TantNameKj).Trim();
            string shortName = LegacyTextHelper.NormalizeString(r.TantNameS);
            return new TantLookupItem(
                Code: r.KTantCd,
                CodeText: codeText,
                NameKj: nameKj,
                ShortName: shortName,
                DisplayText: $"{codeText}  {nameKj}");
        }).ToList();
    }

    public Kzko0020ProductCheckResult CheckProduct(long kshncd)
    {
        KmSyof? syof = _syofRepo.GetByCode(kshncd);
        if (syof is null || syof.DDate.HasValue)
        {
            return new Kzko0020ProductCheckResult(
                Status: Kzko0020ProductCheckStatus.NotFound,
                ProductNameKn: string.Empty,
                Keiridds: 0,
                ErrorMessage: $"入力された{kshncd:00000000}コードは存在しません。");
        }

        if (syof.ZkKb != 1)
        {
            return new Kzko0020ProductCheckResult(
                Status: Kzko0020ProductCheckStatus.NotStockItem,
                ProductNameKn: string.Empty,
                Keiridds: 0,
                ErrorMessage: "指定された商品は在庫品ではありません。");
        }

        int keiridds = 0;
        KmBnri? bnri = _bnriRepo.GetByKey(syof.DaiCd, syof.TyuCd);
        if (bnri is not null && !bnri.DDate.HasValue)
        {
            keiridds = bnri.Keiridds ?? 0;
        }

        return new Kzko0020ProductCheckResult(
            Status: Kzko0020ProductCheckStatus.Ok,
            ProductNameKn: LegacyTextHelper.NormalizeString(syof.ShnNameKn),
            Keiridds: keiridds,
            ErrorMessage: string.Empty);
    }

    public Kzko0020ZjnlLoadResult LoadZjnl(long kdenno)
    {
        KtZjnl? row = _zjnlRepo.GetByDenNo(kdenno);
        if (row is null)
        {
            return new Kzko0020ZjnlLoadResult(
                Status: Kzko0020ZjnlLoadStatus.NotFound,
                SwkFlg: 0,
                NTanCdText: string.Empty,
                NTanNameText: string.Empty,
                CyyText: string.Empty,
                CmmText: string.Empty,
                CddText: string.Empty,
                SyoCdText: string.Empty,
                SyoNameText: string.Empty,
                ProductWarningMessage: null,
                Keiridds: 0,
                ChKbnText: string.Empty,
                IsNegativeAdjustment: false,
                NewZSuuText: string.Empty,
                NewZBokaText: string.Empty,
                TekiyoText: string.Empty);
        }

        if (row.SwkFlg == 1)
        {
            return new Kzko0020ZjnlLoadResult(
                Status: Kzko0020ZjnlLoadStatus.AlreadyJournalized,
                SwkFlg: 1,
                NTanCdText: string.Empty,
                NTanNameText: string.Empty,
                CyyText: string.Empty,
                CmmText: string.Empty,
                CddText: string.Empty,
                SyoCdText: string.Empty,
                SyoNameText: string.Empty,
                ProductWarningMessage: null,
                Keiridds: 0,
                ChKbnText: string.Empty,
                IsNegativeAdjustment: false,
                NewZSuuText: string.Empty,
                NewZBokaText: string.Empty,
                TekiyoText: string.Empty);
        }

        string ntanCdText = row.STantCd.ToString("0000", CultureInfo.InvariantCulture);
        string ntanNameText = GetActiveTantShortName(row.STantCd) ?? string.Empty;

        string chodayStr = row.ChoDay.ToString("00000000", CultureInfo.InvariantCulture);
        string cyyText = chodayStr[..4];
        string cmmText = chodayStr.Substring(4, 2);
        string cddText = chodayStr.Substring(6, 2);
        string syoCdText = row.ShnCd.ToString("00000000", CultureInfo.InvariantCulture);

        Kzko0020ProductCheckResult prodCheck = CheckProduct(row.ShnCd);
        string syoNameText = prodCheck.Status == Kzko0020ProductCheckStatus.Ok
            ? prodCheck.ProductNameKn
            : string.Empty;
        string? productWarning = prodCheck.Status == Kzko0020ProductCheckStatus.Ok
            ? null
            : prodCheck.ErrorMessage;

        string chKbnText = row.Choku.ToString("00", CultureInfo.InvariantCulture);
        bool isNegative = row.Choku is 10 or 11 or 12 or 13;

        string formattedSuu = Math.Abs(row.ChoSuu).ToString("#,##0.00", CultureInfo.InvariantCulture);
        string newZSuuText = formattedSuu.Length >= 14
            ? formattedSuu[..14]
            : formattedSuu.PadLeft(14, ' ');

        long absKinLong = (long)Math.Truncate(Math.Abs(row.ChoKin));
        string formattedBoka = absKinLong.ToString("#,##0", CultureInfo.InvariantCulture);
        string newZBokaText = formattedBoka.Length >= 13
            ? formattedBoka[..13]
            : formattedBoka.PadLeft(13, ' ');

        string tekiyoText = LegacyTextHelper.NormalizeString(row.Tekiyo);

        return new Kzko0020ZjnlLoadResult(
            Status: Kzko0020ZjnlLoadStatus.Ok,
            SwkFlg: row.SwkFlg,
            NTanCdText: ntanCdText,
            NTanNameText: ntanNameText,
            CyyText: cyyText,
            CmmText: cmmText,
            CddText: cddText,
            SyoCdText: syoCdText,
            SyoNameText: syoNameText,
            ProductWarningMessage: productWarning,
            Keiridds: prodCheck.Keiridds,
            ChKbnText: chKbnText,
            IsNegativeAdjustment: isNegative,
            NewZSuuText: newZSuuText,
            NewZBokaText: newZBokaText,
            TekiyoText: tekiyoText);
    }

    public long InsertAdjustment(
        int stantCd,
        long choDay,
        long shnCd,
        int choku,
        decimal absSuu,
        decimal absKin,
        string tekiyo,
        out long assignedDenNo,
        DateTime? now = null)
    {
        DateTime sysDate = (now ?? DateTime.Today).Date;
        bool isNegative = choku is 10 or 11 or 12 or 13;
        decimal choSuu = isNegative ? -Math.Abs(absSuu) : Math.Abs(absSuu);
        decimal choKin = isNegative ? -Math.Abs(absKin) : Math.Abs(absKin);

        var entity = new KtZjnl
        {
            TDate = sysDate,
            KDate = sysDate,
            ChoDay = choDay,
            STantCd = stantCd,
            ShnCd = shnCd,
            Choku = choku,
            ChoSuu = choSuu,
            ChoKin = choKin,
            Tekiyo = tekiyo,
            ZkhnFlg = 0,
            GtjFlg = 0,
            SwkFlg = 0
        };

        try
        {
            long denNo = _zjnlRepo.InsertWithSequence(entity);
            assignedDenNo = denNo;
            return denNo;
        }
        catch
        {
            assignedDenNo = entity.KDenNo;
            throw;
        }
    }

    public void UpdateAdjustment(
        long kdenNo,
        int stantCd,
        long choDay,
        long shnCd,
        int choku,
        decimal absSuu,
        decimal absKin,
        string tekiyo,
        DateTime? now = null)
    {
        DateTime sysDate = (now ?? DateTime.Today).Date;
        bool isNegative = choku is 10 or 11 or 12 or 13;
        decimal choSuu = isNegative ? -Math.Abs(absSuu) : Math.Abs(absSuu);
        decimal choKin = isNegative ? -Math.Abs(absKin) : Math.Abs(absKin);

        var entity = new KtZjnl
        {
            KDate = sysDate,
            KDenNo = kdenNo,
            ChoDay = choDay,
            STantCd = stantCd,
            ShnCd = shnCd,
            Choku = choku,
            ChoSuu = choSuu,
            ChoKin = choKin,
            Tekiyo = tekiyo,
            ZkhnFlg = 0,
            GtjFlg = 0,
            SwkFlg = 0
        };

        _zjnlRepo.Update(entity);
    }

    public void DeleteAdjustment(long kdenNo)
    {
        _zjnlRepo.Delete(kdenNo);
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

