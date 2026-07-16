using OneReview.Domain;
using Microsoft.AspNetCore.Mvc;
namespace OneReview.Controllers;

[ApiController]
[Route("[controller]")]
public class ScoresController(ScoreService scoreService) : ControllerBase
{
    private readonly ScoreService _scoreService = scoreService;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateScoreRequest request)
    {
        Console.WriteLine("CREATE SCORE");
        var score = request.ToDomain();

        await _scoreService.CreateAsync(score);

        return CreatedAtAction(
            actionName: nameof(Get),
            routeValues: new { roundId = score.RoundId, holeNumber = score.HoleNumber, courseId = score.CourseId },
            value: ScoreResponse.FromDomain(score)
        );
    }

    [HttpGet("{roundId:guid}/{holeNumber:int}/{courseId:guid}")]
    public async Task<IActionResult> Get(Guid roundId, int holeNumber, Guid courseId)
    {
    var score = await _scoreService.GetAsync(roundId, holeNumber, courseId);

    return score is null
        ? Problem(statusCode: StatusCodes.Status404NotFound, detail: "Score not found")
        : Ok(ScoreResponse.FromDomain(score));
    }


    public record CreateScoreRequest(
        Guid roundId,
        int holeNumber,
        Guid courseId,
        int strokes
    )
    {
        public Score ToDomain() => new Score
        {
            RoundId = roundId,
            HoleNumber = holeNumber,
            CourseId = courseId,
            Strokes= strokes
        };
    }

    public record ScoreResponse(
        Guid RoundId,
        int HoleNumber,
        Guid CourseId,
        int Strokes
    )
    {
        public static ScoreResponse FromDomain(Score score) => new ScoreResponse(
            score.RoundId,
            score.HoleNumber,
            score.CourseId,
            score.Strokes
        );
    }
}


