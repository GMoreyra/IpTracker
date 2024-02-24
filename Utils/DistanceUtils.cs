using System;
using System.Globalization;
using System.Threading;

namespace Utils;

static public class DistanceUtils
{
    private static readonly double _latBA = -34;
    private static readonly double _lonBA = -64;

    private static double Deg2rad(double deg)
    {
        return (deg * Math.PI / 180.0);
    }
    private static double Rad2deg(double rad)
    {
        return (rad / Math.PI * 180.0);
    }

    private static double StringToDouble(string s)
    {
        var systemSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator[0];
        double result = 0;

        if (!string.IsNullOrEmpty(s))
        {
            if (!s.Contains(","))
            {
                result = double.Parse(s, CultureInfo.InvariantCulture);
            }
            else
            {
                result = Convert.ToDouble(s.Replace(".", systemSeparator.ToString()).Replace(",", systemSeparator.ToString()));
            }
        }

        return result;
    }

    static public double? DistanceToBA(string latitude, string longitude)
    {
        if (string.IsNullOrWhiteSpace(latitude) || string.IsNullOrWhiteSpace(longitude))
        {
            return null;
        }

        var lat = StringToDouble(latitude);
        var lon = StringToDouble(longitude);

        lat = Math.Round(lat);
        lon = Math.Round(lon);

        if ((lat == _latBA) && (lon == _lonBA))
        {
            return 0;
        }
        else
        {
            double theta = _lonBA - lon;
            double dist =
                Math.Sin(Deg2rad(_latBA)) * Math.Sin(Deg2rad(lat)) +
                Math.Cos(Deg2rad(_latBA)) * Math.Cos(Deg2rad(lat)) *
                Math.Cos(Deg2rad(theta));
            dist = Math.Acos(dist);
            dist = Rad2deg(dist);
            dist *= 60 * 1.151 * 1.6094;

            return Math.Round(dist);
        }
    }
}
