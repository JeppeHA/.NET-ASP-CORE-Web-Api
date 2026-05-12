using OneReview.Services;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddScoped<ProductService>();
    builder.Services.AddControllers();
}
var app = builder.Build();
{
    app.MapControllers();
}
app.Run();
