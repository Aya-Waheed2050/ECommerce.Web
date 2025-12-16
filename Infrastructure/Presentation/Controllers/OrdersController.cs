using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObject.OrderDtos;

namespace Presentation.Controllers
{
    [Authorize]
    public class OrdersController(IServiceManager _serviceManager) : ApiBaseController
    {

        [HttpPost]
        public async Task<ActionResult<OrderResult>> CreateOrder(/*[FromBody] */OrderRequest orderDto)
         => Ok(await _serviceManager.OrderService.CreateOrderAsync(orderDto, GetEmailFromToken()));
        

        [AllowAnonymous]
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods()
         => Ok(await _serviceManager.OrderService.GetDeliveryMethodAsync());
        

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResult>>> GetAllOrdersByEmail()
         => Ok(await _serviceManager.OrderService.GetOrdersByEmailAsync(GetEmailFromToken()));
        

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderResult>> GetOrderById(Guid id)
         => Ok(await _serviceManager.OrderService.GetOrderByIdAsync(id));
        

    }
}
