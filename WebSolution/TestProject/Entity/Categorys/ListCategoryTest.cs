using System.IO;
using Catalog.Application.ListCategorys;
using Catalog.Domain.Entitys;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace TestProject.Entity.Categorys
{
    [Collection(nameof(ListCategoryTestFixture))]
    public class ListCategoryTest
    {
        readonly ListCategoryTestFixture _fixture;

        public ListCategoryTest(ListCategoryTestFixture fixture)
        => _fixture = fixture;


        [Fact(DisplayName = nameof(ListCategory))]
        [Trait("ListCategoryTest", "ListCategoryTest - Use Cases")]
        public async Task ListCategory()
        {
            var list = _fixture.GetListValidCategory();
            var categoryMock = _fixture.GetcategoryMock();
            var unityOfWorkMock = _fixture.GetunityOfWorkMock();
            var input = new ListCategoryInput(
                page: 2,
                perPage: 5,
                Search: "exemple",
                Sort: "name",
                Dir: SearchOrder.Asc
            );
            categoryMock.Setup(x => x.Search(
                It.IsAny<SearchInput>(
                    SearchInput.Page == input.Page
                    && SearchInput.PerPage == input.PerPage
                    && SearchInput.Search == input.Search
                    && SearchInput.OrderBy == input.Sort
                    && SearchInput.OrderBy = input.OrderBy
                ),
                It.IsAny<CancellationToken>()
            )).returnsAsync(SearchOutput<Category>(
                ITems: (IReadOnlyList<Category>) list,
                Total: 70
            ));

            var UseCase = new ListCategories(categoryMock.Object);

            var _output = await UseCase.Handle(input, CancellationToken.None);

            
        }
    }
}