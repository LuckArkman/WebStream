using Catalog.Application.Common;
using Catalog.Application.UseCases.Category;
using MediatR;

namespace Catalog.Application.Interfaces;

public interface IUpdateCategory : IRequestHandler<UpdateCategoryInput, CategoryModelOutput>
{
    
}