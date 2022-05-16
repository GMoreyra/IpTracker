using Utils;
using Xunit;

namespace IpTrackerTests
{
    public class DistanceUtilsTests
    {
        [InlineData("1", "1")]
        [InlineData("-1", "-1")]
        [InlineData("0", "0")]
        [InlineData("22.15654", "-64.1231")]
        [Theory]
        public void DistanceToBA_Should_Return_Double(string lat, string lon)
        {
            var result = DistanceUtils.DistanceToBA(lat, lon);
            Assert.IsType<double>(result);
        }

        [Fact]
        public void DistanceToBA_Should_Return_Distance()
        {
            var result = DistanceUtils.DistanceToBA("40", "-4");
            Assert.Equal(10270, result);
        }
    }
}
