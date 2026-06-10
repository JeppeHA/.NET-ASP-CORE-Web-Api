using Microsoft.VisualBasic;

using OneReview.Domain;
using OneReview.Persistence.Repositories;

namespace OneReview.Services.Import;

public class HoleImportParser
{
    public async Task<Hole?> Parse(string[] line, 
    HoleRepository holeRepository,
    CourseRepository courseRepository,
    int holeNumber)
    {        
        
        var course = await courseRepository.GetByNameAsync(line[1]);


        var hole = await holeRepository.GetByIdAsync(
            course.Id,
            holeNumber
        );
        if(hole != null)
        {
            return hole;
        }
        else
        {
            var raw = line[holeNumber + 7];
            if (!int.TryParse(raw, out int par))
                return null;

            return new Hole
            {
                CourseId = course.Id,
                HoleNumber = holeNumber,
                Par = par
            };
        }

    }
}