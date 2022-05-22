using Shouldly;
using Utils;
using Xunit;

namespace Tests
{
    public class StringUtilsTests
    {

        [InlineData("1")]
        [InlineData("1.1")]
        [InlineData("1.1.1")]
        [InlineData("11.11.111")]
        [Theory]
        public void ValidateInput_Should_Return_True(string input)
        {
            var result = StringUtils.ValidateString(input);
            result.ShouldBeTrue();
        }

        [InlineData("")]
        [InlineData(null)]
        [InlineData(".1.1.1")]
        [InlineData("a.1.1")]
        [InlineData("1.1.1.")]
        [Theory]
        public void ValidateInput_Should_Return_False(string input)
        {
            var result = StringUtils.ValidateString(input);
            result.ShouldBeFalse();
        }
    }
}
