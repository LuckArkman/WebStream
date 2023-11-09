using Catalog.Application.GetCategoryTest.Categorys;
using Catalog.Domain.Exceptions;
using FluentAssertions;
using Moq;
using TestProject.Entity.GetCategory;
using Xunit;

namespace TestProject.Entity.Categorys;

[Collection(nameof(GetCategoryTestFixture))]
public class GetCategoryTest
{
    private readonly GetCategoryTestFixture _fixture;

    public GetCategoryTest(GetCategoryTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(GetCategory))]
    [Trait("GetCategoryTest", "CreateCategory - Use Cases")]
    public async Task GetCategory()
    {
        var _categoryMock = _fixture.GetcategoryMock();
        var unityOfWorkMock = _fixture.GetunityOfWorkMock();
        var _category = _fixture.GetValidCategory();
        
        _categoryMock.Setup(x => x.Get(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(_category);
        
        
        var input = new GetCategoryInput(_category.Id);
        var useCase = new GetCategoryUseCase(_categoryMock.Object);
        var output = await useCase.Handle(input, CancellationToken.None);
            
        _categoryMock.Verify(x => x.Get(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
        
        output.Should().NotBeNull();
        output.Name.Should().Be(_category.Name);
        output.Description.Should().Be(_category.Description);
        output.IsActive.Should().Be(_category.IsActive);
        output.Id.Should().Be(_category.Id);
        output.createTime.Should().Be(_category.createTime);

    }
    
    [Fact(DisplayName = nameof(NotFoundCategory))]
    [Trait("GetCategoryTest", "CreateCategory - Use Cases")]
    public async Task NotFoundCategory()
    {
        var _categoryMock = _fixture.GetcategoryMock();
        var unityOfWorkMock = _fixture.GetunityOfWorkMock();
        var newGuid = Guid.NewGuid();
        _categoryMock.Setup(x => x.Get(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>())
        ).ThrowsAsync(new NotFoundException($"Category {newGuid} not Found"));
        
        
        var input = new GetCategoryInput(newGuid);
        var useCase = new GetCategoryUseCase(_categoryMock.Object);
        
        var Task = async () => await useCase.Handle(input, CancellationToken.None);
        
        Task.Should().ThrowAsync<NotFoundException>($"Category {newGuid} not Found");
            
        _categoryMock.Verify(x => x.Get(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}