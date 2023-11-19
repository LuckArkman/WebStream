using Catalog.Application.UseCases.Category;
using Catalog.Domain.Entitys;
using UseCases = Catalog.Application.UseCases.Category;
using Catalog.Domain.Exceptions;
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
        var repositoryMock = _fixture.GetcategoryMock();
        var unitOfWorkMock = _fixture.GetunityOfWorkMock();
        var useCase = new UseCases.CreateCategory(
            repositoryMock.Object, unitOfWorkMock.Object
        );
        var input = _fixture.GetInput();

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(
            repository => repository.Insert(
                It.IsAny<Category>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );
        unitOfWorkMock.Verify(
            uow => uow.Commit(It.IsAny<CancellationToken>()),
            Times.Once
        );
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
        var repositoryMock = _fixture.GetcategoryMock();
        var unitOfWorkMock = _fixture.GetunityOfWorkMock();
        var useCase = new UseCases.CreateCategory(
            repositoryMock.Object, unitOfWorkMock.Object
        );
        var input = new CreateCategoryInput(
            Guid.NewGuid(),
            _fixture.GetValidCategoryName()
        );

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(
            repository => repository.Insert(
                It.IsAny<Category>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );
        unitOfWorkMock.Verify(
            uow => uow.Commit(It.IsAny<CancellationToken>()),
            Times.Once
        );
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
        var repositoryMock = _fixture.GetcategoryMock();
        var unitOfWorkMock = _fixture.GetunityOfWorkMock();
        var useCase = new UseCases.CreateCategory(
            repositoryMock.Object, unitOfWorkMock.Object
        );
        var input = new CreateCategoryInput(
            Guid.NewGuid(),
            _fixture.GetValidCategoryName(),
            _fixture.GetValidCategoryDescription()
        );

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(
            repository => repository.Insert(
                It.IsAny<Category>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );
        unitOfWorkMock.Verify(
            uow => uow.Commit(It.IsAny<CancellationToken>()),
            Times.Once
        );
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
        var useCase = new UseCases.CreateCategory(
            _fixture.GetcategoryMock().Object,
            _fixture.GetunityOfWorkMock().Object
        );

        Func<Task> task = async () => await useCase.Handle(input, CancellationToken.None);

        await task.Should()
            .ThrowAsync<EntityValidationException>()
            .WithMessage(exceptionMessage);
    }
}