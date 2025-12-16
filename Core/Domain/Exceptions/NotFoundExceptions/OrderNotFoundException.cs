namespace Domain.Exceptions.NotFoundExceptions
{
    public sealed class OrderNotFoundException(Guid id)
        : NotFoundException($"Order With Id : {id} Not Found!")
    {
    }
}
