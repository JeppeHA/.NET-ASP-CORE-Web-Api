using Microsoft.VisualBasic;

using OneReview.Domain;
using OneReview.Persistence.Repositories;

namespace OneReview.Services.Import;

public class PlayerImportParser
{
    public async Task<Player?> Parse(string line, 
    PlayerRepository playerRepository,
    int age,
    string gender
    )
    {
        if(string.IsNullOrWhiteSpace(line))
            return null;

         var columns = line.Split(',');
        
        if (columns.Length < 3)
            return null;
        
        var player = await playerRepository.GetByNameAsync(columns[0]);
        if(player != null)
        {
            return player;
        }
        else
        {
            return new Player
            {
                Name = columns[0],
                Age = age,
                Gender = gender
            };
        }


    }
}