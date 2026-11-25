using Application.DTOs;
using Domain.Entities;

namespace Application.Services;

public interface IKuno0040Service
{
    void CheckDatabaseConnection();

    // --- KUNO0040 / 0041 / 0042 CRUD & Lookups ---
    KmKyak? GetCustomerByCode(string kkykCd);
    string? GetActiveCustomerShortName(string kkykCd);
    Kuno0040GroupInfoDto? GetActiveGroupInfo(int gpkCd);
    string? GetActiveAffiliationShortName(int szCd);
    string? GetActiveMeisShortName(int recKb, int meisCd);
    string? GetActiveBankShortName(long gkCd);
    bool IsCardNoDuplicate(long cardNo, string currentKkykCd);
    void InsertCustomer(KmKyak entity);
    void UpdateCustomer(KmKyak entity);
    void SoftDeleteCustomer(string kkykCd, DateTime deletedDate);

    // --- GPKEN (グループコード検索) ---
    IReadOnlyList<GpkenItemDto> GetAllActiveGroups();

    // --- GNKKEN (銀行コード検索) ---
    IReadOnlyList<GnkkenBankItemDto> GetActiveMainBanks();
    IReadOnlyList<GnkkenBranchItemDto> GetActiveBranches(long mainBankCode);

    // --- KYKKEN (顧客コード検索) ---
    IReadOnlyList<KykkenCustomerItemDto> SearchCustomersByKanaOrAlpha(int syubetu, string moji, string moji2, int kykbn, int gpcd);
    IReadOnlyList<KykkenCustomerItemDto> SearchCustomersByName(string keyword, KykkenNameMatchMode matchMode, bool fuzzySearch, int? kykbn, int? gpcd);
}

