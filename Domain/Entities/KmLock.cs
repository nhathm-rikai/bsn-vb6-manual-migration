namespace Domain.Entities;

/// <summary>
/// HAN01.KMLOCK (ロックマスタ)
/// </summary>
public class KmLock
{
    /// <summary>レコード区分 — NUMBER(2)</summary>
    public int KRecKbn { get; set; }

    /// <summary>経理締年月 (YYYYMM) — NUMBER(6)</summary>
    public long KeiYm { get; set; }
}

