namespace Shared.DataTransferObject.OrderDtos
{
    public record OrderItemDto
    {
        public int ProductId { get; init; }
        public string ProductName { get; init; } = string.Empty;
        public string PictureUrl { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public int Quantity { get; init; }


    }
}
