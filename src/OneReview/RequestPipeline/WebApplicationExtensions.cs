using OneReview.Persistence.Database;

namespace OneReview.RequestPipeline;

public static class WebApplicationExtensisons
{
    public static WebApplication InitializeDatabade(this WebApplication app)
    {
        var connectionString = app.Configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        DbInitializer.Initialize(connectionString);

        return app;
    }
}