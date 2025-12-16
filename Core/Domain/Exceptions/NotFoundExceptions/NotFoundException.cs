namespace Domain.Exceptions.NotFoundExceptions
{
    public abstract class NotFoundException(string Message) : Exception(Message)
    {
    }
}
