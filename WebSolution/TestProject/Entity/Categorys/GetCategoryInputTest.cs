using Catalog.Application.GetCategoryTest.Categorys;
using FluentAssertions;
using Moq;
using TestProject.Entity.GetCategory;
using Xunit;

namespace TestProject.Entity.Categorys;

[Collection(nameof(GetCategoryTestFixture))]
public class GetCategoryInputTest
{
    private readonly GetCategoryTestFixture _fixture;

    public GetCategoryInputTest(GetCategoryTestFixture fixture)
        => _fixture = fixture;
    
    [Fact(DisplayName = nameof(ValidationOk))]
    [Trait("GetCategoryInputTest", "CategoryInputTest - Use Cases")]
    public void ValidationOk()
    {
        var input = new GetCategoryInput(Guid.NewGuid());
        var validation = new GetCategoryInputValidator();

        var valid = validation.Validate(input);
        
        valid.Should().NotBeNull();
        valid.IsValid.Should().BeTrue();
        valid.Errors.Should().HaveCount(0);
    }
    
    [Fact(DisplayName = nameof(ValidationInvalidOk))]
    [Trait("GetCategoryInputTest", "CategoryInputTest - Use Cases")]
    public void ValidationInvalidOk()
    {
        var input = new GetCategoryInput(Guid.Empty);
        var validation = new GetCategoryInputValidator();

        var valid = validation.Validate(input);
        
        valid.Should().NotBeNull();
        valid.IsValid.Should().BeFalse();
        valid.Errors[0].ErrorMessage.Should().Be("'Id' deve ser informado.");
    }
}