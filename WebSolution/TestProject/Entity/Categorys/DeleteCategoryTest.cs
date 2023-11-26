using Catalog.Application.UseCases.Category;
using Catalog.Domain.Exceptions;
using Moq;
using FluentAssertions;
using Xunit;

namespace TestProject.Entity.Categorys;

[Collection(nameof(DeleteCategoryTestFixture))]
public  class DeleteCategoryTest
{
    readonly DeleteCategoryTestFixture _fixture;

    public DeleteCategoryTest(DeleteCategoryTestFixture fixture)
        => _fixture = fixture;
    
    [Fact(DisplayName = nameof(DeleteCategory))]
    [Trait("DeleteCategoryTest", "DeleteCategory - Use Cases")]
    public async Task DeleteCategory()
    {
        var categoryMock = _fixture.GetcategoryMock();
        var unityOfWorkMock = _fixture.GetunityOfWorkMock();
        var _categoryvalid = _fixture.GetValidCategory();

        categoryMock.Setup(x => x.Get(
            _categoryvalid.Id, It.IsAny<CancellationToken>())).ReturnsAsync(_categoryvalid);

        var input = new DeleteCategoryInput(_categoryvalid.Id);
        var UseCase = new DeleteCategory(
            categoryMock.Object,
            unityOfWorkMock.Object
        );
        await UseCase.Handle(input, CancellationToken.None);
        
        categoryMock.Verify(x => x.Get(
            _categoryvalid.Id, It.IsAny<CancellationToken>()), Times.Once);
        
        categoryMock.Verify(x => x.Delete(
            _categoryvalid, It.IsAny<CancellationToken>()), Times.Once);
        
        unityOfWorkMock.Verify(x => x.Commit(
            It.IsAny<CancellationToken>()), Times.Once);
        
    }
    
    [Fact(DisplayName = nameof(DeleteCategoryNotFounf))]
    [Trait("DeleteCategoryTest", "DeleteCategory - Use Cases")]
    public async Task DeleteCategoryNotFounf()
    {
        var categoryMock = _fixture.GetcategoryMock();
        var unityOfWorkMock = _fixture.GetunityOfWorkMock();
        var _guid = Guid.NewGuid();

        categoryMock.Setup(x => x.Get(
            _guid, It.IsAny<CancellationToken>())
        ).ThrowsAsync(new NotFoundException($"Category '{_guid}' not found ."));

        var input = new DeleteCategoryInput(_guid);
        var UseCase = new DeleteCategory(
            categoryMock.Object,
            unityOfWorkMock.Object
        );
        var task = async ()
            => await UseCase.Handle(input, CancellationToken.None);

        await task.Should().ThrowAsync<NotFoundException>();
        
        categoryMock.Verify(x => x.Get(
            _guid, It.IsAny<CancellationToken>()), Times.Once);
        
    }
}