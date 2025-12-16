namespace Shared.DataTransferObject.OrderDtos
{
    public record OrderResult
    {
        public Guid Id { get; init; }
        public string BuyerEmail { get; init; } = string.Empty;
        public AddressDto ShipToAddress { get; init; } = new();
        public ICollection<OrderItemDto> Items { get; init; } = [];
        public string Status { get; init; } = string.Empty;
        public string DeliveryMethod { get; init; } = string.Empty;
        public int? DeliveryMethodId { get; init; }
        public decimal SubTotal { get; init; }
        public decimal Total { get; init; }
        public DateTimeOffset OrderDate { get; init; } = DateTimeOffset.UtcNow;
        public string PaymentIntentId { get; init; } = string.Empty; 


    }
}
