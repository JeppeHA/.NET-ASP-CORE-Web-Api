using OneReview.Domain;
using Microsoft.AspNetCore.Mvc;

using System.Reflection.Metadata.Ecma335;

namespace OneReview.Controllers;

[ApiController]
[Route("[controller]")]
public class CoursesController(CourseService courseService) : ControllerBase
{
    private readonly CourseService _courseService = courseService;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCourseRequest request)
    {
        var course = request.ToDomain();

        await _courseService.CreateAsync(course);

        return CreatedAtAction(
            actionName: nameof(Get),
            routeValues: new { Id = course.Id },
            value: CourseResponse.FromDomain(course)
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var course = await _courseService.GetAsync(id);

        return course is null
            ? Problem(statusCode: StatusCodes.Status404NotFound, detail: "Course not found")
            : Ok(CourseResponse.FromDomain(course));
    }


    public record CreateCourseRequest(
        string name,
        int numberOfHoles,
        string difficulty
    )
    {
        public Course ToDomain() => new Course
        {
            Name = name,
            NumberOfHoles = numberOfHoles,
            Difficulty = difficulty,
        };
    }

    public record CourseResponse(
        Guid Id,
        string Name,
        int NumberOfHoles,
        string Difficulty
    )
    {
        public static CourseResponse FromDomain(Course course) => new CourseResponse(
            course.Id,
            course.Name,
            course.NumberOfHoles,
            course.Difficulty
        );
    }
}


