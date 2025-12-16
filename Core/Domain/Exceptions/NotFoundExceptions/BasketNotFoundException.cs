namespace Domain.Exceptions.NotFoundExceptions
{
    public sealed class BasketNotFoundException(string id)
        : NotFoundException($"Basket With Id : {id} Not Found!")
    {
    }
}
