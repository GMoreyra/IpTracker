using Domain.Models;
using System.Collections.Generic;

namespace Tests;

static public class IpToLocationMock
{
    static public IpToLocationModel GetIpToLocationModelMock()
    {
        return new IpToLocationModel
        {
            Country_name = "Argentina",
            Country_code = "ARG",
            Currencies = new List<IpToLocationModel.CurrencyModel>()
            {
                new IpToLocationModel.CurrencyModel()
                {
                    Code = "ARS"
                }
            },
            CurrenciesDollarValue = new List<string>()
            {
                "200"
            },
            Latitude = "-34",
            Longitude = "-58",
            Timezones = new List<string>()
            {
                "UTC-3", "UTC-2", "UTC"
            }
        };
    }
}
