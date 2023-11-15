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
            var _dbContext = _fixture.CreateDBContext(false);
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
            var _dbContext = _fixture.CreateDBContext(false);
            var _categoryex = _fixture.GetExCategory();
            var _category = _fixture.GetExCategoryList(15);
            _category.Add(_categoryex);
            await _dbContext.AddRangeAsync(_category);
            await _dbContext.SaveChangeAsync(CancellationToken.None);
            var _categoryRepository = new CategoryRepository(_fixture.CreateDBContext(true));

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
            var _dbContext = _fixture.CreateDBContext(true);
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
            var _dbContext = _fixture.CreateDBContext(false);
            var _categoryex = _fixture.GetExCategory();
            var _category = _fixture.GetExCategoryList(15);
            
            _category.Add(_categoryex);
            await _dbContext.AddRangeAsync(_category);
            await _dbContext.SaveChangeAsync(CancellationToken.None);
            
            var _categoryRepository = new CategoryRepository(_fixture.CreateDBContext(true));
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
            var _dbContext = _fixture.CreateDBContext(true);
            var _categoryex = _fixture.GetExCategory();
            var _category = _fixture.GetExCategoryList(15); 
            await _dbContext.SaveChangeAsync(CancellationToken.None);
            await _dbContext.AddRangeAsync(_category);
            var _categoryRepository = new CategoryRepository(_fixture.CreateDBContext(false));
            
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
        public void Dispose()
        {
            _fixture.CleanInMemoryDatabase();
        }
    }
}