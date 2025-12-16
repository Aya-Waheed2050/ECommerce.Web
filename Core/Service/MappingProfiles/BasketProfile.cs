using AutoMapper;
using Domain.Models.BasketModule;
using Shared.DataTransferObject.BasketDtos;

namespace Service.MappingProfiles
{
    class BasketProfile : Profile
    {
        public BasketProfile() 
        {
            CreateMap<CustomerBasket , BasketDto>().ReverseMap();
            CreateMap<BasketItem , BasketItemDto>().ReverseMap();
        }
    }
}
