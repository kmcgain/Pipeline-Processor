namespace Pipeline.Model.Extensions
{
    using System;

    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string raw, bool ignoreCase = true)
        {
            if (raw == null)
                throw new ArgumentNullException("raw");

            return (T) Enum.Parse(typeof (T), raw, ignoreCase);
        }
    }
}