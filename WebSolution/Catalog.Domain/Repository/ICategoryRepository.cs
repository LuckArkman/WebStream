using Catalog.Data.Configurations;
using Catalog.Domain.Entitys;
using Catalog.Domain.SeedWork;

namespace Catalog.Domain.Repository;

public interface ICategoryRepository : IGenericRepository<Category>, ISearchableRepository<Category>
{
    public CatalogDbContext _catalogDb { get; set; }

    public Task Update(
        Category _category,
        CancellationToken cancellationToken
    );

    
}