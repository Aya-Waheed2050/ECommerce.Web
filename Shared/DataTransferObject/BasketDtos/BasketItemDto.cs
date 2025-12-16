using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObject.BasketDtos
{
    public record BasketItemDto
    {
        public int Id { get; init; }
        public string ProductName { get; init; } = default!;
        public string PictureUrl { get; init; } = default!;


        [Range(1 , double.MaxValue)]
        public decimal Price { get; init; }

        [Range(1, 100)]
        public int Quantity { get; init; }

    }
}