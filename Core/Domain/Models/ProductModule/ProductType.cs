namespace Domain.Models.ProductModule
{
    public class ProductType : BaseEntity<int>
    {
        public string Name { get; set; } = default!;
    }
}
