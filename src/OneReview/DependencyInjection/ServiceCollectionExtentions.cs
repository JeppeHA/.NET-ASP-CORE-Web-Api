using Microsoft.Extensions.DependencyInjection;

using OneReview.Persistence.Database;
using OneReview.Persistence.Repositories;
using OneReview.Services;
using OneReview.Services.Import;


namespace OneReview.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(
        this IServiceCollection services
    )
    {
        services.AddScoped<PlayerService>();
        services.AddScoped<CourseService>();
        services.AddScoped<HoleService>();
        services.AddScoped<CourseImportService>();
        services.AddScoped<CourseImportParser>();
        return services;
    }

    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddScoped<IDbConnectionFactory>(_ =>
         new NpgsqlConnectionFactory(
            configuration[DbConstants.DefaultConnectionStringPath]!));
        services.AddScoped<PlayerRepository>();
        services.AddScoped<CourseRepository>();
        services.AddScoped<HoleRepository>();
        return services;
    }

    public static IServiceCollection AddGlobalErrorHandling(
        this IServiceCollection services
    )
    {
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Extensions["traceId"] =  context.HttpContext.TraceIdentifier;
            };
        });
        return services;
    }
}