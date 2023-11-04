using Bogus;
using Catalog.Domain.Exceptions;
using FluentAssertions;
using TestProject.Validation;
using Xunit;
namespace Catalog.Domain.Validation
{
    public class DomainValidationTest
    {
        Faker faker { get; set; } = new Faker();
        
        [Fact(DisplayName = "NotNullOk")]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullOk()
        {
            var value = faker.Commerce.ProductName();
            Action action = () => DomainValidation.NotNull(value, "value");
            action.Should().NotThrow<EntityValidationException>();
        }
        /*
        void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new EntityValidationExceptionption($"{nameof(Name)} shold not be empty or null");
            if (Name.Length < 3) throw new EntityValidationException($"{nameof(Name)} shoud be lass 3 characters Long");
            if (Name.Length > 255)
                throw new EntityValidationException($"{nameof(Name)} shoud be lass or equal  255 characters Long");
            if (Description == null)
                throw new EntityValidationException($"{nameof(Description)} shold not be empty or null");
            if (Description.Length > 10000)
                throw new EntityValidationException(
                    $"{nameof(Description)} shoud be lass or equal  10.000 characters Long");
        }
        */
    }
}
