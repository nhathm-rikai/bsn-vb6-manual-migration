using Application.DTOs;

namespace Application.Services;

public interface IKzko0010Service
{
    void CheckDatabaseConnection();

    /// <summary>
    /// VB6 KZKO0010 Form_Load 相当。ロックマスタ(KRECKBN=1)の KEIYM を取得し初期日付を算出する。
    /// </summary>
    Kzko0010InitContextDto GetInitContext(DateTime? now = null);

    /// <summary>
    /// VB6 KZKO0010 txtBMNCD_LostFocus 相当。名称マスタ(KRECKB=4)から部門略称を取得する。
    /// </summary>
    string? GetDepartmentName(int bmnCd);

    /// <summary>
    /// VB6 KZKO0010 txtSHNCD_LostFocus 相当。商品マスタ(DDATE IS NULL)から商品名略称・在庫区分を確認する。
    /// </summary>
    Kzko0010ProductLookupResultDto LookupProduct(long kshncd);

    /// <summary>
    /// VB6 KZKO0010 get_syohin() (部門コード指定) 相当。
    /// </summary>
    IReadOnlyList<Kzko0010TargetProductDto> GetTargetProductsByDepartment(int bmCd);

    /// <summary>
    /// VB6 KZKO0010 get_syohin() (商品コード指定) 相当。
    /// </summary>
    IReadOnlyList<Kzko0010TargetProductDto> GetTargetProductsByCode(long kshncd);

    /// <summary>
    /// VB6 KZKO0010 upd_zaiko() 相当。
    /// 論理削除・在庫対象外商品の当月在庫マスタを削除後、対象商品ごとに在庫マスタを登録/更新する。
    /// </summary>
    void ExecuteZaikoUpdate(
        int year,
        int month,
        int day,
        IReadOnlyList<Kzko0010TargetProductDto> products,
        Action<int>? onItemProcessed = null);
}

