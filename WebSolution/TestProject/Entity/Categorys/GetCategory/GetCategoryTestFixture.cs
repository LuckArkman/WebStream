using Catalog.Application.Interfaces;
using Catalog.Domain.Entitys;
using Catalog.Domain.Repository;
using Moq;
using TestProject.Entity.Common;
using Xunit;

namespace TestProject.Entity.GetCategory;

public class GetCategoryTestFixture : BaseFixture
{
    public Mock<ICategoryRepository> GetcategoryMock() => new ();
    public Mock<IunityOfWork> GetunityOfWorkMock() => new ();
    
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
    
    public Category GetValidCategory()
        => new (faker.Commerce.Categories(1)[0],
            faker.Commerce.ProductDescription());
}