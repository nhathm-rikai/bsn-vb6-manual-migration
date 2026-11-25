namespace Domain.Entities;

/// <summary>
/// 名称マスタ (HAN01.KMMEIS)
/// </summary>
public class KmMeis
{
    public DateTime? TDate { get; set; }
    public DateTime? KDate { get; set; }
    public DateTime? DDate { get; set; }
    public int KRecKb { get; set; }
    public int KMeisCd { get; set; }
    public string MeisyoKn { get; set; } = string.Empty;
    public string MeisyoKj { get; set; } = string.Empty;
    public string MeisyoS { get; set; } = string.Empty;
}

