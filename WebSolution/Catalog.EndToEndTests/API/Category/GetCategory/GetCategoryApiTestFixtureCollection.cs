using Xunit;

namespace Catalog.EndToEndTests.API.Category.GetCategory;

[CollectionDefinition(nameof(GetCategoryApiTestFixture))]
public class GetCategoryApiTestFixtureCollection
    : ICollectionFixture<GetCategoryApiTestFixture>
{ }