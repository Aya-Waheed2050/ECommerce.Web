namespace Shared.DataTransferObject.ProductDtos
{
    public record BrandResultDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}
