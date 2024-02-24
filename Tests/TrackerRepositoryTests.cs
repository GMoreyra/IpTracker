using Data.Repositories;
using Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using Shouldly;
using Microsoft.Extensions.Caching.Distributed;
using Data.Interfaces;

namespace Tests;

public class TrackerRepositoryTests
{
    private Mock<IDistributedCache> _distributedCache;
    private TrackerRepository _repo;
    private List<StatisticModel> _statisticsList;

    private void Init_Test()
    {
        _distributedCache = new Mock<IDistributedCache>();
        _repo = new TrackerRepository(_distributedCache.Object);
        _statisticsList =
        [
            new() {
                CountryName = "Argentina",
                DistanceToBaInKms = 100,
                InvocationCounter = 2
            },
            new() {
                CountryName = "Colombia",
                DistanceToBaInKms = 50,
                InvocationCounter = 1
            },
            new() {
                CountryName = "Peru",
                DistanceToBaInKms = 10,
                InvocationCounter = 3
            },
            new() {
                CountryName = "Canada",
                DistanceToBaInKms = 1001,
                InvocationCounter = 3
            },
            new() {
                CountryName = "Bolivia",
                DistanceToBaInKms = 1001,
                InvocationCounter = 5
            },
            new() {
                CountryName = "Paraguay",
                DistanceToBaInKms = 1001,
                InvocationCounter = 4
            }
        ];
    }

    [Fact]
    public void Should_Not_Throw()
    {
        Action act = () => Init_Test();
        act.ShouldNotThrow();
    }

    [Fact]
    public void ReturnMaxMinStatistics_Should_Return_List()
    {
        Init_Test();
        var result = _repo.ReturnMaxMinStatistics(_statisticsList);
        result.ShouldBeOfType<List<StatisticModel>>();
    }

    [Fact]
    public void ReturnMaxMinStatistics_Should_Return_Max_And_Min()
    {
        Init_Test();
        var result = _repo.ReturnMaxMinStatistics(_statisticsList);
        result[0].CountryName.ShouldBeOneOf("Peru", "Bolivia");
        result[1].CountryName.ShouldBeOneOf("Peru", "Bolivia");
    }

    [Fact]
    public void ReturnMaxMinStatistics_Should_Return_Empty_List()
    {
        Init_Test();
        var result = _repo.ReturnMaxMinStatistics([]);
        result.Count.ShouldBe(0);
    }
}
