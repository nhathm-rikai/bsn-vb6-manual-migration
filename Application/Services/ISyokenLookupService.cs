using Application.DTOs;

namespace Application.Services;

/// <summary>
/// SYOKEN (商品コード検索ダイアログ) 共通サービスインターフェース
/// </summary>
public interface ISyokenLookupService
{
    /// <summary>
    /// VB6 SYOKEN get_bnri / set_bnri 相当 (大分類リスト取得)
    /// </summary>
    IReadOnlyList<SyokenCategoryItemDto> GetMajorCategories();

    /// <summary>
    /// VB6 SYOKEN get_bnri2 / set_bnri2 相当 (中分類リスト取得)
    /// </summary>
    IReadOnlyList<SyokenCategoryItemDto> GetMinorCategories(int daiCd);

    /// <summary>
    /// VB6 SYOKEN get_meka / set_meka 相当 (メーカーコードリスト取得)
    /// </summary>
    IReadOnlyList<MakerLookupItem> GetMakersByMajorCategory(int daiCd);

    /// <summary>
    /// VB6 SYOKEN get_syof / set_syof 相当 (コード検索による商品リスト取得)
    /// </summary>
    IReadOnlyList<SyokenProductItemDto> SearchProductsByCategory(int daiCd, int tyuCd, int? mekaCd);

    /// <summary>
    /// VB6 SYOKEN get_syohin / set_syof 相当 (名称検索による商品リスト取得)
    /// </summary>
    IReadOnlyList<SyokenProductItemDto> SearchProductsByName(
        string keyword,
        SyokenNameMatchMode matchMode,
        bool fuzzySearch,
        int? daiCd,
        int? tyuCd,
        int? mekaCd);
}

