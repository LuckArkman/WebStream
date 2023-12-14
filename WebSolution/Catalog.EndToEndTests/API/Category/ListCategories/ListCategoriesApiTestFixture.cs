using Catalog.Domain.Enum;
using Catalog.EndToEndTests.Common;

namespace Catalog.EndToEndTests.API.Category.ListCategories;

public class ListCategoriesApiTestFixture
    : CategoryBaseFixture
{
    public List<Domain.Entitys.Category> GetExampleCategoriesListWithNames(
        List<string> names
    ) => names.Select(name =>
    {
        var category = GetExampleCategory();
        category.Update(name);
        return category;
    }).ToList();

    public List<Domain.Entitys.Category> CloneCategoriesListOrdered(
        List<Domain.Entitys.Category> categoriesList,
        string orderBy,
        SearchOrder order
    )
    {
        var listClone = new List<Domain.Entitys.Category>(categoriesList);
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