using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObject.IdentityDtos
{
    public record RegisterDto
    {
        [EmailAddress]
        public string Email { get; init; } = default!;
        public string Password { get; init; } = default!;
        public string UserName { get; init; } = default!;
        public string DisplayName { get; init; } = default!;
        
        [Phone]
        public string? PhoneNumber { get; init; } 

    }
}
