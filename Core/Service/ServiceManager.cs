using AutoMapper;
using Domain.Contracts;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Service.Implementation;
using ServiceAbstraction;
using Shared.Common;

namespace Service
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepository _basketRepositories,
           UserManager<ApplicationUser> _userManager, IConfiguration _configuration, IOptions<JwtOptions> _options)
        //: IServiceManager

    {
        private readonly Lazy<IProductService> _lazyProductService =
            new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));

        private readonly Lazy<IBasketService> _lazyBasketService =
            new Lazy<IBasketService>(() => new BasketService(_basketRepositories, _mapper));

        private readonly Lazy<IAuthenticationService> _lazyAuthenticationService =
            new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager, _mapper, _options));

        private readonly Lazy<IOrderService> _lazyOrderService =
            new Lazy<IOrderService>(() => new OrderService(_mapper, _basketRepositories, _unitOfWork));
        
        private readonly Lazy<IPaymentService> _lazyPaymentService =
           new Lazy<IPaymentService>(() => new PaymentService(_configuration, _basketRepositories, _unitOfWork, _mapper));
        //======
        public IProductService ProductService => _lazyProductService.Value;

        public IBasketService BasketService => _lazyBasketService.Value;

        public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;

        public IOrderService OrderService => _lazyOrderService.Value;

        public IPaymentService PaymentService => _lazyPaymentService.Value;
    }
}
