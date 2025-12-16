using Microsoft.AspNetCore.Identity;

namespace Domain.Models.IdentityModule
{
    public class ApplicationUser :  IdentityUser
    {
        public string DisplayName { get; set; } = default!;

        public Address? Address { get; set; }
    }
}
