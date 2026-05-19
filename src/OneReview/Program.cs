using OneReview.Persistence.Database;
using OneReview.Services;
using OneReview.DependencyInjection;
using OneReview.RequestPipeline;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddServices()
    .AddGlobalErrorHandling()
    .AddPersistence(builder.Configuration)
    .AddControllers();
}

var app = builder.Build();
{
    app.UseExceptionHandler();
    app.MapControllers();
    app.InitializeDatabade();
}




app.Run();

   