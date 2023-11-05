using FluentAssertions;
using Moq;
using Xunit;

namespace TestProject.CreateCategorys;

public class CreateCategoryTest
{
    [Fact(DisplayName = nameof(CreateCategory))]
    [Trait("Domain", "CreateCategory - Use Cases")]
    public async void CreateCategory()
    {
        var categoryMock = new Mock<ICategoryRepository>();
        var unityOfWorkMock = new Mock<IunityOfWork>();

        var useCase = new CreateCategory(
            categoryMock.Object,
            unityOfWorkMock.Object
            );

        var input = new CreateCategoryInput(
            "category name",
            "category Description",
            true
            );

        var output = await useCase.Handle(input);
        output.ShouldNotBeNull();
        output.Name.Should.Be("category name");
        output.Description.Should.Be("category Description");
        (output.Id != null && output.Id != Guid.Empty).Should().BeTrue();
        (output.createTime != null && output.createTime != default(DateTime)).Should().BeTrue();
    }
}