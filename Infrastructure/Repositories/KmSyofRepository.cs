using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class KmSyofRepository : IKmSyofRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public KmSyofRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public KmSyof? GetByCode(long kshncd)
    {
        const string sql = """
            SELECT
                TDATE     AS TDate,
                KDATE     AS KDate,
                DDATE     AS DDate,
                KSHNCD    AS KShnCd,
                SHNNAMEKJ AS ShnNameKj,
                SHNNAMEKN AS ShnNameKn,
                SHNNAMES  AS ShnNameS,
                NVL(DAICD, 0)    AS DaiCd,
                NVL(TYUCD, 0)    AS TyuCd,
                NVL(DHSHNCD, 0)  AS DhShnCd,
                NVL(MEKACD, 0)   AS MekaCd,
                NVL(SZKB, 0)     AS SzKb,
                NVL(STDKUR, 0)   AS StdKur,
                NVL(SIRETK, 0)   AS SireTk,
                NVL(GPHNBTK, 0)  AS GphnbTk,
                NVL(JGYOHBTK, 0) AS JgyohbTk,
                NVL(IPKGHBTK, 0) AS IpkghbTk,
                NVL(IPKJHBTK, 0) AS IpkjhbTk,
                NVL(TNICD, 0)    AS TniCd,
                NVL(INSU, 0)     AS InSu,
                NVL(ZKKB, 0)     AS ZkKb,
                NVL(ZKSU, 0)     AS ZkSu,
                NVL(BOKA, 0)     AS Boka
            FROM HAN01.KMSYOF
            WHERE KSHNCD = :KShnCd
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        return conn.QueryFirstOrDefault<KmSyof>(sql, new { KShnCd = kshncd });
    }

    public void Insert(KmSyof entity)
    {
        const string sql = """
            INSERT INTO HAN01.KMSYOF (
                TDATE,
                KDATE,
                DDATE,
                KSHNCD,
                SHNNAMEKJ,
                SHNNAMEKN,
                SHNNAMES,
                DAICD,
                TYUCD,
                DHSHNCD,
                MEKACD,
                SZKB,
                STDKUR,
                SIRETK,
                GPHNBTK,
                JGYOHBTK,
                IPKGHBTK,
                IPKJHBTK,
                TNICD,
                INSU,
                ZKKB,
                ZKSU,
                BOKA
            ) VALUES (
                :TDate,
                :KDate,
                NULL,
                :KShnCd,
                :ShnNameKj,
                :ShnNameKn,
                :ShnNameS,
                :DaiCd,
                :TyuCd,
                :DhShnCd,
                :MekaCd,
                :SzKb,
                :StdKur,
                :SireTk,
                :GphnbTk,
                :JgyohbTk,
                :IpkghbTk,
                :IpkjhbTk,
                :TniCd,
                :InSu,
                :ZkKb,
                :ZkSu,
                :Boka
            )
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        using var tx = conn.BeginTransaction();
        try
        {
            conn.Execute(sql, new
            {
                TDate = (entity.TDate ?? DateTime.Today).Date,
                KDate = (entity.KDate ?? DateTime.Today).Date,
                entity.KShnCd,
                entity.ShnNameKj,
                entity.ShnNameKn,
                entity.ShnNameS,
                entity.DaiCd,
                entity.TyuCd,
                entity.DhShnCd,
                entity.MekaCd,
                entity.SzKb,
                entity.StdKur,
                entity.SireTk,
                entity.GphnbTk,
                entity.JgyohbTk,
                entity.IpkghbTk,
                entity.IpkjhbTk,
                entity.TniCd,
                entity.InSu,
                entity.ZkKb,
                entity.ZkSu,
                entity.Boka
            }, tx);
            tx.Commit();
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    public void Update(KmSyof entity)
    {
        const string sql = """
            UPDATE HAN01.KMSYOF
            SET KDATE     = :KDate,
                DDATE     = NULL,
                SHNNAMEKJ = :ShnNameKj,
                SHNNAMEKN = :ShnNameKn,
                SHNNAMES  = :ShnNameS,
                DAICD     = :DaiCd,
                TYUCD     = :TyuCd,
                DHSHNCD   = :DhShnCd,
                MEKACD    = :MekaCd,
                SZKB      = :SzKb,
                STDKUR    = :StdKur,
                SIRETK    = :SireTk,
                GPHNBTK   = :GphnbTk,
                JGYOHBTK  = :JgyohbTk,
                IPKGHBTK  = :IpkghbTk,
                IPKJHBTK  = :IpkjhbTk,
                TNICD     = :TniCd,
                INSU      = :InSu,
                ZKKB      = :ZkKb,
                ZKSU      = :ZkSu,
                BOKA      = :Boka
            WHERE KSHNCD = :KShnCd
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        using var tx = conn.BeginTransaction();
        try
        {
            conn.Execute(sql, new
            {
                KDate = (entity.KDate ?? DateTime.Today).Date,
                entity.ShnNameKj,
                entity.ShnNameKn,
                entity.ShnNameS,
                entity.DaiCd,
                entity.TyuCd,
                entity.DhShnCd,
                entity.MekaCd,
                entity.SzKb,
                entity.StdKur,
                entity.SireTk,
                entity.GphnbTk,
                entity.JgyohbTk,
                entity.IpkghbTk,
                entity.IpkjhbTk,
                entity.TniCd,
                entity.InSu,
                entity.ZkKb,
                entity.ZkSu,
                entity.Boka,
                entity.KShnCd
            }, tx);
            tx.Commit();
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    public void SoftDelete(long kshncd, DateTime deletedDate)
    {
        const string sql = """
            UPDATE HAN01.KMSYOF
            SET DDATE = :DDate
            WHERE KSHNCD = :KShnCd
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        using var tx = conn.BeginTransaction();
        try
        {
            conn.Execute(sql, new
            {
                DDate = deletedDate.Date,
                KShnCd = kshncd
            }, tx);
            tx.Commit();
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    public IReadOnlyList<KmSyof> SearchActiveByCategory(int daiCd, int tyuCd, int? mekaCd)
    {
        string sql = mekaCd.HasValue
            ? """
              SELECT
                  DDATE     AS DDate,
                  KSHNCD    AS KShnCd,
                  SHNNAMEKN AS ShnNameKn,
                  SHNNAMEKJ AS ShnNameKj
              FROM HAN01.KMSYOF
              WHERE DAICD  = :DaiCd
                AND TYUCD  = :TyuCd
                AND MEKACD = :MekaCd
              ORDER BY SHNNAMEKJ
              """
            : """
              SELECT
                  DDATE     AS DDate,
                  KSHNCD    AS KShnCd,
                  SHNNAMEKN AS ShnNameKn,
                  SHNNAMEKJ AS ShnNameKj
              FROM HAN01.KMSYOF
              WHERE DAICD = :DaiCd
                AND TYUCD = :TyuCd
              ORDER BY SHNNAMEKJ
              """;

        using var conn = _connectionFactory.CreateOpenConnection();
        var rows = conn.Query<KmSyof>(sql, new { DaiCd = daiCd, TyuCd = tyuCd, MekaCd = mekaCd });
        return rows.Where(r => !r.DDate.HasValue).ToList();
    }

    public IReadOnlyList<KmSyof> SearchActiveByName(
        string keyword,
        int matchMode,
        bool fuzzySearch,
        int? daiCd,
        int? tyuCd,
        int? mekaCd)
    {
        string prefix = matchMode is 0 or 2 ? "%" : "";
        string suffix = matchMode is 0 or 1 ? "%" : "";

        var sb = new System.Text.StringBuilder();
        sb.AppendLine("SELECT DDATE AS DDate, KSHNCD AS KShnCd, SHNNAMEKN AS ShnNameKn, SHNNAMEKJ AS ShnNameKj");
        sb.AppendLine("FROM HAN01.KMSYOF");

        if (fuzzySearch)
        {
            sb.Append("WHERE UTL_I18N.TRANSLITERATE(UPPER(TO_SINGLE_BYTE(REPLACE(REPLACE(SHNNAMEKN,' ',''),'　',''))),'kana_fwkatakana') ");
            sb.AppendLine("LIKE :Prefix || UTL_I18N.TRANSLITERATE(UPPER(TO_SINGLE_BYTE(:Keyword)),'kana_fwkatakana') || :Suffix");
        }
        else
        {
            sb.AppendLine("WHERE REPLACE(REPLACE(SHNNAMEKN,' ',''),'　','') LIKE :Prefix || :Keyword || :Suffix");
        }

        if (daiCd.HasValue)
        {
            sb.AppendLine("  AND DAICD = :DaiCd");
            if (tyuCd.HasValue)
            {
                sb.AppendLine("  AND TYUCD = :TyuCd");
            }
        }

        if (mekaCd.HasValue)
        {
            sb.AppendLine("  AND MEKACD = :MekaCd");
        }

        sb.AppendLine("ORDER BY SHNNAMEKJ");

        using var conn = _connectionFactory.CreateOpenConnection();
        var rows = conn.Query<KmSyof>(
            sb.ToString(),
            new
            {
                Prefix = prefix,
                Suffix = suffix,
                Keyword = keyword ?? string.Empty,
                DaiCd = daiCd,
                TyuCd = tyuCd,
                MekaCd = mekaCd
            });
        return rows.Where(r => !r.DDate.HasValue).ToList();
    }

    public IReadOnlyList<KmSyof> GetActiveStockTargetsByDepartment(int bmCd)
    {
        const string sql = """
            SELECT
                KSHNCD        AS KShnCd,
                NVL(INSU, 0)  AS InSu,
                NVL(ZKSU, 0)  AS ZkSu,
                NVL(BOKA, 0)  AS Boka
            FROM HAN01.KMSYOF, HAN01.KMBNRI
            WHERE DAICD = KDAICD
              AND KTYUCD = 0
              AND ZKKB = 1
              AND KMSYOF.DDATE IS NULL
              AND BMCD = :BmCd
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        return conn.Query<KmSyof>(sql, new { BmCd = bmCd }).ToList();
    }

    public IReadOnlyList<KmSyof> GetActiveStockTargetByCode(long kshncd)
    {
        const string sql = """
            SELECT
                KSHNCD        AS KShnCd,
                NVL(INSU, 0)  AS InSu,
                NVL(ZKSU, 0)  AS ZkSu,
                NVL(BOKA, 0)  AS Boka
            FROM HAN01.KMSYOF
            WHERE ZKKB = 1
              AND DDATE IS NULL
              AND KSHNCD = :KShnCd
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        return conn.Query<KmSyof>(sql, new { KShnCd = kshncd }).ToList();
    }
}

