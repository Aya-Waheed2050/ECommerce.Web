namespace Shared.DataTransferObject.OrderDtos
{
    public record DeliveryMethodDto
    {
        public int Id { get; init; }
        public string ShortName { get; init; } = default!;
        public string Description { get; init; } = default!;
        public string DeliveryTime { get; init; } = default!;
        public decimal Cost { get; init; }
    }
}
