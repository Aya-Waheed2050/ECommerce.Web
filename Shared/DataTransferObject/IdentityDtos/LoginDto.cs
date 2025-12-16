using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObject.IdentityDtos
{
    public record LoginDto
    {

        [EmailAddress]
        public string Email { get; init; } = default!;
        public string Password { get; init; } = default!;
    
    }
}
