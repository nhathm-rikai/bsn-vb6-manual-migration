using System.Text;
using Application.Common;
using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class KmKyakRepository : IKmKyakRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public KmKyakRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    private sealed class KyakNameRow
    {
        public DateTime? DDate { get; set; }
        public string? KykNameKj { get; set; }
        public string? KykNameS { get; set; }
    }

    public void TestConnection()
    {
        using var conn = _connectionFactory.CreateOpenConnection();
        conn.ExecuteScalar<int>("SELECT 1 FROM DUAL");
    }

    public string GetActiveCustomerName(string kkykCd)
    {
        const string sql = """
            SELECT
                DDATE     AS DDate,
                KYKNAMEKJ AS KykNameKj
            FROM HAN01.KMKYAK
            WHERE KKYKCD = :KkykCd
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        KyakNameRow? row = conn.QueryFirstOrDefault<KyakNameRow>(sql, new { KkykCd = kkykCd });
        if (row is null || row.DDate.HasValue)
        {
            return string.Empty;
        }

        return LegacyTextHelper.NormalizeString(row.KykNameKj);
    }

    public string? GetActiveShortName(string kkykCd)
    {
        const string sql = """
            SELECT
                DDATE    AS DDate,
                KYKNAMES AS KykNameS
            FROM HAN01.KMKYAK
            WHERE KKYKCD = :KkykCd
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        KyakNameRow? row = conn.QueryFirstOrDefault<KyakNameRow>(sql, new { KkykCd = kkykCd });
        if (row is null || row.DDate.HasValue)
        {
            return null;
        }

        return LegacyTextHelper.NormalizeString(row.KykNameS);
    }

    public KmKyak? GetByCode(string kkykCd)
    {
        const string sql = """
            SELECT
                TDATE            AS TDate,
                KDATE            AS KDate,
                DDATE            AS DDate,
                KKYKCD           AS KKykCd,
                NVL(KKYKKB, 0)   AS KKykKb,
                KYKNAMEKN        AS KykNameKn,
                KYKNAMEKJ        AS KykNameKj,
                KYKNAMETG        AS KykNameTg,
                KYKNAMES         AS KykNameS,
                UBNNO            AS UbnNo,
                ADD1             AS Add1,
                ADD2             AS Add2,
                ADD3             AS Add3,
                TELNO            AS TelNo,
                FAXNO            AS FaxNo,
                SKSCD            AS SksCd,
                SHSCD            AS ShsCd,
                NVL(GPKCD, 0)    AS GpkCd,
                NVL(SZCD, 0)     AS SzCd,
                NVL(BKCD, 0)     AS BkCd,
                NVL(MBNCD, 0)    AS MbnCd,
                NVL(SKNMCD, 0)   AS SknmCd,
                NVL(SEIBT, 0)    AS Seibt,
                NVL(BIRTHDAY, 0) AS Birthday,
                NVL(SINNO, 0)    AS SinNo,
                NVL(HHKNNO, 0)   AS HhknNo,
                NVL(DRPDAY, 0)   AS DrpDay,
                NVL(TKKB, 0)     AS TkKb,
                NVL(SSDD1, 0)    AS Ssdd1,
                NVL(INGTKB1, 0)  AS IngtKb1,
                NVL(INDD1, 0)    AS Indd1,
                NVL(SSDD2, 0)    AS Ssdd2,
                NVL(INGTKB2, 0)  AS IngtKb2,
                NVL(INDD2, 0)    AS Indd2,
                NVL(SSDD3, 0)    AS Ssdd3,
                NVL(INGTKB3, 0)  AS IngtKb3,
                NVL(INDD3, 0)    AS Indd3,
                NVL(SZKKB, 0)    AS SzkKb,
                NVL(SSYOKB, 0)   AS SsyoKb,
                NVL(THYOKB, 0)   AS ThyoKb,
                NVL(SHSDD1, 0)   AS Shsdd1,
                NVL(SHGTKB1, 0)  AS ShgtKb1,
                NVL(SHYDD1, 0)   AS Shydd1,
                NVL(SHSDD2, 0)   AS Shsdd2,
                NVL(SHGTKB2, 0)  AS ShgtKb2,
                NVL(SHYDD2, 0)   AS Shydd2,
                NVL(SHSDD3, 0)   AS Shsdd3,
                NVL(SHGTKB3, 0)  AS ShgtKb3,
                NVL(SHYDD3, 0)   AS Shydd3,
                NVL(SYHZKKB, 0)  AS SyhzkKb,
                NVL(GKCD1, 0)    AS GkCd1,
                NVL(KZSBT1, 0)   AS KzSbt1,
                KZNO1            AS KzNo1,
                KZMEGI1          AS KzMegi1,
                NVL(GKCD2, 0)    AS GkCd2,
                NVL(KZSBT2, 0)   AS KzSbt2,
                KZNO2            AS KzNo2,
                KZMEGI2          AS KzMegi2,
                NVL(MAKCD, 0)    AS MakCd,
                NVL(JIGYO, 0)    AS Jigyo,
                NVL(CARDNO, 0)   AS CardNo,
                NVL(SHRHOHO, 0)  AS ShrHoho,
                NVL(SITE, 0)     AS Site,
                NVL(KSMM, 0)     AS KsMm,
                NVL(KSDD, 0)     AS KsDd,
                TOROKUNO         AS TorokuNo,
                NVL(KAZEIKB, 0)  AS KazeiKb
            FROM HAN01.KMKYAK
            WHERE KKYKCD = :KkykCd
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        KmKyak? row = conn.QueryFirstOrDefault<KmKyak>(sql, new { KkykCd = kkykCd });
        if (row is null)
        {
            return null;
        }

        NormalizeRow(row);
        return row;
    }

    public IReadOnlyList<string> GetCustomerCodesByCardNo(long cardNo)
    {
        const string sql = """
            SELECT KKYKCD
            FROM HAN01.KMKYAK
            WHERE CARDNO = :CardNo
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        return conn.Query<string>(sql, new { CardNo = cardNo })
            .Select(s => LegacyTextHelper.NormalizeString(s).Trim())
            .ToList();
    }

    public void Insert(KmKyak entity)
    {
        const string sql = """
            INSERT INTO HAN01.KMKYAK (
                TDATE, KDATE, DDATE,
                KKYKCD, KKYKKB, KYKNAMEKN, KYKNAMEKJ, KYKNAMETG, KYKNAMES,
                UBNNO, ADD1, ADD2, ADD3, TELNO, FAXNO,
                SKSCD, SHSCD, GPKCD, SZCD, BKCD, MBNCD, SKNMCD,
                SEIBT, BIRTHDAY, SINNO, HHKNNO, DRPDAY, TKKB,
                SSDD1, INGTKB1, INDD1, SSDD2, INGTKB2, INDD2, SSDD3, INGTKB3, INDD3,
                SZKKB, SSYOKB, THYOKB,
                SHSDD1, SHGTKB1, SHYDD1, SHSDD2, SHGTKB2, SHYDD2, SHSDD3, SHGTKB3, SHYDD3,
                SYHZKKB,
                GKCD1, KZSBT1, KZNO1, KZMEGI1,
                GKCD2, KZSBT2, KZNO2, KZMEGI2,
                MAKCD, JIGYO, CARDNO,
                SHRHOHO, SITE, KSMM, KSDD,
                TOROKUNO, KAZEIKB
            ) VALUES (
                :TDate, :KDate, NULL,
                :KKykCd, :KKykKb, :KykNameKn, :KykNameKj, :KykNameTg, :KykNameS,
                :UbnNo, :Add1, :Add2, :Add3, :TelNo, :FaxNo,
                :SksCd, :ShsCd, :GpkCd, :SzCd, :BkCd, :MbnCd, :SknmCd,
                :Seibt, :Birthday, :SinNo, :HhknNo, :DrpDay, :TkKb,
                :Ssdd1, :IngtKb1, :Indd1, :Ssdd2, :IngtKb2, :Indd2, :Ssdd3, :IngtKb3, :Indd3,
                :SzkKb, :SsyoKb, :ThyoKb,
                :Shsdd1, :ShgtKb1, :Shydd1, :Shsdd2, :ShgtKb2, :Shydd2, :Shsdd3, :ShgtKb3, :Shydd3,
                :SyhzkKb,
                :GkCd1, :KzSbt1, :KzNo1, :KzMegi1,
                :GkCd2, :KzSbt2, :KzNo2, :KzMegi2,
                :MakCd, :Jigyo, :CardNo,
                :ShrHoho, :Site, :KsMm, :KsDd,
                :TorokuNo, :KazeiKb
            )
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        using var tran = conn.BeginTransaction();
        try
        {
            conn.Execute(sql, BuildWriteParams(entity), tran);
            tran.Commit();
        }
        catch
        {
            tran.Rollback();
            throw;
        }
    }

    public void Update(KmKyak entity)
    {
        const string sql = """
            UPDATE HAN01.KMKYAK
               SET KDATE     = :KDate,
                   DDATE     = NULL,
                   KKYKKB    = :KKykKb,
                   KYKNAMEKN = :KykNameKn,
                   KYKNAMEKJ = :KykNameKj,
                   KYKNAMETG = :KykNameTg,
                   KYKNAMES  = :KykNameS,
                   UBNNO     = :UbnNo,
                   ADD1      = :Add1,
                   ADD2      = :Add2,
                   ADD3      = :Add3,
                   TELNO     = :TelNo,
                   FAXNO     = :FaxNo,
                   SKSCD     = :SksCd,
                   SHSCD     = :ShsCd,
                   GPKCD     = :GpkCd,
                   SZCD      = :SzCd,
                   BKCD      = :BkCd,
                   MBNCD     = :MbnCd,
                   SKNMCD    = :SknmCd,
                   SEIBT     = :Seibt,
                   BIRTHDAY  = :Birthday,
                   SINNO     = :SinNo,
                   HHKNNO    = :HhknNo,
                   DRPDAY    = :DrpDay,
                   TKKB      = :TkKb,
                   SSDD1     = :Ssdd1,
                   INGTKB1   = :IngtKb1,
                   INDD1     = :Indd1,
                   SSDD2     = :Ssdd2,
                   INGTKB2   = :IngtKb2,
                   INDD2     = :Indd2,
                   SSDD3     = :Ssdd3,
                   INGTKB3   = :IngtKb3,
                   INDD3     = :Indd3,
                   SZKKB     = :SzkKb,
                   SSYOKB    = :SsyoKb,
                   THYOKB    = :ThyoKb,
                   SHSDD1    = :Shsdd1,
                   SHGTKB1   = :ShgtKb1,
                   SHYDD1    = :Shydd1,
                   SHSDD2    = :Shsdd2,
                   SHGTKB2   = :ShgtKb2,
                   SHYDD2    = :Shydd2,
                   SHSDD3    = :Shsdd3,
                   SHGTKB3   = :ShgtKb3,
                   SHYDD3    = :Shydd3,
                   SYHZKKB   = :SyhzkKb,
                   GKCD1     = :GkCd1,
                   KZSBT1    = :KzSbt1,
                   KZNO1     = :KzNo1,
                   KZMEGI1   = :KzMegi1,
                   GKCD2     = :GkCd2,
                   KZSBT2    = :KzSbt2,
                   KZNO2     = :KzNo2,
                   KZMEGI2   = :KzMegi2,
                   MAKCD     = :MakCd,
                   JIGYO     = :Jigyo,
                   CARDNO    = :CardNo,
                   SHRHOHO   = :ShrHoho,
                   SITE      = :Site,
                   KSMM      = :KsMm,
                   KSDD      = :KsDd,
                   TOROKUNO  = :TorokuNo,
                   KAZEIKB   = :KazeiKb
             WHERE KKYKCD    = :KKykCd
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        using var tran = conn.BeginTransaction();
        try
        {
            conn.Execute(sql, BuildWriteParams(entity), tran);
            tran.Commit();
        }
        catch
        {
            tran.Rollback();
            throw;
        }
    }

    public void SoftDelete(string kkykCd, DateTime deletedDate)
    {
        const string sql = """
            UPDATE HAN01.KMKYAK
               SET DDATE  = :DDate
             WHERE KKYKCD = :KkykCd
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        using var tran = conn.BeginTransaction();
        try
        {
            conn.Execute(sql, new { DDate = deletedDate.Date, KkykCd = kkykCd }, tran);
            tran.Commit();
        }
        catch
        {
            tran.Rollback();
            throw;
        }
    }

    public IReadOnlyList<KmKyak> SearchActiveByKanaOrAlpha(int syubetu, string moji, string moji2, int kykbn, int gpcd)
    {
        string pattern1 = $"{moji.Trim()}%";
        string pattern2 = $"{moji2.Trim()}%";

        string sql = syubetu switch
        {
            2 => """
                SELECT DDATE AS DDate, KKYKCD AS KKykCd, KYKNAMEKJ AS KykNameKj
                FROM HAN01.KMKYAK
                WHERE KKYKKB = :Kykbn
                  AND GPKCD = :Gpcd
                  AND (KYKNAMEKN LIKE :Pattern1 OR KYKNAMEKN LIKE :Pattern2)
                ORDER BY KYKNAMEKJ
                """,
            4 => """
                SELECT DDATE AS DDate, KKYKCD AS KKykCd, KYKNAMEKJ AS KykNameKj
                FROM HAN01.KMKYAK
                WHERE KKYKKB = :Kykbn
                  AND GPKCD = :Gpcd
                  AND KYKNAMEKN LIKE :Pattern1
                  AND KYKNAMEKN NOT LIKE 'ﾆｲｶﾞﾀ%'
                ORDER BY KYKNAMEKJ
                """,
            _ => """
                SELECT DDATE AS DDate, KKYKCD AS KKykCd, KYKNAMEKJ AS KykNameKj
                FROM HAN01.KMKYAK
                WHERE KKYKKB = :Kykbn
                  AND GPKCD = :Gpcd
                  AND KYKNAMEKN LIKE :Pattern1
                ORDER BY KYKNAMEKJ
                """
        };

        using var conn = _connectionFactory.CreateOpenConnection();
        var rows = conn.Query<KmKyak>(sql, new { Kykbn = kykbn, Gpcd = gpcd, Pattern1 = pattern1, Pattern2 = pattern2 });
        return rows
            .Where(r => !r.DDate.HasValue)
            .Select(r => new KmKyak
            {
                DDate = r.DDate,
                KKykCd = LegacyTextHelper.NormalizeString(r.KKykCd).Trim(),
                KykNameKj = LegacyTextHelper.NormalizeString(r.KykNameKj)
            })
            .ToList();
    }

    public IReadOnlyList<KmKyak> SearchActiveByName(string keyword, int matchMode, bool fuzzySearch, int? kykbn, int? gpcd)
    {
        var sb = new StringBuilder();
        sb.AppendLine("SELECT DDATE AS DDate, KKYKCD AS KKykCd, KYKNAMEKJ AS KykNameKj");
        sb.AppendLine("FROM HAN01.KMKYAK");

        string prefix = matchMode is 0 or 2 ? "%" : string.Empty;
        string suffix = matchMode is 0 or 1 ? "%" : string.Empty;

        if (fuzzySearch)
        {
            sb.AppendLine("WHERE UTL_I18N.TRANSLITERATE(UPPER(TO_SINGLE_BYTE(REPLACE(REPLACE(KYKNAMEKJ,' ',''),'　',''))),'kana_fwkatakana')");
            sb.AppendLine("      LIKE :Prefix || UTL_I18N.TRANSLITERATE(UPPER(TO_SINGLE_BYTE(:Keyword)),'kana_fwkatakana') || :Suffix");
        }
        else
        {
            sb.AppendLine("WHERE REPLACE(REPLACE(KYKNAMEKJ,' ',''),'　','') LIKE :Prefix || :Keyword || :Suffix");
        }

        if (kykbn.HasValue)
        {
            sb.AppendLine("  AND KKYKKB = :Kykbn");
        }

        if (gpcd.HasValue)
        {
            sb.AppendLine("  AND GPKCD = :Gpcd");
        }

        sb.AppendLine("ORDER BY KYKNAMEKJ");

        using var conn = _connectionFactory.CreateOpenConnection();
        var rows = conn.Query<KmKyak>(
            sb.ToString(),
            new
            {
                Prefix = prefix,
                Suffix = suffix,
                Keyword = keyword ?? string.Empty,
                Kykbn = kykbn,
                Gpcd = gpcd
            });

        return rows
            .Where(r => !r.DDate.HasValue)
            .Select(r => new KmKyak
            {
                DDate = r.DDate,
                KKykCd = LegacyTextHelper.NormalizeString(r.KKykCd).Trim(),
                KykNameKj = LegacyTextHelper.NormalizeString(r.KykNameKj)
            })
            .ToList();
    }

    private static object BuildWriteParams(KmKyak e) => new
    {
        TDate = e.TDate?.Date ?? DateTime.Today,
        KDate = e.KDate?.Date ?? DateTime.Today,
        e.KKykCd,
        e.KKykKb,
        KykNameKn = ToDbString(e.KykNameKn),
        KykNameKj = ToDbString(e.KykNameKj),
        KykNameTg = ToDbString(e.KykNameTg),
        KykNameS = ToDbString(e.KykNameS),
        UbnNo = ToDbString(e.UbnNo),
        Add1 = ToDbString(e.Add1),
        Add2 = ToDbString(e.Add2),
        Add3 = ToDbString(e.Add3),
        TelNo = ToDbString(e.TelNo),
        FaxNo = ToDbString(e.FaxNo),
        SksCd = ToDbString(e.SksCd),
        ShsCd = ToDbString(e.ShsCd),
        e.GpkCd,
        e.SzCd,
        e.BkCd,
        e.MbnCd,
        e.SknmCd,
        e.Seibt,
        e.Birthday,
        e.SinNo,
        e.HhknNo,
        e.DrpDay,
        e.TkKb,
        e.Ssdd1,
        e.IngtKb1,
        e.Indd1,
        e.Ssdd2,
        e.IngtKb2,
        e.Indd2,
        e.Ssdd3,
        e.IngtKb3,
        e.Indd3,
        e.SzkKb,
        e.SsyoKb,
        e.ThyoKb,
        e.Shsdd1,
        e.ShgtKb1,
        e.Shydd1,
        e.Shsdd2,
        e.ShgtKb2,
        e.Shydd2,
        e.Shsdd3,
        e.ShgtKb3,
        e.Shydd3,
        e.SyhzkKb,
        e.GkCd1,
        e.KzSbt1,
        KzNo1 = ToDbString(e.KzNo1),
        KzMegi1 = ToDbString(e.KzMegi1),
        e.GkCd2,
        e.KzSbt2,
        KzNo2 = ToDbString(e.KzNo2),
        KzMegi2 = ToDbString(e.KzMegi2),
        e.MakCd,
        e.Jigyo,
        e.CardNo,
        e.ShrHoho,
        e.Site,
        e.KsMm,
        e.KsDd,
        TorokuNo = ToDbString(e.TorokuNo),
        e.KazeiKb
    };

    private static string? ToDbString(string? value) => string.IsNullOrEmpty(value) ? null : value;

    private static void NormalizeRow(KmKyak r)
    {
        r.KKykCd = LegacyTextHelper.NormalizeString(r.KKykCd).Trim();
        r.KykNameKn = LegacyTextHelper.NormalizeString(r.KykNameKn);
        r.KykNameKj = LegacyTextHelper.NormalizeString(r.KykNameKj);
        r.KykNameTg = LegacyTextHelper.NormalizeString(r.KykNameTg);
        r.KykNameS = LegacyTextHelper.NormalizeString(r.KykNameS);
        r.UbnNo = LegacyTextHelper.NormalizeString(r.UbnNo);
        r.Add1 = LegacyTextHelper.NormalizeString(r.Add1);
        r.Add2 = LegacyTextHelper.NormalizeString(r.Add2);
        r.Add3 = LegacyTextHelper.NormalizeString(r.Add3);
        r.TelNo = LegacyTextHelper.NormalizeString(r.TelNo);
        r.FaxNo = LegacyTextHelper.NormalizeString(r.FaxNo);
        r.SksCd = LegacyTextHelper.NormalizeString(r.SksCd).Trim();
        r.ShsCd = LegacyTextHelper.NormalizeString(r.ShsCd).Trim();
        r.KzNo1 = LegacyTextHelper.NormalizeString(r.KzNo1);
        r.KzMegi1 = LegacyTextHelper.NormalizeString(r.KzMegi1);
        r.KzNo2 = LegacyTextHelper.NormalizeString(r.KzNo2);
        r.KzMegi2 = LegacyTextHelper.NormalizeString(r.KzMegi2);
        r.TorokuNo = LegacyTextHelper.NormalizeString(r.TorokuNo);
    }
}
