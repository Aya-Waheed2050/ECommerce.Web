namespace Domain.Models.OrderModule
{
    public class DeliveryMethod : BaseEntity<int>
    {
        public DeliveryMethod()
        {
            
        }
        public DeliveryMethod(string shortName, string description, decimal price, string deliveryTime)
        {
            ShortName = shortName;
            Description = description;
            Price = price;
            DeliveryTime = deliveryTime;
        }

        public string ShortName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DeliveryTime { get; set; } = string.Empty;
        public decimal Price { get; set; }

    }

}
