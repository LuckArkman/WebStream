
using System.Runtime.Serialization;
using Catalog.Domain.Exceptions;

namespace TestProject.Validation
{
    public class DomainValidation
    {
        public static void NotNull(object value, string fieldName)
        {
            if(value is null)  throw new EntityValidationException($"{fieldName} shold not be empty or null");
        }

        public static void NotNullOrEmpty(string? target, string fieldName)
        {
            if (String.IsNullOrWhiteSpace(target))
                throw new EntityValidationException(
                    $"{fieldName} should not be empty or null");
        }
        
        public static void MinLength(string target, int minLength, string fieldName)
        {
            if (target.Length < minLength)
                throw new EntityValidationException($"{fieldName} should be at least {minLength} characters long");
        }
        public static void MaxLength(string target, int maxLength, string fieldName)
        {
            if (target.Length > maxLength)
                throw new EntityValidationException($"{fieldName} should be less or equal {maxLength} characters long");
        }
    }
}