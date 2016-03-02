using qwik.helpers.Chatters;
using qwik.helpers.Notes;
using qwik.helpers.Secretary;
using System.Collections.Generic;

namespace qwik.helpers.Settings
{
    public class AppSettings : IAppSettings
    {
        public AppSettings()
        {
            ScreenNames = new List<ScreenName>();
            Notes = new List<Note>();
            Messages = new List<Message>();

            MaxMessages = 20;
            MessagesPerScreenName = 2;
        }

        public string Handle { get; set; }
        public IList<ScreenName> ScreenNames { get; }

        private string _trigger;

        public string Trigger
        {
            get { return string.IsNullOrEmpty(_trigger) ? string.Empty : _trigger; }
            set { _trigger = value; }
        }

        public IList<Note> Notes { get; }
        public IList<Message> Messages { get; }
        public int MaxMessages { get; set; }
        public int MessagesPerScreenName { get; set; }

        public string LeftAscii { get; set; }
        public string LeftAsciiColor { get; set; }
        public string LeftAsciiFont { get; set; }
        public bool LeftAsciiBold { get; set; }
        public bool LeftAsciiItalic { get; set; }
        public bool LeftAsciiUnderline { get; set; }

        public string RightAscii { get; set; }
        public string RightAsciiColor { get; set; }
        public string RightAsciiFont { get; set; }
        public bool RightAsciiBold { get; set; }
        public bool RightAsciiItalic { get; set; }
        public bool RightAsciiUnderline { get; set; }
        
        public bool Secretary { get; set; }

        public string SpotifyPlaylist { get; set; }
    }
}