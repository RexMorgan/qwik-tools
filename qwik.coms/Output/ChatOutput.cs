using qwik.helpers;
using qwik.helpers.Settings;
using System.Text;

namespace qwik.coms.Output
{
    public class ChatOutput : IOutput
    {
        private readonly IAppSettings _settings;

        public ChatOutput(IAppSettings settings)
        {
            _settings = settings;
        }

        public void Formatted(string message, params object[] args)
        {
            var builder = new StringBuilder();
            builder.Append(LeftAscii());
            builder.AppendFormat(message, args);
            Chat.Send(builder.ToString());
        }

        private string LeftAscii()
        {
            var ascii = _settings.LeftAscii + " ";
            if (string.IsNullOrWhiteSpace(_settings.LeftAscii)) return string.Empty;
            if (!string.IsNullOrWhiteSpace(_settings.LeftAsciiColor))
                ascii = string.Format("<font color=\"{1}\">{0}</font>", ascii, _settings.LeftAsciiColor);
            if (!string.IsNullOrWhiteSpace(_settings.LeftAsciiFont))
                ascii = string.Format("<font face=\"{1}\">{0}</font>", ascii, _settings.LeftAsciiFont);
            if (_settings.LeftAsciiBold)
                ascii = string.Format("<b>{0}</b>", ascii);
            if (_settings.LeftAsciiItalic)
                ascii = string.Format("<i>{0}</i>", ascii);
            if (_settings.LeftAsciiUnderline)
                ascii = string.Format("<u>{0}</u>", ascii);
            return ascii;
        }
    }
}