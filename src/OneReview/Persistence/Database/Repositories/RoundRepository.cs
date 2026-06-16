namespace OneReview.Persistence.Repositories;
using System.Data;
using OneReview.Persistence.Database;

using Dapper;
using OneReview.Domain;
using Throw;
public class RoundRepository(IDbConnectionFactory dbConnectionFactory)
{
   private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
   public async Task CreateAsync(Round round)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync();

        string query = @"
        INSERT INTO rounds (id, playerId, courseId, roundDate)
        VALUES (@Id, @PlayerId, @CourseId, @RoundDate)";

        var result = await connection.ExecuteAsync(query, round);

        result.Throw().IfNegativeOrZero();
    }
 

    public async Task<Round?> GetByIdAsync(Guid roundId)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync();

        string query = @"
        SELECT id ,playerid, courseId, roundDate
        FROM rounds
        WHERE id = @Id";

        return await connection.QueryFirstOrDefaultAsync<Round>(query, new { Id = roundId });
    }

}