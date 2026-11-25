namespace Application.DTOs;

/// <summary>
/// VB6 KZKO0010 Form_Load 初期化情報 (ロックマスタ KEIYM から算出)
/// </summary>
public sealed record Kzko0010InitContextDto(
    long SvKeiYm,
    long SvKeiYmd,
    string DefaultYearText,
    string DefaultMonthText,
    string DefaultDayText,
    string SystemDateText);

public enum Kzko0010ProductLookupStatus
{
    Found,
    NotFound,
    NotInventoryTarget
}

/// <summary>
/// VB6 KZKO0010 txtSHNCD_LostFocus 商品マスタ検索結果
/// </summary>
public sealed record Kzko0010ProductLookupResultDto(
    Kzko0010ProductLookupStatus Status,
    string ShortName);

/// <summary>
/// VB6 KZKO0010 get_syohin() の対象商品1件 (dwKSHNCD / dfINSU / dfZKSU / dfBOKA)
/// </summary>
public sealed record Kzko0010TargetProductDto(
    long KShnCd,
    decimal InSu,
    decimal ZkSu,
    decimal Boka);

/// <summary>
/// VB6 KZKO0010 の sqlerror 表示用テーブル名・キー値を保持する例外
/// </summary>
public sealed class Kzko0010SqlContextException : Exception
{
    public string TableName { get; }
    public string KeyValue { get; }

    public Kzko0010SqlContextException(string tableName, string keyValue, Exception innerException)
        : base(innerException.Message, innerException)
    {
        TableName = tableName;
        KeyValue = keyValue;
    }
}

