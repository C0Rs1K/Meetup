using AutoMapper;
using Meetup.Application.Models.Address;
using Meetup.Domain.Models;

namespace Meetup.Application.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<AddressModel, AddressEntity>().ReverseMap();
            CreateMap<InsertAddressModel, AddressEntity>();
            CreateMap<UpdateAddressModel, AddressEntity>();
        }
    }
}
