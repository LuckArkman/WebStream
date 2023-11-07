using Catalog.Domain.SeedWork;

namespace Catalog.Domain.Repository;

public interface IGenericRepository<TAggregate> : IRepository  where TAggregate : AggregateRoot
{
    public Task Insert( TAggregate tAggregate, CancellationToken cancellationToken);
    
}