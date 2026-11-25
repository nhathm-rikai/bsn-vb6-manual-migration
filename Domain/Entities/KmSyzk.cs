namespace Domain.Entities;

/// <summary>
/// 所属マスタ (HAN01.KMSYZK)
/// </summary>
public class KmSyzk
{
    public DateTime? TDate { get; set; }
    public DateTime? KDate { get; set; }
    public DateTime? DDate { get; set; }

    /// <summary>所属コード — NUMBER(3)</summary>
    public int KSzCd { get; set; }

    /// <summary>所属区分 — NUMBER(1)</summary>
    public int SzKb { get; set; }

    /// <summary>所属名称(カナ) — VARCHAR2(20 CHAR)</summary>
    public string SzNameKn { get; set; } = string.Empty;

    /// <summary>所属名称(漢字) — VARCHAR2(30 CHAR)</summary>
    public string SzNameKj { get; set; } = string.Empty;

    /// <summary>所属略称 — VARCHAR2(16 CHAR)</summary>
    public string SzNameS { get; set; } = string.Empty;

    /// <summary>ブロックコード — NUMBER(1)</summary>
    public int BrokCd { get; set; }

    /// <summary>主管コード — NUMBER(2)</summary>
    public int SyuknCd { get; set; }

    /// <summary>グループ会社コード — NUMBER(3)</summary>
    public int GpkCd { get; set; }
}

