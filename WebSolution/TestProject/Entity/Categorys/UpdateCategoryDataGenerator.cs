using Catalog.Application.UseCases.Category;

namespace TestProject.Entity.Categorys;

public class UpdateCategoryDataGenerator
{
    public static IEnumerable<object[]> GetCategoriesUpdate(int times)
    {
        var _fixture = new UpdateCategoryTestFixture();
        var category = _fixture.GetTestValidCategory();
        var input = _fixture.GetValidInput(category.Id);
        for (int i = 0; i < times; i++)
        {
            yield return new object[]
            {
                category,
                input
            };
        }
    }
}