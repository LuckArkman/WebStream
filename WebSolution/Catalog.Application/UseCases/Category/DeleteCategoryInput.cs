using MediatR;

namespace Catalog.Application.UseCases.Category;

public class DeleteCategoryInput : IRequest
{
    public Guid Id;

    public DeleteCategoryInput(Guid id) => Id = id;
}