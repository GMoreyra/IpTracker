using Api.Controllers;
using Application.Services;
using Domain.Models;
using Moq;
using Shouldly;
using System;
using Xunit;

namespace Tests
{
    public class IpTrackerControllerTests
    {
        private Mock<IIpTrackerService> _mockService;
        private IpTrackerController _controller;

        private void Init_Test()
        {
            _mockService = new Mock<IIpTrackerService>();
            _controller = new IpTrackerController(_mockService.Object);
            _mockService.Setup(p => p.GetIpInfo(It.IsAny<string>())).ReturnsAsync(new IpInfoModel());
        }

        [Fact]
        public void Should_Not_Throw()
        {
            Action act = () => Init_Test();
            act.ShouldNotThrow();
        }

        [Fact]
        public async void GetData_Should_Return_IpInfo()
        {
            Init_Test();
            var result = await _controller.GetData("1.0.0");

            result.ShouldBeOfType<IpInfoModel>();
        }
    }
}
