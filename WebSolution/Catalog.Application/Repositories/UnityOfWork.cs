using Catalog.Application.Interfaces;
using Catalog.Data.Configurations;
using Catalog.Domain.Repository;

namespace Catalog.Application.Repositories;

public class UnityOfWork : IunityOfWork
{
    private readonly CatalogDbContext _context;

    public UnityOfWork(CatalogDbContext _DbContext)
        => _context = _DbContext;

    public async Task Commit(CancellationToken cancellationToken)
        => await _context.SaveChangeAsync(cancellationToken);

    public Task RollCack(CancellationToken _cancellationToken)
        => Task.CompletedTask;
}