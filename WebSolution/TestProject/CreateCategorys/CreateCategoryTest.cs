using Catalog.Domain.Entitys;
using FluentAssertions;
using Moq;
using Xunit;
using System.Threading;
using Catalog.Application.Interfaces;
using Catalog.Application.UseCases.Category;
using Catalog.Domain.Repository;

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
            Guid.NewGuid(),
            "category name",
            "category Description",
            true,
            DateTime.Now
            );
        var output = await useCase.Handle(input, CancellationToken.None);
        
        categoryMock.Verify(repository => repository.Insert(It.IsAny<Category>(),
            It.IsAny<CancellationToken>()),
            Times.Once());
        
        unityOfWorkMock.Verify(uow => uow.Commit(It.IsAny<CancellationToken>()),
            Times.Once());
        
        output.ShouldNotBeNull();
        output.Name.Should().Be("category name");
        output.Description.Should().Be("category Description");
        (output.Id != null && output.Id != Guid.Empty).Should().BeTrue();
        (output.createTime != null && output.createTime != default(DateTime)).Should().BeTrue();
    }
}