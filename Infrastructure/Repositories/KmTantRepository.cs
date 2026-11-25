using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class KmTantRepository : IKmTantRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public KmTantRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void TestConnection()
    {
        using var conn = _connectionFactory.CreateOpenConnection();
        conn.ExecuteScalar<int>("SELECT 1 FROM DUAL");
    }

    public KmTant? GetByCode(int ktantCd)
    {
        const string sql = """
            SELECT
                TDATE      AS TDate,
                KDATE      AS KDate,
                DDATE      AS DDate,
                KTANTCD    AS KTantCd,
                TANTNAMEKN AS TantNameKn,
                TANTNAMEKJ AS TantNameKj,
                TANTNAMES  AS TantNameS,
                BMCD       AS BmCd
            FROM HAN01.KMTANT
            WHERE KTANTCD = :KTantCd
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        return conn.QueryFirstOrDefault<KmTant>(sql, new { KTantCd = ktantCd });
    }

    public IReadOnlyList<KmTant> GetAllActive()
    {
        const string sql = """
            SELECT
                DDATE      AS DDate,
                KTANTCD    AS KTantCd,
                TANTNAMEKJ AS TantNameKj,
                TANTNAMES  AS TantNameS
            FROM HAN01.KMTANT
            ORDER BY KTANTCD
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        var rows = conn.Query<KmTant>(sql);
        return rows.Where(r => !r.DDate.HasValue).ToList();
    }

    public void Insert(KmTant entity)
    {
        const string sql = """
            INSERT INTO HAN01.KMTANT (
                TDATE,
                KDATE,
                DDATE,
                KTANTCD,
                TANTNAMEKN,
                TANTNAMEKJ,
                TANTNAMES,
                BMCD
            ) VALUES (
                :TDate,
                :KDate,
                NULL,
                :KTantCd,
                :TantNameKn,
                :TantNameKj,
                :TantNameS,
                :BmCd
            )
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        conn.Execute(sql, new
        {
            TDate = entity.TDate?.Date ?? DateTime.Today,
            KDate = entity.KDate?.Date ?? DateTime.Today,
            entity.KTantCd,
            TantNameKn = string.IsNullOrEmpty(entity.TantNameKn) ? null : entity.TantNameKn,
            TantNameKj = string.IsNullOrEmpty(entity.TantNameKj) ? null : entity.TantNameKj,
            TantNameS = string.IsNullOrEmpty(entity.TantNameS) ? null : entity.TantNameS,
            entity.BmCd
        });
    }

    public void Update(KmTant entity)
    {
        const string sql = """
            UPDATE HAN01.KMTANT
               SET KDATE      = :KDate,
                   DDATE      = NULL,
                   TANTNAMEKN = :TantNameKn,
                   TANTNAMEKJ = :TantNameKj,
                   TANTNAMES  = :TantNameS,
                   BMCD       = :BmCd
             WHERE KTANTCD    = :KTantCd
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        conn.Execute(sql, new
        {
            KDate = entity.KDate?.Date ?? DateTime.Today,
            TantNameKn = string.IsNullOrEmpty(entity.TantNameKn) ? null : entity.TantNameKn,
            TantNameKj = string.IsNullOrEmpty(entity.TantNameKj) ? null : entity.TantNameKj,
            TantNameS = string.IsNullOrEmpty(entity.TantNameS) ? null : entity.TantNameS,
            entity.BmCd,
            entity.KTantCd
        });
    }

    public void SoftDelete(int ktantCd, DateTime deletedDate)
    {
        const string sql = """
            UPDATE HAN01.KMTANT
               SET DDATE   = :DDate
             WHERE KTANTCD = :KTantCd
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        conn.Execute(sql, new
        {
            DDate = deletedDate.Date,
            KTantCd = ktantCd
        });
    }
}

