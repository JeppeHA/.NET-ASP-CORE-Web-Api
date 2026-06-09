namespace OneReview.Persistence.Repositories;
using System.Data;
using OneReview.Persistence.Database;

using Dapper;
using OneReview.Domain;
using Throw;
public class CourseRepository(IDbConnectionFactory dbConnectionFactory)
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
   public async Task CreateAsync(Course course)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync();

        string query = @"
        INSERT INTO courses (id, name, numberOfHoles, difficulty)
        VALUES (@Id, @Name, @NumberOfHoles, @Difficulty)";

        var result = await connection.ExecuteAsync(query, course);

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

    public async Task<Course?> GetByIdAsync(Guid courseId)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync();

        string query = @"
        SELECT id, name, numberOfHoles, difficulty
        FROM courses
        WHERE id = @Id";

        return await connection.QueryFirstOrDefaultAsync<Course>(query, new { Id = courseId });
    }
}