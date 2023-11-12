using TestProject.Entity.Categorys;

namespace TestProject.CreateCategorys;

public class CreateCategoryDataGenerator
{
    public static IEnumerable<object[]> GetInvalidInputs(int times = 12)
    {
        var _fixture = new CreateCategoryTestFixture();
        var invalidInput = new List<object[]>();
        var total = 4;
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
                        _fixture.GetInvalidNullDesc(),
                        "Description should not be null"
                    });
                    break;
                }
                case 3:
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