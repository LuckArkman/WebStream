using Catalog.Data.Configurations;
using Catalog.Domain.Entitys;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Base;

public class Singleton
{
    public static Singleton Instance { get; set; }

    public List<Category> _Categories = new List<Category>();
    
    public CatalogDbContext _catalogDb { get; set; }

    public static Singleton _instance()
    {
        if (Instance == null)
        {
            Singleton singleton = new Singleton();
            Instance = singleton;
        }
        return Instance;
    }

    public void  CreateDBContext()
    => _catalogDb = new CatalogDbContext(
            new DbContextOptionsBuilder<CatalogDbContext>()
                .UseInMemoryDatabase($"catalogDB")
                .Options
        );
}