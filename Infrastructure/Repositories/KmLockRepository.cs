using Dapper;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class KmLockRepository : IKmLockRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public KmLockRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void TestConnection()
    {
        using var conn = _connectionFactory.CreateOpenConnection();
        conn.ExecuteScalar<int>("SELECT 1 FROM DUAL");
    }

    public long? GetKeiYm(int krecKbn)
    {
        const string sql = """
            SELECT KEIYM
            FROM HAN01.KMLOCK
            WHERE KRECKBN = :KRecKbn
            """;

        using var conn = _connectionFactory.CreateOpenConnection();
        return conn.QueryFirstOrDefault<long?>(sql, new { KRecKbn = krecKbn });
    }
}

