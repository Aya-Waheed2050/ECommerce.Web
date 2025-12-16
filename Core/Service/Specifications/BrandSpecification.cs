using Domain.Models.ProductModule;
using Shared;

namespace Service.Specifications
{
     public class BrandSpecification : BaseSpecifications<ProductBrand, int>
     {
         public BrandSpecification(BrandAndTypeQueryParams queryParams, bool forDashBoard = false)
             : base(P => P.Name == queryParams.SearchValue)
         { 
         }
     }
    
}
