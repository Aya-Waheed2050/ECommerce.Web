using System.Text.Json;
using Domain.Contracts;
using Domain.Models.IdentityModule;
using Domain.Models.OrderModule;
using Domain.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext,
        UserManager<ApplicationUser> _userManager,
        RoleManager<IdentityRole> _roleManager,
        StoreIdentityDbContext _identityContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            try
            {
                if ((await _dbContext.Database.GetPendingMigrationsAsync()).Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }

                if (!_dbContext.ProductTypes.Any())
                {
                    //string? productTypesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                    FileStream? productTypesData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");

                    List<ProductType>? Types = await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypesData);

                    if (Types is not null && Types.Any())
                        await _dbContext.ProductTypes.AddRangeAsync(Types);
                }

                if (!_dbContext.ProductBrands.Any())
                {
                    FileStream? productTypeData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    List<ProductBrand>? Brands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productTypeData);

                    if (Brands is not null && Brands.Any())
                        await _dbContext.ProductBrands.AddRangeAsync(Brands);
                }

                if (!_dbContext.Products.Any())
                {
                    FileStream? productData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                    List<Product>? Products = await JsonSerializer.DeserializeAsync<List<Product>>(productData);

                    if (Products is not null && Products.Any())
                        await _dbContext.Products.AddRangeAsync(Products);
                }

                if (!_dbContext.DeliveryMethods.Any())
                {
                    FileStream? DeliveryMethodDataStream = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\delivery.json");
                    List<DeliveryMethod>? DeliveryMethods = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(DeliveryMethodDataStream);

                    if (DeliveryMethods is not null && DeliveryMethods.Any())
                        await _dbContext.DeliveryMethods.AddRangeAsync(DeliveryMethods);
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!_userManager.Users.Any())
                {
                    var AdminUser = new ApplicationUser()
                    {
                        DisplayName = "Admin",
                        UserName = "Admin",
                        Email = "Admin@gmail.com",
                        PhoneNumber = "01016688783"
                    };
                    var SuperAdminUser = new ApplicationUser()
                    {
                        DisplayName = "SuperAdmin",
                        UserName = "SuperAdmin",
                        Email = "SuperAdmin@gmail.com",
                        PhoneNumber = "01093747177",
                    };

                    await _userManager.CreateAsync(AdminUser, "P@ssw0rd");
                    await _userManager.CreateAsync(SuperAdminUser, "Pa$$w0rd");

                    await _userManager.AddToRoleAsync(AdminUser, "Admin");
                    await _userManager.AddToRoleAsync(SuperAdminUser, "SuperAdmin");
                }               
            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
