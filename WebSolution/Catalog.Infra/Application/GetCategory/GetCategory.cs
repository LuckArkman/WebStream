using Catalog.Application.Common;
using Catalog.Application.UseCases.Category;
using Catalog.Data.Configurations;

namespace Catalog.Infra.Application.GetCategory;

public class GetCategory : IGetCategory
{
    private readonly CatalogDbContext _context;
    public GetCategory(CatalogDbContext categoryDb) => _context = categoryDb;
    public async Task<CategoryModelOutput> Handle(GetCategoryInput request, CancellationToken cancellationToken)
    {
        var category = await _context.Get(request.Id, cancellationToken);
        return CategoryModelOutput.FromCategory(category);
    }
}