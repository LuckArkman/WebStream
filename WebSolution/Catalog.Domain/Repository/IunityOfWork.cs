namespace Catalog.Domain.Repository;

public interface IunityOfWork
{
    public Task Commit(CancellationToken cancellationToken);
    public Task RollCack(CancellationToken _cancellationToken);
}