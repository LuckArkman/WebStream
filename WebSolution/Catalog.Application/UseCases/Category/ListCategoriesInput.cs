using Catalog.Domain.Enum;
using MediatR;

namespace Catalog.Application.UseCases.Category
{
    public class ListCategoriesInput : PaginatedListInput, IRequest<ListCategoriesOutput>
{
    public ListCategoriesInput(
        int page = 1,
        int perPage = 15,
        string search = "",
        string sort = "",
        SearchOrder dir = SearchOrder.Asc
    ) : base(page, perPage, search, sort, dir)
    { }
    
    public ListCategoriesInput() 
        : base(1, 15, "", "", SearchOrder.Asc)
    { }
}
}