using Xunit;
using Catalog.Domain.Entity;
using Catalog.Domain.Exceptions;

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

    [Theory(DisplayName = nameof(InstantiateErrorNameIsEmpty))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("  ")]
    public void InstantiateErrorNameIsEmpty(string? name)
    {
        Action action = () => new Category(name!, "category Description");
        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name shold not be empty or null", exception.Message);
    }

    [Fact(DisplayName = nameof(InstantiateErrorDescriptionIsEmpty))]
    [Trait("Domain", "Category - Aggregates")]
    public void InstantiateErrorDescriptionIsEmpty()
    {
        Action action = () => new Category("category name", null);
        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Description shold not be empty or null", exception.Message);
    }

    /*
    public void InstantiateErrorWhenNameIsLessThan3Characters()
    {
        Action action = () => Category("category name", null);
        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Description shoud not be empty or null", exception.message);
    }
    */
}