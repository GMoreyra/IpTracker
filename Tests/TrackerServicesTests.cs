using Application.Services;
using AutoMapper;
using Data.Interfaces;
using Domain.Models;
using Mapping.Profiles;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Tests;

public class TrackerServicesTests
{
    private Mock<ITrackerRepository> _mockRepo;
    private TrackerService _service;
    private Mock<IDistributedCache> _distributedCache;

    private void Init_Test()
    {
        var mapper = new MapperConfiguration(m =>
        {
            m.AddProfile(new IpLocationToIpInfoProfile());
        }).CreateMapper();

        _distributedCache = new Mock<IDistributedCache>();
        _mockRepo = new Mock<ITrackerRepository>();
        _service = new TrackerService(_mockRepo.Object, mapper, _distributedCache.Object);
        _mockRepo.Setup(p => p.ReturnCountryInfo(It.IsAny<string>())).ReturnsAsync(new IpToLocationModel());
        _mockRepo.Setup(p => p.ReturnMoneyInfo(It.IsAny<List<string>>())).ReturnsAsync([]);
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
