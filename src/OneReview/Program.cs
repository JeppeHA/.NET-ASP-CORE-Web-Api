using OneReview.Persistence.Database;
using OneReview.Services;
using OneReview.DependencyInjection;
using OneReview.RequestPipeline;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddServices()
    .AddPersistence(builder.Configuration)
    .AddControllers();
}

var app = builder.Build();
{
    app.MapControllers();
    Console.WriteLine("Connection string!!!!");
    Console.WriteLine("Connection string: " + app.Configuration.GetConnectionString("DefaultConnection"));
    app.InitializeDatabade();
}




app.Run();

   