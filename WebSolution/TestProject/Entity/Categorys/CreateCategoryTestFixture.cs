using Catalog.Application.Interfaces;
using Catalog.Application.UseCases.Category;
using Catalog.Domain.Repository;
using Moq;
using TestProject.Entity.Common;
using Xunit;

namespace TestProject.Entity.Categorys;

public class CreateCategoryTestFixture : BaseFixture
{
    public Mock<ICategoryRepository> GetcategoryMock() => new Mock<ICategoryRepository>();
    public Mock<IunityOfWork> GetunityOfWorkMock() => new Mock<IunityOfWork>();
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

    public bool GetRamdomBool() => (new Random()).NextDouble() < 0.5;

    public CreateCategoryInput GetInput() => new(Guid.Empty,
        GetValidCategoryName(),
        GetValidCategoryDescription(),
        GetRamdomBool(),
        DateTime.Now
    );
}

[CollectionDefinition(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTestFixtureCollection : ICollectionFixture<CreateCategoryTestFixture>{
    
}