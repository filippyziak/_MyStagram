using System.Text.RegularExpressions;

namespace MyStagram.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool HasWhitespaces(this string value)
                  => string.IsNullOrWhiteSpace(value) || value.Contains(" ");

        public static bool IsEmailAddress(this string value)
            => Regex.Match(value, @"(^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$)").Success;
    }
}