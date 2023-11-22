using Catalog.Application.Common;
using Catalog.Application.UseCases.Category;
using UseCase = Catalog.Application.UseCases.Category;
using Catalog.Domain.Entitys;
using Catalog.Domain.Exceptions;
using Catalog.Infra.Base;
using Catalog.Infra.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Catalog.Infra.Application.UpdateCategory;

[Collection(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTest
{
    private readonly UpdateCategoryTestFixture _fixture;

    public UpdateCategoryTest(UpdateCategoryTestFixture fixture)
        => _fixture = fixture;

    [Theory(DisplayName = nameof(UpdateCategory))]
    [Trait("UpdateCategoryTest", "UpdateCategoryTest - Infra")]
    [MemberData(
        nameof(UpdateCategoryTestDataGenerator.GetCategoriesToUpdate),
        parameters: 10,
        MemberType = typeof(UpdateCategoryTestDataGenerator)
    )]
    public async Task UpdateCategory(
        Category exampleCategory,
        UpdateCategoryInput input
    )
    {
        
        var _Id = Guid.NewGuid().ToString();
        var _categoryDB = _fixture.CreateDBContext(false, _Id);
        await _categoryDB.AddRangeAsync(exampleCategory);
        _categoryDB.SaveChanges();
        var _categoryRepository = new CategoryRepository(_categoryDB);
        var _unityOfWord = new UnityOfWork(_categoryDB);
        var repositoryMock = _fixture.GetcategoryMock();
        var unitOfWorkMock = _fixture.GetunityOfWorkMock();
        var useCase = new UpdateCategory(
            _categoryRepository,
            unitOfWorkMock.Object
        );

        var output = await useCase.Handle(input, CancellationToken.None);

        var dbCategory = await _categoryDB.Categories.FindAsync(output.Id);
        dbCategory.Should().NotBeNull();
        dbCategory!.Name.Should().Be(input.Name);
        dbCategory.Description.Should().Be(input.Description);
        dbCategory.IsActive.Should().Be((bool)input.IsActive!);
        dbCategory.createTime.Should().Be(output.createTime);
        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().Be((bool)input.IsActive!);
    }

    [Theory(DisplayName = nameof(UpdateCategoryWithoutIsActive))]
    [Trait("UpdateCategoryTest", "UpdateCategoryTest - Infra")]
    [MemberData(
        nameof(UpdateCategoryTestDataGenerator.GetCategoriesToUpdate),
        parameters: 1,
        MemberType = typeof(UpdateCategoryTestDataGenerator)
    )]
    public async Task UpdateCategoryWithoutIsActive(
        Category exampleCategory,
        UpdateCategoryInput exampleInput
    )
    {
        var input = new UpdateCategoryInput(
            exampleCategory.Id,
            exampleCategory.Name,
            exampleCategory.Description,
            exampleCategory.IsActive,
            exampleCategory.createTime
        );
        var _dbContext = _fixture.CreateDBContext();
        _dbContext.Categories.AddRange(exampleCategory);
        _dbContext.SaveChanges();
        var repository = new CategoryRepository(_dbContext);
        var unitOfWork = new UnityOfWork(_dbContext);
        var useCase = new UpdateCategory(repository, unitOfWork);
        
        var output = async ()
        =>await useCase.Handle(input, CancellationToken.None);
        var dbCategory = await _dbContext.Categories.FindAsync(input.Id);
        dbCategory.Should().NotBeNull();
        dbCategory!.Name.Should().Be(input.Name);
        dbCategory.Description.Should().Be(input.Description);
        dbCategory.IsActive.Should().Be(exampleCategory.IsActive);
        dbCategory.createTime.Should().Be(input.createTime);
        output.Should().NotBeNull();
        exampleCategory.Name.Should().Be(input.Name);
        exampleCategory.Description.Should().Be(input.Description);
        exampleCategory.IsActive.Should().Be((bool)input.IsActive!);
    }


    [Theory(DisplayName = nameof(UpdateCategoryOnlyName))]
    [Trait("UpdateCategoryTest", "UpdateCategoryTest - Infra")]
    [MemberData(
        nameof(UpdateCategoryTestDataGenerator.GetCategoriesToUpdate),
        parameters: 10,
        MemberType = typeof(UpdateCategoryTestDataGenerator)
    )]
    public async Task UpdateCategoryOnlyName(
        Category exampleCategory,
        UpdateCategoryInput exampleInput
    )
    {
        var _Id = Guid.NewGuid().ToString();
        var _categoryDB = _fixture.CreateDBContext(false, _Id);
        var _categoryRepository = new CategoryRepository(_categoryDB);
        var _unityOfWord = new UnityOfWork(_categoryDB);
        var input = new UpdateCategoryInput(
            exampleInput.Id,
            exampleInput.Name
        );
        var repositoryMock = _fixture.GetcategoryMock();
        var unitOfWorkMock = _fixture.GetunityOfWorkMock();
        repositoryMock.Setup(x => x.Get(
            exampleCategory.Id,
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(exampleCategory);
        var useCase = new UpdateCategory(
            _categoryRepository,
            unitOfWorkMock.Object
        );

        CategoryModelOutput output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(exampleCategory.Description);
        output.IsActive.Should().Be(exampleCategory.IsActive);
        repositoryMock.Verify(x => x.Get(
            exampleCategory.Id,
            It.IsAny<CancellationToken>())
        , Times.Once);
        repositoryMock.Verify(x => x.Update(
            exampleCategory,
            It.IsAny<CancellationToken>())
        , Times.Once);
        unitOfWorkMock.Verify(x => x.Commit(
            It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Fact(DisplayName = nameof(ThrowWhenCategoryNotFound))]
    [Trait("UpdateCategoryTest", "UpdateCategoryTest - Infra")]
    public async Task ThrowWhenCategoryNotFound()
    {
        var _Id = Guid.NewGuid().ToString();
        var _categoryDB = _fixture.CreateDBContext(false, _Id);
        var _categoryRepository = new CategoryRepository(_categoryDB);
        var _unityOfWord = new UnityOfWork(_categoryDB);
        var repositoryMock = _fixture.GetcategoryMock();
        var unitOfWorkMock = _fixture.GetunityOfWorkMock();
        var input = _fixture.GetValidInput();
        repositoryMock.Setup(x => x.Get(
            input.Id,
            It.IsAny<CancellationToken>())
        ).ThrowsAsync(new NotFoundException($"Category '{input.Id}' not found"));
        var useCase = new UpdateCategory(
            _categoryRepository,
            unitOfWorkMock.Object
        );

        var task = async ()
            => await useCase.Handle(input, CancellationToken.None);

        await task.Should().ThrowAsync<NotFoundException>();
    }

    [Fact(DisplayName = nameof(UpdateThrowsWhenNotFoundCategory))]
    [Trait("UpdateCategoryTest", "UpdateCategoryTest - Infra")]
    public async Task UpdateThrowsWhenNotFoundCategory()
    {
        var input = _fixture.GetValidInput();
        var dbContext = _fixture.CreateDBContext(false,Guid.NewGuid().ToString());
        await dbContext.AddRangeAsync(_fixture.GetExCategoryList());
        dbContext.SaveChanges();
        var repository = new CategoryRepository(dbContext);
        var unitOfWork = new UnityOfWork(dbContext);
        var useCase = new UpdateCategory(repository, unitOfWork);

        var task = async () 
            => await useCase.Handle(input, CancellationToken.None);

        await task.Should().ThrowAsync<NotFoundException>().WithMessage($"Category '{input.Id}' not found.");
    }

    [Theory(DisplayName = nameof(UpdateThrowsWhenCantInstantiateCategory))]
    [Trait("UpdateCategoryTest", "UpdateCategoryTest - Infra")]
    [MemberData(
        nameof(UpdateCategoryTestDataGenerator.GetInvalidInputs),
        parameters: 6,
        MemberType = typeof(UpdateCategoryTestDataGenerator)
    )]
    public async Task UpdateThrowsWhenCantInstantiateCategory(
        UpdateCategoryInput input,
        string expectedExceptionMessage
    )
    {
        var dbContext = _fixture.CreateDBContext(false, Guid.NewGuid().ToString());
        var exampleCategories = _fixture.GetExCategoryList();
        await dbContext.AddRangeAsync(exampleCategories);
        dbContext.SaveChanges();
        var repository = new CategoryRepository(dbContext);
        var unitOfWork = new UnityOfWork(dbContext);
        var useCase = new UpdateCategory(repository, unitOfWork);
        input.Id = exampleCategories[0].Id;

        var task = async () 
            => await useCase.Handle(input, CancellationToken.None);
        
        await task.Should().ThrowAsync<EntityValidationException>()
            .WithMessage(expectedExceptionMessage);
    }
}