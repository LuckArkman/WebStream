using Catalog.Application.Common;
using Catalog.Application.Interfaces;
using Catalog.Application.UseCases.Category;
using Catalog.Domain.Repository;

namespace Catalog.Infra.Application.UpdateCategory;

public class UpdateCategory : IUpdateCategory
{
    readonly ICategoryRepository _categoryRepository;
    readonly IunityOfWork _uinitOfWork;

    public UpdateCategory(ICategoryRepository categoryRepository, IunityOfWork uinitOfWork)
        => (_categoryRepository, _uinitOfWork) = (categoryRepository, uinitOfWork);

    public async Task<CategoryModelOutput> Handle(UpdateCategoryInput request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.Get(request.Id, cancellationToken);
        category.Update(request.Name, request.Description);
        if (
            request.IsActive != null &&
            request.IsActive != category.IsActive
        )
        {
            if ((bool)request.IsActive!) category.Activate();
            else category.NotActivate();
        }
        await _categoryRepository.Update(category, cancellationToken);
        await _uinitOfWork.Commit(cancellationToken);
        return CategoryModelOutput.FromCategory(category);
    }
}