using OneReview.Persistence.Database;

namespace OneReview.RequestPipeline;

public static class WebApplicationExtensisons
{
    public static WebApplication InitializeDatabade(this WebApplication app)
    {
        Console.WriteLine("PATH!!!");
        Console.WriteLine(DbConstants.DefaultConnectionStringPath);
        
        DbInitializer.Initialize(
   
            app.Configuration[DbConstants.DefaultConnectionStringPath]!
        );

        Console.WriteLine(":::");
        Console.WriteLine(app.Configuration["Database:ConnectionStrings:DefaultConnection"]);
        Console.WriteLine("---");
        Console.WriteLine(app.Configuration["Database__ConnectionStrings__DefaultConnection"]);

        return app;
    }
}