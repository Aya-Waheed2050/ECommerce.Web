using Domain.Models.ProductModule;
using Shared;

namespace Service.Specifications
{
    public class TypeSpecifications : BaseSpecifications<ProductType, int>
    {
        public TypeSpecifications(BrandAndTypeQueryParams queryParams, bool forDashBoard = false)
            : base(P => P.Name == queryParams.SearchValue)
        {

        }
    }
}
