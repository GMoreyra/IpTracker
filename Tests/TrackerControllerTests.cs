namespace Tests;

using Api.Controllers;
using Application.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using System;
using Xunit;

public class TrackerControllerTests
{
    private Mock<ITrackerService> _mockService;
    private TrackerController _controller;

    private void Init_Test()
    {
        _mockService = new Mock<ITrackerService>();
        _controller = new TrackerController(_mockService.Object);
        _mockService.Setup(p => p.GetIpInformation(It.IsAny<string>())).ReturnsAsync(new IpInfoModel());
    }

    [Fact]
    public void Should_Not_Throw()
    {
        // Arrange
        Action act = () => Init_Test();

        // Act & Assert
        act.ShouldNotThrow();
    }

    [Fact]
    public async void GetData_Should_Return_Ok()
    {
        // Arrange
        Init_Test();

        // Act
        var result = await _controller.GetGeolocation("1.0.0");

        // Assert
        result.Result.ShouldBeOfType<OkObjectResult>();
    }

    [Fact]
    public async void GetData_Should_Return_BadRequest()
    {
        // Arrange
        Init_Test();

        // Act
        var result = await _controller.GetGeolocation("1.0..0");

        // Assert
        result.Result.ShouldBeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async void GetData_Should_Return_NotFoundResult()
    {
        // Arrange
        Init_Test();
        _mockService.Setup(p => p.GetIpInformation(It.IsAny<string>())).ReturnsAsync(() => null);

        // Act
        var result = await _controller.GetGeolocation("1.0.0");

        // Assert
        result.Result.ShouldBeOfType<NotFoundObjectResult>();
    }
}
