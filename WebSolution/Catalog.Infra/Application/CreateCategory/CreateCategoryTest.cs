using Catalog.Application.UseCases.Category;
using Catalog.Domain.Entitys;
using UseCases = Catalog.Application.UseCases.Category;
using Catalog.Domain.Exceptions;
using Catalog.Infra.Base;
using Catalog.Infra.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace Catalog.Infra.Application.CreateCategory;

[Collection(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTest
{
    private readonly CreateCategoryTestFixture _fixture;

    public CreateCategoryTest(CreateCategoryTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(CreateCategory))]
    [Trait("CreateCategoryTest", "CreateCategoryTest - Infra")]
    public async void CreateCategory()
    {
        var _Id = Guid.NewGuid().ToString();
        var _categoryDB = _fixture.CreateDBContext(false, _Id);
        var _categoryRepository = new CategoryRepository(_categoryDB);
        var _unityOfWord = new UnityOfWork(_categoryDB);
        var useCase = new CreateCategory(
            _categoryRepository,
            _unityOfWord
        );
        var input = _fixture.GetInput();

        var output = await useCase.Handle(input, CancellationToken.None);
        
        var _dbCategory = await _categoryDB.Categories.FindAsync(output.Id);
        _dbCategory.Should().NotBeNull();

        _dbCategory.Name.Should().Be(output.Name);
        _dbCategory.Description.Should().Be(output.Description);
        _dbCategory.IsActive.Should().Be(output.IsActive);
        _dbCategory.createTime.Should().Be(output.createTime);
        
        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().Be(input.IsActive);
        output.Id.Should().NotBeEmpty();
        output.createTime.Should().NotBeSameDateAs(default);
    }

    [Fact(DisplayName = nameof(CreateCategoryWithOnlyName))]
    [Trait("CreateCategoryTest", "CreateCategoryTest - Infra")]
    public async void CreateCategoryWithOnlyName()
    {
        var _Id = Guid.NewGuid().ToString();
        var _categoryDB = _fixture.CreateDBContext(false, _Id);
        var _unityOfWord = new UnityOfWork(_categoryDB);
        var _category = _fixture.GetValidCategory();
        _categoryDB.Categories.Add(_category);
        _categoryDB.SaveChanges();
        var _categoryRepository = new CategoryRepository(_categoryDB);
        var repositoryMock = _fixture.GetcategoryMock();
        var unitOfWorkMock = _fixture.GetunityOfWorkMock();
        var useCase = new CreateCategory(
            _categoryRepository, unitOfWorkMock.Object
        );
        var input = new CreateCategoryInput(
            Guid.NewGuid(),
            _fixture.GetValidCategoryName()
        );

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be("");
        output.IsActive.Should().BeTrue();
        output.Id.Should().NotBeEmpty();
        output.createTime.Should().NotBeSameDateAs(default);
    }

    [Fact(DisplayName = nameof(CreateCategoryWithOnlyNameAndDescription))]
    [Trait("CreateCategoryTest", "CreateCategoryTest - Infra")]
    public async void CreateCategoryWithOnlyNameAndDescription()
    {
        var _Id = Guid.NewGuid().ToString();
        var _categoryDB = _fixture.CreateDBContext(false, _Id);
        var _unityOfWord = new UnityOfWork(_categoryDB);
        var _category = _fixture.GetValidCategory();
        _categoryDB.Categories.Add(_category);
        _categoryDB.SaveChanges();
        
        var _categoryRepository = new CategoryRepository(_categoryDB);
        var repositoryMock = _fixture.GetcategoryMock();
        var unitOfWorkMock = _fixture.GetunityOfWorkMock();
        var useCase = new CreateCategory(
            _categoryRepository, unitOfWorkMock.Object
        );
        var input = new CreateCategoryInput(
            Guid.NewGuid(),
            _fixture.GetValidCategoryName(),
            _fixture.GetValidCategoryDescription()
        );

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().BeTrue();
        output.Id.Should().NotBeEmpty();
        output.createTime.Should().NotBeSameDateAs(default);
    }

    [Theory(DisplayName = nameof(ThrowWhenCantInstantiateCategory))]
    [Trait("CreateCategoryTest", "CreateCategoryTest - Infra")]
    [MemberData(
        nameof(CreateCategoryTestDataGenerator.GetInvalidInputs),
        parameters: 24,
        MemberType = typeof(CreateCategoryTestDataGenerator)
    )]
    public async void ThrowWhenCantInstantiateCategory(
        CreateCategoryInput input,
        string exceptionMessage
    )
    {
        var _Id = Guid.NewGuid().ToString();
        var _categoryDB = _fixture.CreateDBContext(false, _Id);
        var _categoryRepository = new CategoryRepository(_categoryDB);
        var _unityOfWord = new UnityOfWork(_categoryDB);
        var useCase = new CreateCategory(
            _categoryRepository,
            _unityOfWord
        );

        Func<Task> task = async () => await useCase.Handle(input, CancellationToken.None);

        await task.Should()
            .ThrowAsync<EntityValidationException>()
            .WithMessage(exceptionMessage);
    }
}