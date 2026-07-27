using OneReview.Domain;
using Microsoft.AspNetCore.Mvc;
namespace OneReview.Controllers;

[ApiController]
[Route("[controller]")]
public class RoundsController(RoundService roundService) : ControllerBase
{
    private readonly RoundService _roundService = roundService;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoundRequest request)
    {
        Console.WriteLine("Creating round!");
        var round = request.ToDomain();

        await _roundService.CreateAsync(round);

        return CreatedAtAction(
        actionName: nameof(Get),
        routeValues: new { id = round.Id },
        value: RoundResponse.FromDomain(round)
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var round = await _roundService.GetAsync(id);

        return round is null
            ? Problem(statusCode: StatusCodes.Status404NotFound, detail: "Hole not found")
            : Ok(RoundResponse.FromDomain(round));
    }


    public record CreateRoundRequest(
        Guid playerId,
        Guid courseId,
        DateTime roundDate
    )
    {
        public Round ToDomain() => new Round
        {
            PlayerId = playerId,
            CourseId = courseId,
            RoundDate = roundDate == default ? DateTime.UtcNow : roundDate
        };
    }

    public record RoundResponse(
        Guid Id,
        Guid PlayerId,
        Guid CourseId,
        DateTime RoundDate
    )
    {
        public static RoundResponse FromDomain(Round round) => new RoundResponse(
            round.Id,
            round.PlayerId,
            round.CourseId,
            round.RoundDate
        );
    }
}


