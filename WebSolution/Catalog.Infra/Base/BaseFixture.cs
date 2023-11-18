using Bogus;
using Catalog.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infra.Base
{
    public class BaseFixture
    {
        public BaseFixture() => faker = new Faker("pt_BR");

        protected Faker faker { get; set; }
        
        public CatalogDbContext CreateDBContext(bool preserveData = false, string _Id = "")
        {
            
            var context = new CatalogDbContext(
                new DbContextOptionsBuilder<CatalogDbContext>()
                    .UseInMemoryDatabase($"integration-tests-db{_Id}")
                    .Options
            );
            if (preserveData == false) context.Database.EnsureDeleted();
            return context;
        }
        
        public void CleanInMemoryDatabase() => CreateDBContext().Database.EnsureDeleted();

    }
}