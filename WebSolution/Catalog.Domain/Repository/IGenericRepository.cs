using Catalog.Domain.SeedWork;

namespace Catalog.Domain.Repository;

public interface IGenericRepository<TAggregate> : IRepository  where TAggregate : AggregateRoot
{
    public Task Insert( TAggregate tAggregate, CancellationToken cancellationToken);
    public Task<TAggregate> Get( Guid Id, CancellationToken cancellationToken);
    
    public Task Delete(TAggregate tAggregate, CancellationToken cancellationToken);
    
    public Task Update(TAggregate tAggregate, CancellationToken cancellationToken);
    
}