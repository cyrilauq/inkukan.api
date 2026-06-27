namespace Inkukan.Application.Interface
{
    public interface IValidatable<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="EntityValidationException">If the value is not valid</exception>
        /// <returns></returns>
        public Task<bool> EnsureIsValidAsync(T value, CancellationToken cancellationToken = default);
    }
}
