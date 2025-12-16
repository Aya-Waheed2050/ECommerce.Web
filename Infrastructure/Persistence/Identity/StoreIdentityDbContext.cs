using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Identity
{
    public class StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options) 
        : IdentityDbContext<ApplicationUser>(options)
    {

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<Address>().ToTable("Addresses");
            //builder.Entity<ApplicationUser>().ToTable("Users");
            //builder.Entity<IdentityRole>().ToTable("Roles");
            //builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            
            //builder.Ignore<IdentityUserClaim<string>>();
            //builder.Ignore<IdentityUserToken<string>>();
            //builder.Ignore<IdentityUserLogin<string>>();
            //builder.Ignore<IdentityRoleClaim<string>>();
        }
    }

}
