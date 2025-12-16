using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Implementation;
using Service.MappingProfiles;
using ServiceAbstraction;
using Shared.Common;

namespace Service
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection Services ,IConfiguration _configuration)
        {

            //builder.Services.AddAutoMapper(typeof(Service.AssemblyReferencesProfiles).Assembly);
            //Services.AddAutoMapper(cfg => { }, typeof(Service.AssemblyReferencesProfiles).Assembly);
            Services.AddAutoMapper(x => x.AddProfile(new ProductProfile()));
            Services.AddAutoMapper(x => x.AddProfile(new BasketProfile()));
            Services.AddAutoMapper(x => x.AddProfile(new IdentityProfile()));
            Services.AddAutoMapper(x => x.AddProfile(new OrderProfile()));
            Services.AddTransient<PictureUrlResolver>();
            Services.AddTransient<OrderItemPictureUrlResolver>();
            //=======================
            Services.Configure<JwtOptions>(_configuration.GetSection("JwtOptions"));

            Services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();

            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<Func<IProductService>>(provider =>
            () => provider.GetRequiredService<IProductService>());


            Services.AddScoped<IBasketService, BasketService>();
            Services.AddScoped<Func<IBasketService>>(provider =>
            () => provider.GetRequiredService<IBasketService>());

            Services.AddScoped<IOrderService, OrderService>();
            Services.AddScoped<Func<IOrderService>>(provider =>
            () => provider.GetRequiredService<IOrderService>());

            Services.AddScoped<IAuthenticationService, AuthenticationService>();
            Services.AddScoped<Func<IAuthenticationService>>(provider =>
            () => provider.GetRequiredService<IAuthenticationService>());

            Services.AddScoped<ICacheService, CacheService>();

            Services.AddScoped<IPaymentService, PaymentService>();
            Services.AddScoped<Func<IPaymentService>>(provider =>
            () => provider.GetRequiredService<IPaymentService>());

            return Services;
        }


    }
}
