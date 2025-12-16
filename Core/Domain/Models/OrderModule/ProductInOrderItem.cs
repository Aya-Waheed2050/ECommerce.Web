namespace Domain.Models.OrderModule
{
    public class ProductInOrderItem
    {
        public ProductInOrderItem()
        {
            
        }
        public ProductInOrderItem(int productId, string productName, string pictureUrl)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string PictureUrl { get; set; } = string.Empty;

    }
}
