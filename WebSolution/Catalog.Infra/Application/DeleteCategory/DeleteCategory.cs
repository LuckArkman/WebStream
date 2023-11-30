using Catalog.Application.UseCases.Category;
using Catalog.Application.Interfaces;
using Catalog.Domain.Repository;
using MediatR;

namespace Catalog.Infra.Application.DeleteCategory;

public class DeleteCategory : IDeleteCategory
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IunityOfWork _unitOfWork;

    public DeleteCategory(ICategoryRepository categoryRepository, IunityOfWork unitOfWork)
        => (_categoryRepository, _unitOfWork) = (categoryRepository, unitOfWork);

    public async Task<Unit> Handle(DeleteCategoryInput request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.Get(request.Id, cancellationToken);
        await _categoryRepository.Delete(category, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        return Unit.Value;
    }
}