using Catalog.Application.Base;
using Catalog.Data.Configurations;
using Catalog.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace catalog.API.Configurations;

public static class ConnectionsConfiguration
{
    public static IServiceCollection DBConfiguration(this IServiceCollection service)
    => service.addDBConnection();
    
    static IServiceCollection addDBConnection(this IServiceCollection service)
    {
        Singleton._instance().CreateDBContext();
        service.AddDbContext<CatalogDbContext>(options => options.UseMySql(
                "catalogDB")
            );
        return service;
    }
}