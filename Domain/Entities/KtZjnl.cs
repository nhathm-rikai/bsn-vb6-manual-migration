namespace Domain.Entities;

/// <summary>
/// HAN01.KTZJNL (在庫調整ジャーナル)
/// </summary>
public class KtZjnl
{
    public DateTime? TDate { get; set; }
    public DateTime? KDate { get; set; }

    /// <summary>伝票番号 — NUMBER(7)</summary>
    public long KDenNo { get; set; }

    /// <summary>調整日 (YYYYMMDD) — NUMBER(8)</summary>
    public long ChoDay { get; set; }

    /// <summary>入力担当コード — NUMBER(4)</summary>
    public int STantCd { get; set; }

    /// <summary>商品コード — NUMBER(8)</summary>
    public long ShnCd { get; set; }

    /// <summary>調整区分 (10,11,12,13,21,22,23) — NUMBER(2)</summary>
    public int Choku { get; set; }

    /// <summary>調整数量 — NUMBER(11,2)</summary>
    public decimal ChoSuu { get; set; }

    /// <summary>調整金額 — NUMBER(13,2) / NUMBER(10)</summary>
    public decimal ChoKin { get; set; }

    /// <summary>摘要 — VARCHAR2(30)</summary>
    public string? Tekiyo { get; set; }

    /// <summary>在庫反映フラグ — NUMBER(1)</summary>
    public int ZkhnFlg { get; set; }

    /// <summary>月次フラグ — NUMBER(1)</summary>
    public int GtjFlg { get; set; }

    /// <summary>仕訳完了フラグ — NUMBER(1)</summary>
    public int SwkFlg { get; set; }
}

