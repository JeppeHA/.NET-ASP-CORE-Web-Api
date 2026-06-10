using OneReview.Domain;
using Microsoft.AspNetCore.Mvc;
using OneReview.Services.Import;
namespace OneReview.Controllers;


[ApiController]
[Route("[controller]")]
public class ImportController(CourseImportService courseImportService) : ControllerBase
{

private readonly CourseImportService _courseImportService = courseImportService;

[HttpPost]
public async Task<IActionResult> Import(IFormFile file)
{
    using var reader = new StreamReader(file.OpenReadStream());
    var content = await reader.ReadToEndAsync();
    var lines = content.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
    Console.WriteLine("line length: " + lines.Length);
    Console.WriteLine("raw content: " + content);
    Console.WriteLine("content length: " + content.Length);

    await _courseImportService.ImportAsync(lines);
    return Ok();
}
}