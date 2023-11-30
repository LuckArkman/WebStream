using Catalog.Application.Common;
using Catalog.Application.GetCategoryTest.Categorys;
using MediatR;

namespace Catalog.Application.UseCases.Category;

public interface IGetCategory : IRequestHandler<GetCategoryInput, CategoryModelOutput>
{
    
}