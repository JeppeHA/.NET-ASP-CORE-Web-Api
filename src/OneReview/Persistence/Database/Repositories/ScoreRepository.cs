namespace OneReview.Persistence.Repositories;
using System.Data;
using OneReview.Persistence.Database;

using Dapper;
using OneReview.Domain;
using Throw;
public class ScoreRepository(IDbConnectionFactory dbConnectionFactory)
{
   private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
   public async Task CreateAsync(Score score)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync();

        string query = @"
        INSERT INTO scores (roundId, holeNumber, courseId, strokes)
        VALUES (@RoundId, @HoleNumber, @CourseId, @Strokes)";

        var result = await connection.ExecuteAsync(query, score);

        result.Throw().IfNegativeOrZero();
    }
 

    public async Task<Score?> GetByIdAsync(Guid roundId,int holeNumber, Guid courseId)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync();

        string query = @"
        SELECT roundId, holeNumber, courseId, strokes
        FROM scores
        WHERE roundId = @RoundId AND holeNumber = @HoleNumber AND courseId = @CourseId";

        return await connection.QueryFirstOrDefaultAsync<Score>(query, new { RoundId = roundId, HoleNumber = holeNumber, CourseId = courseId});
    }

}