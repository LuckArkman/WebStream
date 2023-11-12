using Catalog.Application.Interfaces;
using Catalog.Domain.Repository;
using MediatR;

namespace Catalog.Application.UseCases.Category;

public class DeleteCategory : IDeleteCategory
{
    readonly IunityOfWork _unityOfWork;
    readonly ICategoryRepository _categoryRepository;
    public DeleteCategory(ICategoryRepository categoryRepository, IunityOfWork unityOfWork)
        =>(_categoryRepository, _unityOfWork)= (categoryRepository, unityOfWork);

    public async Task Handle(DeleteCategoryInput request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.Get(request.Id, cancellationToken);
        await _categoryRepository.Delete(category, cancellationToken);
        await _unityOfWork.Commit(cancellationToken);
    }
}