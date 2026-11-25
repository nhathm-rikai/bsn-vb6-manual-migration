using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class KmBnriRepository : IKmBnriRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public KmBnriRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void TestConnection()
    {
        using var conn = _connectionFactory.CreateOpenConnection();
        conn.ExecuteScalar<int>("SELECT 1 FROM DUAL");
    }

    public KmBnri? GetByKey(int daiCd, int tyuCd)
    {
        const string sql = """
            SELECT
                TDATE    AS TDate,
                KDATE    AS KDate,
                DDATE    AS DDate,
                KDAICD   AS KDaiCd,
                KTYUCD   AS KTyuCd,
                DTNAME   AS DtName,
                BMCD     AS BmCd,
                SRITU    AS Sritu,
                SHCDR    AS ShCdr,
                STEKIYO  AS Stekiyo,
                SRITU2   AS Sritu2,
                KEIRIDDS AS Keiridds
            FROM HAN01.KMBNRI
            WHERE KDAICD = :F001
              AND KTYUCD = :F002
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        return conn.QueryFirstOrDefault<KmBnri>(sql, new { F001 = daiCd, F002 = tyuCd });
    }

    public bool ExistsInProductMaster(int daiCd, int tyuCd)
    {
        const string sql = """
            SELECT 1
            FROM HAN01.KMSYOF
            WHERE DAICD = :DaiCd
              AND TYUCD = :TyuCd
              AND ROWNUM = 1
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        int? exists = conn.QueryFirstOrDefault<int?>(sql, new { DaiCd = daiCd, TyuCd = tyuCd });
        return exists.HasValue;
    }

    public void Insert(KmBnri entity)
    {
        const string sql = """
            INSERT INTO HAN01.KMBNRI (
                TDATE,
                KDATE,
                DDATE,
                KDAICD,
                KTYUCD,
                DTNAME,
                BMCD,
                SRITU,
                STEKIYO,
                SRITU2,
                KEIRIDDS
            ) VALUES (
                :TDate,
                :KDate,
                NULL,
                :F001,
                :F002,
                :F010,
                :F020,
                :F030,
                :F040,
                :F050,
                :F060
            )
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        using var tx = conn.BeginTransaction();
        try
        {
            conn.Execute(sql, new
            {
                TDate = entity.TDate ?? DateTime.Today,
                KDate = entity.KDate ?? DateTime.Today,
                F001 = entity.KDaiCd,
                F002 = entity.KTyuCd,
                F010 = entity.DtName,
                F020 = entity.BmCd,
                F030 = entity.Sritu ?? 0m,
                F040 = entity.Stekiyo ?? 0L,
                F050 = entity.Sritu2 ?? 0m,
                F060 = entity.Keiridds ?? 0
            }, tx);
            tx.Commit();
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    public void Update(KmBnri entity)
    {
        const string sql = """
            UPDATE HAN01.KMBNRI
            SET KDATE    = :KDate,
                DDATE    = NULL,
                DTNAME   = :F010,
                BMCD     = :F020,
                SRITU    = :F030,
                STEKIYO  = :F040,
                SRITU2   = :F050,
                KEIRIDDS = :F060
            WHERE KDAICD = :F001
              AND KTYUCD = :F002
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        using var tx = conn.BeginTransaction();
        try
        {
            conn.Execute(sql, new
            {
                KDate = entity.KDate ?? DateTime.Today,
                F001 = entity.KDaiCd,
                F002 = entity.KTyuCd,
                F010 = entity.DtName,
                F020 = entity.BmCd,
                F030 = entity.Sritu ?? 0m,
                F040 = entity.Stekiyo ?? 0L,
                F050 = entity.Sritu2 ?? 0m,
                F060 = entity.Keiridds ?? 0
            }, tx);
            tx.Commit();
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    public void SoftDelete(int daiCd, int tyuCd, DateTime deletedDate)
    {
        const string sql = """
            UPDATE HAN01.KMBNRI
            SET DDATE = :DDate
            WHERE KDAICD = :F001
              AND KTYUCD = :F002
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        using var tx = conn.BeginTransaction();
        try
        {
            conn.Execute(sql, new
            {
                DDate = deletedDate.Date,
                F001 = daiCd,
                F002 = tyuCd
            }, tx);
            tx.Commit();
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    public void UpdateProductSeq(int daiCd, int newShCdr, DateTime kDate)
    {
        const string sql = """
            UPDATE HAN01.KMBNRI
            SET KDATE = :KDate,
                SHCDR = :ShCdr
            WHERE KDAICD = :DaiCd
              AND KTYUCD = 0
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        using var tx = conn.BeginTransaction();
        try
        {
            conn.Execute(sql, new
            {
                KDate = kDate.Date,
                ShCdr = newShCdr,
                DaiCd = daiCd
            }, tx);
            tx.Commit();
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    public IReadOnlyList<KmBnri> GetActiveMajorCategories()
    {
        const string sql = """
            SELECT
                DDATE  AS DDate,
                KDAICD AS KDaiCd,
                KTYUCD AS KTyuCd,
                DTNAME AS DtName
            FROM HAN01.KMBNRI
            WHERE KTYUCD = 0
            ORDER BY KDAICD
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        var rows = conn.Query<KmBnri>(sql);
        return rows.Where(r => !r.DDate.HasValue).ToList();
    }

    public IReadOnlyList<KmBnri> GetActiveMinorCategories(int daiCd)
    {
        const string sql = """
            SELECT
                DDATE  AS DDate,
                KDAICD AS KDaiCd,
                KTYUCD AS KTyuCd,
                DTNAME AS DtName
            FROM HAN01.KMBNRI
            WHERE KDAICD = :DaiCd
            ORDER BY KTYUCD
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        var rows = conn.Query<KmBnri>(sql, new { DaiCd = daiCd });
        return rows.Where(r => !r.DDate.HasValue && r.KTyuCd != 0).ToList();
    }
}
