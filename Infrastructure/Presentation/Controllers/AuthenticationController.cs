using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObject.IdentityDtos;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager) : ApiBaseController
    {

        [HttpPost("login")]  //POST BaseUrl/Authentication/login
        public async Task<ActionResult<UserResultDto>> Login(LoginDto loginDto)
         => Ok(await _serviceManager.AuthenticationService.LoginAsync(loginDto));
        

        [HttpPost("register")] //POST BaseUrl/Authentication/register
        public async Task<ActionResult<UserResultDto>> Register(RegisterDto registerDto)
         => Ok(await _serviceManager.AuthenticationService.RegisterAsync(registerDto));
        

        [HttpGet("emailexists")] //Get BaseUrl/Authentication/emailexists
        public async Task<ActionResult<bool>> CheckEmail(string email)
         => Ok(await _serviceManager.AuthenticationService.CheckEmailExistAsync(email));
        

        [Authorize]
        [HttpGet("CurrentUser")] //Get BaseUrl/Authentication/CurrentUser
        public async Task<ActionResult<UserResultDto>> GetCurrentUser()
         => Ok(await _serviceManager.AuthenticationService.GetCurrentUserAsync(GetEmailFromToken()));
        

        [Authorize]
        [HttpGet("address")] //Get BaseUrl/Authentication/address
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        => Ok(await _serviceManager.AuthenticationService.GetCurrentUserAddressAsync(GetEmailFromToken()));
       


        [Authorize]
        [HttpPut("address")] //Put BaseUrl/Authentication/address
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto addressDto)
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);
            AddressDto? updatedAddress = await _serviceManager.AuthenticationService.UpdateCurrentUserAddressAsync(email , addressDto);
            return Ok(updatedAddress);
        }
    }
}
