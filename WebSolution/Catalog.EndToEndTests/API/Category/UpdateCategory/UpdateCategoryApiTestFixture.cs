using Catalog.Application.UseCases.Category;
using Catalog.EndToEndTests.Common;

namespace Catalog.EndToEndTests.API.Category.UpdateCategory;

public class UpdateCategoryApiTestFixture
    : CategoryBaseFixture
{
    public UpdateCategoryApiInput GetExampleInput()
        => new (
            GetValidCategoryName(),
            GetValidCategoryDescription(),
            getRandomBoolean()
        );
}