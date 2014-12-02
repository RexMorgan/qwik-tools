namespace qwik.helpers.Chatters
{
    public static class StringExtensions
    {
        public static ScreenName ToScreenName(this string screenname)
        {
            return new ScreenName(screenname);
        }
    }
}