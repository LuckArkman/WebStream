using Bogus;
using Catalog.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infra.Base
{
    public class BaseFixture
    {
        public BaseFixture() => faker = new Faker("pt_BR");

        protected Faker faker { get; set; }
        
        public CatalogDbContext CreateDBContext()
            => new CatalogDbContext(new DbContextOptionsBuilder<CatalogDbContext>()
                .UseInMemoryDatabase("Integration-tests-db")
                .Options);

    }
}