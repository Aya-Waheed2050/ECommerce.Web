namespace Shared.DataTransferObject.ProductDtos
{
    public record TypeResultDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}
