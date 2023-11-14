using System.Collections.ObjectModel;
using Catalog.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentAssertions;
namespace Catalog.Infra.Repositories
{
    [Collection(nameof(CategoryRepositoryTestFixture))]
    public class CategoryRepositoryTest
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
            var _categoryRepository = new CategoryRepository(_dbContext);

            var _task = async () =>
                await _categoryRepository.Get(
                    Id, CancellationToken.None);
            await _task.Should().ThrowAsync<NotFoundException>().WithMessage($"Category '{Id}' not found.");

        } 
    }
}