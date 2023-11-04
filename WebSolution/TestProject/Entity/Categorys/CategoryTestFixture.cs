using Catalog.Domain.Entitys;
using TestProject.Entity.Categorys;
using Xunit;

namespace TestProject.Entity.Categorys
{
    public class CategoryTestFixture
    {
        public Category GetValidCategory() => new ("category name","category Description");
    }
}
[CollectionDefinition(nameof(CategoryTestFixture))]
public class CategoryTestFixtureCollection : ICollectionFixture<CategoryTestFixture>{
    
}