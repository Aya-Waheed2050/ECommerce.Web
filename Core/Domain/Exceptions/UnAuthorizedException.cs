namespace Domain.Exceptions
{
    public sealed class UnAuthorizedException(string message = "Invalid Email Or Password!") 
        : Exception(message)
    {
    }
}
