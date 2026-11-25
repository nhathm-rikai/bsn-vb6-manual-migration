namespace Domain.Entities;

/// <summary>
/// 銀行マスタ (HAN01.KMGNKO)
/// </summary>
public class KmGnko
{
    public DateTime? DDate { get; set; }
    public long KGkCd { get; set; }
    public string GkNameKj { get; set; } = string.Empty;
    public string GkNameS { get; set; } = string.Empty;
}

