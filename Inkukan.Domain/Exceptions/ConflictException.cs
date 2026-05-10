namespace InkShelf.Domain.Exceptions
{
    public class ConflictException(string message) : CustomException(message)
    {
    }
}
