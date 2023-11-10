using Catalog.Application.GetCategoryTest.Categorys;
using Catalog.Application.UseCases.Category;
using Moq;
using Xunit;

namespace TestProject.Entity.Categorys;

[Collection(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTest
{
    readonly UpdateCategoryTestFixture _fixture;

    public UpdateCategoryTest(UpdateCategoryTestFixture fixture)
        => _fixture = fixture;
    
    [Fact(DisplayName = nameof(UpdateCategory))]
    [Trait("UpdateCategoryTest", "UpdateCategory - Use Cases")]
    public async Task UpdateCategory()
    {
        var categoryMock = _fixture.GetcategoryMock();
        var unityOfWorkMock = _fixture.GetunityOfWorkMock();
        var _categoryvalid = _fixture.GetTestValidCategory();

        categoryMock.Setup(x => x.Get(
            _categoryvalid.Id, It.IsAny<CancellationToken>())
        ).ReturnsAsync(_categoryvalid);

        var input = new UpdateCategoryInput(
            _categoryvalid.Id,
            _fixture.GetValidCategoryName(),
            _fixture.GetValidCategoryDescription(),
            !_categoryvalid.IsActive);
        var UseCase = new UpdateCategory(
            categoryMock.Object,
            unityOfWorkMock.Object);

        var output = await UseCase.Handle(input);

        output.Should().NotBetNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().Be(input.IsActive);
        
        categoryMock.Verify(x => x.Get(
            _categoryvalid.Id, It.IsAny<CancellationToken>()),
            Times.Once);

        categoryMock.Verify();(x => x.Update(
                _categoryvalid, It.IsAny<CancellationToken>()),
            Times.Once);
        
        unityOfWorkMock.Verify(x => x.Commit(
                It.IsAny<CancellationToken>()),
            Times.Once);
        







    }
}