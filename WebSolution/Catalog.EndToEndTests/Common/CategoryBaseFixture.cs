using Catalog.Domain.Entitys;
using Catalog.EndToEndTests.Base;

namespace Catalog.EndToEndTests.Common;

public class CategoryBaseFixture
    : BaseFixture
{
    public CategoryPersistence Persistence;

    public CategoryBaseFixture()
        : base()
    {
        Persistence = new CategoryPersistence(
            CreateDbContext()
        );
    }

    public string GetValidCategoryName()
    {
        var categoryName = "";
        while (categoryName.Length < 3)
            categoryName = faker.Commerce.Categories(1)[0];
        if (categoryName.Length > 255)
            categoryName = categoryName[..255];
        return categoryName;
    }

    public string GetValidCategoryDescription()
    {
        var categoryDescription =
            faker.Commerce.ProductDescription();
        if (categoryDescription.Length > 10_000)
            categoryDescription =
                categoryDescription[..10_000];
        return categoryDescription;
    }

    public bool getRandomBoolean()
        => new Random().NextDouble() < 0.5;

    public string GetInvalidNameTooShort()
        => faker.Commerce.ProductName().Substring(0, 2);

    public string GetInvalidNameTooLong()
    {
        var tooLongNameForCategory = faker.Commerce.ProductName();
        while (tooLongNameForCategory.Length <= 255)
            tooLongNameForCategory = $"{tooLongNameForCategory} {faker.Commerce.ProductName()}";
        return tooLongNameForCategory;
    }

    public string GetInvalidDescriptionTooLong()
    {
        var tooLongDescriptionForCategory = faker.Commerce.ProductDescription();
        while (tooLongDescriptionForCategory.Length <= 10_000)
            tooLongDescriptionForCategory = $"{tooLongDescriptionForCategory} {faker.Commerce.ProductDescription()}";
        return tooLongDescriptionForCategory;
    }

    public Category GetExampleCategory()
        => new(
            GetValidCategoryName(),
            GetValidCategoryDescription(),
            getRandomBoolean()
        );

    public List<Category> GetExampleCategoriesList(int listLength = 15)
        => Enumerable.Range(1, listLength).Select(
            _ => new Category(
                GetValidCategoryName(),
                GetValidCategoryDescription(),
                getRandomBoolean()
            )
        ).ToList();

}