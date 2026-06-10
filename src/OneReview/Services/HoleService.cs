using OneReview.Domain;
using OneReview.Persistence.Repositories;




public class HoleService(HoleRepository holeRepository)
{
    private readonly HoleRepository _holeRepository = holeRepository;
    public async Task CreateAsync(Hole hole)
    {
        // store players in the database
        await _holeRepository.CreateAsync(hole);
    }

    public async Task<Hole?> GetAsync(Guid courseId, int holeNumber)
    {
       return await _holeRepository.GetByIdAsync(courseId, holeNumber);
    }

    
   /* public async Task DeleteAsync(Guid courseId)
    {
        await _playerRepository.DeleteAsync(playerId);
    }
    */
}   