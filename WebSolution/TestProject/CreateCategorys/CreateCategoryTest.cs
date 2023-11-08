using Catalog.Domain.Entitys;
using FluentAssertions;
using Moq;
using Xunit;
using System.Threading;
using Catalog.Application.Interfaces;
using Catalog.Application.UseCases.Category;
using Catalog.Domain.Exceptions;
using Catalog.Domain.Repository;
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
    [MemberData(nameof(GetInvalidInputs))]
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

    public static IEnumerable<object[]> GetInvalidInputs()
    {
        var _fixture = new CreateCategoryTestFixture();
        var invalidInput = new List<object[]>();
        var inputNullName = _fixture.GetInput();
        while (inputNullName.Name != null)
        {
            inputNullName.Name = null;

        }
        invalidInput.Add(new object[]
        {
            inputNullName,
            "Name should not be empty or null"
        });
        var input = _fixture.GetInput();
        while (input.Name.Length > 3)
        {
            input.Name = "12";

        }
        invalidInput.Add(new object[]
        {
            input,
            "Name should be at least 3 characters long"
        });
        var inputName = _fixture.GetInput();
        while (inputName.Name.Length < 255)
        {
            inputName.Name = $"{inputName.Name} {_fixture.faker.Commerce.ProductName()}";
        }
        invalidInput.Add(new object[]
        {
            inputName,
            "Name should be less or equal 255 characters Long"
        });
        
        var inputDesc = _fixture.GetInput();
        while (inputDesc.Description != null)
        {
            inputDesc.Description = null;
        }
        invalidInput.Add(new object[]
        {
            inputDesc,
            "Description should not be null"
        });
        
        var inputDescMax = _fixture.GetInput();
        while (inputDescMax.Description.Length < 10000)
        {
            inputDescMax.Description = $"{inputDescMax.Description} {_fixture.faker.Commerce.ProductDescription()}";
        }
        invalidInput.Add(new object[]
        {
            inputDescMax,
            "Description should be less or equal 10000 characters long"
        });
        
        return invalidInput;
    }
}