namespace Domain.Exceptions.NotFoundExceptions
{
    public sealed class AddressNotFoundException(string userName)
        : NotFoundException($"User {userName} Has No Address!")
    {
    }
}
