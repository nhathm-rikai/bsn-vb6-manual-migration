using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class VtUjnlRepository : IVtUjnlRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public VtUjnlRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void TestConnection()
    {
        using var conn = _connectionFactory.CreateOpenConnection();
        conn.ExecuteScalar<int>("SELECT 1 FROM DUAL");
    }

    public IReadOnlyList<VtUjnl> GetDetails(long fromYmd, long toYmd, int wHani, int fromDaiCd, int toDaiCd, int tantCd)
    {
        string whereFilter = wHani switch
        {
            2 => "WHERE HBIDAY >= :FromYmd AND HBIDAY <= :ToYmd AND DAICD >= :FromDaiCd AND DAICD <= :ToDaiCd AND KEIKU <> 0",
            3 => "WHERE HBIDAY >= :FromYmd AND HBIDAY <= :ToYmd AND STANTCD = :TantCd AND KEIKU <> 0",
            _ => "WHERE HBIDAY >= :FromYmd AND HBIDAY <= :ToYmd AND KEIKU <> 0"
        };

        string sql = $"""
            SELECT
                TDATE     AS TDate,
                KDATE     AS KDate,
                DDATE     AS DDate,
                NVL(KDENNO, 0)  AS KDenNo,
                NVL(KGYONO, 0)  AS KGyoNo,
                HBSCD           AS HbsCd,
                NVL(GPKCD, 0)   AS GpkCd,
                NVL(SZCD, 0)    AS SzCd,
                NVL(SINNO, 0)   AS SinNo,
                NVL(HBIDAY, 0)  AS HbiDay,
                NVL(TBMCD, 0)   AS TBmCd,
                NVL(STANTCD, 0) AS STantCd,
                NVL(NTANTCD, 0) AS NTantCd,
                NVL(TORICD, 0)  AS ToriCd,
                NVL(KEIKU, 0)   AS KeiKu,
                NVL(KEIDD, 0)   AS KeiDd,
                NVL(SYBNCD, 0)  AS SybnCd,
                NVL(DAICD, 0)   AS DaiCd,
                NVL(TYUCD, 0)   AS TyuCd,
                NVL(SHNCD, 0)   AS ShnCd,
                SHNNAMEN1       AS ShnNamen1,
                SHNNAMEN2       AS ShnNamen2,
                NVL(URISU, 0)   AS UriSu,
                NVL(TNICD, 0)   AS TniCd,
                NVL(URITAN, 0)  AS UriTan,
                NVL(HANKIN, 0)  AS HanKin,
                NVL(ZEIKIN, 0)  AS ZeiKin,
                NVL(GOKKIN, 0)  AS GokKin,
                NVL(ZEIKU, 0)   AS ZeiKu,
                TEKIYO          AS Tekiyo,
                NVL(NKZFLG, 0)  AS NkzFlg,
                SKSCD           AS SksCd,
                NVL(NKHOHO, 0)  AS NkHoho,
                NVL(DNPKU, 0)   AS DnpKu,
                NVL(DNPFLG, 0)  AS DnpFlg,
                NVL(GTJFLG, 0)  AS GtjFlg,
                NVL(SWKFLG, 0)  AS SwkFlg,
                NVL(CKSFLG, 0)  AS CksFlg,
                NVL(DELFLG, 0)  AS DelFlg,
                NVL(BKFLG, 0)   AS BkFlg
            FROM HAN01.VTUJNL
            {whereFilter}
            ORDER BY DAICD, BKFLG, KEIKU, KEIDD, SKSCD, HBSCD, HBIDAY, KDENNO, KGYONO
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        return conn.Query<VtUjnl>(sql, new
        {
            FromYmd = fromYmd,
            ToYmd = toYmd,
            FromDaiCd = fromDaiCd,
            ToDaiCd = toDaiCd,
            TantCd = tantCd
        }).ToList();
    }

    public IReadOnlyList<VtUjnlSummary> GetSummaries(long fromYmd, long toYmd, int wHani, int fromDaiCd, int toDaiCd, int tantCd)
    {
        string whereFilter = wHani switch
        {
            2 => "WHERE HBIDAY >= :FromYmd AND HBIDAY <= :ToYmd AND DAICD >= :FromDaiCd AND DAICD <= :ToDaiCd AND KEIKU <> 0",
            3 => "WHERE HBIDAY >= :FromYmd AND HBIDAY <= :ToYmd AND STANTCD = :TantCd AND KEIKU <> 0",
            _ => "WHERE HBIDAY >= :FromYmd AND HBIDAY <= :ToYmd AND KEIKU <> 0"
        };

        string sql = $"""
            SELECT
                NVL(SUM(HANKIN), 0) AS SumHanKin,
                NVL(SUM(ZEIKIN), 0) AS SumZeiKin,
                NVL(SUM(GOKKIN), 0) AS SumGokKin,
                NVL(DAICD, 0)       AS DaiCd,
                NVL(KEIKU, 0)       AS KeiKu,
                NVL(KEIDD, 0)       AS KeiDd,
                SKSCD               AS SksCd,
                HBSCD               AS HbsCd,
                NVL(BKFLG, 0)       AS BkFlg
            FROM HAN01.VTUJNL
            {whereFilter}
            GROUP BY DAICD, BKFLG, KEIKU, KEIDD, SKSCD, HBSCD
            ORDER BY DAICD, BKFLG, KEIKU, KEIDD, SKSCD, HBSCD
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        return conn.Query<VtUjnlSummary>(sql, new
        {
            FromYmd = fromYmd,
            ToYmd = toYmd,
            FromDaiCd = fromDaiCd,
            ToDaiCd = toDaiCd,
            TantCd = tantCd
        }).ToList();
    }
}

