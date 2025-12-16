using AutoMapper;
using Domain.Models.ProductModule;
using Shared.DataTransferObject.ProductDtos;

namespace Service.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductType, TypeResultDto>();
            CreateMap<ProductBrand, BrandResultDto>();

            CreateMap<Product , ProductResultDto>()
                .ForMember(dist => dist.BrandName , options => options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dist => dist.TypeName , options => options.MapFrom(src => src.ProductType.Name))
                .ForMember(dist => dist.PictureUrl , options => options.MapFrom<PictureUrlResolver>());
        
        }
    }
}
