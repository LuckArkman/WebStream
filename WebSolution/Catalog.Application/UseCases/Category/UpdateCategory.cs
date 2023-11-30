using Catalog.Application.Common;
using Catalog.Application.Interfaces;
using Catalog.Application.Repositories;

namespace Catalog.Application.UseCases.Category;

public class UpdateCategory : IUpdateCategory
{
    public readonly IUnityOfWork _unityOfWork;
    public readonly ICategoryRepository _categoryRepository;

    public UpdateCategory(ICategoryRepository categoryRepository, IUnityOfWork unityOfWork)
        => (_categoryRepository, _unityOfWork) = (categoryRepository, unityOfWork);

    public async Task<CategoryModelOutput> Handle(UpdateCategoryInput request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.Get(request.Id, cancellationToken);
        category.Update(request.Name, request.Description);
        if (
            request.IsActive != null &&
            request.IsActive != category.IsActive
        )
            if ((bool)request.IsActive!) category.Activate();
            else category.NotActivate();
        await _categoryRepository.Update(category, cancellationToken);
        await _unityOfWork.Commit(cancellationToken);
        return CategoryModelOutput.FromCategory(category);
    }
}