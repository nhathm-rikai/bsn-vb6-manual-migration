namespace Domain.Entities;

/// <summary>
/// 売上ジャーナルビュー (HAN01.VTUJNL) — 売上明細用 (get_ujnl1)
/// </summary>
public class VtUjnl
{
    public DateTime? TDate { get; set; }
    public DateTime? KDate { get; set; }
    public DateTime? DDate { get; set; }
    public long KDenNo { get; set; }
    public int KGyoNo { get; set; }
    public string HbsCd { get; set; } = string.Empty;
    public int GpkCd { get; set; }
    public int SzCd { get; set; }
    public int SinNo { get; set; }
    public long HbiDay { get; set; }
    public int TBmCd { get; set; }
    public int STantCd { get; set; }
    public int NTantCd { get; set; }
    public int ToriCd { get; set; }
    public int KeiKu { get; set; }
    public int KeiDd { get; set; }
    public int SybnCd { get; set; }
    public int DaiCd { get; set; }
    public int TyuCd { get; set; }
    public long ShnCd { get; set; }
    public string ShnNamen1 { get; set; } = string.Empty;
    public string ShnNamen2 { get; set; } = string.Empty;
    public decimal UriSu { get; set; }
    public int TniCd { get; set; }
    public decimal UriTan { get; set; }
    public decimal HanKin { get; set; }
    public decimal ZeiKin { get; set; }
    public decimal GokKin { get; set; }
    public int ZeiKu { get; set; }
    public string Tekiyo { get; set; } = string.Empty;
    public int NkzFlg { get; set; }
    public string SksCd { get; set; } = string.Empty;
    public int NkHoho { get; set; }
    public int DnpKu { get; set; }
    public int DnpFlg { get; set; }
    public int GtjFlg { get; set; }
    public int SwkFlg { get; set; }
    public int CksFlg { get; set; }
    public int DelFlg { get; set; }
    public int BkFlg { get; set; }
}

/// <summary>
/// 売上ジャーナル集計行 (HAN01.VTUJNL) — 大分類販売先別明細用 (get_ujnl2)
/// </summary>
public class VtUjnlSummary
{
    public int DaiCd { get; set; }
    public int BkFlg { get; set; }
    public int KeiKu { get; set; }
    public int KeiDd { get; set; }
    public string SksCd { get; set; } = string.Empty;
    public string HbsCd { get; set; } = string.Empty;
    public decimal SumHanKin { get; set; }
    public decimal SumZeiKin { get; set; }
    public decimal SumGokKin { get; set; }
}

