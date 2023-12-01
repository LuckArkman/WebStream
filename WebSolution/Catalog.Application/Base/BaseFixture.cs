using Bogus;
using Catalog.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Base
{
    public class BaseFixture
    {
        public BaseFixture() => faker = new Faker("pt_BR");

        protected Faker faker { get; set; }
        
        public CatalogDbContext CreateDBContext()
        {
            
            var context = new CatalogDbContext(
                new DbContextOptionsBuilder<CatalogDbContext>()
                    .UseInMemoryDatabase($"integration-tests-db")
                    .Options
            );
            return context;
        }
    }
}