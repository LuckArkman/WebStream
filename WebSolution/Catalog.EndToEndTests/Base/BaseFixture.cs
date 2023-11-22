using Bogus;
using Keycloak.AuthServices.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Catalog.Data.Configurations;
using System.Net.Http;

namespace Catalog.EndToEndTests.Base;

public class BaseFixture: IDisposable
{
    protected Faker faker { get; set; }

    public CustomWebApplicationFactory<Program> WebAppFactory { get; set; }
    public HttpClient _httpClient { get; set; }
    public ApiClient ApiClient { get; set; }
    private readonly string _dbConnectionString;

    public BaseFixture()
    {
        faker = new Faker("pt_BR");
        WebAppFactory = new CustomWebApplicationFactory<Program>();
        _httpClient = WebAppFactory.CreateClient();
        ApiClient = new ApiClient(_httpClient);
    }

    public CatalogDbContext CreateDbContext()
     => new (
            new DbContextOptionsBuilder<CatalogDbContext>()
                .UseInMemoryDatabase($"end2end-tests-db")
                .Options
        );
    public void CleanPersistence()
    {
        var context = CreateDbContext();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        WebAppFactory.Dispose();
    }
}