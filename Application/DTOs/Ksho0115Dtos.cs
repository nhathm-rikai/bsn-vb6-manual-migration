namespace Application.DTOs;

public enum Ksho0115Field
{
    None = 0,
    KNen,
    KTuki,
    KHi,
    MNen,
    MTuki,
    MHi,
    FromDaiCd,
    ToDaiCd,
    TantCd
}

public sealed class Ksho0115ExportRequest
{
    public string KNenText { get; init; } = string.Empty;
    public string KTukiText { get; init; } = string.Empty;
    public string KHiText { get; init; } = string.Empty;
    public string MNenText { get; init; } = string.Empty;
    public string MTukiText { get; init; } = string.Empty;
    public string MHiText { get; init; } = string.Empty;

    /// <summary>1 = すべて(optALL), 2 = 大分類コード範囲指定(optHANI), 3 = 営業担当者コード指定(OptTANT)</summary>
    public int HaniMode { get; init; } = 1;
    public string FromDaiCdText { get; init; } = string.Empty;
    public string ToDaiCdText { get; init; } = string.Empty;
    public string TantCdText { get; init; } = string.Empty;

    /// <summary>1 = 売上明細(opt1), 2 = 大分類販売先別明細(opt2)</summary>
    public int OutputKubun { get; init; } = 1;
}

public sealed class Ksho0115ExportResult
{
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;
    public Ksho0115Field ErrorField { get; init; } = Ksho0115Field.None;
    public bool IsExclamation { get; init; }
    public int ExportedCount { get; init; }
    public string OutputFilePath { get; init; } = string.Empty;

    public static Ksho0115ExportResult Ok(string message, int count, string filePath) => new()
    {
        Success = true,
        Message = message,
        ExportedCount = count,
        OutputFilePath = filePath
    };

    public static Ksho0115ExportResult Fail(
        string message,
        Ksho0115Field field = Ksho0115Field.None,
        bool isExclamation = false) => new()
    {
        Success = false,
        Message = message,
        ErrorField = field,
        IsExclamation = isExclamation
    };
}

