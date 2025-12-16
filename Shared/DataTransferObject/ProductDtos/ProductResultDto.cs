namespace Shared.DataTransferObject.ProductDtos
{
    public record ProductResultDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string PictureUrl { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public string BrandName { get; init; } = string.Empty;
        public string TypeName { get; init; } = string.Empty;

    }
}
