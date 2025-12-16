using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObject.BasketDtos;

namespace Presentation.Controllers
{
    [Authorize]
    public class BasketController (IServiceManager _serviceManager): ApiBaseController
    {
        [HttpGet] 
        public async Task<ActionResult<BasketDto>> GetBasket(string Key)
         => Ok(await _serviceManager.BasketService.GetAsync(Key));
        

        [HttpPost]   
        public async Task<ActionResult<BasketDto>> CreateOrUpdate(BasketDto basket)
         => Ok(await _serviceManager.BasketService.CreateOrUpdateAsync(basket));


        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(string id)
         => Ok(await _serviceManager.BasketService.DeleteAsync(id));
        
        
    }
}
