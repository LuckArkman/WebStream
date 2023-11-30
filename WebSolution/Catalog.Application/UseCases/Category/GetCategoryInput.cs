using Catalog.Application.Common;
using MediatR;

namespace Catalog.Application.UseCases.Category;

public class GetCategoryInput : IRequest<CategoryModelOutput>
{

    public Guid Id { get; set; }
    public GetCategoryInput(Guid categoryId) => Id = categoryId;
}