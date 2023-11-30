using Catalog.Application.Base;
using Catalog.Domain.Entitys;

namespace Catalog.Application.Repositories;

public class UnityOfWorkTestFixture : BaseFixture
{
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
        => Enumerable.Range(1, length).Select(_ =>GetValidCategory()).ToList();

}