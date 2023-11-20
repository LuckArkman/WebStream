using Bogus;
using Catalog.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Catalog.EndToEndTests.Base;

public class BaseFixture
{
    public BaseFixture() => faker = new Faker("pt_BR");

    protected Faker faker { get; set; }
        
    public CatalogDbContext CreateDBContext(string _Id = "")
    {
            
        var context = new CatalogDbContext(
            new DbContextOptionsBuilder<CatalogDbContext>()
                .UseInMemoryDatabase($"end2end-tests-db{_Id}")
                .Options
        );
        return context;
    }
        
    public void CleanInMemoryDatabase() => CreateDBContext().Database.EnsureDeleted();
}