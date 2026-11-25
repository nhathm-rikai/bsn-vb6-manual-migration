using Application.DTOs;

namespace Application.Services;

public interface IKtbo0020Service : ISyokenLookupService, ITankenLookupService
{
    void VerifyConnection();

    /// <summary>
    /// VB6 KTBO0020 get_kyak 相当 (有効な顧客漢字名称 KYKNAMEKJ を返す。未存在・削除済は null)
    /// </summary>
    string? GetActiveCustomerName(string hbsCd);

    /// <summary>
    /// VB6 KTBO0020 get_tant 相当 (有効な担当漢字名称 TANTNAMEKJ を返す。未存在・削除済は null)
    /// </summary>
    string? GetActiveTantName(int tantCd);

    /// <summary>
    /// VB6 KTBO0020 get_bnri1 相当 (大分類コード KTYUCD=0 の有効な DTNAME を返す。未存在・削除済は null)
    /// </summary>
    string? GetActiveMajorCategoryName(int daiCd);

    /// <summary>
    /// VB6 KTBO0020 get_bnri2 相当 (大分類+中分類の有効な DTNAME を返す。未存在・削除済は null)
    /// </summary>
    string? GetActiveMinorCategoryName(int daiCd, int tyuCd);

    /// <summary>
    /// VB6 KTBO0020 get_syof 相当 (有効な商品カナ名称 SHNNAMEKN を返す。未存在・削除済は null)
    /// </summary>
    string? GetActiveProductKanaName(long shnCd);

    /// <summary>
    /// VB6 KTBO0020 allist_Set + KTBO0021 データ整形相当 (最大999件表示)
    /// </summary>
    Ktbo0020SearchResultDto SearchTransactions(Ktbo0020SearchCriteriaDto criteria);
}

