using Xunit;

namespace TestProject.Entity.Categorys
{
    [CollectionDefinition(nameof(ListCategoryTestFixture))]
    public class ListCategoryTestFixtureCollection: ICollectionFixture<ListCategoryTestFixture>{}
}