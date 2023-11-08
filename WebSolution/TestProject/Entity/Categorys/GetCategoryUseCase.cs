using Catalog.Application.UseCases.Category;
using Catalog.Domain.Repository;
using Moq;

namespace TestProject.Entity.Categorys;

public class GetCategoryUseCase
{
    public GetCategoryUseCase(Mock<ICategoryRepository> categoryMock)
    {
        
    }

    public async Task<CreateCategoryOutput> Handle(GetCategoryInput input, CancellationToken none)
    {
        return null;
    }
}