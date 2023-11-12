using Catalog.Domain.Repository;

namespace Catalog.Application.Interfaces;

public interface IunityOfWork
{
    public Task Commit(CancellationToken cancellationToken);
}