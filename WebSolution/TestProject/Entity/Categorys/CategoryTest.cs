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
        var dateTimeBefore = DateTime.Now;
        var category = new Category(validData.Name, validData.Description);
        var dateTimeAfter = DateTime.Now;
        Assert.NotNull(category);
        Assert.Equal(validData.Name, category.Name);
        Assert.Equal(validData.Description, category.Description);
        Assert.NotEqual(default(Guid), category.Id);
        Assert.NotEqual(default(DateTime), category.createTime);
        Assert.True(category.createTime > dateTimeBefore);
        Assert.True(category.createTime < dateTimeAfter);
        Assert.True(category.IsActive);
    }

    [Theory(DisplayName = nameof(InstantiateIsActive))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData(true)]
    [InlineData(false)]
    public void InstantiateIsActive(bool IsActive)
    {
        //Arrange
        var validData = new
        {
            Name = "category name",
            Description = "category Description",
        };
        var dateTimeBefore = DateTime.Now;
        //Act
        var category = new Category(validData.Name, validData.Description, IsActive);
        var dateTimeAfter = DateTime.Now;
        Assert.NotNull(category);
        Assert.Equal(validData.Name, category.Name);
        Assert.Equal(validData.Description, category.Description);
        Assert.NotEqual(default(Guid), category.Id);
        Assert.NotEqual(default(DateTime), category.createTime);
        Assert.True(category.createTime > dateTimeBefore);
        Assert.True(category.createTime < dateTimeAfter);
        Assert.Equal(IsActive , category.IsActive);
    }

    [Fact(DisplayName = nameof(InstantiateErrorNameIsEmpty))]
    [Trait("Domain", "Category - Aggregates")]

    public void InstantiateErrorNameIsEmpty()
    {
        Action action = () => Category("category name", null);
        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Description shoud not be empty or null", exception.message);
    }

    public void InstantiateErrorWhenNameIsLessThan3Characters()
    {
        Action action = () => Category("category name", null);
        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Description shoud not be empty or null", exception.message);
    }
}