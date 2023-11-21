using Catalog.Data.Configurations;
using Catalog.Domain.Entitys;
using Microsoft.EntityFrameworkCore;

namespace Catalog.EndToEndTests.Common;

public class CategoryPersistence
{
    private readonly CatalogDbContext _context;

    public CategoryPersistence(CatalogDbContext context)
        => _context = context;

    public async Task<Category?> GetById(Guid id)
        => await _context
            .Categories.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task InsertList(List<Category> categories)
    {
        await _context.Categories.AddRangeAsync(categories);
        await _context.SaveChangesAsync();
    }
}