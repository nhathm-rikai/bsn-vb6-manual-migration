namespace Domain.Entities;

/// <summary>
/// 商品マスタ (HAN01.KMSYOF)
/// ※VB6 KUNO0010.FRM の仕様通り、DBカラム SHNNAMEKJ には商品名(カナ)、
///   SHNNAMEKN には商品名(漢字)が格納される点に注意。
/// </summary>
public class KmSyof
{
    public DateTime? TDate { get; set; }
    public DateTime? KDate { get; set; }
    public DateTime? DDate { get; set; }
    public long KShnCd { get; set; }

    /// <summary>
    /// DBカラム SHNNAMEKJ (VARCHAR2(20 CHAR)) — VB6 txtSNMK (商品名カナ) に対応
    /// </summary>
    public string ShnNameKj { get; set; } = string.Empty;

    /// <summary>
    /// DBカラム SHNNAMEKN (VARCHAR2(40 CHAR)) — VB6 txtSNMKJ (商品名漢字) に対応
    /// </summary>
    public string ShnNameKn { get; set; } = string.Empty;

    /// <summary>
    /// DBカラム SHNNAMES (VARCHAR2(20 CHAR)) — VB6 txtSNMS (商品名略称) に対応
    /// </summary>
    public string ShnNameS { get; set; } = string.Empty;

    public int DaiCd { get; set; }
    public int TyuCd { get; set; }
    public long DhShnCd { get; set; }
    public int MekaCd { get; set; }
    public int SzKb { get; set; }
    public decimal StdKur { get; set; }
    public decimal SireTk { get; set; }
    public decimal GphnbTk { get; set; }
    public decimal JgyohbTk { get; set; }
    public decimal IpkghbTk { get; set; }
    public decimal IpkjhbTk { get; set; }
    public int TniCd { get; set; }
    public decimal InSu { get; set; }
    public int ZkKb { get; set; }
    public decimal ZkSu { get; set; }
    public decimal Boka { get; set; }
}

