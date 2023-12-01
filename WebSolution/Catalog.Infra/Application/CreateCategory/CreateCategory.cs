using Catalog.Application.Common;
using Catalog.Application.Repositories;
using Catalog.Application.UseCases.Category;
using Catalog.Domain.Entitys;

namespace Catalog.Infra.Application.CreateCategory;

public class CreateCategory: ICreateCategory
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnityOfWork _unitOfWork;

    public CreateCategory(
        ICategoryRepository categoryRepository,
        IUnityOfWork unitOfWork
    )
    {
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryModelOutput> Handle(
        CreateCategoryInput input, 
        CancellationToken cancellationToken)
    {
        var category = new Category(
            input.Name,
            input.Description,
            input.IsActive
        );

        await _categoryRepository.Insert(category, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        
        return CategoryModelOutput.FromCategory(category);
    }
}