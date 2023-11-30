using Catalog.Data.Configurations;
using Catalog.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace catalog.API.Configurations;

public static class ConnectionsConfiguration
{
    public static IServiceCollection DBConfiguration(this IServiceCollection service)
    {
        service.addDBConnection();
        return service;
    }
    
    static IServiceCollection addDBConnection(this IServiceCollection service)
    {
        service.AddDbContext<CatalogDbContext>(options => options.UseInMemoryDatabase(
                "catalogDB")
            );
        return service;
    }
}