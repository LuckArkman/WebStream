using Catalog.Application.Common;
using Catalog.Application.GetCategoryTest.Categorys;
using Catalog.Application.UseCases.Category;
using Catalog.Data.Configurations;
using Catalog.Domain.Repository;

namespace Catalog.Infra.Application.GetCategory;

public class GetCategory : IGetCategory
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly CatalogDbContext _context;

    public GetCategory(CatalogDbContext categoryDb) => _context = categoryDb;

    public GetCategory(ICategoryRepository categoryRepository) => _categoryRepository = categoryRepository;

    public async Task<CategoryModelOutput> Handle(GetCategoryInput request, CancellationToken cancellationToken)
    {
        var category = await _context.Get(request.Id, cancellationToken);
        return CategoryModelOutput.FromCategory(category);
    }
}