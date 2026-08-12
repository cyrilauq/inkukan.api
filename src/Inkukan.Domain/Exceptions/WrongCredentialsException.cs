namespace Inkukan.Domain.Exceptions;

public class WrongCredentialsException(string message) : Exception(message)
{
}
