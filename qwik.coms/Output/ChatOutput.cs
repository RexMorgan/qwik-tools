using qwik.helpers;
using qwik.helpers.Settings;
using qwik.helpers.Timer;
using System;
using System.Collections.Concurrent;
using JetBrains.Annotations;

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

        public void Output(string message)
        {
            ChatMessages.Enqueue($"{LeftAscii()}{message}");
        }

        public void OutputFormatted(string message, params object[] arguments)
        {
            Output(string.Format(message, arguments));
        }

        private string LeftAscii()
        {
            var ascii = _settings.LeftAscii + " ";
            if (string.IsNullOrWhiteSpace(_settings.LeftAscii)) return string.Empty;
            if (!string.IsNullOrWhiteSpace(_settings.LeftAsciiColor))
                ascii = $"<font color=\"{_settings.LeftAsciiColor}\">{ascii}</font>";
            if (!string.IsNullOrWhiteSpace(_settings.LeftAsciiFont))
                ascii = $"<font face=\"{_settings.LeftAsciiFont}\">{ascii}</font>";
            if (_settings.LeftAsciiBold)
                ascii = $"<b>{ascii}</b>";
            if (_settings.LeftAsciiItalic)
                ascii = $"<i>{ascii}</i>";
            if (_settings.LeftAsciiUnderline)
                ascii = $"<u>{ascii}</u>";
            return ascii;
        }
    }
}