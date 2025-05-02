namespace EComNetMonolith.Shared.Exceptions;

public class NotFoundException: Exception
{
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string name, object key) : base($"Entity \"{name}\" with \"{key}\" key not found")
    {
    }
    public NotFoundException() : base("The requested resource was not found.")
    {
    }
}
