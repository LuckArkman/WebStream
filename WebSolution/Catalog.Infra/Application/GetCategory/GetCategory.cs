using Catalog.Application.Common;
using Catalog.Application.GetCategoryTest.Categorys;
using Catalog.Application.UseCases.Category;

namespace Catalog.Infra.Application.GetCategory;

public class GetCategory : IGetCategory
{
    public Task<CategoryModelOutput> Handle(GetCategoryInput request, CancellationToken cancellationToken)
        => null;
}