using System.Globalization;

namespace Application.DTOs;

/// <summary>
/// VB6 KZKO0030 Form_Load で算出される年月・日付コンテキスト
/// </summary>
public sealed record Kzko0030DateContextDto(
    long G0Yymm,
    long G1Yymm,
    long G6Yymm,
    long G36Yymm,
    long FromYmd,
    long ToYmd,
    string DefaultYearText,
    string DefaultMonthText,
    string SystemDateText);

/// <summary>
/// VB6 KZKO0030.BAS t_meisai 構造体 1:1 対応 DTO
/// </summary>
public sealed record ZaikoMeisaiDto(
    long KzkDay,
    decimal ZnmtSuu,
    long ZnmtKin,
    decimal TgnySuu,
    long TgnyKin,
    decimal TgsySuu,
    long TgsyKin,
    decimal TgcsSuu,
    long TgcsKin,
    decimal TgmtSuu,
    long TgmtKin,
    int Idx)
{
    public string KzkDayText
    {
        get
        {
            string s = KzkDay.ToString("000000", CultureInfo.InvariantCulture);
            return $"{s[..4]}/{s.Substring(4, 2)}";
        }
    }

    public string ZnmtSuuText => ZnmtSuu.ToString("#,##0.00", CultureInfo.InvariantCulture);
    public string ZnmtKinText => ZnmtKin.ToString("#,##0", CultureInfo.InvariantCulture);
    public string TgnySuuText => TgnySuu.ToString("#,##0.00", CultureInfo.InvariantCulture);
    public string TgnyKinText => TgnyKin.ToString("#,##0", CultureInfo.InvariantCulture);
    public string TgsySuuText => TgsySuu.ToString("#,##0.00", CultureInfo.InvariantCulture);
    public string TgsyKinText => TgsyKin.ToString("#,##0", CultureInfo.InvariantCulture);
    public string TgcsSuuText => TgcsSuu.ToString("#,##0.00", CultureInfo.InvariantCulture);
    public string TgcsKinText => TgcsKin.ToString("#,##0", CultureInfo.InvariantCulture);
    public string TgmtSuuText => TgmtSuu.ToString("#,##0.00", CultureInfo.InvariantCulture);
    public string TgmtKinText => TgmtKin.ToString("#,##0", CultureInfo.InvariantCulture);
}

/// <summary>
/// VB6 SYOKEN.BAS wk_dai / wk_tyu 対応 (大分類・中分類コンボアイテム)
/// </summary>
public sealed record SyokenCategoryItemDto(
    int Code,
    string Name,
    string DisplayText);

/// <summary>
/// VB6 SYOKEN.BAS wk_shin 対応 (商品コードコンボアイテム)
/// </summary>
public sealed record SyokenProductItemDto(
    long KShnCd,
    string CodeText,
    string ShnNameKn,
    string DisplayText);

/// <summary>
/// VB6 SYOKEN.FRM Option1/Option2/Option3 (部分一致・前方一致・後方一致)
/// </summary>
public enum SyokenNameMatchMode
{
    Partial = 0,
    Prefix = 1,
    Suffix = 2
}

