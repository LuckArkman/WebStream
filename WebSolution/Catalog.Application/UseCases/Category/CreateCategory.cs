using Catalog.Application.Common;
using Catalog.Application.Repositories;

namespace Catalog.Application.UseCases.Category;

public class CreateCategory : ICreateCategory
{
    public readonly IUnityOfWork _unityOfWork;
    public readonly ICategoryRepository _categoryRepository;

    public CreateCategory(ICategoryRepository categoryRepository, IUnityOfWork unityOfWork)
    {
        this._categoryRepository = categoryRepository;
        this._unityOfWork = unityOfWork;
    }
    
    public async Task<CategoryModelOutput> Handle(CreateCategoryInput input, CancellationToken cancellationToken)
    {
        var category = new Domain.Entitys.Category(input.Name, input.Description, input.IsActive);
        
        await _categoryRepository.Insert(category, cancellationToken);
        await _unityOfWork.Commit(cancellationToken);
        
        return CategoryModelOutput.FromCategory(category);
    }
}