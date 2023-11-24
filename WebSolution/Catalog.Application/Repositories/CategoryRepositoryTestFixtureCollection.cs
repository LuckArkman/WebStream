using Xunit;

namespace Catalog.Application.Repositories
{
    [CollectionDefinition(nameof(CategoryRepositoryTestFixture))]
    public class CategoryRepositoryTestFixtureCollection : ICollectionFixture<CategoryRepositoryTestFixture>{}
}