using OneReview.Persistence.Database;
using OneReview.Services;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddScoped<ProductService>();
    builder.Services.AddControllers();
}
var app = builder.Build();
{
    app.MapControllers();

   DbInitializer.Initialize(app.Configuration["Database:ConnectionStrings:DefaultConnection"]!);

   Console.Out.Flush();
}
app.Run();

   