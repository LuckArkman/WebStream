using Bogus;
using Keycloak.AuthServices.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Catalog.Data.Configurations;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Catalog.EndToEndTests.Base;

public class BaseFixture: IDisposable
{
    protected Faker Faker { get; set; }

    public CustomWebApplicationFactory<Program> WebAppFactory { get; set; }
    public HttpClient HttpClient { get; set; }
    public ApiClient ApiClient { get; set; }
    private readonly string _dbConnectionString;

    public BaseFixture()
    {
        Faker = new Faker("pt_BR");
        WebAppFactory = new CustomWebApplicationFactory<Program>();
        HttpClient = WebAppFactory.CreateClient();
        var configuration = WebAppFactory.Services
            .GetRequiredService<IConfiguration>();
        var keycloakOptions = configuration
            .GetSection(KeycloakAuthenticationOptions.Section)
            .Get<KeycloakAuthenticationOptions>();
        ApiClient = new ApiClient(HttpClient, keycloakOptions);
        ArgumentNullException.ThrowIfNull(configuration);
        _dbConnectionString = configuration
            .GetConnectionString("CatalogDb");
    }

    public CatalogDbContext CreateDbContext(bool preserveData = false, string _Id = "")
    {
        var context = new CatalogDbContext(
            new DbContextOptionsBuilder<CatalogDbContext>()
                .UseInMemoryDatabase($"end2end-tests-db{_Id}")
                .Options
        );
        if (preserveData == false) context.Database.EnsureDeleted();
        return context;
    }
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