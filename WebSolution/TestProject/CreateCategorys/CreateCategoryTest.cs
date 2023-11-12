using Catalog.Domain.Entitys;
using FluentAssertions;
using Moq;
using Xunit;
using Catalog.Application.UseCases.Category;
using Catalog.Domain.Exceptions;
using TestProject.Entity.Categorys;

namespace TestProject.CreateCategorys;

[Collection(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTest
{
    CreateCategoryTestFixture _fixture;

    public CreateCategoryTest(CreateCategoryTestFixture fixture) => _fixture = fixture;
    
    [Fact(DisplayName = nameof(CreateCategory))]
    [Trait("CreateCategoryTest", "CreateCategory - Use Cases")]
    public async void CreateCategory()
    {
        var categoryMock = _fixture.GetcategoryMock();
        var unityOfWorkMock = _fixture.GetunityOfWorkMock();
        var useCase = new CreateCategory(
            categoryMock.Object,
            unityOfWorkMock.Object
            );

        var input = _fixture.GetInput();
        var output = await useCase.Handle(input, CancellationToken.None);
        
        categoryMock.Verify(repository => repository.Insert(It.IsAny<Category>(),
            It.IsAny<CancellationToken>()),
            Times.Once());
        
        unityOfWorkMock.Verify(uow => uow.Commit(It.IsAny<CancellationToken>()),
            Times.Once());
        
        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().Be(input.IsActive);
        output.Id.Should().NotBeEmpty();
        output.createTime.Should().NotBeSameDateAs(default(DateTime));
    }
    [Theory(DisplayName = nameof(CreateCategoryThrowWhenInstantiate))]
    [Trait("CreateCategoryTest", "CreateCategory - Use Cases")]
    [MemberData(
        nameof(CreateCategoryDataGenerator.GetInvalidInputs), 
        24,
        MemberType = typeof(CreateCategoryDataGenerator)
    )]
    public async void CreateCategoryThrowWhenInstantiate(CreateCategoryInput input, string exception)
    {
        var categoryMock = _fixture.GetcategoryMock();
        var unityOfWorkMock = _fixture.GetunityOfWorkMock();

        var useCase = new CreateCategory(
            _fixture.GetcategoryMock().Object,
            _fixture.GetunityOfWorkMock().Object
        );
        Func<Task> action = async () => await useCase.Handle(input, CancellationToken.None);
        await action.Should().ThrowAsync<EntityValidationException>().WithMessage(exception);
    }
    
    [Fact(DisplayName = nameof(CreateCategoryOnlyName))]
    [Trait("CreateCategoryTest", "CreateCategory - Use Cases")]
    public async void CreateCategoryOnlyName()
    {
        var categoryMock = _fixture.GetcategoryMock();
        var unityOfWorkMock = _fixture.GetunityOfWorkMock();

        var useCase = new CreateCategory(
            categoryMock.Object,
            unityOfWorkMock.Object
        );
        
        var input = new CreateCategoryInput(
            null,
            _fixture.GetValidCategoryName(),
            "",
            true,
            null
            
        );
        
        var output = await useCase.Handle(input, CancellationToken.None);
        
        categoryMock.Verify(repository => repository.Insert(It.IsAny<Category>(),
                It.IsAny<CancellationToken>()),
            Times.Once());
        
        unityOfWorkMock.Verify(uow => uow.Commit(It.IsAny<CancellationToken>()),
            Times.Once());
        
        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be("");
        output.IsActive.Should().Be(input.IsActive);
        output.Id.Should().NotBeEmpty();
        output.createTime.Should().NotBeSameDateAs(default(DateTime));
    }
    
    [Fact(DisplayName = nameof(CreateCategoryOnlyNameAndDescription))]
    [Trait("CreateCategoryTest", "CreateCategory - Use Cases")]
    public async void CreateCategoryOnlyNameAndDescription()
    {
        var categoryMock = _fixture.GetcategoryMock();
        var unityOfWorkMock = _fixture.GetunityOfWorkMock();

        var useCase = new CreateCategory(
            categoryMock.Object,
            unityOfWorkMock.Object
        );
        
        var input = new CreateCategoryInput(
            null,
            _fixture.GetValidCategoryName(),
            _fixture.GetValidCategoryDescription(),
            true,
            null
            
        );
        
        var output = await useCase.Handle(input, CancellationToken.None);
        
        categoryMock.Verify(repository => repository.Insert(It.IsAny<Category>(),
                It.IsAny<CancellationToken>()),
            Times.Once());
        
        unityOfWorkMock.Verify(uow => uow.Commit(It.IsAny<CancellationToken>()),
            Times.Once());
        
        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().Be(input.IsActive);
        output.Id.Should().NotBeEmpty();
        output.createTime.Should().NotBeSameDateAs(default(DateTime));
    }
}