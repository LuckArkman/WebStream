using Catalog.Application.Interfaces;
using Catalog.Domain.Repository;

namespace Catalog.Application.UseCases.Category;

public class CreateCategory : ICreateCategory
{
    public readonly IunityOfWork _unityOfWork;
    public readonly ICategoryRepository _categoryRepository;

    public CreateCategory(ICategoryRepository categoryRepository, IunityOfWork unityOfWork)
    {
        this._categoryRepository = categoryRepository;
        this._unityOfWork = unityOfWork;
    }
    
    public async Task<CreateCategoryOutput> Handle(CreateCategoryInput input, CancellationToken cancellationToken)
    {
        var category = new Domain.Entitys.Category(input.Name, input.Description, input.IsActive);
        
        await _categoryRepository.Insert(category, cancellationToken);
        await _unityOfWork.Commit(cancellationToken);
        
        return CreateCategoryOutput.FromCategory(category);
    }
}