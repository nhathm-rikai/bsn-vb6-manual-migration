using System.Globalization;
using Application.Common;
using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

/// <summary>
/// KTBO0020 (売上履歴照会) アプリケーションサービス
/// </summary>
public class Ktbo0020Service : IKtbo0020Service
{
    private const int MakerRecKb = 9;
    private const int MaxDisplayRows = 999;

    private readonly IKtUjnlRepository _ujnlRepo;
    private readonly IKmKyakRepository _kyakRepo;
    private readonly IKmTantRepository _tantRepo;
    private readonly IKmBnriRepository _bnriRepo;
    private readonly IKmSyofRepository _syofRepo;
    private readonly IKmMeisRepository _meisRepo;

    public Ktbo0020Service(
        IKtUjnlRepository ujnlRepo,
        IKmKyakRepository kyakRepo,
        IKmTantRepository tantRepo,
        IKmBnriRepository bnriRepo,
        IKmSyofRepository syofRepo,
        IKmMeisRepository meisRepo)
    {
        _ujnlRepo = ujnlRepo;
        _kyakRepo = kyakRepo;
        _tantRepo = tantRepo;
        _bnriRepo = bnriRepo;
        _syofRepo = syofRepo;
        _meisRepo = meisRepo;
    }

    public void VerifyConnection()
    {
        _ujnlRepo.TestConnection();
    }

    public string? GetActiveCustomerName(string hbsCd)
    {
        KmKyak? entity = _kyakRepo.GetByCode(hbsCd);
        if (entity is null || entity.DDate.HasValue)
        {
            return null;
        }

        return LegacyTextHelper.NormalizeString(entity.KykNameKj);
    }

    public string? GetActiveTantName(int tantCd)
    {
        KmTant? entity = _tantRepo.GetByCode(tantCd);
        if (entity is null || entity.DDate.HasValue)
        {
            return null;
        }

        return LegacyTextHelper.NormalizeString(entity.TantNameKj);
    }

    public string? GetActiveMajorCategoryName(int daiCd)
    {
        KmBnri? entity = _bnriRepo.GetByKey(daiCd, 0);
        if (entity is null || entity.DDate.HasValue)
        {
            return null;
        }

        return LegacyTextHelper.NormalizeString(entity.DtName);
    }

    public string? GetActiveMinorCategoryName(int daiCd, int tyuCd)
    {
        KmBnri? entity = _bnriRepo.GetByKey(daiCd, tyuCd);
        if (entity is null || entity.DDate.HasValue)
        {
            return null;
        }

        return LegacyTextHelper.NormalizeString(entity.DtName);
    }

    public string? GetActiveProductKanaName(long shnCd)
    {
        KmSyof? entity = _syofRepo.GetByCode(shnCd);
        if (entity is null || entity.DDate.HasValue)
        {
            return null;
        }

        return LegacyTextHelper.NormalizeString(entity.ShnNameKn);
    }

    public Ktbo0020SearchResultDto SearchTransactions(Ktbo0020SearchCriteriaDto criteria)
    {
        // 最大1000件取得し、999件超を検知する (VB6 allist_Set 1:1)
        IReadOnlyList<KtUjnlInquiryRow> rawRows = _ujnlRepo.SearchTransactions(
            criteria.HbsCd,
            criteria.StantCd,
            criteria.DaiCd,
            criteria.TyuCd,
            criteria.ShnCd,
            criteria.FromYmd,
            criteria.ToYmd,
            maxRows: MaxDisplayRows + 1);

        if (rawRows.Count == 0)
        {
            return new Ktbo0020SearchResultDto
            {
                Rows = Array.Empty<Ktbo0020RowDto>(),
                TruncatedOver999 = false,
                TotalPages = 0
            };
        }

        bool truncatedOver999 = rawRows.Count > MaxDisplayRows;
        int displayCount = Math.Min(rawRows.Count, MaxDisplayRows);

        var customerCache = new Dictionary<string, string>(StringComparer.Ordinal);
        var resultRows = new List<Ktbo0020RowDto>(displayCount);

        for (int i = 0; i < displayCount; i++)
        {
            KtUjnlInquiryRow row = rawRows[i];
            string hbsCdTrim = row.Hbscd.Trim();

            if (!customerCache.TryGetValue(hbsCdTrim, out string? customerName))
            {
                string? activeName = GetActiveCustomerName(hbsCdTrim);
                if (activeName is not null)
                {
                    customerName = activeName;
                }
                else
                {
                    // VB6 KTBO0021.FRM line 3080: 未存在時は10桁販売先コード設定
                    customerName = long.TryParse(hbsCdTrim, NumberStyles.Integer, CultureInfo.InvariantCulture, out long codeNum)
                        ? codeNum.ToString("0000000000", CultureInfo.InvariantCulture)
                        : hbsCdTrim.PadLeft(10, '0');
                }

                customerCache[hbsCdTrim] = customerName;
            }

            resultRows.Add(new Ktbo0020RowDto
            {
                RowNo = i + 1,
                HbsCd = hbsCdTrim,
                CustomerName = customerName,
                Kdenno = row.Kdenno,
                Hbiday = row.Hbiday,
                Urisu = row.Urisu,
                Uritan = row.Uritan,
                Hankin = row.Hankin,
                ShnNameN1 = row.ShnNameN1,
                ShnNameN2 = row.ShnNameN2,
                StantCd = row.StantCd
            });
        }

        int totalPages = (displayCount + 9) / 10;

        return new Ktbo0020SearchResultDto
        {
            Rows = resultRows,
            TruncatedOver999 = truncatedOver999,
            TotalPages = totalPages
        };
    }

    // =========================================================================
    // ITankenLookupService (TANKEN 共通検索ダイアログ用)
    // =========================================================================

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

    // =========================================================================
    // ISyokenLookupService (SYOKEN 共通検索ダイアログ用)
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

