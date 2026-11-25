using System.Text;
using Application.Common;
using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class KtUjnlRepository : IKtUjnlRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public KtUjnlRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void TestConnection()
    {
        using var conn = _connectionFactory.CreateOpenConnection();
        conn.ExecuteScalar<int>("SELECT 1 FROM DUAL");
    }

    public IReadOnlyList<KtUjnlInquiryRow> SearchTransactions(
        string? hbsCd,
        int? stantCd,
        int? daiCd,
        int? tyuCd,
        long? shnCd,
        int fromYmd,
        int toYmd,
        int maxRows = 1000)
    {
        var whereBuilder = new StringBuilder();
        var parameters = new DynamicParameters();

        if (!string.IsNullOrWhiteSpace(hbsCd))
        {
            whereBuilder.Append("HBSCD = :HbsCd AND ");
            parameters.Add("HbsCd", hbsCd.Trim());
        }

        if (stantCd.HasValue)
        {
            whereBuilder.Append("STANTCD = :StantCd AND ");
            parameters.Add("StantCd", stantCd.Value);
        }

        if (daiCd.HasValue)
        {
            whereBuilder.Append("DAICD = :DaiCd AND ");
            parameters.Add("DaiCd", daiCd.Value);
        }

        if (tyuCd.HasValue)
        {
            whereBuilder.Append("TYUCD = :TyuCd AND ");
            parameters.Add("TyuCd", tyuCd.Value);
        }

        if (shnCd.HasValue)
        {
            whereBuilder.Append("SHNCD = :ShnCd AND ");
            parameters.Add("ShnCd", shnCd.Value);
        }

        whereBuilder.Append("HBIDAY BETWEEN :FromYmd AND :ToYmd");
        parameters.Add("FromYmd", fromYmd);
        parameters.Add("ToYmd", toYmd);
        parameters.Add("MaxRows", maxRows);

        string whereClause = whereBuilder.ToString();

        // VB6 KTBO0020.FRM allist_Set 1:1 SQL:
        // ORDER BY 1, 3, 8, 10 (HBSCD, HBIDAY, SHNNAMEN1, STANTCD)
        string sql = $"""
            SELECT * FROM (
                SELECT
                    HBSCD           AS Hbscd,
                    NVL(KDENNO, 0)  AS Kdenno,
                    NVL(HBIDAY, 0)  AS Hbiday,
                    NVL(URISU, 0)   AS Urisu,
                    NVL(URITAN, 0)  AS Uritan,
                    NVL(HANKIN, 0)  AS Hankin,
                    NVL(SHNCD, 0)   AS ShnCd,
                    SHNNAMEN1       AS ShnNameN1,
                    SHNNAMEN2       AS ShnNameN2,
                    NVL(STANTCD, 0) AS StantCd
                FROM HAN01.KTUJNL
                WHERE {whereClause}
                UNION ALL
                SELECT
                    HBSCD           AS Hbscd,
                    NVL(KDENNO, 0)  AS Kdenno,
                    NVL(HBIDAY, 0)  AS Hbiday,
                    NVL(URISU, 0)   AS Urisu,
                    NVL(URITAN, 0)  AS Uritan,
                    NVL(HANKIN, 0)  AS Hankin,
                    NVL(SHNCD, 0)   AS ShnCd,
                    SHNNAMEN1       AS ShnNameN1,
                    SHNNAMEN2       AS ShnNameN2,
                    NVL(STANTCD, 0) AS StantCd
                FROM HAN01.KTUJNL2
                WHERE {whereClause}
                ORDER BY 1, 3, 8, 10
            )
            WHERE ROWNUM <= :MaxRows
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        List<KtUjnlInquiryRow> rows = conn.Query<KtUjnlInquiryRow>(sql, parameters).ToList();
        foreach (KtUjnlInquiryRow row in rows)
        {
            row.Hbscd = LegacyTextHelper.NormalizeString(row.Hbscd);
            row.ShnNameN1 = LegacyTextHelper.NormalizeString(row.ShnNameN1);
            row.ShnNameN2 = LegacyTextHelper.NormalizeString(row.ShnNameN2);
        }

        return rows;
    }
}

