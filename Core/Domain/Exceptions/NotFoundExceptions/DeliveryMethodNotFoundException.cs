namespace Domain.Exceptions.NotFoundExceptions
{
    public sealed class DeliveryMethodNotFoundException(int id)
        : NotFoundException($"No DeliveryMethod Found With Id : {id}!")
    {
    }
}
