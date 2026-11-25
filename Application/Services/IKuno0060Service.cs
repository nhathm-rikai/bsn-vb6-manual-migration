using Application.DTOs;

namespace Application.Services;

public interface IKuno0060Service
{
    void CheckDatabaseConnection();

    /// <summary>
    /// 所属コード検索 (txtSYCD_LostFocus 相当)
    /// </summary>
    SyzkRecordDto FindByCode(int kszCd);

    /// <summary>
    /// 所属区分名称取得 (KRECKB = 8, get_meis(8, code) 相当)
    /// </summary>
    string? GetKubunName(int kubunCd);

    /// <summary>
    /// ブロック名称取得 (KRECKB = 1, get_meis(1, code) 相当)
    /// </summary>
    string? GetBlockName(int blkCd);

    /// <summary>
    /// 主管名称取得 (KRECKB = 2, get_meis(2, code) 相当)
    /// </summary>
    string? GetSyukanName(int skanCd);

    /// <summary>
    /// グループ会社略称取得 (get_grpf 相当)
    /// </summary>
    string? GetGroupCompanyName(int gpkCd);

    /// <summary>
    /// 設定ボタン押下時の入力検証・登録/更新処理 (cmdOK_Click 相当)
    /// </summary>
    Kuno0060ActionResult Save(Kuno0060SaveInput input, DateTime now);

    /// <summary>
    /// 削除ボタン押下時の論理削除処理 (cmdDEL_Click 相当)
    /// </summary>
    Kuno0060ActionResult Delete(int kszCd, DateTime now);
}
