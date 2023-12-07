using Xunit;

namespace Catalog.EndToEndTests.API.Category.DeleteCategory;

[CollectionDefinition(nameof(DeleteCategoryApiTestFixture))]
public class DeleteCategoryApiTestFixtureCollection
    : ICollectionFixture<DeleteCategoryApiTestFixture>
{ }