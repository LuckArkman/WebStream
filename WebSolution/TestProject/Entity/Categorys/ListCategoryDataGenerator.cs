
using Catalog.Application.ListCategorys;
using Catalog.Application.UseCases.Category;
namespace TestProject.Entity.Categorys
{
    public class ListCategoryDataGenerator
    {
        public static IEnumerable<object[]> GetInputsWithoutAllParameter(int value = 15){
            var _fixture = new ListCategoryTestFixture();
            var _input = _fixture.GetExampleInput();
            for(int i = 0; i < value; i++){

                switch(i % 5){

                    case 0:
                    {
                        yield return new object[]{
                            new ListCategoriesInput()
                        };
                        break;
                    }
                    case 1:
                    {
                        yield return new object[]{
                            new ListCategoriesInput(
                                _input.Page
                            )
                        };
                        break;
                    }
                    case 2:
                    {
                        yield return new object[]{
                            new ListCategoriesInput(
                                _input.Page,
                                 _input.PerPage
                            )
                        };
                        break;
                    }
                    case 3:
                    {
                        yield return new object[]{
                            new ListCategoriesInput(
                                _input.Page,
                                _input.PerPage,
                                _input.Search

                            )
                        };
                        break;
                    }
                    case 4:
                    {
                        yield return new object[]{
                            new ListCategoriesInput(
                                _input.Page,
                                _input.PerPage,
                                _input.Search,
                                _input.Sort
                            )
                        };
                        break;
                    }
                    default:
                    {
                        yield return new object[]{
                            _input
                        };
                        break;
                    }
                }
            }
        }
    }
}