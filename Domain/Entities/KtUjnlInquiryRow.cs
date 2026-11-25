namespace Domain.Entities;

/// <summary>
/// VB6 KTBO0020 allist_Set 検索結果1行分 (HAN01.KTUJNL UNION ALL HAN01.KTUJNL2)
/// </summary>
public class KtUjnlInquiryRow
{
    public string Hbscd { get; set; } = string.Empty;
    public long Kdenno { get; set; }
    public int Hbiday { get; set; }
    public decimal Urisu { get; set; }
    public decimal Uritan { get; set; }
    public long Hankin { get; set; }
    public long ShnCd { get; set; }
    public string ShnNameN1 { get; set; } = string.Empty;
    public string ShnNameN2 { get; set; } = string.Empty;
    public int StantCd { get; set; }
}

