using Application.DTOs;

namespace Application.Services;

public interface IKuno0160Service
{
    void CheckDatabaseConnection();

    /// <summary>
    /// 担当コード検索 (Onelist_Set 相当)
    /// </summary>
    TantRecordDto FindByCode(int ktantCd);

    /// <summary>
    /// 部門名称取得 (KRECKB = 4, get_meis(4, bcd, nm) 相当)
    /// </summary>
    string? GetBumonName(int bmCd);

    /// <summary>
    /// 設定ボタン押下時の入力検証・登録/更新処理 (cmdOK_Click 相当)
    /// </summary>
    Kuno0160ActionResult Save(Kuno0160SaveInput input, DateTime now);

    /// <summary>
    /// 削除ボタン押下時の論理削除処理 (cmdDEL_Click 相当)
    /// </summary>
    Kuno0160ActionResult Delete(int ktantCd, DateTime now);
}

