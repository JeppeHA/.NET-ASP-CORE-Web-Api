using System.Data;
using Npgsql;
using OneReview.Persistence.Database;


public class NpgsqlConnectionFactory(string connectionString) : IDbConnectionFactory
{
    private readonly string _connectionString = connectionString;

    public async Task<IDbConnection> CreateConnectionAsync()
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        Console.WriteLine("DB CONNECTED SUCCESSFULLY");
        return connection;
    }
}