using AutoMapper;
using Domain.Models;
using Mapping.Profiles;
using Shouldly;
using System;
using Xunit;

namespace Tests
{
    public class MappingProfileTests
    {
        private IpToLocationModel _ipToLocationModel;
        private IpInfoModel _ipInfoMode;
        private IMapper _mapper;

        private void Init_Test()
        {
            _ipToLocationModel = IpToLocationMock.GetIpToLocationModelMock();
            _mapper = new MapperConfiguration(m =>
            {
                m.AddProfile(new IpLocationToIpInfoProfile());
            }).CreateMapper();


        }

        [Fact]
        public void Should_Not_Throw()
        {
            Action act = () => Init_Test();
            act.ShouldNotThrow();
        }

        [Fact]
        public void Mapping_Should_Mapp_Full_Data()
        {
            Init_Test();
            _ipInfoMode = _mapper.Map<IpInfoModel>(_ipToLocationModel);

            _ipInfoMode.ShouldNotBeNull();
            _ipInfoMode.Country.ShouldBe(_ipToLocationModel.Country_name);
            _ipInfoMode.ISOCode.ShouldBe(_ipToLocationModel.Country_code);
            _ipInfoMode.Currency.ShouldBe(_ipToLocationModel.GetCurrency());
            _ipInfoMode.DistanceToBA.ShouldBe(_ipToLocationModel.GetDistance());
            _ipInfoMode.Timezone.ShouldBe(_ipToLocationModel.GetTimeZone());
        }

        [Fact]
        public void Mapping_Should_Mapp_Missing_Data()
        {
            Init_Test();
            _ipToLocationModel.Latitude = null;
            _ipToLocationModel.Timezones = null;
            _ipToLocationModel.CurrenciesDollarValue = null;
            _ipToLocationModel.Currencies = null;
            _ipInfoMode = _mapper.Map<IpInfoModel>(_ipToLocationModel);

            _ipInfoMode.ShouldNotBeNull();
            _ipInfoMode.Currency.ShouldBe("Missing information");
            _ipInfoMode.DistanceToBA.ShouldBe("Missing information");
            _ipInfoMode.Timezone.ShouldBe("Missing information");
        }
    }
}
