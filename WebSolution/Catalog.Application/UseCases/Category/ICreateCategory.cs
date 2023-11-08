using MediatR;

namespace Catalog.Application.UseCases.Category;

public interface ICreateCategory : IRequestHandler<CreateCategoryInput, CreateCategoryOutput>
{
    public Task<CreateCategoryOutput> Handle(CreateCategoryInput input, CancellationToken none);
}