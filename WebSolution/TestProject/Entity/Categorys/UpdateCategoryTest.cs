using Catalog.Application.GetCategoryTest.Categorys;
using Catalog.Application.UseCases.Category;
using Catalog.Domain.Entitys;
using Catalog.Domain.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace TestProject.Entity.Categorys;

[Collection(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTest
{
    readonly UpdateCategoryTestFixture _fixture;

    public UpdateCategoryTest(UpdateCategoryTestFixture fixture)
        => _fixture = fixture;
    
    [Theory(DisplayName = nameof(UpdateCategory))]
    [Trait("UpdateCategoryTest", "UpdateCategory - Use Cases")]
    [MemberData(nameof(UpdateCategoryDataGenerator.GetCategoriesUpdate),
        10,
        MemberType = typeof(UpdateCategoryDataGenerator)
    )]
    public async Task UpdateCategory(Category _categoryvalid, UpdateCategoryInput _input)
    {
        var categoryMock = _fixture.GetcategoryMock();
        var unityOfWorkMock = _fixture.GetunityOfWorkMock();
        var input = new UpdateCategoryInput(
            _input.Id,
            _input.Name,
            _input.Description,
            _input.IsActive,
            _input.createTime
        );
        
        categoryMock.Setup(x => x.Get(
            _categoryvalid.Id, It.IsAny<CancellationToken>())
        ).ReturnsAsync(_categoryvalid);
        var UseCase = new UpdateCategory(
            categoryMock.Object,
            unityOfWorkMock.Object);

        var output = await UseCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(_categoryvalid.Description);
        
        categoryMock.Verify(x => x.Get(
                _categoryvalid.Id, It.IsAny<CancellationToken>()),
            Times.Once);

        categoryMock.Verify(x => x.Update(
                _categoryvalid, It.IsAny<CancellationToken>()),
            Times.Once);
        
        unityOfWorkMock.Verify(x => x.Commit(
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
    
    [Fact(DisplayName = nameof(UpdateCategoryNotFound))]
    [Trait("UpdateCategoryTest", "UpdateCategory - Use Cases")]
    public async Task UpdateCategoryNotFound()
    {
        var categoryMock = _fixture.GetcategoryMock();
        var unityOfWorkMock = _fixture.GetunityOfWorkMock();
        var _guid = Guid.NewGuid();
        var input = _fixture.GetValidInput(Guid.NewGuid());
        
        categoryMock.Setup(x => x.Get(
            input.Id, It.IsAny<CancellationToken>())
        ).ThrowsAsync(new NotFoundException($"Category{input.Id} not found ."));
        var UseCase = new UpdateCategory(
            categoryMock.Object,
            unityOfWorkMock.Object);

        var task = async () 
            => await UseCase.Handle(input, CancellationToken.None);

        task.Should().ThrowAsync<NotFoundException>();
        
        categoryMock.Verify(x => x.Get(
                input.Id, It.IsAny<CancellationToken>()),
            Times.Once);
    }
    
    
}