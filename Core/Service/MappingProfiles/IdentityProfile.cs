using AutoMapper;
using Domain.Models.IdentityModule;
using Shared.DataTransferObject.IdentityDtos;

namespace Service.MappingProfiles
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
