using Catalog.Application.UseCases.Category;
using Catalog.Domain.Entitys;
using Catalog.Domain.Enum;
using Catalog.Infra.Common;

namespace Catalog.Infra.Application.ListCategories;

public class ListCategoriesTestFixture
    : CategoryUseCaseBaseFixture
{
    public List<Category> GetExampleCategoriesListWithNames(
        List<string> names
    ) => names.Select(name =>
    {
        var category = GetValidCategory();
        category.Update(name);
        return category;
    }).ToList();
    public UpdateCategoryInput GetValidInput(Guid? id = null)
        => new(
            id ?? Guid.NewGuid(),
            GetValidCategoryName(),
            GetValidCategoryDescription(),
            GetRamdomBool()
        );

    public UpdateCategoryInput GetInvalidInputShortName()
    {
        var invalidInputShortName = GetValidInput();
        invalidInputShortName.Name =
            invalidInputShortName.Name.Substring(0, 2);
        return invalidInputShortName;
    }

    public UpdateCategoryInput GetInvalidInputTooLongName()
    {
        var invalidInputTooLongName = GetValidInput();
        var tooLongNameForCategory = faker.Commerce.ProductName();
        while (tooLongNameForCategory.Length <= 255)
            tooLongNameForCategory = $"{tooLongNameForCategory} {faker.Commerce.ProductName()}";
        invalidInputTooLongName.Name = tooLongNameForCategory;
        return invalidInputTooLongName;
    }
    
    public List<Category> GetExCategoryList(int length = 10)
        => Enumerable.Range(0, length).Select(_ =>GetValidCategory()).ToList();
    public List<Category> CloneCategoriesListOrdered(
        List<Category> categoriesList,
        string orderBy,
        SearchOrder order
    )
    {
        var listClone = new List<Category>(categoriesList);
        var orderedEnumerable = (orderBy.ToLower(), order) switch
        {
            ("name", SearchOrder.Asc) => listClone.OrderBy(x => x.Name)
                .ThenBy(x => x.Id),
            ("name", SearchOrder.Desc) => listClone.OrderByDescending(x => x.Name)
                .ThenByDescending(x => x.Id),
            ("id", SearchOrder.Asc) => listClone.OrderBy(x => x.Id),
            ("id", SearchOrder.Desc) => listClone.OrderByDescending(x => x.Id),
            ("createdat", SearchOrder.Asc) => listClone.OrderBy(x => x.createTime),
            ("createdat", SearchOrder.Desc) => listClone.OrderByDescending(x => x.createTime),
            _ => listClone.OrderBy(x => x.Name).ThenBy(x => x.Id),
        };
        return orderedEnumerable.ToList();
    }
}