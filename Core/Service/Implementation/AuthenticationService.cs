using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Domain.Exceptions;
using Domain.Exceptions.NotFoundExceptions;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.Common;
using Shared.DataTransferObject.IdentityDtos;

namespace Service.Implementation
{
    public class AuthenticationService(
        UserManager<ApplicationUser> _userManager, IMapper _mapper,IOptions<JwtOptions> _options)
        : IAuthenticationService
    {
        public async Task<UserResultDto> LoginAsync(LoginDto login)
        {
            // Check If Email Exist
            ApplicationUser? user = await _userManager.FindByEmailAsync(login.Email)
                ?? throw new UnAuthorizedException();

            // Check Password
            bool isPasswordValid = await _userManager.CheckPasswordAsync(user, login.Password);
            if (!isPasswordValid) throw new UnAuthorizedException();
            return new UserResultDto(user.Email, await CreateTokenAsync(user) , user.DisplayName);
        }


        public async Task<UserResultDto> RegisterAsync(RegisterDto register)
        {
            // Mapping RegisterDto => Application User
            ApplicationUser? User = new ApplicationUser()
            {
                DisplayName = register.DisplayName,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
                UserName = register.UserName
            };
            // Create User [Application User]
            IdentityResult? result = await _userManager.CreateAsync(User, register.Password);

            if (result.Succeeded)
               return new UserResultDto(User.Email, await CreateTokenAsync(User), User.DisplayName);            
            else
            {
                List<string>? Errors = result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(Errors);
            }
        }


        public async Task<bool> CheckEmailExistAsync(string email)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);
            return user is not null;
        }

        public async Task<UserResultDto> GetCurrentUserAsync(string email)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);
            return (user is null) ? throw new UserNotFoundException(email) :
                  new UserResultDto(user.DisplayName, await CreateTokenAsync(user), user.Email);
        }

        public async Task<AddressDto> GetCurrentUserAddressAsync(string email)
        {
            ApplicationUser? user = await _userManager.Users.Include(u => u.Address)
                                               .FirstOrDefaultAsync(u => u.Email == email) ?? throw new UserNotFoundException(email);
            return (user.Address is null) ? throw new AddressNotFoundException(user.UserName):
                _mapper.Map<Address, AddressDto>(user.Address);

        }

        public async Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto addressDto)
        {
            ApplicationUser? user = await _userManager.Users.Include(u => u.Address)
                                              .FirstOrDefaultAsync(u => u.Email == email) ?? throw new UserNotFoundException(email);
            if (user.Address is not null)
            {
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;
                user.Address.City = addressDto.City;
                user.Address.Country = addressDto.Country;
                user.Address.Street = addressDto.Street;
            }
            else
            {
                user.Address = _mapper.Map<AddressDto, Address>(addressDto);
            }
            await _userManager.UpdateAsync(user);
            return _mapper.Map<Address,AddressDto>(user.Address);
        }



        #region Helper Method

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var jwtOptions = _options.Value; 
            //claims
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email , user.Email),
                new Claim(ClaimTypes.Name , user.DisplayName),
                new Claim(ClaimTypes.NameIdentifier , user.Id)
            };

            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            // signingCredentials
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));
            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            // Token
            var Token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(jwtOptions.ExpirationInDays),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }


        #endregion

    }
}
