using Catalog.Application.UseCases.Category;
using Catalog.Domain.Entitys;
using TestProject.Entity.Common;

namespace TestProject.Entity.Categorys;

public class UpdateCategoryDataGenerator : BaseFixture
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

    public static IEnumerable<object[]> GetInvalidInputs(int times = 12)
    {
        var _fixture = new UpdateCategoryTestFixture();
        var invalidInput = new List<object[]>();
        var total = 3;
        for (int i = 0; i < times; i++)
        {
            switch (i % total)
            {
                case 0:
                {
                    invalidInput.Add(new object[]
                    {
                        _fixture.GetInvalidName(),
                        "Name should be at least 3 characters long"
                    });
                    break;
                }
                case 1:
                {
                    invalidInput.Add(new object[]
                    {
                        _fixture.GetInvalidLongName(),
                        "Name should be less or equal 255 characters Long"
                    });
                    break;
                }
                case 2:
                {
                    invalidInput.Add(new object[]
                    {
                        _fixture.GetInvalidLongDesc(),
                        "Description should be less or equal 10000 characters long"
                    });
                    break;
                }
                default:
                    break;
            }
        }

        return invalidInput;
    }  
    
    
}