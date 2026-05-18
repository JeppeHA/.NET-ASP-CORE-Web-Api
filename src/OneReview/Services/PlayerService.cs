using OneReview.Domain;
using OneReview.Persistence.Repositories;


public class PlayerService(PlayerRepository playerRepository)
{
    private readonly PlayerRepository _playerRepository = playerRepository;
    public async Task CreateAsync(Player player)
    {
        // store players in the database
        await _playerRepository.CreateAsync(player);
    }

    public async Task<Player?> GetAsync(Guid playerId)
    {
       return await _playerRepository.GetByIdAsync(playerId);
    }
    
}   