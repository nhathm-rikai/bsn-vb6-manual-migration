namespace Domain.Entities;

/// <summary>
/// 商品分類マスタ (HAN01.KMBNRI)
/// </summary>
public class KmBnri
{
    public DateTime? TDate { get; set; }
    public DateTime? KDate { get; set; }
    public DateTime? DDate { get; set; }
    public int KDaiCd { get; set; }
    public int KTyuCd { get; set; }
    public string DtName { get; set; } = string.Empty;
    public int BmCd { get; set; }
    public decimal? Sritu { get; set; }
    public int? ShCdr { get; set; }
    public long? Stekiyo { get; set; }
    public decimal? Sritu2 { get; set; }
    public int? Keiridds { get; set; }
}

