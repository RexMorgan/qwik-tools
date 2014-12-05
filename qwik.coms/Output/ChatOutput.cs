using qwik.helpers;
using qwik.helpers.Settings;
using qwik.helpers.Timer;
using System;
using System.Collections.Concurrent;
using System.Text;

namespace qwik.coms.Output
{
    public class ChatOutput : IOutput
    {
        private static readonly ConcurrentQueue<string> ChatMessages = new ConcurrentQueue<string>();

        private readonly IChatRateLimiter _chatRateLimiter;

        private void SendChatMessage(TimeSpan arg1, TimeSpan arg2)
        {
            if (ChatMessages.IsEmpty) return;
            if (_chatRateLimiter.IsRateLimited()) return;

            string chatMessage;
            if (ChatMessages.TryDequeue(out chatMessage))
            {
                Chat.Send(chatMessage);
                _chatRateLimiter.MessageSent();
            }
        }

        private readonly IAppSettings _settings;

        public ChatOutput(IAppSettings settings, IChatRateLimiter chatRateLimiter)
        {
            TimerFactory.CreateUnmanaged(TimeSpan.FromMilliseconds(100), SendChatMessage);

            _settings = settings;
            _chatRateLimiter = chatRateLimiter;
        }

        public void Formatted(string message, params object[] arguments)
        {
            var builder = new StringBuilder();
            builder.Append(LeftAscii());
            builder.AppendFormat(message, arguments);
            ChatMessages.Enqueue(builder.ToString());
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