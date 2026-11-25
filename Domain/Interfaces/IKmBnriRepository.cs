using Domain.Entities;

namespace Domain.Interfaces;

public interface IKmBnriRepository
{
    void TestConnection();
    KmBnri? GetByKey(int daiCd, int tyuCd);
    bool ExistsInProductMaster(int daiCd, int tyuCd);
    void Insert(KmBnri entity);
    void Update(KmBnri entity);
    void SoftDelete(int daiCd, int tyuCd, DateTime deletedDate);

    /// <summary>
    /// VB6 KUNO0010 cmdOK_Click 商品登録時の大分類マスタ(KTYUCD=0) 商品コード連番(SHCDR)更新
    /// </summary>
    void UpdateProductSeq(int daiCd, int newShCdr, DateTime kDate);

    /// <summary>
    /// VB6 SYOKEN.FRM get_bnri() 相当。
    /// 有効な大分類(KTYUCD = 0 かつ DDATE IS NULL)を KDAICD 昇順で取得する。
    /// </summary>
    IReadOnlyList<KmBnri> GetActiveMajorCategories();

    /// <summary>
    /// VB6 SYOKEN.FRM get_bnri2(dai) 相当。
    /// 指定大分類の有効な中分類(KTYUCD &lt;&gt; 0 かつ DDATE IS NULL)を KTYUCD 昇順で取得する。
    /// </summary>
    IReadOnlyList<KmBnri> GetActiveMinorCategories(int daiCd);
}

