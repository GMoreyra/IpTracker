using Utils;
using Xunit;
using Shouldly;

namespace Tests;

public class DistanceUtilsTests
{
    [InlineData("", "")]
    [InlineData(" ", " ")]
    [InlineData(null, null)]
    [InlineData(null, "1")]
    [InlineData("1", null)]
    [Theory]
    public void DistanceToBA_Should_Return_0(string lat, string lon)
    {
        var result = DistanceUtils.DistanceToBA(lat, lon);

        result.ShouldBeNull();
    }

    [InlineData("1", "1")]
    [InlineData("-1", "-1")]
    [InlineData("0", "0")]
    [InlineData("22.15654", "-64.1231")]
    [Theory]
    public void DistanceToBA_Should_Return_Double(string lat, string lon)
    {
        var result = DistanceUtils.DistanceToBA(lat, lon);

        result.ShouldBeOfType<double>();
    }

    [Fact]
    public void DistanceToBA_Should_Return_Distance()
    {
        var result = DistanceUtils.DistanceToBA("40", "-4");

        result.ShouldBe(10270);
    }
}
