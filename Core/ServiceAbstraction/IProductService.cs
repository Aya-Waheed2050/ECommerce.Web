using Shared;
using Shared.DataTransferObject.ProductDtos;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParameters parameters);
        Task<ProductResultDto> GetProductByIdAsync(int id);
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();

    }
}
