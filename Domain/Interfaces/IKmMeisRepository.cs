using Domain.Entities;

namespace Domain.Interfaces;

public interface IKmMeisRepository
{
    /// <summary>
    /// VB6 GLOBAL.BAS get_meis(pintR, pintCD, pstrName) 相当。
    /// レコード未存在または削除済み(DDATE IS NOT NULL)の場合は null を返す。
    /// </summary>
    string? GetShortName(int recKb, int meisCd);

    /// <summary>
    /// VB6 KZKO0010 txtBMNCD_LostFocus 相当。
    /// DDATE 判定を行わず KRECKB・KMEISCD で MEISYOS を取得する。
    /// </summary>
    string? GetShortNameRaw(int recKb, int meisCd);

    /// <summary>
    /// VB6 MAKKEN.FRM get_mkacd 相当。
    /// 指定したレコード区分・コード範囲の有効レコード(DDATE IS NULL)を KMEISCD 昇順で返す。
    /// </summary>
    IReadOnlyList<KmMeis> GetActiveByRange(int recKb, int minCode, int maxCode);

    /// <summary>
    /// VB6 TAIKEN.FRM get_tanicd 相当。
    /// 指定したレコード区分の有効レコード(DDATE IS NULL)を KMEISCD 昇順で返す。
    /// </summary>
    IReadOnlyList<KmMeis> GetActiveByRecKb(int recKb);

    void TestConnection();
    KmMeis? GetByKey(int recKb, int meisCd);
    void Insert(KmMeis entity);
    void Update(KmMeis entity);
    void SoftDelete(int recKb, int meisCd, DateTime deletedDate);
}
