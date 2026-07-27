using OneReview.Domain;
using Microsoft.AspNetCore.Mvc;
using OneReview.Services.Import;
namespace OneReview.Controllers;


[ApiController]
[Route("[controller]")]
public class ImportController(
    CourseImportService courseImportService,
    PlayerImportService playerImportService,
    HoleImportService holeImportService,
    RoundImportService roundImportService
    ) : ControllerBase
{

private readonly CourseImportService _courseImportService = courseImportService;
private readonly PlayerImportService _playerImportService = playerImportService;

private readonly HoleImportService _holeImportService = holeImportService;

private readonly RoundImportService _roundImportService = roundImportService;

[HttpPost]
public async Task<IActionResult> Import(IFormFile file, [FromForm] int age, [FromForm] string gender)
{
    using var reader = new StreamReader(file.OpenReadStream());
    var content = await reader.ReadToEndAsync();
    var lines = content.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

    await _courseImportService.ImportAsync(lines);
    await _holeImportService.ImportAsync(lines);
    await _playerImportService.ImportAsync(lines, age, gender);
    await _roundImportService.ImportAsync(lines);
    
    return Ok();
}
}