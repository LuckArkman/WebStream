using Catalog.Data.Configurations;
using Catalog.Domain.Entitys;
using Catalog.Domain.Repository;
using Catalog.Domain.SeedWork;

namespace Catalog.Application.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>, ISearchableRepository<Category>
    {
        public IUnityOfWork _unityOfWork { get; set; }
        public ICategoryRepository _categoryRepository { get; set; }
        public CatalogDbContext _catalogDb { get; set; }

        public Task Update(
            Category _category,
            CancellationToken cancellationToken
        );
    }
}