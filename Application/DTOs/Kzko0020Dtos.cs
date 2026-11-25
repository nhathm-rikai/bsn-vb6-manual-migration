namespace Application.DTOs;

/// <summary>
/// VB6 TANKEN.BAS wk_tant 対応 (担当コード検索コンボアイテム)
/// </summary>
public sealed record TantLookupItem(
    int Code,
    string CodeText,
    string NameKj,
    string ShortName,
    string DisplayText);

/// <summary>
/// VB6 KZKO0020 Form_Load でロックマスタ(KMLOCK)から算出される初期コンテキスト
/// </summary>
public sealed record Kzko0020InitContextDto(
    long SvKeiYm,
    long SvKeiYmd,
    string GlbCyy,
    string GlbCmm,
    string GlbCdd,
    string SystemDateText);

public enum Kzko0020ProductCheckStatus
{
    Ok = 0,
    NotFound = 1,
    NotStockItem = 2
}

/// <summary>
/// VB6 KZKO0020 get_kmsyof の判定結果
/// </summary>
public sealed record Kzko0020ProductCheckResult(
    Kzko0020ProductCheckStatus Status,
    string ProductNameKn,
    int Keiridds,
    string ErrorMessage,
    string SqlErrorScreen = "",
    string SqlErrorTable = "",
    string SqlErrorKey = "");

public enum Kzko0020ZjnlLoadStatus
{
    Ok = 0,
    NotFound = 1,
    AlreadyJournalized = 2
}

/// <summary>
/// VB6 KZKO0020 get_zjnl の読込結果
/// </summary>
public sealed record Kzko0020ZjnlLoadResult(
    Kzko0020ZjnlLoadStatus Status,
    int SwkFlg,
    string NTanCdText,
    string NTanNameText,
    string CyyText,
    string CmmText,
    string CddText,
    string SyoCdText,
    string SyoNameText,
    string? ProductWarningMessage,
    int Keiridds,
    string ChKbnText,
    bool IsNegativeAdjustment,
    string NewZSuuText,
    string NewZBokaText,
    string TekiyoText);

