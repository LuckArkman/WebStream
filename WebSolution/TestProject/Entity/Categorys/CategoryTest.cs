using Xunit;
using Catalog.Domain.Entitys;
using Catalog.Domain.Exceptions;
using FluentAssertions;

namespace TestProject.Entity.Categorys
{
    [Collection(nameof(CategoryTestFixture))]
    public class CategoryTest
    {
        readonly CategoryTestFixture _categoryTestFixture;

        public CategoryTest(CategoryTestFixture categoryTestFixture) => _categoryTestFixture = categoryTestFixture;

        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain", "Category - Aggregates")]
        public void Instantiate()
        {
            //Arrange
            var validData = _categoryTestFixture.GetValidCategory();
            var dateTimeBefore = DateTime.Now;
            var category = new Category(validData.Name, validData.Description);
            var dateTimeAfter = DateTime.Now.AddMilliseconds(1);
            category.Should().NotBeNull();
            category.Name.Should().Be(validData.Name);
            category.Description.Should().Be(validData.Description);
            category.Id.Should().NotBeEmpty();
            category.createTime.Should().NotBeSameDateAs(default(DateTime));
            (category.createTime > dateTimeBefore).Should().BeTrue();
            (category.createTime < dateTimeAfter).Should().BeTrue();
            (category.IsActive).Should().BeTrue();
        }

        [Theory(DisplayName = nameof(InstantiateIsActive))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData(true)]
        [InlineData(false)]
        public void InstantiateIsActive(bool IsActive)
        {
            var validData = _categoryTestFixture.GetValidCategory();
            var dateTimeBefore = DateTime.Now;
            var category = new Category(validData.Name, validData.Description, IsActive);
            var dateTimeAfter = DateTime.Now.AddMilliseconds(1);
            category.Should().NotBeNull();
            category.Name.Should().Be(validData.Name);
            category.Description.Should().Be(validData.Description);
            category.Id.Should().NotBeEmpty();
            category.createTime.Should().NotBeSameDateAs(default(DateTime));
            (category.createTime > dateTimeBefore).Should().BeTrue();
            (category.createTime < dateTimeAfter).Should().BeTrue();
            category.IsActive.Should().Be(IsActive);
        }

        [Theory(DisplayName = nameof(InstantiateErrorNameIsEmpty))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("  ")]
        public void InstantiateErrorNameIsEmpty(string? name)
        {
            var category = _categoryTestFixture.GetValidCategory();
            Action action = () => category.Update(name!, "category Description");
            action.Should().Throw<EntityValidationException>().WithMessage("Name should not be empty or null");
        }

        [Fact(DisplayName = nameof(InstantiateErrorDescriptionIsEmpty))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateErrorDescriptionIsEmpty()
        {
            Action action = () => new Category("category name", null);
            action.Should().Throw<EntityValidationException>().WithMessage("Description should not be null");
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThan3Characters))]
        [Trait("Domain", "Category - Aggregates")]
        [MemberData(nameof(GetNames), parameters: 10)]
        public void InstantiateErrorWhenNameIsLessThan3Characters(string? name)
        {
            Action action = () => new Category(name!, "category Description");
            action.Should().Throw<EntityValidationException>("Name shoud be lass 3 characters Long");
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGraterThan255Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateErrorWhenNameIsGraterThan255Characters()
        {
            var name = string.Join(null, Enumerable.Range(0, 256).Select(_ => "a").ToArray());

            Action action = () => new Category(name, "category Description");
            action.Should().Throw<EntityValidationException>()
                .WithMessage("Name should be less or equal 255 characters Long");
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsGraterThan10000Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateErrorWhenDescriptionIsGraterThan10000Characters()
        {
            var name = string.Join(null, Enumerable.Range(0, 10001).Select(_ => "a").ToArray());

            Action action = () => new Category("name", name);
            action.Should().Throw<EntityValidationException>()
                .WithMessage("Description should be less or equal 10000 characters long");
        }

        [Fact(DisplayName = nameof(InstantiateSetActive))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateSetActive()
        {
            //Arrange
            var validData = _categoryTestFixture.GetValidCategory();
            var dateTimeBefore = DateTime.Now;
            //Act
            var category = new Category(validData.Name, validData.Description, true);
            category.Activate();
            category.IsActive.Should().BeTrue();
        }

        [Fact(DisplayName = nameof(InstantiatesetNotActive))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiatesetNotActive()
        {
            //Arrange
            var validData = _categoryTestFixture.GetValidCategory();
            var dateTimeBefore = DateTime.Now;
            //Act
            var category = new Category(validData.Name, validData.Description, true);
            category.NotActivate();
            category.IsActive.Should().BeFalse();
        }

        [Fact(DisplayName = nameof(InstantiateSetUpdate))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateSetUpdate()
        {
            //Arrange
            var validData = _categoryTestFixture.GetValidCategory();
            var dateTimeBefore = DateTime.Now;
            //Act
            var category = _categoryTestFixture.GetValidCategory();
            var values = new
            {
                Name = "New name",
                Description = "category Description",
            };
            category.Update(values.Name, values.Description);
            category.Name.Should().Be(values.Name);
            category.Description.Should().Be(values.Description);
        }

        [Fact(DisplayName = nameof(InstantiateSetUpdateName))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateSetUpdateName()
        {
            //Arrange
            var validData = _categoryTestFixture.GetValidCategory();
            var dateTimeBefore = DateTime.Now;
            //Act
            var category = _categoryTestFixture.GetValidCategory();
            var values = new
            {
                Name = "New name",
                Description = category.Description,
            };
            category.Update(values.Name, values.Description);
            category.Name.Should().Be(values.Name);
            category.Description.Should().Be(values.Description);
        }

        [Fact(DisplayName = nameof(InstantiateSetUpdateDescription))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateSetUpdateDescription()
        {
            //Arrange
            var validData = _categoryTestFixture.GetValidCategory();
            var dateTimeBefore = DateTime.Now;
            //Act
            var category = _categoryTestFixture.GetValidCategory();
            var values = new
            {
                Name = category.Name,
                Description = "new Description",
            };
            category.Update(values.Name, values.Description);
            category.Name.Should().Be(values.Name);
            category.Description.Should().Be(values.Description);
        }

        [Theory(DisplayName = nameof(InstantiateUpdateErrorWhenNameIsLessThan3Characters))]
        [Trait("Domain", "Category - Aggregates")]
        [MemberData(nameof(GetNames), parameters: 50)]
        public void InstantiateUpdateErrorWhenNameIsLessThan3Characters(string? name)
        {
            var category = _categoryTestFixture.GetValidCategory();
            Action action = () => category.Update(name!, category.Description);
            var exception = Assert.Throws<EntityValidationException>(action);
            Assert.Equal("Name should be at least 3 characters long", exception.Message);
        }

        public static IEnumerable<object[]> GetNames(int numberOfTests)
        {
            var fixture = new CategoryTestFixture();
            for (int i = 0; i < numberOfTests; i++)
            {
                var IsOdd = i % 2 == 1;
                yield return new object[]{fixture.GetValidCategoryName()[..(IsOdd ? 1 : 2)]};
            }
        }
        
    [Fact(DisplayName = nameof(InstantiateUpdateErrorWhenNameIsGraterThan255Characters))]
    [Trait("Domain", "Category - Aggregates")]
        public void InstantiateUpdateErrorWhenNameIsGraterThan255Characters()
        {
            var name = string.Join(null, Enumerable.Range(0, 256).Select(_ => "a").ToArray());
            var category = _categoryTestFixture.GetValidCategory();
            Action action = () => category.Update(name);
            action.Should().Throw<EntityValidationException>().WithMessage("Name should be less or equal 255 characters Long");
        }

        [Fact(DisplayName = nameof(InstantiateUpdateErrorWhenDescriptionIsGraterThan10000Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateUpdateErrorWhenDescriptionIsGraterThan10000Characters()
        {
            var name = string.Join(null, Enumerable.Range(0, 10001).Select(_ => "a").ToArray());
            var category = _categoryTestFixture.GetValidCategory();
            Action action = () => category.Update("category name", name);
            action.Should().Throw<EntityValidationException>().WithMessage("Description should be less or equal 10000 characters long");
        }
    }
}