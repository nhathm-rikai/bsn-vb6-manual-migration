using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace Infrastructure.Data;

public class OracleConnectionFactory : IDbConnectionFactory
{
    public const string DefaultConnectionString =
        "Data Source=localhost:1521/XE;User Id=HAN01;Password=H999;";

    private readonly string _connectionString;

    public OracleConnectionFactory(string? connectionString = null)
    {
        _connectionString = !string.IsNullOrWhiteSpace(connectionString)
            ? connectionString
            : Environment.GetEnvironmentVariable("KOUSYO_ORACLE_CONN") ?? DefaultConnectionString;
    }

    public IDbConnection CreateOpenConnection()
    {
        var conn = new OracleConnection(_connectionString);
        conn.Open();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = "ALTER SESSION SET NLS_DATE_FORMAT = 'RR/MM/DD'";
        cmd.ExecuteNonQuery();

        return conn;
    }
}

