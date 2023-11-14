using Catalog.Data.Configurations;
using Catalog.Domain.Entitys;
using Catalog.Infra.Base;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infra.Repositories
{
    public class CategoryRepositoryTestFixture : BaseFixture
    {
        public CatalogDbContext CreateDBContext()
        => new (new DbContextOptionsBuilder<CatalogDbContext>()
        .UseInMemoryDatabase("Integration-tests-db")
        .Options);

        public Category GetExCategory()
        {
            return new(faker.Commerce.Categories(1)[0], faker.Commerce.ProductDescription(), GetRamdomBool());
        }
        
        public bool GetRamdomBool() => (new Random()).NextDouble() < 0.5;
    
        public Category GetValidCategory() => new (faker.Commerce.Categories(1)[0],faker.Commerce.ProductDescription());
        
        public Category GetTestValidCategory() => new (faker.Commerce.Categories(1)[0],faker.Commerce.ProductDescription(), GetRamdomBool());

        public string GetValidCategoryName(){
            var categoryName = "";
            while(categoryName.Length < 3)
            {
                categoryName = faker.Commerce.Categories(1)[0];
            }
            if (categoryName.Length > 255)
            {
                categoryName = categoryName[..255];
            }
            return categoryName;
        }

        public string GetValidCategoryDescription(){
            var categoryDescription = faker.Commerce.ProductDescription();
            if(categoryDescription.Length > 10000)
            {
                categoryDescription = categoryDescription[..10000];
            }
            return categoryDescription;
        }
    }
}