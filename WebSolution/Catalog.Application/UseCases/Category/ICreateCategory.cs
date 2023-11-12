using Catalog.Application.Common;
using MediatR;
namespace Catalog.Application.UseCases.Category;

public interface ICreateCategory : IRequestHandler<CreateCategoryInput, CategoryModelOutput>
{
}