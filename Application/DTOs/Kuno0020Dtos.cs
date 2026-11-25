namespace Application.DTOs;

public enum Kuno0020Field
{
    None = 0,
    DaiCd,
    TyuCd,
    DtName,
    BmCd,
    Srt,
    Tekyo,
    Srt2
}

/// <summary>
/// VB6 KUNO0020 kmbnri_check の取得結果モデル
/// </summary>
public class CategoryLookupResult
{
    public bool Exists { get; init; }
    public string DeletedDateText { get; init; } = string.Empty;
    public bool IsDeleted => !string.IsNullOrWhiteSpace(DeletedDateText);
    public string DtName { get; init; } = string.Empty;
    public int BmCd { get; init; }
    public string SrtText { get; init; } = "0.00";
    public string TekyoText { get; init; } = string.Empty;
    public string Srt2Text { get; init; } = "0.00";
    public int KeiriDds { get; init; }
}

public class CategorySaveRequest
{
    public string DaiCdText { get; init; } = string.Empty;
    public string TyuCdText { get; init; } = string.Empty;
    public string DtNameText { get; init; } = string.Empty;
    public string BmCdText { get; init; } = string.Empty;
    public string BmNameLabelText { get; init; } = string.Empty;
    public string SrtText { get; init; } = string.Empty;
    public string TekyoText { get; init; } = string.Empty;
    public string Srt2Text { get; init; } = string.Empty;
    public int KeiriDds { get; init; }
}

public class Kuno0020ActionResult
{
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;
    public Kuno0020Field ErrorField { get; init; } = Kuno0020Field.None;
    public bool IsExclamation { get; init; }

    public static Kuno0020ActionResult Ok(string message) => new()
    {
        Success = true,
        Message = message
    };

    public static Kuno0020ActionResult Fail(string message, Kuno0020Field field = Kuno0020Field.None, bool isExclamation = false) => new()
    {
        Success = false,
        Message = message,
        ErrorField = field,
        IsExclamation = isExclamation
    };
}

