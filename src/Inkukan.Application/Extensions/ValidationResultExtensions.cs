using FluentValidation.Results;

namespace Inkukan.Application.Extensions
{
    public static class ValidationResultExtensions
    {
        public static IEnumerable<string> GetErrorMessages(this ValidationResult validationResult)
            => validationResult.Errors.Select(x => x.ErrorMessage);
    }
}
