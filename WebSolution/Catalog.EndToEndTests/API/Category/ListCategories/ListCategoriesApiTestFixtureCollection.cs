using Xunit;

namespace Catalog.EndToEndTests.API.Category.ListCategories;

[CollectionDefinition(nameof(ListCategoriesApiTestFixture))]
public class ListCategoriesApiTestFixtureCollection
    : ICollectionFixture<ListCategoriesApiTestFixture>
{ }