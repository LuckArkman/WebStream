using Catalog.Application.GetCategoryTest.Categorys;
using Catalog.Infra.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace Catalog.Infra.Application.GetCategory;

[Collection(nameof(GetCategoryTestFixture))]
public class GetCategoryTest
{
    private readonly GetCategoryTestFixture _fixture;

    public GetCategoryTest(GetCategoryTestFixture fixture) => _fixture = fixture;
    
    [Fact(DisplayName = nameof(GetCategory))]
    [Trait("GetCategoryTestFixture", "CategoryRepositoryTest - Infra")]
    public async Task GetCategory()
    {
        var _Id = Guid.NewGuid().ToString();
        var _categoryDB = _fixture.CreateDBContext(false, _Id);
        var _category = _fixture.GetValidCategory();
        _categoryDB.Categories.Add(_category);
        _categoryDB.SaveChanges();
        
        var _categoryRepository = new CategoryRepository(_categoryDB);
        var input = new GetCategoryInput(_category.Id);
        var useCase = new GetCategory(_categoryDB);
        
        var output = await useCase.Handle(input, CancellationToken.None);
        
        output.Should().NotBeNull();
        output.Name.Should().Be(_category.Name);
        output.Description.Should().Be(_category.Description);
        output.IsActive.Should().Be(_category.IsActive);
        output.Id.Should().Be(_category.Id);
        output.createTime.Should().Be(_category.createTime);

    }
}