using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class KtZjnlRepository : IKtZjnlRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public KtZjnlRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public KtZjnl? GetByDenNo(long kdenno)
    {
        const string sql = """
            SELECT
                TDATE            AS TDate,
                KDATE            AS KDate,
                KDENNO           AS KDenNo,
                NVL(CHODAY, 0)   AS ChoDay,
                NVL(STANTCD, 0)  AS STantCd,
                NVL(SHNCD, 0)    AS ShnCd,
                NVL(CHOKU, 0)    AS Choku,
                NVL(CHOSUU, 0)   AS ChoSuu,
                NVL(CHOKIN, 0)   AS ChoKin,
                TEKIYO           AS Tekiyo,
                NVL(ZKHNFLG, 0)  AS ZkhnFlg,
                NVL(GTJFLG, 0)   AS GtjFlg,
                NVL(SWKFLG, 0)   AS SwkFlg
            FROM HAN01.KTZJNL
            WHERE KDENNO = :KDenNo
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        return conn.QueryFirstOrDefault<KtZjnl>(sql, new { KDenNo = kdenno });
    }

    public long InsertWithSequence(KtZjnl entity)
    {
        const string seqSql = "SELECT HAN01.KSZCDN.NEXTVAL FROM DUAL";
        const string insertSql = """
            INSERT INTO HAN01.KTZJNL (
                TDATE,
                KDATE,
                KDENNO,
                CHODAY,
                STANTCD,
                SHNCD,
                CHOKU,
                CHOSUU,
                CHOKIN,
                TEKIYO,
                ZKHNFLG,
                GTJFLG,
                SWKFLG
            ) VALUES (
                :TDate,
                :KDate,
                :KDenNo,
                :ChoDay,
                :STantCd,
                :ShnCd,
                :Choku,
                :ChoSuu,
                :ChoKin,
                :Tekiyo,
                0,
                0,
                0
            )
            """;

        entity.KDenNo = 0;

        using var conn = _connectionFactory.CreateOpenConnection();
        using var tx = conn.BeginTransaction();
        try
        {
            long? nextVal = conn.ExecuteScalar<long?>(seqSql, transaction: tx);
            if (!nextVal.HasValue)
            {
                throw new InvalidOperationException("伝票採番エラー: HAN01.KSZCDN.NEXTVAL が取得できませんでした。");
            }

            long dfDenNo = 6000000L + nextVal.Value;
            entity.KDenNo = dfDenNo;

            conn.Execute(insertSql, new
            {
                TDate = (entity.TDate ?? DateTime.Today).Date,
                KDate = (entity.KDate ?? DateTime.Today).Date,
                KDenNo = dfDenNo,
                entity.ChoDay,
                entity.STantCd,
                entity.ShnCd,
                entity.Choku,
                entity.ChoSuu,
                entity.ChoKin,
                Tekiyo = string.IsNullOrEmpty(entity.Tekiyo) ? null : entity.Tekiyo
            }, tx);

            tx.Commit();
            return dfDenNo;
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    public void Update(KtZjnl entity)
    {
        const string sql = """
            UPDATE HAN01.KTZJNL
               SET KDATE   = :KDate,
                   CHODAY  = :ChoDay,
                   STANTCD = :STantCd,
                   SHNCD   = :ShnCd,
                   CHOKU   = :Choku,
                   CHOSUU  = :ChoSuu,
                   CHOKIN  = :ChoKin,
                   TEKIYO  = :Tekiyo,
                   ZKHNFLG = 0,
                   GTJFLG  = 0,
                   SWKFLG  = 0
             WHERE KDENNO  = :KDenNo
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        using var tx = conn.BeginTransaction();
        try
        {
            conn.Execute(sql, new
            {
                KDate = (entity.KDate ?? DateTime.Today).Date,
                entity.ChoDay,
                entity.STantCd,
                entity.ShnCd,
                entity.Choku,
                entity.ChoSuu,
                entity.ChoKin,
                Tekiyo = string.IsNullOrEmpty(entity.Tekiyo) ? null : entity.Tekiyo,
                entity.KDenNo
            }, tx);

            tx.Commit();
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    public void Delete(long kdenno)
    {
        const string sql = """
            DELETE FROM HAN01.KTZJNL
             WHERE KDENNO = :KDenNo
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        using var tx = conn.BeginTransaction();
        try
        {
            conn.Execute(sql, new { KDenNo = kdenno }, tx);
            tx.Commit();
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }
}

