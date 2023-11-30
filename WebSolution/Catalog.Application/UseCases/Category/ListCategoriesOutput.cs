using Catalog.Application.Common;

namespace Catalog.Application.UseCases.Category
{
    public class ListCategoriesOutput
        : PaginatedListOutput<CategoryModelOutput>
    {
        public ListCategoriesOutput(
            int page, 
            int perPage, 
            int total, 
            IReadOnlyList<CategoryModelOutput> items) 
            : base(page, perPage, total, items)
        {
        }
    }
}