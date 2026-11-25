using Application.DTOs;

namespace Application.Services;

public interface IKzko0020Service : ISyokenLookupService, ITankenLookupService
{
    void CheckDatabaseConnection();

    /// <summary>
    /// VB6 KZKO0020 Form_Load ロックマスタ(KMLOCK KRECKBN=1)取得および初期調整日算出
    /// </summary>
    Kzko0020InitContextDto GetInitContext(DateTime? now = null);

    /// <summary>
    /// VB6 KZKO0020 get_kmtant 相当 (有効な担当者の略称 TANTNAMES を返す。未存在・削除済は null)
    /// </summary>
    string? GetActiveTantShortName(int ktantCd);

    /// <summary>
    /// VB6 KZKO0020 get_kmsyof 相当 (商品マスタ存在・削除・在庫区分ZKKB=1チェック + 商品分類KEIRIDDS取得)
    /// </summary>
    Kzko0020ProductCheckResult CheckProduct(long kshncd);

    /// <summary>
    /// VB6 KZKO0020 get_zjnl 相当 (在庫調整ジャーナル読込)
    /// </summary>
    Kzko0020ZjnlLoadResult LoadZjnl(long kdenno);

    /// <summary>
    /// VB6 KZKO0020 file_write 相当 (KSZCDN採番 + KTZJNL登録)
    /// </summary>
    long InsertAdjustment(
        int stantCd,
        long choDay,
        long shnCd,
        int choku,
        decimal absSuu,
        decimal absKin,
        string tekiyo,
        out long assignedDenNo,
        DateTime? now = null);

    /// <summary>
    /// VB6 KZKO0020 file_update 相当 (KTZJNL更新)
    /// </summary>
    void UpdateAdjustment(
        long kdenNo,
        int stantCd,
        long choDay,
        long shnCd,
        int choku,
        decimal absSuu,
        decimal absKin,
        string tekiyo,
        DateTime? now = null);

    /// <summary>
    /// VB6 KZKO0020 file_delete 相当 (KTZJNL削除)
    /// </summary>
    void DeleteAdjustment(long kdenNo);
}

