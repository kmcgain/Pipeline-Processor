namespace Pipeline.Model.Extensions
{
    public static class StringFormatExtensions
    {
        public static string WithParams(this string template, params object[] subs)
        {
            return string.Format(template, subs);
        }
    }
}