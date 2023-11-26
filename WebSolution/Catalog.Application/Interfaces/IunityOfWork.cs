using Catalog.Domain.Repository;

namespace Catalog.Application.Interfaces;

public interface IunityOfWork
{
    public Task Commit(CancellationToken cancellationToken);
    public Task RollCack(CancellationToken _cancellationToken);
}