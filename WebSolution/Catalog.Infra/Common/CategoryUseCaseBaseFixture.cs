using Catalog.Application.Interfaces;
using Catalog.Domain.Entitys;
using Catalog.Domain.Repository;
using Catalog.Infra.Base;
using Moq;

namespace Catalog.Infra.Common;

public class CategoryUseCaseBaseFixture : BaseFixture
{
    public Mock<ICategoryRepository> GetcategoryMock() => new ();
    public Mock<IunityOfWork> GetunityOfWorkMock() => new ();
    
    public bool GetRamdomBool() => (new Random()).NextDouble() < 0.5;
    
    public Category GetValidCategory() => new (GetValidCategoryName(),GetValidCategoryDescription(), GetRamdomBool());
        
    public Category GetTestValidCategory() => new (GetValidCategoryName(),GetValidCategoryDescription(), GetRamdomBool());

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