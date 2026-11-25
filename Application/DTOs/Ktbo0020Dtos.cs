namespace Application.DTOs;

public sealed class Ktbo0020SearchCriteriaDto
{
    public string? HbsCd { get; init; }
    public int? StantCd { get; init; }
    public int? DaiCd { get; init; }
    public int? TyuCd { get; init; }
    public long? ShnCd { get; init; }
    public int FromYmd { get; init; }
    public int ToYmd { get; init; }
}

public sealed class Ktbo0020RowDto
{
    public int RowNo { get; init; }
    public string HbsCd { get; init; } = string.Empty;
    public string CustomerName { get; init; } = string.Empty;
    public long Kdenno { get; init; }
    public int Hbiday { get; init; }
    public decimal Urisu { get; init; }
    public decimal Uritan { get; init; }
    public long Hankin { get; init; }
    public string ShnNameN1 { get; init; } = string.Empty;
    public string ShnNameN2 { get; init; } = string.Empty;
    public int StantCd { get; init; }
}

public sealed class Ktbo0020SearchResultDto
{
    public IReadOnlyList<Ktbo0020RowDto> Rows { get; init; } = Array.Empty<Ktbo0020RowDto>();
    public bool TruncatedOver999 { get; init; }
    public int TotalPages { get; init; }
}

