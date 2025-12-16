namespace Shared.DataTransferObject.BasketDtos
{
    public record BasketDto
    {
        public string Id { get; init; }
        public ICollection<BasketItemDto> Items { get; init; } = [];

        public string? ClientSecret { get; init; }
        public string? PaymentIntentId { get; init; }
        public int? DeliveryMethodId { get; init; }
        public decimal? ShippingPrice { get; init; }
    }
}
