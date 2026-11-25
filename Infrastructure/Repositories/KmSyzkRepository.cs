using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class KmSyzkRepository : IKmSyzkRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public KmSyzkRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void TestConnection()
    {
        using var conn = _connectionFactory.CreateOpenConnection();
        conn.ExecuteScalar<int>("SELECT 1 FROM DUAL");
    }

    public KmSyzk? GetByCode(int kszCd)
    {
        const string sql = """
            SELECT
                TDATE    AS TDate,
                KDATE    AS KDate,
                DDATE    AS DDate,
                KSZCD    AS KSzCd,
                SZKB     AS SzKb,
                SZNAMEKN AS SzNameKn,
                SZNAMEKJ AS SzNameKj,
                SZNAMES  AS SzNameS,
                BROKCD   AS BrokCd,
                SYUKNCD  AS SyuknCd,
                GPKCD    AS GpkCd
            FROM HAN01.KMSYZK
            WHERE KSZCD = :KSzCd
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        return conn.QueryFirstOrDefault<KmSyzk>(sql, new { KSzCd = kszCd });
    }

    public void Insert(KmSyzk entity)
    {
        const string sql = """
            INSERT INTO HAN01.KMSYZK (
                TDATE,
                KDATE,
                DDATE,
                KSZCD,
                SZKB,
                SZNAMEKN,
                SZNAMEKJ,
                SZNAMES,
                BROKCD,
                SYUKNCD,
                GPKCD
            ) VALUES (
                :TDate,
                :KDate,
                NULL,
                :KSzCd,
                :SzKb,
                :SzNameKn,
                :SzNameKj,
                :SzNameS,
                :BrokCd,
                :SyuknCd,
                :GpkCd
            )
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        conn.Execute(sql, new
        {
            TDate = entity.TDate?.Date ?? DateTime.Today,
            KDate = entity.KDate?.Date ?? DateTime.Today,
            entity.KSzCd,
            entity.SzKb,
            SzNameKn = string.IsNullOrEmpty(entity.SzNameKn) ? null : entity.SzNameKn,
            SzNameKj = string.IsNullOrEmpty(entity.SzNameKj) ? null : entity.SzNameKj,
            SzNameS = string.IsNullOrEmpty(entity.SzNameS) ? null : entity.SzNameS,
            entity.BrokCd,
            entity.SyuknCd,
            entity.GpkCd
        });
    }

    public void Update(KmSyzk entity)
    {
        const string sql = """
            UPDATE HAN01.KMSYZK
               SET KDATE    = :KDate,
                   DDATE    = NULL,
                   SZKB     = :SzKb,
                   SZNAMEKN = :SzNameKn,
                   SZNAMEKJ = :SzNameKj,
                   SZNAMES  = :SzNameS,
                   BROKCD   = :BrokCd,
                   SYUKNCD  = :SyuknCd,
                   GPKCD    = :GpkCd
             WHERE KSZCD    = :KSzCd
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        conn.Execute(sql, new
        {
            KDate = entity.KDate?.Date ?? DateTime.Today,
            entity.SzKb,
            SzNameKn = string.IsNullOrEmpty(entity.SzNameKn) ? null : entity.SzNameKn,
            SzNameKj = string.IsNullOrEmpty(entity.SzNameKj) ? null : entity.SzNameKj,
            SzNameS = string.IsNullOrEmpty(entity.SzNameS) ? null : entity.SzNameS,
            entity.BrokCd,
            entity.SyuknCd,
            entity.GpkCd,
            entity.KSzCd
        });
    }

    public void SoftDelete(int kszCd, DateTime deletedDate)
    {
        const string sql = """
            UPDATE HAN01.KMSYZK
               SET DDATE = :DDate
             WHERE KSZCD = :KSzCd
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        conn.Execute(sql, new
        {
            DDate = deletedDate.Date,
            KSzCd = kszCd
        });
    }
}
