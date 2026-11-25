using Domain.Entities;

namespace Domain.Interfaces;

public interface IKtUjnlRepository
{
    void TestConnection();

    /// <summary>
    /// VB6 KTBO0020 allist_Set 相当。
    /// HAN01.KTUJNL と HAN01.KTUJNL2 を UNION ALL で結合し、
    /// ORDER BY 1, 3, 8, 10 (HBSCD, HBIDAY, SHNNAMEN1, STANTCD) で最大 maxRows 件取得する。
    /// </summary>
    IReadOnlyList<KtUjnlInquiryRow> SearchTransactions(
        string? hbsCd,
        int? stantCd,
        int? daiCd,
        int? tyuCd,
        long? shnCd,
        int fromYmd,
        int toYmd,
        int maxRows = 1000);
}

