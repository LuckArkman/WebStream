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
    
    DbSet<Category> _categories => _catalogDb.Set<Category>();
    public CategoryRepository(CatalogDbContext dbContext)
    => _catalogDb = dbContext;

    public async Task Insert(Category category, CancellationToken none)
    => await _categories.AddAsync(category, none);

    public async Task<Category> Get(Guid Id, CancellationToken cancellationToken)
    {
       var category = await _categories.AsNoTracking().FirstOrDefaultAsync(
           x => x.Id == Id
            , cancellationToken);

       if (category == null)NotFoundException.ThrowIfNull(category,$"Category '{Id}' not found.");
       return category!;
    }

    public Task Delete(Category tAggregate, CancellationToken cancellationToken)
    =>Task .FromResult(_categories.Remove(tAggregate));

    public Task Update(Category _category, CancellationToken cancellationToken)
    => Task.FromResult(_categories.Update(_category));

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
        var skip = (input.Page - 1) * input.PerPage;
        var query = _categories.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(input.Search))
            query = query.Where(x => x.Name.Contains(input.Search));
        
        
        var total = await query.CountAsync();
        var items = await query.AsNoTracking().Skip(skip)
            .Take(input.PerPage).ToListAsync();
        return new (
            input.Page,
            input.PerPage,
            total,
            items
        );

    }
}
