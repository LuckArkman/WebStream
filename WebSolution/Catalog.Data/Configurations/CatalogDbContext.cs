using Catalog.Domain.Entitys;
using Catalog.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Data.Configurations;

public class CatalogDbContext : DbContext
{
    public DbSet<Category> Categories => Set<Category>();
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder builder) => builder.ApplyConfiguration(new CategoryConfigurations());

    public async Task SaveChangeAsync(CancellationToken _cancellationToken)
    {
        await Task.CompletedTask;
    }
    
    public async Task<Category> Get(Guid id, CancellationToken cancellationToken)
    {
        var category = await Categories.AsNoTracking().FirstOrDefaultAsync(
            x => x.Id == id,
            cancellationToken
        );
        NotFoundException.ThrowIfNull(category, $"Category '{id}' not found.");
        return category!;
    }
}