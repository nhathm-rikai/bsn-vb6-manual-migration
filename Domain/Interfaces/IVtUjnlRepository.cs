using Domain.Entities;

namespace Domain.Interfaces;

public interface IVtUjnlRepository
{
    void TestConnection();

    /// <summary>
    /// VB6 KSHO0115 get_ujnl1 相当 (売上明細)
    /// wHani: 1 = すべて, 2 = 大分類コード範囲指定, 3 = 営業担当者コード指定
    /// </summary>
    IReadOnlyList<VtUjnl> GetDetails(long fromYmd, long toYmd, int wHani, int fromDaiCd, int toDaiCd, int tantCd);

    /// <summary>
    /// VB6 KSHO0115 get_ujnl2 相当 (大分類販売先別明細)
    /// wHani: 1 = すべて, 2 = 大分類コード範囲指定, 3 = 営業担当者コード指定
    /// </summary>
    IReadOnlyList<VtUjnlSummary> GetSummaries(long fromYmd, long toYmd, int wHani, int fromDaiCd, int toDaiCd, int tantCd);
}

