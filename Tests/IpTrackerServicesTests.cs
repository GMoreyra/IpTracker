using Application.Services;
using AutoMapper;
using Domain.Models;
using Mapping.Profiles;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using Shouldly;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Utils;
using System.Threading;
using Application.Interfaces;
using Data.Interfaces;

namespace Tests
{
    public class IpTrackerServicesTests
    {
        private Mock<IIpTrackerRepository> _mockRepo;
        private IIpTrackerService _service;
        private Mock<IDistributedCache> _distributedCache;

        private void Init_Test()
        {
            var mapper = new MapperConfiguration(m =>
            {
                m.AddProfile(new IpLocationToIpInfoProfile());
            }).CreateMapper();

            _distributedCache = new Mock<IDistributedCache>();
            _mockRepo = new Mock<IIpTrackerRepository>();
            _service = new IpTrackerService(_mockRepo.Object, mapper, _distributedCache.Object);
            _mockRepo.Setup(p => p.ReturnCountryInfo(It.IsAny<string>())).ReturnsAsync(new IpToLocationModel());
            _mockRepo.Setup(p => p.ReturnMoneyInfo(It.IsAny<List<string>>())).ReturnsAsync(new List<string>());
        }

        [Fact]
        public void Should_Not_Throw()
        {
            Action act = () => Init_Test();
            act.ShouldNotThrow();
        }

        [Fact]
        public async Task GetIpInfo_Should_Return_IpInfo()
        {
            Init_Test();
            var result = await _service.GetIpInfo("1.0.0");

            result.ShouldBeOfType<IpInfoModel>();
        }
    }
}
