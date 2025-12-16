using Shared.DataTransferObject.IdentityDtos;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        Task<UserResultDto> LoginAsync(LoginDto login);
        Task<UserResultDto> RegisterAsync(RegisterDto register);
        Task<bool> CheckEmailExistAsync(string email);
        Task<AddressDto> GetCurrentUserAddressAsync(string email);
        Task<AddressDto> UpdateCurrentUserAddressAsync(string email , AddressDto addressDto);
        Task<UserResultDto> GetCurrentUserAsync(string email);

    }

}
