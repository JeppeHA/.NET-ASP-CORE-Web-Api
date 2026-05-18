using OneReview.Domain;


public class PlayerService
{
    // Temp database
    private static readonly List<Player> _playersRepositoiry = new List<Player>();

    public void Create(Player player)
    {
        if(!_playersRepositoiry.Any(p => p.Id == player.Id))
        {
            AddPlayer(player);
        }
    }

    public void AddPlayer(Player player)
    {
        _playersRepositoiry.Add(player);
    }

    public Player? Get(Guid playerId)
    {
        return _playersRepositoiry.Find(x => x.Id == playerId);
    }
}   