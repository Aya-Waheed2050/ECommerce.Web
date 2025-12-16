namespace Domain.Exceptions
{
    public sealed class BadRequestException(List<string> errors) : Exception("Validation Failed")
    {
        public List<string> Errors { get; } = errors;
    }
}
