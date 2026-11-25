namespace Application.DTOs;

public enum Kuno0160Field
{
    None = 0,
    TaCd,
    TaNmk,
    TaNmkj,
    TaNms,
    BuCd
}

public sealed class TantRecordDto
{
    public bool Exists { get; init; }
    public bool IsDeleted { get; init; }
    public int KTantCd { get; init; }
    public string TantNameKn { get; init; } = string.Empty;
    public string TantNameKj { get; init; } = string.Empty;
    public string TantNameS { get; init; } = string.Empty;
    public int BmCd { get; init; }
    public string BmName { get; init; } = string.Empty;
}

public sealed class Kuno0160SaveInput
{
    public bool IsUpdate { get; init; }
    public string TaCdText { get; init; } = string.Empty;
    public string TaNmkText { get; init; } = string.Empty;
    public string TaNmkjText { get; init; } = string.Empty;
    public string TaNmsText { get; init; } = string.Empty;
    public string BuCdText { get; init; } = string.Empty;
    public string BuCdLabelText { get; init; } = string.Empty;
}

public sealed class Kuno0160ActionResult
{
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;
    public Kuno0160Field ErrorField { get; init; } = Kuno0160Field.None;
    public bool IsExclamation { get; init; }
    public bool ClearBuCdOnError { get; init; }

    public static Kuno0160ActionResult Ok(string message) => new()
    {
        Success = true,
        Message = message
    };

    public static Kuno0160ActionResult Fail(
        string message,
        Kuno0160Field field = Kuno0160Field.None,
        bool isExclamation = true,
        bool clearBuCdOnError = false) => new()
    {
        Success = false,
        Message = message,
        ErrorField = field,
        IsExclamation = isExclamation,
        ClearBuCdOnError = clearBuCdOnError
    };
}

