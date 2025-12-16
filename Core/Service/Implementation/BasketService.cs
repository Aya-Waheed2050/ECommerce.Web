using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Exceptions.NotFoundExceptions;
using Domain.Models.BasketModule;
using ServiceAbstraction;
using Shared.DataTransferObject.BasketDtos;

namespace Service.Implementation
{
    public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {

        public async Task<BasketDto> GetAsync(string Key)
        {
            CustomerBasket? basket = await _basketRepository.GetAsync(Key);
            return (basket is not null) ?
                 _mapper.Map<CustomerBasket, BasketDto>(basket) :
                   throw new BasketNotFoundException(Key);
        }

        public async Task<BasketDto> CreateOrUpdateAsync(BasketDto basket)
        {
            CustomerBasket? customerBasket = _mapper.Map<BasketDto, CustomerBasket>(basket);
            CustomerBasket? IsCreatedOrUpdated = await _basketRepository.CreateOrUpdateAsync(customerBasket);
            return (IsCreatedOrUpdated is not null) ?
                 await GetAsync(basket.Id) :
                 throw new Exception("Can Not Create Or Update Basket Now, Try Again Later!");
        }

        public async Task<bool> DeleteAsync(string Key)
        => await _basketRepository.DeleteAsync(Key);


    }
}
