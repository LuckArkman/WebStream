using MediatR;

namespace Catalog.Application.GetCategoryTest.Categorys;

public class GetCategoryInput : IRequest<GetCategoryOutPut>
{

    public Guid Id { get; set; }
    public GetCategoryInput(Guid categoryId) => Id = categoryId;
}