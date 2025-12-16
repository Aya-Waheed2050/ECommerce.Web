using Domain.Exceptions.NotFoundExceptions;

namespace Domain.Exceptions
{
    public sealed class UserNotFoundException(string email)
        : NotFoundException($"User With Email {email} Is not Exist!")
    {
    }
}
