using System.IO;
using Catalog.Application.Common;
using Catalog.Application.ListCategorys;
using Catalog.Application.UseCases.Category;
using Catalog.Domain.Entitys;
using Catalog.Domain.Enum;
using FluentAssertions;
using Moq;
using Xunit;
using Xunit.Abstractions;
using Catalog.Domain.SeedWork;

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
            var input = _fixture.GetExampleInput();
            var _searchOutput = new SearchOutput<Category>(
                    currentPage: input.Page,
                    perPage: input.PerPage,
                    items: (IReadOnlyList<Category>) list,
                    total: (new Random()).Next(50,200)
                );
            categoryMock.Setup(x => x.Search(
            It.Is<SearchInput>(
                searchInput => searchInput.Page == input.Page
                && searchInput.PerPage == input.PerPage
                && searchInput.Search == input.Search
                && searchInput.OrderBy == input.Sort
                && searchInput.Order == input.Dir
            ),
            It.IsAny<CancellationToken>()
            )).ReturnsAsync(_searchOutput);
            var useCase = new ListCategories(categoryMock.Object);

            var output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Page.Should().Be(_searchOutput.CurrentPage);
            output.PerPage.Should().Be(_searchOutput.PerPage);
            output.Total.Should().Be(_searchOutput.Total);
            output.Items.Should().HaveCount(_searchOutput.Items.Count);
            ((List<CategoryModelOutput>)output.Items).ForEach(outputItem =>
            {
                var repositoryCategory = _searchOutput.Items
                    .FirstOrDefault(x => x.Id == outputItem.Id);
                outputItem.Should().NotBeNull();
                outputItem.Name.Should().Be(repositoryCategory!.Name);
                outputItem.Description.Should().Be(repositoryCategory!.Description);
                outputItem.IsActive.Should().Be(repositoryCategory!.IsActive);
                outputItem.createTime.Should().Be(repositoryCategory!.createTime);
            });
            categoryMock.Verify(x => x.Search(
                It.Is<SearchInput>(
                    searchInput => searchInput.Page == input.Page
                    && searchInput.PerPage == input.PerPage
                    && searchInput.Search == input.Search
                    && searchInput.OrderBy == input.Sort
                    && searchInput.Order == input.Dir
                ),
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }

        [Theory(DisplayName = nameof(ListCategoryLimitedParameter))]
        [Trait("ListCategoryTest", "ListCategoryTest - Use Cases")]
        [MemberData(nameof(ListCategoryDataGenerator.GetInputsWithoutAllParameter),
        parameters: 15,
        MemberType = typeof(ListCategoryDataGenerator))]
        public async Task ListCategoryLimitedParameter(ListCategoriesInput input)
        {
            var list = _fixture.GetListValidCategory();
            var categoryMock = _fixture.GetcategoryMock();
            var unityOfWorkMock = _fixture.GetunityOfWorkMock();
            
            var _searchOutput = new SearchOutput<Category>(
                    currentPage: input.Page,
                    perPage: input.PerPage,
                    items: (IReadOnlyList<Category>) list,
                    total: (new Random()).Next(50,200)
                );
                categoryMock.Setup(x => x.Search(
                It.Is<SearchInput>(
                    searchInput => searchInput.Page == input.Page
                    && searchInput.PerPage == input.PerPage
                    && searchInput.Search == input.Search
                    && searchInput.OrderBy == input.Sort
                    && searchInput.Order == input.Dir
                ),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(_searchOutput);
            var useCase = new ListCategories(categoryMock.Object);

            var output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Page.Should().Be(_searchOutput.CurrentPage);
            output.PerPage.Should().Be(_searchOutput.PerPage);
            output.Total.Should().Be(_searchOutput.Total);
            output.Items.Should().HaveCount(_searchOutput.Items.Count);
            ((List<CategoryModelOutput>)output.Items).ForEach(outputItem =>
            {
                var repositoryCategory = _searchOutput.Items
                    .FirstOrDefault(x => x.Id == outputItem.Id);
                outputItem.Should().NotBeNull();
                outputItem.Name.Should().Be(repositoryCategory!.Name);
                outputItem.Description.Should().Be(repositoryCategory!.Description);
                outputItem.IsActive.Should().Be(repositoryCategory!.IsActive);
                outputItem.createTime.Should().Be(repositoryCategory!.createTime);
            });
            categoryMock.Verify(x => x.Search(
                It.Is<SearchInput>(
                    searchInput => searchInput.Page == input.Page
                    && searchInput.PerPage == input.PerPage
                    && searchInput.Search == input.Search
                    && searchInput.OrderBy == input.Sort
                    && searchInput.Order == input.Dir
                ),
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }
        [Fact(DisplayName = nameof(ListOkWhenEmpty))]
        [Trait("ListCategoryTest", "ListCategoryTest - Use Cases")]
        public async Task ListOkWhenEmpty()
        {
            var repositoryMock = _fixture.GetcategoryMock();
            var input = _fixture.GetExampleInput();
            var outputRepositorySearch = new SearchOutput<Category>(
                currentPage: input.Page,
                perPage: input.PerPage,
                items: new List<Category>().AsReadOnly(),
                total: 0
            );
            repositoryMock.Setup(x => x.Search(
                It.Is<SearchInput>(
                    searchInput => searchInput.Page == input.Page
                    && searchInput.PerPage == input.PerPage
                    && searchInput.Search == input.Search
                    && searchInput.OrderBy == input.Sort
                    && searchInput.Order == input.Dir
                ),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(outputRepositorySearch);
            var useCase = new ListCategories(repositoryMock.Object);

            var output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Page.Should().Be(outputRepositorySearch.CurrentPage);
            output.PerPage.Should().Be(outputRepositorySearch.PerPage);
            output.Total.Should().Be(0);
            output.Items.Should().HaveCount(0);

            repositoryMock.Verify(x => x.Search(
                It.Is<SearchInput>(
                    searchInput => searchInput.Page == input.Page
                    && searchInput.PerPage == input.PerPage
                    && searchInput.Search == input.Search
                    && searchInput.OrderBy == input.Sort
                    && searchInput.Order == input.Dir
                ),
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }
    }
}