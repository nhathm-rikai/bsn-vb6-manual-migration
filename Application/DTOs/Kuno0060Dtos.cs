namespace Application.DTOs;

public enum Kuno0060Field
{
    None = 0,
    SyCd,
    Kubun,
    SyNmk,
    SyNmkj,
    SyNms,
    Blk,
    Skan,
    Gcd
}

public sealed class SyzkRecordDto
{
    public bool Exists { get; init; }
    public bool IsDeleted { get; init; }
    public int KSzCd { get; init; }
    public int SzKb { get; init; }
    public string SzKbName { get; init; } = string.Empty;
    public string SzNameKn { get; init; } = string.Empty;
    public string SzNameKj { get; init; } = string.Empty;
    public string SzNameS { get; init; } = string.Empty;
    public int BrokCd { get; init; }
    public string BrokName { get; init; } = string.Empty;
    public int SyuknCd { get; init; }
    public string SyuknName { get; init; } = string.Empty;
    public int GpkCd { get; init; }
    public string GpkName { get; init; } = string.Empty;
}

public sealed class Kuno0060SaveInput
{
    public bool IsUpdate { get; init; }
    public string SyCdText { get; init; } = string.Empty;
    public string KubunText { get; init; } = string.Empty;
    public string KubunLabelText { get; init; } = string.Empty;
    public string SyNmkText { get; init; } = string.Empty;
    public string SyNmkjText { get; init; } = string.Empty;
    public string SyNmsText { get; init; } = string.Empty;
    public string BlkText { get; init; } = string.Empty;
    public string BlkLabelText { get; init; } = string.Empty;
    public string SkanText { get; init; } = string.Empty;
    public string SkanLabelText { get; init; } = string.Empty;
    public string GcdText { get; init; } = string.Empty;
    public string GcdLabelText { get; init; } = string.Empty;
}

public sealed class Kuno0060ActionResult
{
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;
    public Kuno0060Field ErrorField { get; init; } = Kuno0060Field.None;
    public bool IsExclamation { get; init; }

    public static Kuno0060ActionResult Ok(string message) => new()
    {
        Success = true,
        Message = message
    };

    public static Kuno0060ActionResult Fail(
        string message,
        Kuno0060Field field = Kuno0060Field.None,
        bool isExclamation = false) => new()
    {
        Success = false,
        Message = message,
        ErrorField = field,
        IsExclamation = isExclamation
    };
}
