
using OneReview.Domain;
using Microsoft.AspNetCore.Mvc;
namespace OneReview.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayerController(PlayerService playerService) : ControllerBase
{
    private readonly PlayerService _playerService = playerService;

    [HttpPost]
    public IActionResult Create(CreatePlayerRequest request)
    {
        // mapping to internal representation
        var player = request.ToDomian();

        // invoking the use case
        _playerService.Create(player);

        // mapping to external representation

        return CreatedAtAction(
            actionName: nameof(Get),
            routeValues: new {Id = player.Id},
            value: PlayerResponse.FromDomain(player)
        );

    }

    [HttpGet("{Id}")]
    public IActionResult Get(Guid Id)
    {
        var player = _playerService.Get(Id);
        return player is null 
        ? Problem(statusCode: StatusCodes.Status404NotFound, detail: $"player not found") :
        Ok(PlayerResponse.FromDomain(player));
    }
    



    public record CreatePlayerRequest(
    string Name,
    int Age,
    string Gender
    )
    {
       public Player ToDomian()
        {
            return new Player()
            {
                Name = Name,
                Age = Age,
                Gender = Gender
            };
        }
    }

    public record PlayerResponse(
        Guid Id,
        string Name,
        int Age,
        string Gender
    )
    {
        public static PlayerResponse FromDomain(Player player)
        {
            return new PlayerResponse(
                player.Id,
                player.Name,
                player.Age,
                player.Gender
            );
        }
    }
    

}




