using System.Globalization;
using Application.Common;
using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class Kuno0040Service : IKuno0040Service
{
    private readonly IKmKyakRepository _kyakRepo;
    private readonly IKmGrpfRepository _grpfRepo;
    private readonly IKmSyzkRepository _syzkRepo;
    private readonly IKmMeisRepository _meisRepo;
    private readonly IKmGnkoRepository _gnkoRepo;

    public Kuno0040Service(
        IKmKyakRepository kyakRepo,
        IKmGrpfRepository grpfRepo,
        IKmSyzkRepository syzkRepo,
        IKmMeisRepository meisRepo,
        IKmGnkoRepository gnkoRepo)
    {
        _kyakRepo = kyakRepo;
        _grpfRepo = grpfRepo;
        _syzkRepo = syzkRepo;
        _meisRepo = meisRepo;
        _gnkoRepo = gnkoRepo;
    }

    public void CheckDatabaseConnection()
    {
        _kyakRepo.TestConnection();
    }

    public KmKyak? GetCustomerByCode(string kkykCd)
    {
        return _kyakRepo.GetByCode(kkykCd);
    }

    public string? GetActiveCustomerShortName(string kkykCd)
    {
        return _kyakRepo.GetActiveShortName(kkykCd);
    }

    public Kuno0040GroupInfoDto? GetActiveGroupInfo(int gpkCd)
    {
        KmGrpf? row = _grpfRepo.GetActiveByCode(gpkCd);
        if (row is null)
        {
            return null;
        }

        return new Kuno0040GroupInfoDto(row.GpkNameS, row.KyoDd, row.KysCd);
    }

    public string? GetActiveAffiliationShortName(int szCd)
    {
        KmSyzk? row = _syzkRepo.GetByCode(szCd);
        if (row is null || row.DDate.HasValue)
        {
            return null;
        }

        return LegacyTextHelper.NormalizeString(row.SzNameS);
    }

    public string? GetActiveMeisShortName(int recKb, int meisCd)
    {
        return _meisRepo.GetShortName(recKb, meisCd);
    }

    public string? GetActiveBankShortName(long gkCd)
    {
        return _gnkoRepo.GetActiveShortName(gkCd);
    }

    public bool IsCardNoDuplicate(long cardNo, string currentKkykCd)
    {
        IReadOnlyList<string> codes = _kyakRepo.GetCustomerCodesByCardNo(cardNo);
        return codes.Any(c => !string.Equals(c, currentKkykCd, StringComparison.Ordinal));
    }

    public void InsertCustomer(KmKyak entity)
    {
        _kyakRepo.Insert(entity);
    }

    public void UpdateCustomer(KmKyak entity)
    {
        _kyakRepo.Update(entity);
    }

    public void SoftDeleteCustomer(string kkykCd, DateTime deletedDate)
    {
        _kyakRepo.SoftDelete(kkykCd, deletedDate);
    }

    public IReadOnlyList<GpkenItemDto> GetAllActiveGroups()
    {
        IReadOnlyList<KmGrpf> rows = _grpfRepo.GetAllActive();
        return rows.Select(r =>
        {
            string codeText = r.KGpkCd.ToString("000", CultureInfo.InvariantCulture);
            return new GpkenItemDto(
                Code: r.KGpkCd,
                CodeText: codeText,
                NameKj: r.GpkNameKj,
                NameS: r.GpkNameS,
                DisplayText: $"{codeText}  {r.GpkNameKj.Trim()}");
        }).ToList();
    }

    public IReadOnlyList<GnkkenBankItemDto> GetActiveMainBanks()
    {
        IReadOnlyList<KmGnko> rows = _gnkoRepo.GetActiveMainBanks();
        return rows.Select(r =>
        {
            string codeText = r.KGkCd.ToString("0000000", CultureInfo.InvariantCulture);
            return new GnkkenBankItemDto(
                Code: r.KGkCd,
                CodeText: codeText,
                NameKj: r.GkNameKj,
                DisplayText: $"{codeText}  {r.GkNameKj.Trim()}");
        }).ToList();
    }

    public IReadOnlyList<GnkkenBranchItemDto> GetActiveBranches(long mainBankCode)
    {
        IReadOnlyList<KmGnko> rows = _gnkoRepo.GetActiveBranches(mainBankCode);
        return rows.Select(r =>
        {
            string codeText = r.KGkCd.ToString("0000000", CultureInfo.InvariantCulture);
            return new GnkkenBranchItemDto(
                Code: r.KGkCd,
                CodeText: codeText,
                NameKj: r.GkNameKj,
                NameS: r.GkNameS,
                DisplayText: $"{codeText}  {r.GkNameKj.Trim()}");
        }).ToList();
    }

    public IReadOnlyList<KykkenCustomerItemDto> SearchCustomersByKanaOrAlpha(int syubetu, string moji, string moji2, int kykbn, int gpcd)
    {
        IReadOnlyList<KmKyak> rows = _kyakRepo.SearchActiveByKanaOrAlpha(syubetu, moji, moji2, kykbn, gpcd);
        return MapCustomerItems(rows);
    }

    public IReadOnlyList<KykkenCustomerItemDto> SearchCustomersByName(string keyword, KykkenNameMatchMode matchMode, bool fuzzySearch, int? kykbn, int? gpcd)
    {
        IReadOnlyList<KmKyak> rows = _kyakRepo.SearchActiveByName(keyword, (int)matchMode, fuzzySearch, kykbn, gpcd);
        return MapCustomerItems(rows);
    }

    private static IReadOnlyList<KykkenCustomerItemDto> MapCustomerItems(IReadOnlyList<KmKyak> rows)
    {
        return rows.Select(r =>
        {
            string code = FormatCustomerCode(r.KKykCd);
            return new KykkenCustomerItemDto(
                Code: code,
                NameKj: r.KykNameKj,
                DisplayText: $"{code}  {r.KykNameKj.Trim()}");
        }).ToList();
    }

    private static string FormatCustomerCode(string raw)
    {
        string trimmed = raw.Trim();
        if (long.TryParse(trimmed, NumberStyles.None, CultureInfo.InvariantCulture, out long num))
        {
            return num.ToString("0000000000", CultureInfo.InvariantCulture);
        }

        return trimmed.PadLeft(10, '0');
    }
}

