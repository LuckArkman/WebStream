using Catalog.Application.Repositories;
using Catalog.Application.UseCases.Category;
using UseCase = Catalog.Application.UseCases.Category;
using Catalog.Domain.Exceptions;
using Catalog.Infra.Base;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Catalog.Infra.Application.DeleteCategory;

[Collection(nameof(DeleteCategoryTestFixture))]
public class DeleteCategoryTest : IDisposable
{
    private readonly DeleteCategoryTestFixture _fixture;

    public DeleteCategoryTest(DeleteCategoryTestFixture fixture) 
        => _fixture = fixture;

    [Fact(DisplayName = nameof(DeleteCategory))]
    [Trait("DeleteCategoryTestFixture", "DeleteCategoryTestFixture - Infra")]
    public async Task DeleteCategory()
    {
        var dbContext = _fixture.CreateDBContext(true, Guid.NewGuid().ToString());
        var categoryExample = _fixture.GetValidCategory();
        var exampleList = _fixture.GetExCategoryList(10);
        await dbContext.AddRangeAsync(exampleList, CancellationToken.None);
        await dbContext.AddAsync(categoryExample);
        var repository = new CategoryRepository(dbContext);
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var unitOfWork = new UnityOfWork(dbContext);
        var useCase = new DeleteCategory(
            repository, unitOfWork
        );
        var input = new DeleteCategoryInput(categoryExample.Id);

        var ouput = useCase.Handle(input, CancellationToken.None);
        
        ouput.Should().NotBeNull();
    }

    [Fact(DisplayName = nameof(DeleteCategoryThrowsWhenNotFound))]
    [Trait("DeleteCategoryTestFixture", "DeleteCategoryTestFixture - Infra")]
    public async Task DeleteCategoryThrowsWhenNotFound()
    {
        var dbContext = _fixture.CreateDBContext(false, Guid.NewGuid().ToString());
        var exampleList = _fixture.GetExCategoryList(10);
        await dbContext.AddRangeAsync(exampleList);
        await dbContext.SaveChangesAsync();
        var repository = new CategoryRepository(dbContext);
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var unitOfWork = new UnityOfWork(dbContext);
        var useCase = new DeleteCategory(
            repository, unitOfWork
        );
        var input = new DeleteCategoryInput(Guid.NewGuid());

        var task = async () 
            => await useCase.Handle(input, CancellationToken.None);

        await task.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Category '{input.Id}' not found.");

    }

    public void Dispose()
    {
        _fixture.CleanInMemoryDatabase();
    }
}