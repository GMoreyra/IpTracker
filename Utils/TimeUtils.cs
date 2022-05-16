using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    static public class TimeUtils
    {
        static public Dictionary<string, DateTime> TimeToUTCDictionary(List<string> time)
        {
            var utcTimes = new Dictionary<string, DateTime>();
            DateTime dateNowUtcFormat = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);

            time.ForEach(time =>
            {
                if (time == "UTC")
                {
                    utcTimes.Add("UTC", dateNowUtcFormat);
                }
                else
                {
                    var timeNoWhiteSpace = string.Concat(time.Where(c => !char.IsWhiteSpace(c)));
                    var onlyNumber = timeNoWhiteSpace.Replace("UTC", "").Replace(":00", "");
                    var dateModified = dateNowUtcFormat.AddHours(double.Parse(onlyNumber));

                    if (!utcTimes.ContainsKey(timeNoWhiteSpace))
                    {
                        utcTimes.Add(timeNoWhiteSpace, dateModified);
                    }
                }
            });

            return utcTimes;
        }

        static public string DictionaryToString(Dictionary<string, DateTime> dictionary)
        {
            var dictionaryString = string.Empty;
            foreach (var keyValues in dictionary)
            {
                dictionaryString += keyValues.Value + " (" + keyValues.Key + ") o ";
            }

            return dictionaryString.TrimEnd('o', ' ');
        }
    }
}
