using Castle.Core.Configuration;
using Catalog.Data.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.API.Configurations;

public static class ConnectionsConfiguration
{
    /*
    public static IServiceCollection AddAppConections(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbConnection(configuration);
        return services;
    }

    private static IServiceCollection AddDbConnection(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration
            .GetConnectionString("CatalogDb");
        services.AddDbContext<CatalogDbContext>(
            options => options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)
            )
        );
        return services;
    }

    public static WebApplication MigrateDatabase(
        this WebApplication app)
    {
        var environment = Environment
            .GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (environment == "EndToEndTest") return app;
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider
            .GetRequiredService<CatalogDbContext>();
        dbContext.Database.Migrate();
        return app;
    }
    */
}