using OneReview.Domain;
using Microsoft.AspNetCore.Mvc;
namespace OneReview.Controllers;

[ApiController]
[Route("[controller]")]
public class HolesController(HoleService holeService) : ControllerBase
{
    private readonly HoleService _holeService = holeService;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateHoleRequest request)
    {
        var hole = request.ToDomain();

        await _holeService.CreateAsync(hole);

        return CreatedAtAction(
            actionName: nameof(Get),
            routeValues: new { courseId = hole.CourseId, holeNumber = hole.HoleNumber },
            value: HoleResponse.FromDomain(hole)
        );
    }

    [HttpGet("{courseId:guid}/{holeNumber:int}")]
    public async Task<IActionResult> Get(Guid courseId, int holeNumber)
    {
    var hole = await _holeService.GetAsync(courseId, holeNumber);

    return hole is null
        ? Problem(statusCode: StatusCodes.Status404NotFound, detail: "Hole not found")
        : Ok(HoleResponse.FromDomain(hole));
    }


    public record CreateHoleRequest(
        int holeNumber,
        Guid courseId,
        int par
    )
    {
        public Hole ToDomain() => new Hole
        {
            HoleNumber = holeNumber,
            CourseId = courseId,
            Par = par
        };
    }

    public record HoleResponse(
        int HoleNumber,
        Guid CourseId,
        int Par
    )
    {
        public static HoleResponse FromDomain(Hole hole) => new HoleResponse(
            hole.HoleNumber,
            hole.CourseId,
            hole.Par
        );
    }
}


