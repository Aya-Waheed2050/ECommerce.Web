using AutoMapper;
using Domain.Models.OrderModule;
using Shared.DataTransferObject.OrderDtos;

namespace Service.MappingProfiles
{
     class OrderProfile : Profile
    {
        public OrderProfile() 
        {
            CreateMap<AddressDto , OrderAddress>().ReverseMap();
            CreateMap<Order, OrderResult>()
                .ForMember(D => D.Status, O => O.MapFrom(s => s.Status.ToString()))
                .ForMember(D => D.DeliveryMethod, O => O.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(D => D.Total, O => O.MapFrom(s => s.SubTotal + s.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(D => D.ProductName, O => O.MapFrom(s => s.Product.ProductName))
                .ForMember(D => D.ProductId, O => O.MapFrom(s => s.Product.ProductId))
                .ForMember(D => D.PictureUrl, O => O.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<DeliveryMethod, DeliveryMethodDto>()
                .ForMember(D => D.Cost, O => O.MapFrom(s => s.Price));
                
        }
    }
}
