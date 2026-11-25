namespace Domain.Entities;

/// <summary>
/// グループ会社マスタ (HAN01.KMGRPF)
/// </summary>
public class KmGrpf
{
    public DateTime? DDate { get; set; }
    public int KGpkCd { get; set; }
    public string GpkNameKj { get; set; } = string.Empty;
    public string GpkNameS { get; set; } = string.Empty;
    public int KyoDd { get; set; }
    public string KysCd { get; set; } = string.Empty;
}

