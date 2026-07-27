using Microsoft.VisualBasic;

using OneReview.Domain;
using OneReview.Persistence.Repositories;

namespace OneReview.Services.Import;

public class RoundImportParser
{
    public async Task<Round?> Parse(string line, 
    PlayerRepository playerRepository,
    CourseRepository courseRepository,
    DateTime date)
{   
    string[] str = line.Split(',');

    var player = await playerRepository.GetByNameAsync(str[0]);
    var course = await courseRepository.GetByNameAsync(str[1]);

    if (player is null)
    {
        Console.WriteLine($"Player not found: '{str[0]}'");
        return null;
    }

    if (course is null)
    {
        Console.WriteLine($"Course not found: '{str[1]}'");
        return null;
    }

    return new Round
    {
        PlayerId = player.Id,
        CourseId = course.Id,
        RoundDate = date,
    };
}

    
    }
