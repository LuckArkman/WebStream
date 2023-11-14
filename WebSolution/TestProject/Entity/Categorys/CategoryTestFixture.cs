using Catalog.Domain.Entitys;
using TestProject.Commom;
using TestProject.Entity.Categorys;
using TestProject.Entity.Common;
using Xunit;

namespace TestProject.Entity.Categorys
{
    public class CategoryTestFixture : CategoryUseCaseBaseFixture{}
}
[CollectionDefinition(nameof(CategoryTestFixture))]
public class CategoryTestFixtureCollection : ICollectionFixture<CategoryTestFixture>{
    
}