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
    
    public CreateCategoryInput GetInvalidNullName()
    {
        var input = GetInput();
        while (input.Name != null)
        {
            input.Name = null;

        }
        return input;
    }

    public CreateCategoryInput GetInvalidName()
    {
        var input = GetInput();
        while (input.Name.Length > 3)
        {
            input.Name = "12";

        }
        return input;
    }
    
    public CreateCategoryInput GetInvalidLongName()
    {
        var input = GetInput();
        while (input.Name.Length < 255)
        {
            input.Name = $"{input.Name} {faker.Commerce.ProductName()}";
        }
        return input;
    }
    public CreateCategoryInput GetInvalidNullDesc()
    {
        var input = GetInput();
        while (input.Description != null)
        {
            input.Description = null;
        }
        return input;
    }
    public CreateCategoryInput GetInvalidLongDesc()
    {
        var input = GetInput();
        while (input.Description.Length < 10000)
        {
            input.Description = $"{input.Description} {faker.Commerce.ProductDescription()}";
        }
        return input;
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