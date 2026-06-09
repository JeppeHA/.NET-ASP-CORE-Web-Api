namespace OneReview.Persistence.Repositories;
using System.Data;
using OneReview.Persistence.Database;

using Dapper;
using OneReview.Domain;
using Throw;
public class PlayerRepository(IDbConnectionFactory dbConnectionFactory)
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
    public async Task CreateAsync(Player player)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync();

        string query = @"
        INSERT INTO players (id, name, age, gender)
        VALUES (@Id, @Name, @Age, @Gender)";

        var result = await connection.ExecuteAsync(query, player);

        result.Throw().IfNegativeOrZero();
    }

    public async Task DeleteAsync(Guid playerId)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync();

        string query = @"DELETE FROM players WHERE Id = @Id";

         int rowsAffected = await connection.ExecuteAsync(
        query,
        new { Id = playerId }
        );

    }

    public async Task<Player?> GetByIdAsync(Guid playerId)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync();

        string query = @"
        SELECT id, name, age, gender
        FROM players
        WHERE id = @Id";

        return await connection.QueryFirstOrDefaultAsync<Player>(query, new { Id = playerId });
    }
}