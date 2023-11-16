using System.Collections;
using Catalog.Data.Configurations;
using Catalog.Domain.Entitys;
using Catalog.Infra.Base;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infra.Repositories
{
    public class CategoryRepositoryTestFixture : BaseFixture
    {
        public CatalogDbContext CreateDBContext(bool value = false)
        {
            
            var context = new CatalogDbContext(new DbContextOptionsBuilder<CatalogDbContext>()
                .UseInMemoryDatabase("Integration-tests-db")
                .Options);
            if (!value) context.Database.EnsureDeleted();
            return context;
        }
        

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

        public List<Category> GetExCategoryList(int length = 10)
            => Enumerable.Range(0, length).Select(_ =>GetValidCategory()).ToList();

        public List<Category> GetExCategoriesList(List<string> name)
            => name.Select(n =>
            {
                var category = GetValidCategory();
                category.Update(n);
                return category;
            }).ToList();

        public void CleanInMemoryDatabase()
        => CreateDBContext().Database.EnsureDeleted();
    }
}