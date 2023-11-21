using Catalog.Application.UseCases.Category;
using Catalog.EndToEndTests.Common;
using Xunit;

namespace Catalog.EndToEndTests.API.Category.CreateCategory;

public class CreateCategoryAPITestFixture
    : CategoryBaseFixture
{
    public CreateCategoryInput getExampleInput()
        => new(
            Guid.NewGuid(),
            GetValidCategoryName(),
            GetValidCategoryDescription(),
            getRandomBoolean()
        );
}