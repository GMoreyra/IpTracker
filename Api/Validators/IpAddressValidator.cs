namespace Api.Validators;

using System.Text.RegularExpressions;

/// <summary>
/// Provides functionality to validate IP addresses.
/// </summary>
public static partial class IpAddressValidator
{
    /// <summary>
    /// Validates if the provided string is a valid IP address.
    /// </summary>
    /// <param name="ipAddress">The string to validate.</param>
    /// <returns>True if the string is a valid IP address, otherwise false.</returns>
    static public bool IsIpAddressValid(string ipAddress)
    {
        if (string.IsNullOrEmpty(ipAddress))
        {
            return false;
        }

        var rx = IpAdressRegex();

        return rx.IsMatch(ipAddress);
    }

    /// <summary>
    /// Provides a regular expression for IP address validation.
    /// </summary>
    /// <returns>A Regex object for IP address validation.</returns>
    [GeneratedRegex(@"^\d+(\.\d+)*$")]
    private static partial Regex IpAdressRegex();
}
