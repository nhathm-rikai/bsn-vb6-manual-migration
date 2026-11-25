namespace Domain.Entities;

/// <summary>
/// 担当マスタ (HAN01.KMTANT)
/// </summary>
public class KmTant
{
    public DateTime? TDate { get; set; }
    public DateTime? KDate { get; set; }
    public DateTime? DDate { get; set; }

    /// <summary>担当コード — NUMBER(4)</summary>
    public int KTantCd { get; set; }

    /// <summary>担当名(カナ) — VARCHAR2(20 CHAR)</summary>
    public string TantNameKn { get; set; } = string.Empty;

    /// <summary>担当名(漢字) — VARCHAR2(30 CHAR)</summary>
    public string TantNameKj { get; set; } = string.Empty;

    /// <summary>担当名(略称) — VARCHAR2(16 CHAR)</summary>
    public string TantNameS { get; set; } = string.Empty;

    /// <summary>部門コード — NUMBER(2)</summary>
    public int BmCd { get; set; }
}

