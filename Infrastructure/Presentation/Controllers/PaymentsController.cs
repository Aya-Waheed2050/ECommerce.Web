using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObject.BasketDtos;

namespace Presentation.Controllers
{
    [Authorize]
    public class PaymentsController(IServiceManager _serviceManager) : ApiBaseController
    {

        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent(string basketId)
         => Ok(await _serviceManager.PaymentService.CreateOrUpdatePaymentIntentAsync(basketId));
        


        [AllowAnonymous]
        [HttpPost("webhook")]
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeHeaders = Request.Headers["Stripe-Signature"];

            await _serviceManager.PaymentService.UpdatePaymentStatusAsync(json, stripeHeaders);
            return new EmptyResult();
        }


    }
}