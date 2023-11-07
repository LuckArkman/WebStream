using Catalog.Application.Interfaces;
using Catalog.Domain.Repository;
using Catalog.Domain.Entitys;

namespace Catalog.Application.UseCases.Category;

public class CreateCategory : ICreateCategory
{
    private readonly IunityOfWork _unityOfWork;
    private readonly ICategoryRepository _categoryRepository;
    public CreateCategory(ICategoryRepository categoryMock, IunityOfWork unityOfWork)
    {
        
    }
    public Task<CreateCategoryOutput> Handle(CreateCategoryInput input, CancellationToken none) => null;
}