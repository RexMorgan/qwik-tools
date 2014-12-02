using System.Text.RegularExpressions;

namespace qwik.helpers
{
    public static class Regexes
    {
        public static readonly Regex Spaces = new Regex(@"\s", RegexOptions.Compiled);
        public static readonly Regex HotKey = new Regex("&", RegexOptions.Compiled);
    }
}