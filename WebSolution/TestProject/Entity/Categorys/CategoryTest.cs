using Xunit;
using Catalog.Domain.Entity;

namespace TestProject.Entity.Categorys;

public class CategoryTest
{
    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "Category - Aggregates")]
    public void Instantiate()
    {
        //Arrange
        var validData = new
        {
            Name = "category name",
            Description = "category Description",
        };
        //Act
        var category = new Category(validData.Name,
                                    validData.Description);
        Assert.NotNull(category);
        Assert.Equal(validData.Name, category.Name);
        Assert.Equal(validData.Description, category.Description);
        
    }
}