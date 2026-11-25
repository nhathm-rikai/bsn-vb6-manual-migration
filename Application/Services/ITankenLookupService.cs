using Application.DTOs;

namespace Application.Services;

/// <summary>
/// TANKEN (担当コード検索ダイアログ) 共通サービスインターフェース
/// </summary>
public interface ITankenLookupService
{
    /// <summary>
    /// VB6 TANKEN get_tantcd / set_tantcd 相当 (有効な担当者一覧取得)
    /// </summary>
    IReadOnlyList<TantLookupItem> GetTantLookupList();
}

