using Catalog.Domain.Entitys;
using Catalog.Infra.Data;

namespace Catalog.Infra.Repositories;

public class CategoryRepository
{
    CatalogDbContext _catalogDb;
    public CategoryRepository(CatalogDbContext dbContext)
    => _catalogDb = dbContext;

    public async Task Insert(Category category, CancellationToken none)
    {
        
    }
}
