using System.Collections.Generic;
using Utils;

namespace Domain.Models
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

        private readonly string _missingData = "Missing information";

        public List<string> GetCurrenciesList()
        {
            if (Currencies == null || Currencies.Count == 0)
            {
                return new List<string>();
            }

            var _currenciesList = new List<string>();
            Currencies.ForEach(x => _currenciesList.Add(x.Code.ToString()));

            return _currenciesList;
        }

        public string GetDistance()
        {
            if (string.IsNullOrWhiteSpace(Latitude) || string.IsNullOrWhiteSpace(Longitude))
            {
                return _missingData;
            }

            return $"{DistanceUtils.DistanceToBA(Latitude, Longitude)} KMs";
        }

        public string GetCurrency()
        {
            var currency = string.Empty;
            var index = 0;
            var currencyList = GetCurrenciesList();

            if ((CurrenciesDollarValue == null || CurrenciesDollarValue.Count == 0) && currencyList.Count == 0)
            {
                return _missingData;
            }

            currencyList.ForEach(c =>
            {
                currency = $"{c} (1 {c} = {CurrenciesDollarValue[index++]} U$S) o ";
            });

            return currency.TrimEnd('o', ' ');
        }

        public string GetTimeZone()
        {
            if (Timezones == null || Timezones.Count == 0)
            {
                return _missingData;
            }

            var dictionary = TimeUtils.TimeToUTCDictionary(Timezones);

            return TimeUtils.DictionaryToString(dictionary);
        }
    }
}

