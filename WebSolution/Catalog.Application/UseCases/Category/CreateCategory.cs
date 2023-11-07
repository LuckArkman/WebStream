using Catalog.Application.Interfaces;
using Catalog.Domain.Repository;

namespace Catalog.Application.UseCases.Category;

public class CreateCategory : ICreateCategory
{
    private readonly IunityOfWork _unityOfWork;
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategory(ICategoryRepository categoryRepository, IunityOfWork unityOfWork)
    {
        this._categoryRepository = categoryRepository;
        this._unityOfWork = unityOfWork;
    }


    public async Task<CreateCategoryOutput> Handle(CreateCategoryInput input, CancellationToken cancellationToken)
    {
        var category = new Domain.Entitys.Category(input.Name, input.Description, input.IsActive);
        _categoryRepository.Insert(category, cancellationToken);
        _unityOfWork.Commit(cancellationToken);
        return new CreateCategoryOutput(category.Id, category.Name, category.Description, category.IsActive, category.createTime);
    }
}