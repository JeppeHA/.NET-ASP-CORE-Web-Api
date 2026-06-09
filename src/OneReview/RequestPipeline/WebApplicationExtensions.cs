using System.Security.Cryptography.X509Certificates;
using OneReview.Persistence.Database;
using Microsoft.AspNetCore.Diagnostics;
using System;

namespace OneReview.RequestPipeline;

public static class WebApplicationExtensisons
{
    public static WebApplication InitializeDatabade(this WebApplication app)
    {
        var connectionString = app.Configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        DbInitializer.Initialize(connectionString);

        DbInitializer.Initialize(
   
            app.Configuration[DbConstants.DefaultConnectionStringPath]!
        );
        return app;
    }

    public static WebApplication UseGlobalErrorHandling(this WebApplication app)
    {  
        // Catches error and redirects it to a given route
        app.UseExceptionHandler("/error");

        app.Map("/error", (HttpContext httpContext) =>
        {
            Exception? exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            if(exception is null)
            {
                // Handle unexpected case
                return Results.Problem();
            }
            // global error handling
            return Results.Problem();
        });
        return app;
    }
}