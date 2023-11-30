using Catalog.Application.UseCases.Category;
using Catalog.Domain.Validation;
using FluentAssertions;
using Xunit;

namespace TestProject.Entity.Categorys
{
    [Collection(nameof(UpdateCategoryTestFixture))]
    public class UpdateCategoryInputValidatorTest
    {
        readonly UpdateCategoryTestFixture _fixture;

        public UpdateCategoryInputValidatorTest(UpdateCategoryTestFixture fixture)
        => _fixture = fixture;
        
        [Fact(DisplayName = nameof(UpdateCategoryIdnull))]
        [Trait("UpdateCategoryInputValidator", "UpdateCategoryInputValidator - Use Cases")]
        
        public async void UpdateCategoryIdnull()
        {
            var categoryMock = _fixture.GetcategoryMock();
            var unityOfWorkMock = _fixture.GetunityOfWorkMock();
            var input = _fixture.GetValidInput(Guid.Empty);
            var validator = new UpdateCategoryInputValidator();
            
            var _validResult = validator.Validate(input);
            

            _validResult.Should().NotBeNull();
            _validResult.IsValid.Should().BeFalse();
            _validResult.Errors.Should().HaveCount(1);
            _validResult.Errors[0].ErrorMessage.Should().Be("'Id' must not be empty.");

        }

        [Fact(DisplayName = nameof(UpdateCategoryIdNotnull))]
        [Trait("UpdateCategoryInputValidator", "UpdateCategoryInputValidator - Use Cases")]
        
        public async void UpdateCategoryIdNotnull()
        {
            var categoryMock = _fixture.GetcategoryMock();
            var unityOfWorkMock = _fixture.GetunityOfWorkMock();
            var input = _fixture.GetValidInput(Guid.NewGuid());
            var validator = new UpdateCategoryInputValidator();
            
            var _validResult = validator.Validate(input);
            

            _validResult.Should().NotBeNull();
            _validResult.IsValid.Should().BeTrue();
            _validResult.Errors.Should().HaveCount(0);

        }
    }
}