using Catalog.Application.Common;
using Catalog.Application.Interfaces;
using Catalog.Data.Configurations;
using Catalog.Domain.Repository;

namespace Catalog.Application.UseCases.Category;

public class CreateCategory : ICreateCategory
{
    public readonly IunityOfWork _unityOfWork;
    public readonly ICategoryRepository _categoryRepository;
    private readonly CatalogDbContext _catalogDb;

    public CreateCategory(ICategoryRepository categoryRepository, IunityOfWork unityOfWork)
    {
        this._categoryRepository = categoryRepository;
        this._unityOfWork = unityOfWork;
    }
    
    public CreateCategory(CatalogDbContext context, IunityOfWork unityOfWork)
    {
        this._catalogDb = context;
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