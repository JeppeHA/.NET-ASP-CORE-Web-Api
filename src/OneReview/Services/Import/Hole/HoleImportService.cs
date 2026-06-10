using OneReview.Persistence.Repositories;

namespace OneReview.Services.Import;

public class HoleImportService
{
    private readonly HoleRepository _holeRepository;

    private readonly CourseRepository _courseRepository;
    private readonly HoleImportParser _parser;

    public HoleImportService(HoleRepository holeRepository,
    CourseRepository courseRepository,
    HoleImportParser parser)
    {
        _holeRepository = holeRepository;
        _parser = parser;
        _courseRepository = courseRepository;
    }

    public async Task ImportAsync(string[] lines)
    {
        
        if (lines.Length < 2) return;
        var columns = lines[1].Split(',');

        int numberOfHoles = columns.Length - 8;
        for(int i = 1; i <= numberOfHoles; i++)
        {
            var hole = await _parser.Parse(columns, _holeRepository, _courseRepository, i);
            if (hole is null) return;
            await _holeRepository.CreateAsync(hole);
        }
        

        
    }
}