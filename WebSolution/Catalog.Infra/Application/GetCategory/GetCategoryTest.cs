using Catalog.Application.UseCases.Category;
using Catalog.Domain.Exceptions;
using Catalog.Infra.Repositories;
using FluentAssertions;
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
        var repositoryMock = _fixture.GetcategoryMock();
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
        output.CreatedAt.Should().Be(_category.createTime);
    }
    
    [Fact(DisplayName = nameof(NotFoundExceptionWhenCategoryDoesntExist))]
    [Trait("GetCategoryTestFixture", "CategoryRepositoryTest - Infra")]
    public async Task NotFoundExceptionWhenCategoryDoesntExist()
    {
        var _Id = Guid.NewGuid().ToString();
        var _repositoryMock = _fixture.GetcategoryMock();
        var _categoryDB = _fixture.CreateDBContext(false, _Id);
        var _category = _fixture.GetValidCategory();
        _categoryDB.Categories.Add(_category);
        _categoryDB.SaveChanges();
        
        var _categoryRepository = new CategoryRepository(_categoryDB);
        var input = new GetCategoryInput(Guid.NewGuid());
        var useCase = new GetCategory(_categoryDB);

        var task = async ()
            => await useCase.Handle(input, CancellationToken.None);

        await task.Should().ThrowAsync<NotFoundException>().WithMessage($"Category '{input.Id}' not found.");
    }
}