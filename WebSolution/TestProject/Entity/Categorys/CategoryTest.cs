using System;
using System.Linq;
using Xunit;
using Catalog.Domain.Entitys;
using Catalog.Domain.Exceptions;

namespace TestProject.Entity.Categorys
{

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
            Assert.Equal(IsActive, category.IsActive);
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

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThan3Characters))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("1")]
        [InlineData("12")]
        public void InstantiateErrorWhenNameIsLessThan3Characters(string? name)
        {
            Action action = () => new Category(name!, "category Description");
            var exception = Assert.Throws<EntityValidationException>(action);
            Assert.Equal("Name shoud be lass 3 characters Long", exception.Message);
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGraterThan255Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateErrorWhenNameIsGraterThan255Characters()
        {
            var name = string.Join(null, Enumerable.Range(0, 256).Select(_ => "a").ToArray());

            Action action = () => new Category(name, "category Description");
            var exception = Assert.Throws<EntityValidationException>(action);
            Assert.Equal("Name shoud be lass or equal  255 characters Long", exception.Message);
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsGraterThan10000Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateErrorWhenDescriptionIsGraterThan10000Characters()
        {
            var name = string.Join(null, Enumerable.Range(0, 10001).Select(_ => "a").ToArray());

            Action action = () => new Category("name", name);
            var exception = Assert.Throws<EntityValidationException>(action);
            Assert.Equal("Description shoud be lass or equal  10.000 characters Long", exception.Message);
        }
        
        [Fact(DisplayName = nameof(InstantiateSetActive))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateSetActive()
        {
            //Arrange
            var validData = new
            {
                Name = "category name",
                Description = "category Description",
            };
            var dateTimeBefore = DateTime.Now;
            //Act
            var category = new Category(validData.Name, validData.Description, true);
            category.Activate();
            Assert.True(category.IsActive);
        }
        
        [Fact(DisplayName = nameof(InstantiatesetNotActive))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiatesetNotActive()
        {
            //Arrange
            var validData = new
            {
                Name = "category name",
                Description = "category Description",
            };
            var dateTimeBefore = DateTime.Now;
            //Act
            var category = new Category(validData.Name, validData.Description, true);
            category.NotActivate();
            Assert.False(category.IsActive);
        }
    }
}