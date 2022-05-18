using AutoMapper;
using Domain.Models;

namespace Mapping.Profiles
{
    public class IpLocationToIpInfoProfile : Profile
    {
        public IpLocationToIpInfoProfile()
        {
            CreateMap<IpToLocationModel, IpInfoModel>()
                .ForMember(d => d.Country, o => o.MapFrom(s => s.Country_name))
                .ForMember(d => d.ISOCode, o => o.MapFrom(s => s.Country_code))
                .ForMember(d => d.Timezone, o => o.MapFrom(s => s.GetTimeZone()))
                .ForMember(d => d.Currency, o => o.MapFrom(s => s.GetCurrency()))
                .ForMember(d => d.DistanceToBA, o => o.MapFrom(s => s.GetDistance()));
        }
    }
}
