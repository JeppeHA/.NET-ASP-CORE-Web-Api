using OneReview.Domain;
using Microsoft.AspNetCore.Mvc;

using System.Reflection.Metadata.Ecma335;

namespace OneReview.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayersController(PlayerService playerService) : ControllerBase
{
    private readonly PlayerService _playerService = playerService;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePlayerRequest request)
    {
        var player = request.ToDomain();

        await _playerService.CreateAsync(player);

        return CreatedAtAction(
            actionName: nameof(Get),
            routeValues: new { Id = player.Id },
            value: PlayerResponse.FromDomain(player)
        );
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
    await _playerService.DeleteAsync(id);
    return Ok();
    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var player = await _playerService.GetAsync(id);

        return player is null
            ? Problem(statusCode: StatusCodes.Status404NotFound, detail: "Player not found")
            : Ok(PlayerResponse.FromDomain(player));
    }

    public record CreatePlayerRequest(
        string Name,
        int Age,
        string Gender
    )
    {
        public Player ToDomain() => new Player
        {
            Name = Name,
            Age = Age,
            Gender = Gender
        };
    }

    public record PlayerResponse(
        Guid Id,
        string Name,
        int Age,
        string Gender
    )
    {
        public static PlayerResponse FromDomain(Player player) => new PlayerResponse(
            player.Id,
            player.Name,
            player.Age,
            player.Gender
        );
    }
}


