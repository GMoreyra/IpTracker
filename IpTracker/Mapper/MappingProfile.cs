using AutoMapper;
using IpTracker.Models;

namespace IpTracker.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IpToLocationModel, IpInfo>()
                .ForMember(d => d.Country, o => o.MapFrom(s => s.Country_name))
                .ForMember(d => d.ISOCode, o => o.MapFrom(s => s.Country_code))
                .ForMember(d => d.Timezone, o => o.MapFrom(s => s.GetTimeZone()))
                .ForMember(d => d.Currency, o => o.MapFrom(s => s.GetCurrency()))
                .ForMember(d => d.DistanceToBA, o => o.MapFrom(s => s.GetDistance()));
        }
    }
}
