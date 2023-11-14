using Microsoft.EntityFrameworkCore;

namespace Catalog.Infra.Data;

public class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
    {
        
    }

    public async Task SaveChangeAsync(CancellationToken none)
    {
        throw new NotImplementedException();
    }
}