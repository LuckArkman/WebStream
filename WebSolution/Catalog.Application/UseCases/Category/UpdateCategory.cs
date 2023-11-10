using Catalog.Application.Interfaces;
using Catalog.Domain.Repository;

namespace Catalog.Application.UseCases.Category;

public class UpdateCategory
{
    public readonly IunityOfWork _unityOfWork;
    public readonly ICategoryRepository _categoryRepository;

    public UpdateCategory(ICategoryRepository categoryRepository, IunityOfWork unityOfWork)
        => (_categoryRepository, _unityOfWork) = (categoryRepository, unityOfWork);
}