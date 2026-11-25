using System.Globalization;
using Application.Common;
using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class KmGnkoRepository : IKmGnkoRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public KmGnkoRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public string? GetActiveShortName(long kgkCd)
    {
        const string sql = """
            SELECT
                DDATE   AS DDate,
                KGKCD   AS KGkCd,
                GKNAMES AS GkNameS
            FROM HAN01.KMGNKO
            WHERE KGKCD = :KGkCd
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        KmGnko? row = conn.QueryFirstOrDefault<KmGnko>(sql, new { KGkCd = kgkCd });
        if (row is null || row.DDate.HasValue)
        {
            return null;
        }

        return LegacyTextHelper.NormalizeString(row.GkNameS);
    }

    public IReadOnlyList<KmGnko> GetActiveMainBanks()
    {
        const string sql = """
            SELECT
                DDATE    AS DDate,
                KGKCD    AS KGkCd,
                GKNAMEKJ AS GkNameKj,
                GKNAMES  AS GkNameS
            FROM HAN01.KMGNKO
            ORDER BY KGKCD
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        var rows = conn.Query<KmGnko>(sql);
        return rows
            .Where(r => !r.DDate.HasValue && r.KGkCd.ToString("0000000", CultureInfo.InvariantCulture).EndsWith("000", StringComparison.Ordinal))
            .Select(r => new KmGnko
            {
                DDate = r.DDate,
                KGkCd = r.KGkCd,
                GkNameKj = LegacyTextHelper.NormalizeString(r.GkNameKj),
                GkNameS = LegacyTextHelper.NormalizeString(r.GkNameS)
            })
            .ToList();
    }

    public IReadOnlyList<KmGnko> GetActiveBranches(long mainBankCode)
    {
        const string sql = """
            SELECT
                DDATE    AS DDate,
                KGKCD    AS KGkCd,
                GKNAMEKJ AS GkNameKj,
                GKNAMES  AS GkNameS
            FROM HAN01.KMGNKO
            WHERE KGKCD >= :MinCode
              AND KGKCD <= :MaxCode
            ORDER BY KGKCD
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        var rows = conn.Query<KmGnko>(sql, new { MinCode = mainBankCode + 1, MaxCode = mainBankCode + 999 });
        return rows
            .Where(r => !r.DDate.HasValue)
            .Select(r => new KmGnko
            {
                DDate = r.DDate,
                KGkCd = r.KGkCd,
                GkNameKj = LegacyTextHelper.NormalizeString(r.GkNameKj),
                GkNameS = LegacyTextHelper.NormalizeString(r.GkNameS)
            })
            .ToList();
    }
}

