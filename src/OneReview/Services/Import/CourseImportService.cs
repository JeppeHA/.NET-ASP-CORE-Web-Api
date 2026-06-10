using OneReview.Persistence.Repositories;
using OneReview.Services.Import;
public class CourseImportService
{
    private readonly CourseRepository _courseRepository;

      private readonly CourseImportParser _parser;

    public CourseImportService(CourseRepository courseRepository, CourseImportParser parser)
    {
        _courseRepository = courseRepository;
    }

    public async Task ImportAsync(string filePath)
    {
        var lines = await File.ReadAllLinesAsync(filePath);

        foreach (var line in lines)
        {
            var course = _parser.Parse(line); 
            await _courseRepository.CreateAsync(course);
        }
    }
}