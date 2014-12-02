using qwik.helpers.Navigation;
using System;
using System.Linq;

namespace qwik.helpers
{
    public class ScreenNameRetriever : IScreenNameRetriever
    {
        private const string WelcomeMessage = "Welcome, ";

        private readonly IKeywordBrowser _keywordBrowser;

        public ScreenNameRetriever(IKeywordBrowser keywordBrowser)
        {
            _keywordBrowser = keywordBrowser;
        }

        public string GetScreenName()
        {
            var screenname = GrabFromTitle();
            if (!string.IsNullOrWhiteSpace(screenname)) return screenname;

            _keywordBrowser.Browse("welcome");

            var stopWaiting = DateTime.Now.Add(TimeSpan.FromSeconds(5));
            do
            {
                screenname = GrabFromTitle();
                if (!string.IsNullOrWhiteSpace(screenname)) return screenname;
            } while (DateTime.Now <= stopWaiting);
            return null;
        }

        private string GrabFromTitle()
        {
            return (from child in Windows.ChildWindows()
                select Windows.GetWindowTitle(child)
                into title
                where title.StartsWith(WelcomeMessage)
                select title.Substring(WelcomeMessage.Length).TrimEnd('!')).FirstOrDefault();
        }
    }

    public interface IScreenNameRetriever
    {
        string GetScreenName();
    }
}