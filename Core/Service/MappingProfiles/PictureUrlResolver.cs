using AutoMapper;
using Domain.Models.ProductModule;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObject.ProductDtos;

namespace Service.MappingProfiles
{
    public class PictureUrlResolver(IConfiguration _configuration) : IValueResolver<Product, ProductResultDto, string>
    {
        public string Resolve(Product source, ProductResultDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl)) return string.Empty;
            {
                string baseUrl = _configuration.GetSection("Urls")["BaseUrl"]!;
                return $"{baseUrl}{source.PictureUrl}";
            }
        }
    }
}
