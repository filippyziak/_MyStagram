using System;

namespace MyStagram.Core.Helpers
{
    public class Utils
    {
        public static string Id() => Guid.NewGuid().ToString("N").Substring(0, 16).ToUpper();

        public static string NewGuid(int length = 16) => Guid.NewGuid().ToString().Replace("-", "").Substring(0, length);
   
        #region enum
        public static string EnumToString<T>(T value) where T : struct, IConvertible
            => Enum.GetName(typeof(T), value);

        #endregion
    }
}