using Catalog.Application.Common;
using Catalog.Application.Interfaces;
using Catalog.Application.UseCases.Category;
using Catalog.Domain.Entitys;
using Catalog.Domain.Repository;

namespace Catalog.Infra.Application.CreateCategory;

public class CreateCategory: ICreateCategory
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IunityOfWork _unitOfWork;

    public CreateCategory(
        ICategoryRepository categoryRepository,
        IunityOfWork unitOfWork
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