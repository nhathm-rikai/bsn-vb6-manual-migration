using Domain.Entities;

namespace Domain.Interfaces;

public interface IKmSyofRepository
{
    KmSyof? GetByCode(long kshncd);
    void Insert(KmSyof entity);
    void Update(KmSyof entity);
    void SoftDelete(long kshncd, DateTime deletedDate);

    /// <summary>
    /// VB6 SYOKEN.FRM get_syof(dai, tyu, meka, mekaflg) 相当。
    /// 大分類・中分類(・メーカーコード)で有効な商品を SHNNAMEKJ 昇順で取得する。
    /// </summary>
    IReadOnlyList<KmSyof> SearchActiveByCategory(int daiCd, int tyuCd, int? mekaCd);

    /// <summary>
    /// VB6 SYOKEN.FRM get_syohin() 相当。
    /// 商品名カナ(SHNNAMEKN)の部分一致・前方一致・後方一致（あいまい検索・分類コード絞込対応）で有効な商品を SHNNAMEKJ 昇順で取得する。
    /// </summary>
    IReadOnlyList<KmSyof> SearchActiveByName(
        string keyword,
        int matchMode,
        bool fuzzySearch,
        int? daiCd,
        int? tyuCd,
        int? mekaCd);

    /// <summary>
    /// VB6 KZKO0010 get_syohin() (部門コード指定) 相当。
    /// HAN01.KMSYOF と HAN01.KMBNRI(KTYUCD=0) を結合し、ZKKB=1 かつ DDATE IS NULL の対象商品を取得する。
    /// </summary>
    IReadOnlyList<KmSyof> GetActiveStockTargetsByDepartment(int bmCd);

    /// <summary>
    /// VB6 KZKO0010 get_syohin() (商品コード指定) 相当。
    /// HAN01.KMSYOF から ZKKB=1 かつ DDATE IS NULL の指定商品を取得する。
    /// </summary>
    IReadOnlyList<KmSyof> GetActiveStockTargetByCode(long kshncd);
}

