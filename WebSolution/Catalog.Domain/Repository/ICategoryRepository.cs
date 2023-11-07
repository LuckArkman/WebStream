using Catalog.Domain.Entitys;
using Moq;

namespace Catalog.Domain.Repository;

public interface ICategoryRepository : IGenericRepository<Category>
{
    
    public Task<IReadOnlyList<Guid>> GetIdsListByIds(
        List<Guid> ids,
        CancellationToken cancellationToken
    );
    
    public Task<IReadOnlyList<Guid>> GetListByIds(
        List<Guid> ids,
        CancellationToken cancellationToken
    );
}