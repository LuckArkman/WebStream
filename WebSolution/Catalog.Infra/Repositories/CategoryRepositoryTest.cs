using System.Collections.ObjectModel;
using Xunit;
namespace Catalog.Infra.Repositories
{
    [Collection(nameof(CategoryRepositoryTestFixture))]
    public class CategoryRepositoryTest
    {
        readonly CategoryRepositoryTestFixture _fixture;

        public CategoryRepositoryTest(CategoryRepositoryTestFixture fixture)
        => _fixture = fixture;
        
    }
}