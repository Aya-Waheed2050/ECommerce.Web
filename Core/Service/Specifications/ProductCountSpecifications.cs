using Domain.Models.ProductModule;
using Shared;

namespace Service.Specifications
{
    class ProductCountSpecifications : BaseSpecifications<Product, int>
    {
        public ProductCountSpecifications(ProductSpecificationParameters queryParams)
            : base(p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId)
            && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
            && (string.IsNullOrWhiteSpace(queryParams.Search) || p.Name.ToLower().Contains(queryParams.Search.ToLower())))
        {

        }

    }
}
