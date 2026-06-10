using OneReview.Persistence.Repositories;

namespace OneReview.Services.Import;

public class PlayerImportService
{
    private readonly PlayerRepository _playerRepository;
    private readonly PlayerImportParser _parser;

    public PlayerImportService(PlayerRepository playerRepository, PlayerImportParser parser)
    {
        _playerRepository = playerRepository;
        _parser = parser;
    }

    public async Task ImportAsync(string[] lines, int age, string gender)
    {
        
        if (lines.Length < 2) return;
        for(int i = 2; i < lines.Length; i++)
        {
            var course = await _parser.Parse(lines[i], _playerRepository, age, gender);
            if (course is null) return;
            await _playerRepository.CreateAsync(course);
        }
        
    }
}