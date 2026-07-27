using OneReview.Persistence.Repositories;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
namespace OneReview.Services.Import;

public class RoundImportService
{
    private readonly RoundRepository _roundRepository;
    private readonly PlayerRepository _playerRepository;
    private readonly CourseRepository _courseRepository;
    private readonly RoundImportParser _parser;

    public RoundImportService(
        RoundRepository roundRepository,
        PlayerRepository playerRepository,
        CourseRepository courseRepository,
        RoundImportParser parser)
    {
        _roundRepository = roundRepository;
        _playerRepository = playerRepository;
        _courseRepository = courseRepository;
        _parser = parser;
    }

    public async Task ImportAsync(string[] lines)
    {
        if (lines.Length < 2) return;
        string[] values = lines[1].Split(',');

        DateTime date = ParseRoundDate(values[3]);
        Console.WriteLine("DATE " + date);

        for (int i = 2; i < lines.Length; i++)
        {
            var round = await _parser.Parse(lines[i], _playerRepository, _courseRepository, date);
            if (round is null) continue;

            Console.WriteLine("New round object: " + round.ToString());
            await _roundRepository.CreateAsync(round);
        }
    }

    DateTime ParseRoundDate(string raw)
{
    string normalized = Regex.Replace(raw, @"([+-]\d{2})(\d{2})$", "$1:$2");

    DateTimeOffset dto = DateTimeOffset.ParseExact(
        normalized,
        "yyyy-MM-dd HHmmzzz",
        CultureInfo.InvariantCulture
    );

    return dto.UtcDateTime; // DateTime, Kind = Utc
}
}
