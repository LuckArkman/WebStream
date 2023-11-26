using Catalog.Application.Common;
using MediatR;

namespace Catalog.Application.GetCategoryTest.Categorys;

public class GetCategoryInput : IRequest<CategoryModelOutput>
{

    public Guid Id { get; set; }
    public GetCategoryInput(Guid categoryId) => Id = categoryId;
}