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

        [InlineData("10 kms")]
        [InlineData("100 KmS")]
        [InlineData("1000 KMS")]
        [Theory]
        public void StringKmsToInt_Should_Return_Int(string input)
        {
            var result = StringUtils.StringKmsToInt(input);
            result.ShouldBeOfType<int>();
        }

        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [Theory]
        public void StringKmsToInt_Should_Return_0(string input)
        {
            var result = StringUtils.StringKmsToInt(input);
            result.ShouldBe(0);
        }
    }
}
