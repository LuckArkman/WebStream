using Catalog.Domain.Entitys;
using Catalog.Infra.Common;

namespace Catalog.Infra.Application.DeleteCategory;

public class DeleteCategoryTestFixture : CategoryUseCaseBaseFixture
{
    public List<Category> GetExCategoryList(int length = 10)
        => Enumerable.Range(0, length).Select(_ =>GetValidCategory()).ToList();
}