using Domain.Entities;

namespace Domain.Interfaces;

public interface IKmKyakRepository
{
    void TestConnection();

    /// <summary>
    /// VB6 KSHO0115 get_kmkyak(strCODE, strNAME) 相当。
    /// HAN01.KMKYAK から顧客漢字名称(KYKNAMEKJ)を取得する。
    /// 未存在または削除済み(DDATE IS NOT NULL)の場合は空文字を返す。
    /// </summary>
    string GetActiveCustomerName(string kkykCd);

    /// <summary>
    /// VB6 KUNO0040 Onelist_Set 相当。全カラムを取得する（削除済み含む）。
    /// </summary>
    KmKyak? GetByCode(string kkykCd);

    /// <summary>
    /// VB6 KUNO0040/KUNO0041 get_kyak(cd, nm) 相当。
    /// 有効(DDATE IS NULL)な顧客略称(KYKNAMES)を返す。未存在・削除済みの場合は null。
    /// </summary>
    string? GetActiveShortName(string kkykCd);

    /// <summary>
    /// VB6 KUNO0040 cmdSET_Click カード番号重複チェック相当。
    /// 指定 CARDNO に一致する顧客コード(KKYKCD)一覧を返す。
    /// </summary>
    IReadOnlyList<string> GetCustomerCodesByCardNo(long cardNo);

    void Insert(KmKyak entity);
    void Update(KmKyak entity);
    void SoftDelete(string kkykCd, DateTime deletedDate);

    /// <summary>
    /// VB6 KYKKEN get_kyak(syubetu, moji, moji2, kykbn, gpcd) 相当。
    /// syubetu: 1:ｶﾅ, 2:英字, 3:新潟, 4:ｶﾅ(ﾆ･ﾆｲｶﾞﾀ除く)
    /// </summary>
    IReadOnlyList<KmKyak> SearchActiveByKanaOrAlpha(int syubetu, string moji, string moji2, int kykbn, int gpcd);

    /// <summary>
    /// VB6 KYKKEN get_kyak2() 相当（名称検索・あいまい検索・区分/グループ絞込対応）。
    /// matchMode: 0:部分一致, 1:前方一致, 2:後方一致
    /// </summary>
    IReadOnlyList<KmKyak> SearchActiveByName(string keyword, int matchMode, bool fuzzySearch, int? kykbn, int? gpcd);
}
