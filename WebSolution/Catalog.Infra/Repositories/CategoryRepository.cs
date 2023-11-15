using Catalog.Data.Configurations;
using Catalog.Domain.Entitys;
using Catalog.Domain.Exceptions;
using Catalog.Domain.Repository;
using Catalog.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infra.Repositories;

public class CategoryRepository : ICategoryRepository
{
    CatalogDbContext _catalogDb;
    
    DbSet<Category> Categories => _catalogDb.Set<Category>();
    public CategoryRepository(CatalogDbContext dbContext)
    => _catalogDb = dbContext;

    public async Task Insert(Category category, CancellationToken none)
    => await Categories.AddAsync(category, none);

    public async Task<Category> Get(Guid Id, CancellationToken cancellationToken)
    {
       var category = await Categories.AsNoTracking().FirstOrDefaultAsync(
           x => x.Id == Id
            , cancellationToken);

       if (category == null)NotFoundException.ThrowIfNull(category,$"Category '{Id}' not found.");
       return category!;
    }

    public Task Delete(Category tAggregate, CancellationToken cancellationToken)
    =>Task .FromResult(Categories.Remove(tAggregate));

    public Task Update(Category _category, CancellationToken cancellationToken)
    => Task.FromResult(Categories.Update(_category));

    public Task<IReadOnlyList<Guid>> GetIdsListByIds(List<Guid> ids, CancellationToken cancellationToken)
    {
        return null;
    }

    public Task<IReadOnlyList<Guid>> GetListByIds(List<Guid> ids, CancellationToken cancellationToken)
    {
        return null;
    }

    public async Task<SearchOutput<Category>> Search(SearchInput input, CancellationToken cancellationToken)
    {
        var total = await Categories.CountAsync();
        var items = await Categories.ToListAsync();
        return new SearchOutput<Category>(
            input.Page,
            input.PerPage,
            total,
            items
        );

    }
}
