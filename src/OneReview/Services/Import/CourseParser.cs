namespace OneReview.Services.Import;

public class CourseImportParser
{
    public Course Parse(string line)
    {
        var columns = line.Split(',');
        return new Course
        {
            Name = columns[0],
            // map the rest...
        };
    }
}