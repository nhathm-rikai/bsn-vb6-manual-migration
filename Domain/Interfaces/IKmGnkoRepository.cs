using Domain.Entities;

namespace Domain.Interfaces;

public interface IKmGnkoRepository
{
    /// <summary>
    /// VB6 KUNO0042 get_gnko(cd, nm) 相当。
    /// 有効(DDATE IS NULL)な銀行略称(GKNAMES)を返す。
    /// </summary>
    string? GetActiveShortName(long kgkCd);

    /// <summary>
    /// VB6 GNKKEN get_gnko() 相当。
    /// 有効(DDATE IS NULL)かつ下3桁が "000" の銀行マスタを KGKCD 昇順で返す。
    /// </summary>
    IReadOnlyList<KmGnko> GetActiveMainBanks();

    /// <summary>
    /// VB6 GNKKEN get_gnko2(hsgcd) 相当。
    /// 指定銀行コード+1 ～ +999 の有効(DDATE IS NULL)な本支店マスタを KGKCD 昇順で返す。
    /// </summary>
    IReadOnlyList<KmGnko> GetActiveBranches(long mainBankCode);
}

