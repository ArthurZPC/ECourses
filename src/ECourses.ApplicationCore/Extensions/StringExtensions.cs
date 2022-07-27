namespace ECourses.ApplicationCore.Extensions
{
    public static class StringExtensions
    {
        public static string F(this string stringToFormat, params object?[] args)
        {
            return string.Format(stringToFormat, args);
        }
    }
}
