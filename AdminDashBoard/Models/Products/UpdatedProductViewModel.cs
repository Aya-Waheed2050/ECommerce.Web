using Domain.Models.ProductModule;
using System.ComponentModel.DataAnnotations;

namespace AdminDashBoard.Models.Products
{
    public class UpdatedProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name is Required")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Product Description is Required")]
        public string Description { get; set; } = null!;

        public IFormFile? Image { get; set; } 

        public string? PictureUrl { get; set; }

        [Required(ErrorMessage = "Product Price is Required")]
        [Range(1, 10000)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Product Brand is Required")]
        public int BrandId { get; set; }

        public ProductBrand? Brand { get; set; }

        [Required(ErrorMessage = "Product Type is Required")]
        public int TypeId { get; set; }

        public ProductType? Type { get; set; }

    }
}
