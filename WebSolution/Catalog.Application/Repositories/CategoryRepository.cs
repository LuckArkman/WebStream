using Catalog.Data.Configurations;
using Catalog.Domain.Entitys;
using Catalog.Domain.Enum;
using Catalog.Domain.Exceptions;
using Catalog.Domain.Repository;
using Catalog.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Repositories;

public class CategoryRepository : ICategoryRepository
{
    public CatalogDbContext _catalogDb { get; set; }

    public DbSet<Category> _categories => _catalogDb.Set<Category>();
    public CategoryRepository(CatalogDbContext dbContext)
    => _catalogDb = dbContext;

    public async Task Insert(Category category, CancellationToken none)
    {
        await _catalogDb.AddRangeAsync(category);
        await _catalogDb.SaveChangeAsync(none);
        await _categories.AddAsync(category, none);
    }

    public async Task<Category> Get(Guid Id, CancellationToken cancellationToken)
    {
        var category = await _catalogDb.Get(Id, cancellationToken);
       if (category is null)NotFoundException.ThrowIfNull(category,$"Category '{Id}' not found.");
       return category!;
    }

    public Task Delete(Category tAggregate, CancellationToken cancellationToken)
    =>Task.FromResult(_categories.Remove(tAggregate));

    public async Task Update(Category _category, CancellationToken cancellationToken) => await Task.FromResult(_categories.Update(_category));

    public async Task<SearchOutput<Category>> Search(
        SearchInput input, 
        CancellationToken cancellationToken)
    {
        var toSkip = (input.Page - 1) * input.PerPage;
        var query = _categories.AsNoTracking();
        query = AddOrderToQuery(query, input.OrderBy, input.Order);
        if(!String.IsNullOrWhiteSpace(input.Search))
            query = query.Where(x => x.Name.Contains(input.Search));
        var total = await query.CountAsync();
        var items = await query
            .Skip(toSkip)
            .Take(input.PerPage)
            .ToListAsync();
        return new(input.Page, input.PerPage, total, items);
    }
    
    private IQueryable<Category> AddOrderToQuery(
        IQueryable<Category> query,
        string orderProperty,
        SearchOrder order
    )
    { 
        var orderedQuery = (orderProperty.ToLower(), order) switch
        {
            ("name", SearchOrder.Asc) => query.OrderBy(x => x.Name)
                .ThenBy(x => x.Id),
            ("name", SearchOrder.Desc) => query.OrderByDescending(x => x.Name)
                .ThenByDescending(x => x.Id),
            ("id", SearchOrder.Asc) => query.OrderBy(x => x.Id),
            ("id", SearchOrder.Desc) => query.OrderByDescending(x => x.Id),
            ("createdat", SearchOrder.Asc) => query.OrderBy(x => x.createTime),
            ("createdat", SearchOrder.Desc) => query.OrderByDescending(x => x.createTime),
            _ => query.OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
        };
        return orderedQuery;
    }
}
