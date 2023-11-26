using Catalog.Application.UseCases.Category;
using MediatR;

namespace Catalog.Application.Interfaces;

public interface IDeleteCategory : IRequestHandler<DeleteCategoryInput>
{
    
}