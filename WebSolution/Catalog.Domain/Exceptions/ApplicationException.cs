namespace Catalog.Domain.Exceptions;

public abstract class ApplicationException: Exception
{
    public ApplicationException(string? message) : base(message)
    {

    }
}