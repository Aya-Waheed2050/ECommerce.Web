namespace Domain.Models.BasketModule
{
    public class CustomerBasket
    {
        public string Id { get; set; } // Guid : Created From Client [Front_End]
        public ICollection<BasketItem> Items { get; set; } = [];
        public string? ClientSecret { get; set; }
        public string? PaymentIntentId { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal? ShippingPrice { get; set; }
    }
}
