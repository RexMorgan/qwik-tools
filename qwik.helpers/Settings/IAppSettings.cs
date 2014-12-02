using qwik.helpers.Chatters;
using qwik.helpers.Notes;
using qwik.helpers.Secretary;
using System.Collections.Generic;

namespace qwik.helpers.Settings
{
    public interface IAppSettings
    {
        string Handle { get; set; }
        IList<ScreenName> ScreenNames { get; }
        string Trigger { get; set; }

        IList<Note> Notes { get; }

        IList<Message> Messages { get; }
        int MaxMessages { get; set; }
        int MessagesPerScreenName { get; set; }

        string LeftAscii { get; set; }
        string LeftAsciiColor { get; set; }
        string LeftAsciiFont { get; set; }
        bool LeftAsciiBold { get; set; }
        bool LeftAsciiItalic { get; set; }
        bool LeftAsciiUnderline { get; set; }

        string RightAscii { get; set; }
        string RightAsciiColor { get; set; }
        string RightAsciiFont { get; set; }
        bool RightAsciiBold { get; set; }
        bool RightAsciiItalic { get; set; }
        bool RightAsciiUnderline { get; set; }
        
        bool Secretary { get; set; }

        string SpotifyPlaylist { get; set; }
    }
}