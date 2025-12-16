using Domain.Contracts;
using Domain.Models.OrderModule;

namespace Service.Specifications
{
    class OrderWithIncludesSpecifications : BaseSpecifications<Order , Guid>
    {
        // Get All Order By Email
        public OrderWithIncludesSpecifications(string email):base(o => o.BuyerEmail == email)
        {
            AddIncludes(o => o.DeliveryMethod);   
            AddIncludes(o => o.Items);
            AddOrderByDescending(o => o.OrderDate);
        }


        // Get Order By Id
        public OrderWithIncludesSpecifications(Guid id) : base(o => o.Id == id)
        {
            AddIncludes(o => o.DeliveryMethod);
            AddIncludes(o => o.Items);
        }

    }

}
