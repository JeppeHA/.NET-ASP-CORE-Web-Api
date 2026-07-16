using OneReview.Domain;
using OneReview.Persistence.Repositories;




public class ScoreService(ScoreRepository scoreRepository)
{
    private readonly ScoreRepository _scoreRepository = scoreRepository;
    public async Task CreateAsync(Score score)
    {
        // store players in the database
        await _scoreRepository.CreateAsync(score);
    }

    public async Task<Score?> GetAsync(Guid roundId, int holeNumber, Guid courseId)
    {
       return await _scoreRepository.GetByIdAsync(roundId, holeNumber, courseId);
    }

    
   /* public async Task DeleteAsync(Guid courseId)
    {
        await _playerRepository.DeleteAsync(playerId);
    }
    */
}   