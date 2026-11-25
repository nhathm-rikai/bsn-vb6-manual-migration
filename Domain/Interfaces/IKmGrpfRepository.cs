using Domain.Entities;

namespace Domain.Interfaces;

public interface IKmGrpfRepository
{
    /// <summary>
    /// VB6 KUNO0060 get_grpf(nm) 相当。
    /// HAN01.KMGRPF から有効(DDATE IS NULL)なグループ会社略称(GPKNAMES)を返す。
    /// 未存在または削除済みの場合は null を返す。
    /// </summary>
    string? GetActiveShortName(int kgpkCd);

    /// <summary>
    /// VB6 KUNO0040 get_grpf(cd, nm, sday, scd) 相当。
    /// 有効(DDATE IS NULL)なグループ会社マスタ情報(GPKNAMES, KYODD, KYSCD)を返す。
    /// </summary>
    KmGrpf? GetActiveByCode(int kgpkCd);

    /// <summary>
    /// VB6 GPKEN get_GPcd() / KYKKEN get_grpf() 相当。
    /// 有効(DDATE IS NULL)なグループ会社マスタ全件を KGPKCD 昇順で返す。
    /// </summary>
    IReadOnlyList<KmGrpf> GetAllActive();
}
