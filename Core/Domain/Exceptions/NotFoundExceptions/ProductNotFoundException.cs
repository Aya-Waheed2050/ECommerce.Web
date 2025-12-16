namespace Domain.Exceptions.NotFoundExceptions
{
    public sealed class ProductNotFoundException(int id)
        : NotFoundException($"Product With Id {id} Not Found!")
    {
    }
}
