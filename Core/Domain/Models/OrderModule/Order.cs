namespace Domain.Models.OrderModule
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {
            
        }

        public Order(string buyerEmail, OrderAddress shipToAddress, ICollection<OrderItem> items,
            decimal subTotal, DeliveryMethod deliveryMethod , string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            Items = items;
            SubTotal = subTotal;
            DeliveryMethod = deliveryMethod;
            PaymentIntentId = paymentIntentId;
        }

        public string BuyerEmail { get; set; } = string.Empty;
        public OrderAddress ShipToAddress { get; set; } 
        public ICollection<OrderItem> Items { get; set; } = [];
        public OrderPaymentStatus Status { get; set; } = OrderPaymentStatus.Pending;

        // Navigational Property
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public int? DeliveryMethodId { get; set; } // FK
        public decimal SubTotal { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public string PaymentIntentId { get; set; } = string.Empty;


    }
}
