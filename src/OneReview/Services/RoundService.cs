using OneReview.Domain;
using OneReview.Persistence.Repositories;



public class RoundService(RoundRepository roundRepository)
{
    private readonly RoundRepository _roundRepository = roundRepository;
    public async Task CreateAsync(Round round)
    {
        // store players in the database
        await _roundRepository.CreateAsync(round);
    }

    public async Task<Round?> GetAsync(Guid roundId)
    {
       return await _roundRepository.GetByIdAsync(roundId);
    }

    
   /* public async Task DeleteAsync(Guid courseId)
    {
        await _playerRepository.DeleteAsync(playerId);
    }
    */
}   