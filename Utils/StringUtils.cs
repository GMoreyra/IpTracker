using System.Text.RegularExpressions;

namespace Utils;

static public class StringUtils
{
    static public int StringKmsToInt(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return 0;
        }

        string result = input?.ToUpper()?.Replace(" KMS", "");
        return int.Parse(result);
    }
}