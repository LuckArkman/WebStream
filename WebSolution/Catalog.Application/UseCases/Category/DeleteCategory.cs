using Catalog.Application.Interfaces;
using Catalog.Application.Repositories;
using MediatR;

namespace Catalog.Application.UseCases.Category;

public class DeleteCategory : IDeleteCategory
{
    readonly IUnityOfWork _unityOfWork;
    readonly ICategoryRepository _categoryRepository;
    public DeleteCategory(ICategoryRepository categoryRepository, IUnityOfWork unityOfWork)
        =>(_categoryRepository, _unityOfWork)= (categoryRepository, unityOfWork);

    public async Task<Unit> Handle(DeleteCategoryInput request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.Get(request.Id, cancellationToken);
        await _categoryRepository.Delete(category, cancellationToken);
        await _unityOfWork.Commit(cancellationToken);
        return Unit.Value;
    }
}