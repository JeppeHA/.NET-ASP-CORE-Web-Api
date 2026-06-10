using OneReview.Persistence.Repositories;

namespace OneReview.Services.Import;
public class CourseImportParser
{
    public async Task<Course?> Parse(string line, CourseRepository courseRepository)
    {

        if (string.IsNullOrWhiteSpace(line))
        return null;

        var columns = line.Split(',');

        Console.WriteLine("Line: " + line);

        if (columns.Length < 3)
            return null;

        var course = await courseRepository.GetByNameAsync(columns[1]);
        if (course != null)
        {   
            return course;
        }
        else
        {
           int numberOfHoles = columns.Length - 8;
           string difficulty = columns[2].ToString();

            return new Course
            {
                Name = columns[1],
                NumberOfHoles = numberOfHoles,
                Difficulty = difficulty
            };

           
        };
    }
        
}