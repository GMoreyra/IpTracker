using Domain.Models;

namespace Tests;

static public class IpToLocationMock
{
    static public IpToLocationModel GetIpToLocationModelMock()
    {
        return new IpToLocationModel
        {
            Country_name = "Argentina",
            Country_code = "ARG",
            Currencies =
            [
                new()
                {
                    Code = "ARS"
                }
            ],
            CurrenciesDollarValue =
            [
                "200"
            ],
            Latitude = "-34",
            Longitude = "-58",
            Timezones =
            [
                "UTC-3", "UTC-2", "UTC"
            ]
        };
    }
}
