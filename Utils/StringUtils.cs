using System.Text.RegularExpressions;

namespace Utils
{
    static public class StringUtils
    {
        static public bool ValidateString(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            var rx = new Regex(@"^\d+(\.\d+)*$");
            return rx.IsMatch(input);
        }

        static public int StringKmsToInt(string input)
        {
            string result = input?.ToUpper()?.Replace(" KMS", "");
            return int.Parse(result);
        }
    }
}