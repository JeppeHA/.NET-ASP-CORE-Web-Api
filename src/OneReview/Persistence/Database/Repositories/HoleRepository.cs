namespace OneReview.Persistence.Repositories;
using System.Data;
using OneReview.Persistence.Database;

using Dapper;
using OneReview.Domain;
using Throw;
public class HoleRepository(IDbConnectionFactory dbConnectionFactory)
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
   public async Task CreateAsync(Hole hole)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync();

        string query = @"
        INSERT INTO Holes (holeNumber, courseId, par)
        VALUES (@HoleNumber, @CourseId, @Par)";

        var result = await connection.ExecuteAsync(query, hole);

        result.Throw().IfNegativeOrZero();
    }
 /*
    public async Task DeleteAsync(Guid playerId)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync();

        string query = @"DELETE FROM players WHERE Id = @Id";

         int rowsAffected = await connection.ExecuteAsync(
        query,
        new { Id = playerId }
        );

    }
    */

    public async Task<Hole?> GetByIdAsync(Guid courseId, int holeNumber)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync();

        string query = @"
        SELECT holeNumber, courseId, par
        FROM Holes
        WHERE courseId = @courseId AND holeNumber = @holeNumber";

        return await connection.QueryFirstOrDefaultAsync<Hole>(query, new { CourseId = courseId, HoleNumber = holeNumber });
    }
}