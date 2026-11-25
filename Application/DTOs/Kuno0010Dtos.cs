namespace Application.DTOs;

public enum Kuno0010Field
{
    None = 0,
    Scd,
    Snmk,
    Snmkj,
    Snms,
    Dcd,
    Tcd,
    Dscd,
    Mcd,
    Hjkk,
    Stk,
    Ghtk,
    Jhtk,
    Ikhtk,
    Iohtk,
    Tani,
    Isu,
    Zsu,
    Bk
}

/// <summary>
/// MAKKEN (メーカーコード検索) コンボボックス項目
/// </summary>
public class MakerLookupItem
{
    public int Code { get; init; }
    public string CodeText => Code.ToString("0000");
    public string FullName { get; init; } = string.Empty;
    public string ShortName { get; init; } = string.Empty;
    public string DisplayText => $"{Code:0000}  {FullName.Trim()}";
}

/// <summary>
/// TAIKEN (単位コード検索) コンボボックス項目
/// </summary>
public class UnitLookupItem
{
    public int Code { get; init; }
    public string CodeText => Code.ToString("00");
    public string FullName { get; init; } = string.Empty;
    public string ShortName { get; init; } = string.Empty;
    public string DisplayText => $"{Code:00}  {FullName.Trim()}";
}

/// <summary>
/// KUNO0010 txtSCD_LostFocus の商品マスタ読込結果
/// </summary>
public class ProductDetailResult
{
    public bool Exists { get; init; }
    public string DeletedDateText { get; init; } = string.Empty;
    public bool IsDeleted => !string.IsNullOrWhiteSpace(DeletedDateText);

    public string SnmkText { get; init; } = string.Empty;
    public string SnmkjText { get; init; } = string.Empty;
    public string SnmsText { get; init; } = string.Empty;
    public string DcdText { get; init; } = string.Empty;
    public string DcdName { get; init; } = string.Empty;
    public string TcdText { get; init; } = string.Empty;
    public string TcdName { get; init; } = string.Empty;
    public string DscdText { get; init; } = string.Empty;
    public string DscdName { get; init; } = string.Empty;
    public string McdText { get; init; } = string.Empty;
    public string McdName { get; init; } = string.Empty;
    public int SzKb { get; init; }
    public string HjkkText { get; init; } = string.Empty;
    public string StkText { get; init; } = string.Empty;
    public string GhtkText { get; init; } = string.Empty;
    public string JhtkText { get; init; } = string.Empty;
    public string IkhtkText { get; init; } = string.Empty;
    public string IohtkText { get; init; } = string.Empty;
    public string TaniText { get; init; } = string.Empty;
    public string TaniName { get; init; } = string.Empty;
    public string IsuText { get; init; } = string.Empty;
    public int ZkKb { get; init; }
    public string ZsuText { get; init; } = string.Empty;
    public string BkText { get; init; } = string.Empty;

    /// <summary>
    /// DB格納値の numeric_hensyu チェックで万一エラーとなった場合のフィールド
    /// </summary>
    public Kuno0010Field NumericErrorField { get; init; } = Kuno0010Field.None;
}

/// <summary>
/// KUNO0010 cmdOK_Click 登録・修正リクエスト
/// </summary>
public class ProductSaveRequest
{
    public string ScdText { get; init; } = string.Empty;
    public string SnmkText { get; init; } = string.Empty;
    public string SnmkjText { get; init; } = string.Empty;
    public string SnmsText { get; init; } = string.Empty;
    public string DcdText { get; init; } = string.Empty;
    public string DcdLabelText { get; init; } = string.Empty;
    public string TcdText { get; init; } = string.Empty;
    public string TcdLabelText { get; init; } = string.Empty;
    public string DscdText { get; init; } = string.Empty;
    public string DscdLabelText { get; init; } = string.Empty;
    public string McdText { get; init; } = string.Empty;
    public string McdLabelText { get; init; } = string.Empty;
    public bool HasTaxSelection { get; init; }
    public int SzKb { get; init; }
    public string HjkkText { get; init; } = string.Empty;
    public string StkText { get; init; } = string.Empty;
    public string GhtkText { get; init; } = string.Empty;
    public string JhtkText { get; init; } = string.Empty;
    public string IkhtkText { get; init; } = string.Empty;
    public string IohtkText { get; init; } = string.Empty;
    public string TaniText { get; init; } = string.Empty;
    public string TaniLabelText { get; init; } = string.Empty;
    public string IsuText { get; init; } = string.Empty;
    public bool HasZaikoSelection { get; init; }
    public int ZkKb { get; init; }
    public string ZsuText { get; init; } = string.Empty;
    public string BkText { get; init; } = string.Empty;
}

public class Kuno0010ActionResult
{
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;
    public Kuno0010Field ErrorField { get; init; } = Kuno0010Field.None;
    public bool IsExclamation { get; init; }
    public bool IsStopFatal { get; init; }
    public bool ClearTcdOnError { get; init; }

    public static Kuno0010ActionResult Ok(string message) => new()
    {
        Success = true,
        Message = message
    };

    public static Kuno0010ActionResult Fail(
        string message,
        Kuno0010Field field = Kuno0010Field.None,
        bool isExclamation = false,
        bool isStopFatal = false,
        bool clearTcdOnError = false) => new()
    {
        Success = false,
        Message = message,
        ErrorField = field,
        IsExclamation = isExclamation,
        IsStopFatal = isStopFatal,
        ClearTcdOnError = clearTcdOnError
    };
}

