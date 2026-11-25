namespace Domain.Entities;

/// <summary>
/// HAN01.KMZAIKO (在庫マスタ / 在庫履歴テーブル)
/// </summary>
public class KmZaiko
{
    public DateTime? TDate { get; set; }
    public DateTime? KDate { get; set; }
    public DateTime? DDate { get; set; }

    /// <summary>在庫年月 (YYYYMM) — NUMBER(6)</summary>
    public long KzkDay { get; set; }

    /// <summary>商品コード — NUMBER(8)</summary>
    public long KShnCd { get; set; }

    /// <summary>前月末在庫数量 — NUMBER(10,2)</summary>
    public decimal ZnmtSuu { get; set; }

    /// <summary>前月末在庫簿価 — NUMBER(10)</summary>
    public long ZnmtKin { get; set; }

    /// <summary>当月入庫数量 — NUMBER(10,2)</summary>
    public decimal TgnySuu { get; set; }

    /// <summary>当月入庫簿価 — NUMBER(10)</summary>
    public long TgnyKin { get; set; }

    /// <summary>当月出庫数量 — NUMBER(10,2)</summary>
    public decimal TgsySuu { get; set; }

    /// <summary>当月出庫簿価 — NUMBER(10)</summary>
    public long TgsyKin { get; set; }

    /// <summary>当月調整数量 — NUMBER(10,2)</summary>
    public decimal TgcsSuu { get; set; }

    /// <summary>当月調整簿価 — NUMBER(10)</summary>
    public long TgcsKin { get; set; }

    /// <summary>当月末在庫数量 — NUMBER(10,2)</summary>
    public decimal TgmtSuu { get; set; }

    /// <summary>当月末在庫簿価 — NUMBER(10)</summary>
    public long TgmstKin { get; set; }
}

