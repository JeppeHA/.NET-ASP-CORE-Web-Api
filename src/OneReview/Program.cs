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
    app.UseDeveloperExceptionPage(); 
    app.UseRouting();            
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.InitializeDatabade();
}

app.Run();

// Start in terminal
//docker exec -it <container_name> psql -U <username> -d <database>
// Quit in terminal
// /q
   