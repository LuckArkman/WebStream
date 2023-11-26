using Bogus;
using Catalog.Domain.Exceptions;
using FluentAssertions;
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
            action.Should().NotThrow();
        }

        [Theory(DisplayName = nameof(NotNullOrEmptyThrowWhenEmpty))]
        [Trait("Domain", "DomainValidation - Validation")]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void NotNullOrEmptyThrowWhenEmpty(string? target)
        {
            string fieldName = faker.Commerce.ProductName().Replace(" ", "");

            Action action =
                () => DomainValidation.NotNullOrEmpty(target, fieldName);

            action.Should().Throw<EntityValidationException>()
                .WithMessage($"{fieldName} should not be empty or null");
        }

        [Fact(DisplayName = "NotNullEmptyOk")]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullEmptyOk()
        {
            var target = faker.Commerce.ProductName();
            string fieldName = faker.Commerce.ProductName().Replace(" ", "");

            Action action = () => DomainValidation.NotNullOrEmpty(target, fieldName);
            action.Should().NotThrow();
        }

        [Theory(DisplayName = nameof(MinLengthThrowWhenLess))]
        [Trait("Domain", "DomainValidation - Validation")]
        [MemberData(nameof(GetValuesSmallerThanMin), parameters: 10)]
        public void MinLengthThrowWhenLess(string target, int minLength)
        {
            string fieldName = faker.Commerce.ProductName().Replace(" ", "");

            Action action =
                () => DomainValidation.MinLength(target, minLength, fieldName);

            action.Should().Throw<EntityValidationException>()
                .WithMessage($"{fieldName} should be at least {minLength} characters long");
        }

        public static IEnumerable<object[]> GetValuesSmallerThanMin(int numberOftests = 5)
        {
            yield return new object[] { "123456", 10 };
            var faker = new Faker();
            for (int i = 0; i < (numberOftests - 1); i++)
            {
                var example = faker.Commerce.ProductName();
                var minLength = example.Length + (new Random()).Next(1, 20);
                yield return new object[] { example, minLength };
            }
        }

        [Theory(DisplayName = nameof(MinLengthOk))]
        [Trait("Domain", "DomainValidation - Validation")]
        [MemberData(nameof(GetValuesGreaterThanMin), parameters: 10)]
        public void MinLengthOk(string target, int minLength)
        {
            string fieldName = faker.Commerce.ProductName().Replace(" ", "");

            Action action =
                () => DomainValidation.MinLength(target, minLength, fieldName);

            action.Should().NotThrow();
        }

        public static IEnumerable<object[]> GetValuesGreaterThanMin(int numberOftests = 5)
        {
            yield return new object[] { "123456", 6 };
            var faker = new Faker();
            for (int i = 0; i < (numberOftests - 1); i++)
            {
                var example = faker.Commerce.ProductName();
                var minLength = example.Length - (new Random()).Next(1, 5);
                yield return new object[] { example, minLength };
            }
        }

        [Theory(DisplayName = nameof(maxLengthThrowWhenGreater))]
        [Trait("Domain", "DomainValidation - Validation")]
        [MemberData(nameof(GetValuesGreaterThanMax), parameters: 10)]
        public void maxLengthThrowWhenGreater(string target, int maxLength)
        {
            string fieldName = faker.Commerce.ProductName().Replace(" ", "");

            Action action =
                () => DomainValidation.MaxLength(target, maxLength, fieldName);

            action.Should().Throw<EntityValidationException>()
                .WithMessage($"{fieldName} should be less or equal {maxLength} characters long");
        }

        public static IEnumerable<object[]> GetValuesGreaterThanMax(int numberOftests = 5)
        {
            yield return new object[] { "123456", 5 };
            var faker = new Faker();
            for (int i = 0; i < (numberOftests - 1); i++)
            {
                var example = faker.Commerce.ProductName();
                var maxLength = example.Length - (new Random()).Next(1, 5);
                yield return new object[] { example, maxLength };
            }
        }

        [Theory(DisplayName = nameof(maxLengthOk))]
        [Trait("Domain", "DomainValidation - Validation")]
        [MemberData(nameof(GetValuesLessThanMax), parameters: 10)]
        public void maxLengthOk(string target, int maxLength)
        {
            string fieldName = faker.Commerce.ProductName().Replace(" ", "");

            Action action =
                () => DomainValidation.MaxLength(target, maxLength, fieldName);

            action.Should().NotThrow();
        }

        public static IEnumerable<object[]> GetValuesLessThanMax(int numberOftests = 5)
        {
            yield return new object[] { "123456", 6 };
            var faker = new Faker();
            for (int i = 0; i < (numberOftests - 1); i++)
            {
                var example = faker.Commerce.ProductName();
                var maxLength = example.Length + (new Random()).Next(0, 5);
                yield return new object[] { example, maxLength };
            }
        }
    }
}