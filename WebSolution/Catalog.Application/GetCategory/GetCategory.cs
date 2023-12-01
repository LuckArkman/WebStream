using Catalog.Application.Common;
using Catalog.Application.Repositories;
using Catalog.Application.UseCases.Category;

namespace Catalog.Application.GetCategoryTest.Categorys;

public class GetCategory: IGetCategory
{
    private readonly ICategoryRepository _category;
    public GetCategory(ICategoryRepository categoryMock) => _category = categoryMock;

    public async Task<CategoryModelOutput> Handle(GetCategoryInput request, CancellationToken cancellationToken)
    {
        
        var category = await _category.Get(request.Id, cancellationToken);
        return CategoryModelOutput.FromCategory(category);

    }
}