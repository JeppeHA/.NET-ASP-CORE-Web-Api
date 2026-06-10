using OneReview.Persistence.Repositories;

namespace OneReview.Services.Import;
public class CourseImportService
{
    private readonly CourseRepository _courseRepository;

    private readonly CourseImportParser _parser;

    public CourseImportService(CourseRepository courseRepository, CourseImportParser parser)
    {
        _courseRepository = courseRepository;
        _parser = parser;
    }

    public async Task ImportAsync(string[] lines)
{
    if (lines.Length < 2) return;

    var course = await _parser.Parse(lines[1], _courseRepository);
    if (course is null) return;
    await _courseRepository.CreateAsync(course);
}
}