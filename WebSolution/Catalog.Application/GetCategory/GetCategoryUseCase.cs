using Catalog.Application.Common;
using Catalog.Application.Repositories;
using Catalog.Application.UseCases.Category;
using Catalog.Domain.Repository;
using MediatR;
using Moq;

namespace Catalog.Application.GetCategoryTest.Categorys;

public class GetCategoryUseCase: IGetCategory
{
    private readonly ICategoryRepository _category;
    public GetCategoryUseCase(ICategoryRepository categoryMock) => _category = categoryMock;

    public async Task<CategoryModelOutput> Handle(GetCategoryInput request, CancellationToken cancellationToken)
    {
        
        var category = await _category.Get(request.Id, cancellationToken);
        return CategoryModelOutput.FromCategory(category);

    }
}