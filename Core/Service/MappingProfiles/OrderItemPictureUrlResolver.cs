using AutoMapper;
using Domain.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObject.OrderDtos;

namespace Service.MappingProfiles
{
    internal class OrderItemPictureUrlResolver(IConfiguration _configuration) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
                return string.Empty;
            else
            {
                var url = $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.Product.PictureUrl}";
                return url;
            }
        }
    }
}
