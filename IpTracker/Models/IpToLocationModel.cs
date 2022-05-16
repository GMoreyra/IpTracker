﻿using System.Collections.Generic;
using Utils;

namespace IpTracker.Models
{
    public class IpToLocationModel
    {
        public string Country_name { get; set; }
        public string Country_code { get; set; }
        public List<string> Timezones { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public List<CurrencyModel> Currencies { get; set; }
        public List<string> CurrenciesDollarValue { get; set; }

        public class CurrencyModel
        {
            public string Code { get; set; }
        }

        public List<string> CurrenciesList
        {
            get
            {
                var _CurrenciesList = new List<string>();
                Currencies.ForEach(x => _CurrenciesList.Add(x.Code.ToString()));
                return _CurrenciesList;                
            }
        }

        public string GetDistance()
        {
            return DistanceUtils.DistanceToBA(Latitude, Longitude).ToString() + " KMs";
        }

        public string GetCurrency()
        {
            var currency = string.Empty;
            var index = 0;

            CurrenciesList.ForEach(c =>
            {
                currency = c + " (1 " + c + " = " + CurrenciesDollarValue[index++] + " U$S) o";
            });

            return currency.TrimEnd('o', ' ');
        }

        public string GetTimeZone()
        {
            var dictionary = TimeUtils.TimeToUTCDictionary(Timezones);

            return TimeUtils.DictionaryToString(dictionary);
        }
    }
}
