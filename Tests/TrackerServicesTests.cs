using Application.Interfaces.Repositories;
using Application.Services;
using Domain.Models;
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
        _distributedCache = new Mock<IDistributedCache>();
        _mockRepo = new Mock<ITrackerRepository>();
        _service = new TrackerService(_mockRepo.Object, _distributedCache.Object);
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
        var result = await _service.GetIpInformation("1.0.0");

        result.ShouldBeOfType<IpInfoModel>();
    }
}
