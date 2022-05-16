using System;
using System.Collections.Generic;
using Xunit;
using Utils;

namespace Tests
{
    public class TimeUtilsTests
    {
        private readonly List<string> _utcTimeList = new()
        {
            "UTC-12:00",
            "UTC + 12:00",
            "UTC + 12:00",
            "UTC + 00:00",
            "UTC"
        };

        [Fact]
        public void TimeToUTC_Should_Return_Dictionary()
        {
            var result = TimeUtils.TimeToUTCDictionary(_utcTimeList);

            Assert.IsType<Dictionary<string, DateTime>>(result);
        }

        [Fact]
        public void TimeToUTC_Should_Contain_All_Keys()
        {
            var result = TimeUtils.TimeToUTCDictionary(_utcTimeList);
            var containsAll =
                result.ContainsKey("UTC-12:00") &&
                result.ContainsKey("UTC+12:00") &&
                result.ContainsKey("UTC+00:00") &&
                result.ContainsKey("UTC");

            Assert.True(containsAll);
        }

        [Fact]
        public void TimeToUTC_Should_Return_Date()
        {
            var result = TimeUtils.TimeToUTCDictionary(_utcTimeList);
            var dateFromDictionary = result.GetValueOrDefault("UTC+12:00");
            var dateNowUtcFormat = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
            var dateModified = dateNowUtcFormat.AddHours(12);

            Assert.Equal(dateModified.ToShortDateString(), dateFromDictionary.ToShortDateString());
        }
    }
}
