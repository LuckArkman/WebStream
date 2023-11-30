using System.Collections;
using Catalog.Application.Base;
using Catalog.Data.Configurations;
using Catalog.Domain.Entitys;
using Catalog.Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Repositories
{
    public class CategoryRepositoryTestFixture : BaseFixture
    {
        public Category GetExCategory()
        {
            return new(faker.Commerce.Categories(1)[0], faker.Commerce.ProductDescription(), GetRamdomBool());
        }
        public List<Category> GetExCategoriesList(List<string> name)
            => name.Select(n =>
            {
                var category = GetValidCategory();
                category.Update(n);
                return category;
            }).ToList();
        
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

        public List<Category> GetCloneCategoryList(List<Category> _categories, string order, SearchOrder orderBy)
        {
            var listClone = new List<Category>(_categories);
            var listIEnumerable = (order, orderBy) switch
            {
                ("name", SearchOrder.Asc) => listClone.OrderBy(x => x.Name),
                ("name", SearchOrder.Desc) => listClone.OrderByDescending(x => x.Name),
                _ => listClone.OrderBy(x => x.Name)

            };
            return listIEnumerable.ToList();

        }
        
        public List<Category> GetCloneOrderCategoryList(List<Category> _categories, string orderBy, SearchOrder order)
        {
            var listClone = new List<Category>(_categories);
            var listIEnumerable = (orderBy, order) switch
            {
                ("name", SearchOrder.Asc) => listClone.OrderBy(x => x.Name),
                ("name", SearchOrder.Desc) => listClone.OrderByDescending(x => x.Name),
                _ => listClone.OrderBy(x => x.Name)

            };
            return listIEnumerable.ToList();

        }

    }
}