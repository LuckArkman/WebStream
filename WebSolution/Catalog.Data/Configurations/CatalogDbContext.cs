using Catalog.Domain.Entitys;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Data.Configurations;

public class CatalogDbContext : DbContext
{
    public DbSet<Category> Categories => Set<Category>();
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder builder) 
        => builder.ApplyConfiguration(new CategoryConfigurations());

    public async Task SaveChangeAsync(CancellationToken _cancellationToken)
    {
        await Task.CompletedTask;
    }
}