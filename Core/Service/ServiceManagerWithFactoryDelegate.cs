using ServiceAbstraction;

namespace Service
{
    public class ServiceManagerWithFactoryDelegate(
        Func<IProductService> _productFactory,
        Func<IBasketService> _basketFactory,
        Func<IOrderService> _orderFactory,
        Func<IAuthenticationService> _authenticationFactory,
        Func<IPaymentService> _paymentFactory
        ) : IServiceManager
    {
        public IProductService ProductService => _productFactory.Invoke();

        public IBasketService BasketService => _basketFactory.Invoke();

        public IAuthenticationService AuthenticationService => _authenticationFactory.Invoke();

        public IOrderService OrderService => _orderFactory.Invoke();

        public IPaymentService PaymentService => _paymentFactory.Invoke();
    }
}
