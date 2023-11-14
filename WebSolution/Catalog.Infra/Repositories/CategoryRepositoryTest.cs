using System.Collections.ObjectModel;
using Catalog.Infra.Data;
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

            var _dbCategory = await _dbContext.Categories.Find(_category.Id);
            _dbCategory.Should().NotBeNull();
            _dbCategory.Should().Be(_category.Name);
            _dbCategory.Should().Be(_category.Description);
            _dbCategory.Should().Be(_category.IsActive);
            _dbCategory.Should().Be(_category.createTime);

        }        
    }
}