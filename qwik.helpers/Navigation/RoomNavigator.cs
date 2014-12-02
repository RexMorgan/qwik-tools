using System;
using System.Threading;

namespace qwik.helpers.Navigation
{
    public class RoomNavigator : IRoomNavigator
    {
        private readonly IKeywordBrowser _keywordBrowser;

        public RoomNavigator(IKeywordBrowser keywordBrowser)
        {
            _keywordBrowser = keywordBrowser;
        }

        public bool Navigate(string roomKeyword)
        {
            Chat.Close();
            _keywordBrowser.Browse(roomKeyword);

            var stopChecking = DateTime.Now.Add(TimeSpan.FromSeconds(5));
            var chat = Chat.ChatEditBox();
            while (chat == IntPtr.Zero && stopChecking > DateTime.Now)
            {
                chat = Chat.ChatEditBox();
                Thread.Sleep(10);
            }
            Thread.Sleep(150);
            return chat != IntPtr.Zero;
        }
    }
}