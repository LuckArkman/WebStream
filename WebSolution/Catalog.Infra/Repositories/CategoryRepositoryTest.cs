using System.Collections.ObjectModel;
using Catalog.Domain.Entitys;
using Catalog.Domain.Enum;
using Catalog.Domain.Exceptions;
using Catalog.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentAssertions;
namespace Catalog.Infra.Repositories
{
    [Collection(nameof(CategoryRepositoryTestFixture))]
    public class CategoryRepositoryTest : IDisposable
    {
        readonly CategoryRepositoryTestFixture _fixture;

        public CategoryRepositoryTest(CategoryRepositoryTestFixture fixture)
        => _fixture = fixture;

        [Fact(DisplayName = nameof(Insert))]
        [Trait("CategoryRepositoryTest", "CategoryRepositoryTest - Infra")]
        public async Task Insert()
        {
            var _dbContext = _fixture.CreateDBContext();
            var _category = _fixture.GetExCategory();
            var _categoryRepository = new CategoryRepository(_dbContext);

            await _categoryRepository.Insert(_category, CancellationToken.None);
            await _dbContext.SaveChangeAsync(CancellationToken.None);

            var _dbCategory = await _dbContext.Categories.FindAsync(_category.Id);
            _dbCategory.Should().NotBeNull();
            _dbCategory.Name.Should().Be(_category.Name);
            _dbCategory.Description.Should().Be(_category.Description);
            _dbCategory.IsActive.Should().Be(_category.IsActive);
            _dbCategory.createTime.Should().Be(_category.createTime);

        } 
        
        [Fact(DisplayName = nameof(GetCategoryThrowIfNotFound))]
        [Trait("CategoryRepositoryTest", "CategoryRepositoryTest - Infra")]
        public async Task GetCategoryThrowIfNotFound()
        {
            Guid Id = Guid.NewGuid();
            var _dbContext = _fixture.CreateDBContext();
            var _categoryex = _fixture.GetExCategory();
            var _category = _fixture.GetExCategoryList(15);
            _category.Add(_categoryex);
            await _dbContext.AddRangeAsync(_category);
            await _dbContext.SaveChangeAsync(CancellationToken.None);
            var _categoryRepository = new CategoryRepository(_fixture.CreateDBContext());

            var _task = async () =>
                await _categoryRepository.Get(
                    Id, CancellationToken.None);
            await _task.Should().ThrowAsync<NotFoundException>().WithMessage($"Category '{Id}' not found.");

        } 
        
        [Fact(DisplayName = nameof(UpdateCategory))]
        [Trait("CategoryRepositoryTest", "CategoryRepositoryTest - Infra")]
        public async Task UpdateCategory()
        {
            Guid Id = Guid.NewGuid();
            var newCategpory = _fixture.GetExCategory();
            var _dbContext = _fixture.CreateDBContext();
            var _categoryex = _fixture.GetExCategory();
            var _category = _fixture.GetExCategoryList(15);
            await _dbContext.SaveChangeAsync(CancellationToken.None);
            await _dbContext.AddRangeAsync(_category);
            
            
            var _categoryRepository = new CategoryRepository(_dbContext);
           _categoryex.Update(newCategpory.Name, newCategpory.Description);
            
            await _categoryRepository.Update(_categoryex, CancellationToken.None);
            await _dbContext.SaveChangeAsync(CancellationToken.None);
            
            var _dbCategory = await _dbContext.Categories.FindAsync(
                _categoryex.Id);
            
            _dbCategory.Should().NotBeNull();
            _dbCategory!.Name.Should().Be(newCategpory.Name);
            _dbCategory.Description.Should().Be(newCategpory.Description);
            _dbCategory.IsActive.Should().Be(_categoryex.IsActive);
            _dbCategory.createTime.Should().Be(_categoryex.createTime);

        } 
        
        [Fact(DisplayName = nameof(DeleteCategory))]
        [Trait("CategoryRepositoryTest", "CategoryRepositoryTest - Infra")]
        public async Task DeleteCategory()
        {
            Guid Id = Guid.NewGuid();
            var _dbContext = _fixture.CreateDBContext();
            var _categoryex = _fixture.GetExCategory();
            var _category = _fixture.GetExCategoryList(15);
            
            _category.Add(_categoryex);
            await _dbContext.AddRangeAsync(_category);
            await _dbContext.SaveChangeAsync(CancellationToken.None);
            
            var _categoryRepository = new CategoryRepository(_fixture.CreateDBContext());
            _categoryRepository.Delete(_categoryex, CancellationToken.None);
            await _dbContext.SaveChangeAsync(CancellationToken.None);
            var _dbCategory = await _fixture.CreateDBContext().Categories.FindAsync(_categoryex.Id);
            
            _dbCategory.Should().BeNull();
        } 
        
        
        [Fact(DisplayName = nameof(SearchCategory))]
        [Trait("CategoryRepositoryTest", "CategoryRepositoryTest - Infra")]
        public async Task SearchCategory()
        {
            Guid Id = Guid.NewGuid();
            var _dbContext = _fixture.CreateDBContext();
            var _categoryex = _fixture.GetExCategory();
            var _category = _fixture.GetExCategoryList(15); 
            await _dbContext.SaveChangeAsync(CancellationToken.None);
            await _dbContext.AddRangeAsync(_category);
            var _categoryRepository = new CategoryRepository(_fixture.CreateDBContext());
            
            var _search = new SearchInput(
                1,
                20,
                "",
                "",
                SearchOrder.Asc);

            var _output = await _categoryRepository.Search(_search, CancellationToken.None);
            
            _output.Should().NotBeNull();
            _output.Items.Should().NotBeNull();
            _output.CurrentPage.Should().Be(_search.Page);
            _output.PerPage.Should().Be(_search.PerPage);
            foreach ( Category item in _output.Items)
            {
                var obj = _category.Find(x
                    => x.Id == item.Id);
                obj.Should().NotBeNull();
                obj.Name.Should().Be(item.Name);
                obj.Description.Should().Be(item.Description);
                obj.IsActive.Should().Be(item.IsActive);
                obj.createTime.Should().Be(item.createTime);
            }
        } 
        
        [Fact(DisplayName = nameof(SearchEmptyCategory))]
        [Trait("CategoryRepositoryTest", "CategoryRepositoryTest - Infra")]
        public async Task SearchEmptyCategory()
        {
            Guid Id = Guid.NewGuid();
            var _dbContext = _fixture.CreateDBContext();
            var _categoryRepository = new CategoryRepository(_fixture.CreateDBContext());
            var _searchInput = new SearchInput(
                1,
                20,
                "",
                "",
                SearchOrder.Asc);

            var _output = await _categoryRepository.Search(_searchInput, CancellationToken.None);
            
            _output.Should().NotBeNull();
            _output.Items.Should().NotBeNull();
            _output.CurrentPage.Should().Be(_searchInput.Page);
            _output.PerPage.Should().Be(_searchInput.PerPage);
            _output.Items.Should().HaveCount(0);
        }

        [Theory(DisplayName = nameof(SearchListCategory))]
        [Trait("CategoryRepositoryTest", "CategoryRepositoryTest - Infra")]
        [InlineData(7,1,5,5)]
        public async Task SearchListCategory(int categoriesgenerated,
            int page,
            int perPage,
            int items
        )
        {
            Guid Id = Guid.NewGuid();
            var _dbContext = _fixture.CreateDBContext();
            var _categoryex = _fixture.GetExCategory();
            var _category = _fixture.GetExCategoryList(categoriesgenerated);
            await _dbContext.SaveChangeAsync(CancellationToken.None);
            await _dbContext.AddRangeAsync(_category);
            var _categoryRepository = new CategoryRepository(_fixture.CreateDBContext());

            var _search = new SearchInput(
                page,
                perPage,
                "",
                "",
                SearchOrder.Asc);

            var _output = await _categoryRepository.Search(_search, CancellationToken.None);

            _output.Should().NotBeNull();
            _output.Items.Should().NotBeNull();
            _output.CurrentPage.Should().Be(_search.Page);
            _output.PerPage.Should().Be(_search.PerPage);
            foreach (Category item in _output.Items)
            {
                var obj = _category.Find(x
                    => x.Id == item.Id);
                obj.Should().NotBeNull();
                obj.Name.Should().Be(item.Name);
                obj.Description.Should().Be(item.Description);
                obj.IsActive.Should().Be(item.IsActive);
                obj.createTime.Should().Be(item.createTime);
            }
        }
        
        [Theory(DisplayName = nameof(SearchItemCategory))]
        [Trait("CategoryRepositoryTest", "CategoryRepositoryTest - Infra")]
        [InlineData("Action",1,1,1)]
        [InlineData("Horror",1,1,1)]
        [InlineData("Comedy",1,1,1)]
        [InlineData("Sci-fi",1,1,1)]
        public async Task SearchItemCategory(
            string search,
            int page,
            int perPage,
            int items
        )
        {
            Guid Id = Guid.NewGuid();
            var _dbContext = _fixture.CreateDBContext();
            var _categoryex = _fixture.GetExCategory();
            var _category = _fixture.GetExCategoriesList(new List<string>()
            {
                "Action",
                "Horror",
                "Comedy",
                "Sci-fi",
            });
            await _dbContext.SaveChangeAsync(CancellationToken.None);
            await _dbContext.AddRangeAsync(_category);
            var _categoryRepository = new CategoryRepository(_fixture.CreateDBContext());

            var _search = new SearchInput(
                page,
                perPage,
                search,
                "",
                SearchOrder.Asc);

            var _output = await _categoryRepository.Search(_search, CancellationToken.None);

            _output.Should().NotBeNull();
            _output.Items.Should().NotBeNull();
            _output.CurrentPage.Should().Be(_search.Page);
            _output.PerPage.Should().Be(_search.PerPage);
            foreach (Category item in _output.Items)
            {
                var obj = _category.Find(x
                    => x.Id == item.Id);
                obj.Should().NotBeNull();
                obj.Name.Should().Be(item.Name);
                obj.Description.Should().Be(item.Description);
                obj.IsActive.Should().Be(item.IsActive);
                obj.createTime.Should().Be(item.createTime);
            }
        }
        
        [Theory(DisplayName = nameof(SearchOrdenedCategory))]
        [Trait("CategoryRepositoryTest", "CategoryRepositoryTest - Infra")]
        [InlineData("name","asc")]
        public async Task SearchOrdenedCategory(
            string search,
            string type
        )
        {
            var _dbContext = _fixture.CreateDBContext();
            var _category = _fixture.GetExCategoryList(10);
            await _dbContext.AddRangeAsync(_category);
            await _dbContext.SaveChangeAsync(CancellationToken.None);
            var _categoryRepository = new CategoryRepository(_dbContext);
            var _searchOrder = type.ToLower() == "asc" ? SearchOrder.Asc : SearchOrder.Desc;
            var _search = new SearchInput(10, 20, "", type, _searchOrder);
            var _output = await _categoryRepository.Search(_search, CancellationToken.None);
            var expected = _fixture.GetCloneCategoryList(_category, type, _searchOrder);
            
            _output.Should().NotBeNull();
            _output.Items.Should().NotBeNull();
            _output.CurrentPage.Should().Be(_search.Page);
            _output.PerPage.Should().Be(_search.PerPage);
            foreach (Category item in _output.Items)
            {
                var obj = _category.Find(x
                    => x.Id == item.Id);
                obj.Should().NotBeNull();
                obj.Name.Should().Be(item.Name);
                obj.Description.Should().Be(item.Description);
                obj.IsActive.Should().Be(item.IsActive);
                obj.createTime.Should().Be(item.createTime);
            }
        }
        
        [Theory(DisplayName = nameof(SearchItemOrdenedCategory))]
        [Trait("CategoryRepositoryTest", "CategoryRepositoryTest - Infra")]
        [InlineData("name","asc")]
        public async Task SearchItemOrdenedCategory(
            string search,
            string order
        )
        {
            var dbContext = _fixture.CreateDBContext();
            var exampleCategoriesList =
                _fixture.GetExCategoryList(10);
            await dbContext.AddRangeAsync(exampleCategoriesList);
            await dbContext.SaveChangesAsync(CancellationToken.None);
            var categoryRepository = new CategoryRepository(dbContext);
            var searchOrder = order.ToLower() == "asc" ? SearchOrder.Asc : SearchOrder.Desc;
            var searchInput = new SearchInput(1, 20, "", search.ToLower(), searchOrder);

            var output = await categoryRepository.Search(searchInput, CancellationToken.None);

            var expectedOrderedList = _fixture.GetCloneCategoryList(
                exampleCategoriesList,
                search,
                searchOrder
            );
            output.Should().NotBeNull();
            output.Items.Should().NotBeEmpty();
            output.CurrentPage.Should().Be(searchInput.Page);
            output.PerPage.Should().Be(searchInput.PerPage);
            output.Total.Should().Be(exampleCategoriesList.Count);
            output.Items.Should().HaveCount(exampleCategoriesList.Count);
            for(int indice = 0; indice < expectedOrderedList.Count; indice++)
            {
                var expectedItem = expectedOrderedList[indice];
                var outputItem = output.Items[indice];
                expectedItem.Should().NotBeNull();
                outputItem.Should().NotBeNull();
                outputItem.Name.Should().Be(expectedItem!.Name);
            }
        }

        public void Dispose()
        {
            //_fixture.CleanInMemoryDatabase();
        }
    }
}