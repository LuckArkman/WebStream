using Catalog.Application.UseCases.Category;
using Catalog.Domain.Repository;
using MediatR;
using Moq;

namespace Catalog.Application.GetCategoryTest.Categorys;

public class GetCategoryUseCase: IRequestHandler<GetCategoryInput, GetCategoryOutPut>
{
    private readonly ICategoryRepository _category;
    public GetCategoryUseCase(ICategoryRepository categoryMock) => _category = categoryMock;

    public Task<GetCategoryOutPut> Handle(GetCategoryInput request, CancellationToken cancellationToken)
    {
        var category = _category.Get(request.Id, cancellationToken);
        return null;

    }
}