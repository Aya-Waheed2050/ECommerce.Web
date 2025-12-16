namespace Shared.DataTransferObject.OrderDtos
{
    public class OrderRequest
    {
        public string BasketId { get; set; } = string.Empty;
        public int DeliveryMethodId { get; set; } = default!;
        public AddressDto ShipToAddress { get; set; } = default!;

    }
}
