using Application.Common;
using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class KmMeisRepository : IKmMeisRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public KmMeisRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    private sealed class MeisRow
    {
        public DateTime? DDate { get; set; }
        public string? Meisyos { get; set; }
    }

    public string? GetShortName(int recKb, int meisCd)
    {
        const string sql = """
            SELECT
                DDATE   AS DDate,
                MEISYOS AS Meisyos
            FROM HAN01.KMMEIS
            WHERE KRECKB  = :RecKb
              AND KMEISCD = :MeisCd
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        MeisRow? row = conn.QueryFirstOrDefault<MeisRow>(sql, new { RecKb = recKb, MeisCd = meisCd });
        if (row is null || row.DDate.HasValue)
        {
            return null;
        }

        return LegacyTextHelper.NormalizeString(row.Meisyos);
    }

    public string? GetShortNameRaw(int recKb, int meisCd)
    {
        const string sql = """
            SELECT MEISYOS AS Meisyos
            FROM HAN01.KMMEIS
            WHERE KRECKB  = :RecKb
              AND KMEISCD = :MeisCd
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        MeisRow? row = conn.QueryFirstOrDefault<MeisRow>(sql, new { RecKb = recKb, MeisCd = meisCd });
        if (row is null)
        {
            return null;
        }

        return LegacyTextHelper.NormalizeString(row.Meisyos);
    }

    public IReadOnlyList<KmMeis> GetActiveByRange(int recKb, int minCode, int maxCode)
    {
        const string sql = """
            SELECT
                DDATE    AS DDate,
                KRECKB   AS KRecKb,
                KMEISCD  AS KMeisCd,
                MEISYOKJ AS MeisyoKj,
                MEISYOS  AS MeisyoS
            FROM HAN01.KMMEIS
            WHERE KRECKB  = :RecKb
              AND KMEISCD >= :MinCode
              AND KMEISCD <= :MaxCode
            ORDER BY KMEISCD
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        var rows = conn.Query<KmMeis>(sql, new { RecKb = recKb, MinCode = minCode, MaxCode = maxCode });
        return rows.Where(r => !r.DDate.HasValue).ToList();
    }

    public IReadOnlyList<KmMeis> GetActiveByRecKb(int recKb)
    {
        const string sql = """
            SELECT
                DDATE    AS DDate,
                KRECKB   AS KRecKb,
                KMEISCD  AS KMeisCd,
                MEISYOKJ AS MeisyoKj,
                MEISYOS  AS MeisyoS
            FROM HAN01.KMMEIS
            WHERE KRECKB = :RecKb
            ORDER BY KMEISCD
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        var rows = conn.Query<KmMeis>(sql, new { RecKb = recKb });
        return rows.Where(r => !r.DDate.HasValue).ToList();
    }

    public void TestConnection()
    {
        using var conn = _connectionFactory.CreateOpenConnection();
        conn.ExecuteScalar<int>("SELECT 1 FROM DUAL");
    }

    public KmMeis? GetByKey(int recKb, int meisCd)
    {
        const string sql = """
            SELECT
                TDATE    AS TDate,
                KDATE    AS KDate,
                DDATE    AS DDate,
                KRECKB   AS KRecKb,
                KMEISCD  AS KMeisCd,
                MEISYOKN AS MeisyoKn,
                MEISYOKJ AS MeisyoKj,
                MEISYOS  AS MeisyoS
            FROM HAN01.KMMEIS
            WHERE KRECKB  = :RecKb
              AND KMEISCD = :MeisCd
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        KmMeis? row = conn.QueryFirstOrDefault<KmMeis>(sql, new { RecKb = recKb, MeisCd = meisCd });
        if (row is null)
        {
            return null;
        }

        row.MeisyoKn = LegacyTextHelper.NormalizeString(row.MeisyoKn);
        row.MeisyoKj = LegacyTextHelper.NormalizeString(row.MeisyoKj);
        row.MeisyoS = LegacyTextHelper.NormalizeString(row.MeisyoS);
        return row;
    }

    public void Insert(KmMeis entity)
    {
        const string sql = """
            INSERT INTO HAN01.KMMEIS (
                TDATE,
                KDATE,
                DDATE,
                KRECKB,
                KMEISCD,
                MEISYOKN,
                MEISYOKJ,
                MEISYOS
            ) VALUES (
                :TDate,
                :KDate,
                NULL,
                :KRecKb,
                :KMeisCd,
                :MeisyoKn,
                :MeisyoKj,
                :MeisyoS
            )
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        using var tran = conn.BeginTransaction();
        try
        {
            conn.Execute(sql, new
            {
                TDate = entity.TDate?.Date ?? DateTime.Today,
                KDate = entity.KDate?.Date ?? DateTime.Today,
                entity.KRecKb,
                entity.KMeisCd,
                MeisyoKn = string.IsNullOrEmpty(entity.MeisyoKn) ? null : entity.MeisyoKn,
                MeisyoKj = string.IsNullOrEmpty(entity.MeisyoKj) ? null : entity.MeisyoKj,
                MeisyoS = string.IsNullOrEmpty(entity.MeisyoS) ? null : entity.MeisyoS
            }, tran);
            tran.Commit();
        }
        catch
        {
            tran.Rollback();
            throw;
        }
    }

    public void Update(KmMeis entity)
    {
        const string sql = """
            UPDATE HAN01.KMMEIS
               SET KDATE    = :KDate,
                   DDATE    = NULL,
                   MEISYOKN = :MeisyoKn,
                   MEISYOKJ = :MeisyoKj,
                   MEISYOS  = :MeisyoS
             WHERE KRECKB   = :KRecKb
               AND KMEISCD  = :KMeisCd
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        using var tran = conn.BeginTransaction();
        try
        {
            conn.Execute(sql, new
            {
                KDate = entity.KDate?.Date ?? DateTime.Today,
                MeisyoKn = string.IsNullOrEmpty(entity.MeisyoKn) ? null : entity.MeisyoKn,
                MeisyoKj = string.IsNullOrEmpty(entity.MeisyoKj) ? null : entity.MeisyoKj,
                MeisyoS = string.IsNullOrEmpty(entity.MeisyoS) ? null : entity.MeisyoS,
                entity.KRecKb,
                entity.KMeisCd
            }, tran);
            tran.Commit();
        }
        catch
        {
            tran.Rollback();
            throw;
        }
    }

    public void SoftDelete(int recKb, int meisCd, DateTime deletedDate)
    {
        const string sql = """
            UPDATE HAN01.KMMEIS
               SET DDATE   = :DDate
             WHERE KRECKB  = :RecKb
               AND KMEISCD = :MeisCd
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        using var tran = conn.BeginTransaction();
        try
        {
            conn.Execute(sql, new { DDate = deletedDate.Date, RecKb = recKb, MeisCd = meisCd }, tran);
            tran.Commit();
        }
        catch
        {
            tran.Rollback();
            throw;
        }
    }
}
