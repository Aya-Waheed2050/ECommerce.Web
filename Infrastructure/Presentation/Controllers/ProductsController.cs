using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObject.ProductDtos;
using Presentation.Attributes;

namespace Presentation.Controllers
{
    public class ProductsController(IServiceManager _serviceManager) : ApiBaseController
    {

        [Cache]
        [HttpGet]    // BaseUrl/api/Products
        public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetAllProductsAsync([FromQuery]ProductSpecificationParameters parameters)
         => Ok(await _serviceManager.ProductService.GetAllProductsAsync(parameters));
        

        [HttpGet("{id:int}")]   // BaseUrl/api/Products/10
        public async Task<ActionResult<ProductResultDto>> GetProductById(int id)
         => Ok(await _serviceManager.ProductService.GetProductByIdAsync(id));
        

        [HttpGet("Brands")] // BaseUrl/api/Products/Brands
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrandsAsync()
         => Ok(await _serviceManager.ProductService.GetAllBrandsAsync());
        

        [HttpGet("Types")] // BaseUrl/api/Products/Types
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypesAsync()
         => Ok(await _serviceManager.ProductService.GetAllTypesAsync());
        

    }
}
