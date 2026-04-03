namespace InkShelf.Domain.Exceptions
{
    public class EntityValidationException(string message, IEnumerable<string> errors) : Exception(message)
    {
        public IEnumerable<string> Errors => errors;
    }
}
