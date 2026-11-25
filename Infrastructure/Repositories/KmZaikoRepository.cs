using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class KmZaikoRepository : IKmZaikoRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public KmZaikoRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    private sealed class BalanceRow
    {
        public decimal TgmtSuu { get; set; }
        public long TgmstKin { get; set; }
    }

    private sealed class SumSuuKinRow
    {
        public decimal SumSuu { get; set; }
        public long SumKin { get; set; }
    }

    public IReadOnlyList<KmZaiko> GetHistoryFromMonth(long kshncd, long fromYearMonth)
    {
        const string sql = """
            SELECT
                TDATE            AS TDate,
                KDATE            AS KDate,
                KZKDAY           AS KzkDay,
                KSHNCD           AS KShnCd,
                NVL(ZNMTSUU, 0)  AS ZnmtSuu,
                NVL(ZNMTKIN, 0)  AS ZnmtKin,
                NVL(TGNYSUU, 0)  AS TgnySuu,
                NVL(TGNYKIN, 0)  AS TgnyKin,
                NVL(TGSYSUU, 0)  AS TgsySuu,
                NVL(TGSYKIN, 0)  AS TgsyKin,
                NVL(TGCSSUU, 0)  AS TgcsSuu,
                NVL(TGCSKIN, 0)  AS TgcsKin,
                NVL(TGMTSUU, 0)  AS TgmtSuu,
                NVL(TGMSTKIN, 0) AS TgmstKin
            FROM HAN01.KMZAIKO
            WHERE KZKDAY >= :FromYearMonth
              AND KSHNCD = :KShnCd
            ORDER BY KZKDAY
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        return conn.Query<KmZaiko>(sql, new { FromYearMonth = fromYearMonth, KShnCd = kshncd }).ToList();
    }

    public (decimal TgmtSuu, long TgmstKin)? GetEndingBalance(long kshncd, long yearMonth)
    {
        const string sql = """
            SELECT
                NVL(TGMTSUU, 0)  AS TgmtSuu,
                NVL(TGMSTKIN, 0) AS TgmstKin
            FROM HAN01.KMZAIKO
            WHERE KZKDAY = :YearMonth
              AND KSHNCD = :KShnCd
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        BalanceRow? row = conn.QueryFirstOrDefault<BalanceRow>(sql, new { YearMonth = yearMonth, KShnCd = kshncd });
        if (row is null)
        {
            return null;
        }

        return (row.TgmtSuu, row.TgmstKin);
    }

    public (decimal SumSirSu, long SumSirKin)? GetMonthlyPurchaseTotal(long shncd, long fromYmd, long toYmd)
    {
        const string sql = """
            SELECT
                NVL(SUM(SIRSU), 0)  AS SumSuu,
                NVL(SUM(SIRKIN), 0) AS SumKin
            FROM HAN01.KTSJNL
            WHERE SHNCD = :ShnCd
              AND (SIRDAY BETWEEN :FromYmd AND :ToYmd)
              AND CKSFLG = 1
            GROUP BY SHNCD
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        SumSuuKinRow? row = conn.QueryFirstOrDefault<SumSuuKinRow>(sql, new { ShnCd = shncd, FromYmd = fromYmd, ToYmd = toYmd });
        if (row is null)
        {
            return null;
        }

        return (row.SumSuu, row.SumKin);
    }

    public (decimal SumChoSuu, long SumChoKin)? GetMonthlyAdjustmentTotal(long shncd, long fromYmd, long toYmd)
    {
        const string sql = """
            SELECT
                NVL(SUM(CHOSUU), 0) AS SumSuu,
                NVL(SUM(CHOKIN), 0) AS SumKin
            FROM HAN01.KTZJNL
            WHERE SHNCD = :ShnCd
              AND (CHODAY >= :FromYmd AND CHODAY <= :ToYmd)
            GROUP BY SHNCD
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        SumSuuKinRow? row = conn.QueryFirstOrDefault<SumSuuKinRow>(sql, new { ShnCd = shncd, FromYmd = fromYmd, ToYmd = toYmd });
        if (row is null)
        {
            return null;
        }

        return (row.SumSuu, row.SumKin);
    }

    public (decimal SumChoSuu, long SumChoKin)? GetMonthlyPositiveAdjustmentTotal(long shncd, long fromYmd, long toYmd)
    {
        const string sql = """
            SELECT
                NVL(SUM(CHOSUU), 0) AS SumSuu,
                NVL(SUM(CHOKIN), 0) AS SumKin
            FROM HAN01.KTZJNL
            WHERE SHNCD = :ShnCd
              AND (CHODAY >= :FromYmd AND CHODAY <= :ToYmd)
              AND (CHOKU = 21 OR CHOKU = 22 OR CHOKU = 23)
            GROUP BY SHNCD
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        SumSuuKinRow? row = conn.QueryFirstOrDefault<SumSuuKinRow>(sql, new { ShnCd = shncd, FromYmd = fromYmd, ToYmd = toYmd });
        if (row is null)
        {
            return null;
        }

        return (row.SumSuu, row.SumKin);
    }

    public decimal? GetMonthlySalesQuantity(long shncd, long fromYmd, long toYmd)
    {
        const string sql = """
            SELECT
                NVL(SUM(URISU), 0) AS SumSuu
            FROM HAN01.KTUJNL
            WHERE SHNCD = :ShnCd
              AND (HBIDAY BETWEEN :FromYmd AND :ToYmd)
              AND CKSFLG = 1
            GROUP BY SHNCD
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        return conn.QueryFirstOrDefault<decimal?>(sql, new { ShnCd = shncd, FromYmd = fromYmd, ToYmd = toYmd });
    }

    public int DeleteInvalidProductsForMonth(long kzkDay)
    {
        const string sql = """
            DELETE FROM HAN01.KMZAIKO
            WHERE KZKDAY = :KzkDay
              AND KSHNCD IN (SELECT KSHNCD FROM HAN01.KMSYOF WHERE DDATE IS NOT NULL OR ZKKB = 0)
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        using var tran = conn.BeginTransaction();
        try
        {
            int affected = conn.Execute(sql, new { KzkDay = kzkDay }, tran);
            tran.Commit();
            return affected;
        }
        catch
        {
            tran.Rollback();
            throw;
        }
    }

    public void Insert(KmZaiko entity)
    {
        const string sql = """
            INSERT INTO HAN01.KMZAIKO (
                TDATE,
                KDATE,
                DDATE,
                KZKDAY,
                KSHNCD,
                ZNMTSUU,
                ZNMTKIN,
                TGNYSUU,
                TGNYKIN,
                TGSYSUU,
                TGSYKIN,
                TGCSSUU,
                TGCSKIN,
                TGMTSUU,
                TGMSTKIN
            ) VALUES (
                :TDate,
                :KDate,
                NULL,
                :KzkDay,
                :KShnCd,
                :ZnmtSuu,
                :ZnmtKin,
                :TgnySuu,
                :TgnyKin,
                :TgsySuu,
                :TgsyKin,
                :TgcsSuu,
                :TgcsKin,
                :TgmtSuu,
                :TgmstKin
            )
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        using var tran = conn.BeginTransaction();
        try
        {
            conn.Execute(sql, entity, tran);
            tran.Commit();
        }
        catch
        {
            tran.Rollback();
            throw;
        }
    }

    public void Update(KmZaiko entity)
    {
        const string sql = """
            UPDATE HAN01.KMZAIKO
            SET KDATE    = :KDate,
                ZNMTSUU  = :ZnmtSuu,
                ZNMTKIN  = :ZnmtKin,
                TGNYSUU  = :TgnySuu,
                TGNYKIN  = :TgnyKin,
                TGSYSUU  = :TgsySuu,
                TGSYKIN  = :TgsyKin,
                TGCSSUU  = :TgcsSuu,
                TGCSKIN  = :TgcsKin,
                TGMTSUU  = :TgmtSuu,
                TGMSTKIN = :TgmstKin
            WHERE KZKDAY = :KzkDay
              AND KSHNCD = :KShnCd
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        using var tran = conn.BeginTransaction();
        try
        {
            conn.Execute(sql, entity, tran);
            tran.Commit();
        }
        catch
        {
            tran.Rollback();
            throw;
        }
    }
}

