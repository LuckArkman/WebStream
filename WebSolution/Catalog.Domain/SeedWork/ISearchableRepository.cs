using Catalog.Domain.SeedWork;

namespace Catalog.Domain.SeedWork
{
    public interface ISearchableRepository<Taggregate>
    where Taggregate : AggregateRoot
    {
        Task<SearchOutput<Taggregate>> Search(
            SearchInput input,
            CancellationToken cancellationToken
        );
    }
}