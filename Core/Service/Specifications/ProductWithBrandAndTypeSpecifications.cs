using Domain.Models.ProductModule;
using Shared;

namespace Service.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product , int>
    {
        // Get All Product With Type And Brands
        public ProductWithBrandAndTypeSpecifications(ProductSpecificationParameters Parameters , bool forDashBoard = false)
            :base(p => (!Parameters.BrandId.HasValue || p.BrandId == Parameters.BrandId) 
                    && (!Parameters.TypeId.HasValue || p.TypeId == Parameters.TypeId)
                    && (string.IsNullOrWhiteSpace(Parameters.Search) || p.Name.ToLower().Contains(Parameters.Search.ToLower())))
        // Where(p=> p.BrandId == BrandId && p=> p.TypeId == TypeId)
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);

            switch (Parameters.Sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                        break;
                default:
                    break;
            }
            if (!forDashBoard)
                ApplyPagination(Parameters.PageSize , Parameters.PageIndex);
        }

        // GetById
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
        } 

    }
}
