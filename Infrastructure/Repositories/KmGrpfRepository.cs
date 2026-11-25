using Application.Common;
using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class KmGrpfRepository : IKmGrpfRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public KmGrpfRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public string? GetActiveShortName(int kgpkCd)
    {
        KmGrpf? row = GetActiveByCode(kgpkCd);
        return row?.GpkNameS;
    }

    public KmGrpf? GetActiveByCode(int kgpkCd)
    {
        const string sql = """
            SELECT
                DDATE          AS DDate,
                KGPKCD         AS KGpkCd,
                GPKNAMEKJ      AS GpkNameKj,
                GPKNAMES       AS GpkNameS,
                NVL(KYODD, 0)  AS KyoDd,
                KYSCD          AS KysCd
            FROM HAN01.KMGRPF
            WHERE KGPKCD = :KgpkCd
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        KmGrpf? row = conn.QueryFirstOrDefault<KmGrpf>(sql, new { KgpkCd = kgpkCd });
        if (row is null || row.DDate.HasValue)
        {
            return null;
        }

        row.GpkNameKj = LegacyTextHelper.NormalizeString(row.GpkNameKj);
        row.GpkNameS = LegacyTextHelper.NormalizeString(row.GpkNameS);
        row.KysCd = LegacyTextHelper.NormalizeString(row.KysCd).Trim();
        return row;
    }

    public IReadOnlyList<KmGrpf> GetAllActive()
    {
        const string sql = """
            SELECT
                DDATE          AS DDate,
                KGPKCD         AS KGpkCd,
                GPKNAMEKJ      AS GpkNameKj,
                GPKNAMES       AS GpkNameS,
                NVL(KYODD, 0)  AS KyoDd,
                KYSCD          AS KysCd
            FROM HAN01.KMGRPF
            ORDER BY KGPKCD
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        var rows = conn.Query<KmGrpf>(sql);
        return rows
            .Where(r => !r.DDate.HasValue)
            .Select(r => new KmGrpf
            {
                DDate = r.DDate,
                KGpkCd = r.KGpkCd,
                GpkNameKj = LegacyTextHelper.NormalizeString(r.GpkNameKj),
                GpkNameS = LegacyTextHelper.NormalizeString(r.GpkNameS),
                KyoDd = r.KyoDd,
                KysCd = LegacyTextHelper.NormalizeString(r.KysCd).Trim()
            })
            .ToList();
    }
}
