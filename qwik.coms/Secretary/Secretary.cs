using qwik.chatscan;
using qwik.coms.idler;
using qwik.coms.Listener;
using qwik.coms.Output;
using qwik.helpers.Secretary;
using qwik.helpers.Settings;
using System;
using System.Linq;

namespace qwik.coms.Secretary
{
    public class Secretary : ISecretary
    {
        private readonly IAppSettings _settings;
        private readonly IAppSettingsWriter _settingsWriter;
        private readonly IOutput _output;
        private readonly IIdler _idler;

        public Secretary(IAppSettings settings, IAppSettingsWriter settingsWriter, IOutput output, IIdler idler)
        {
            _settings = settings;
            _settingsWriter = settingsWriter;
            _output = output;
            _idler = idler;
        }

        public void Start()
        {
            _idler.IdlingStarted += StartTakingMessages;
            _idler.IdlingStopped += StopTakingMessages;
            if (_idler.IsIdling) StartTakingMessages(null);
        }

        public void Stop()
        {
            _idler.IdlingStarted -= StartTakingMessages;
            _idler.IdlingStopped -= StopTakingMessages;
            if (_idler.IsIdling) StopTakingMessages(TimeSpan.Zero);
        }

        private void StartTakingMessages(string reason)
        {
            ChatScanner.Instance.NewMessage += TakeMessage;
            _idler.IdlingNotification += LeaveMessageBlurb;
        }

        private void StopTakingMessages(TimeSpan idlingDuration)
        {
            ChatScanner.Instance.NewMessage -= TakeMessage;
            _idler.IdlingNotification -= LeaveMessageBlurb;
        }

        private void LeaveMessageBlurb(TimeSpan x, TimeSpan y)
        {
            _output.Formatted("Leave a message with my secretary, type .{0} <i>msg</i>", _settings.Handle);
        }

        private void TakeMessage(ChatMessage chatMessage)
        {
            var messageTrigger = string.Format(".{0} ", _settings.Handle).ToLower();
            if (!chatMessage.Trigger(messageTrigger)) return;

            if (_settings.Messages.Count >= _settings.MaxMessages)
            {
                _output.Formatted("Maximum number of messages reached: {0}", _settings.MaxMessages);
                return;
            }

            var messagesLeftByScreenName = _settings.Messages.Count(x => x.Sender.Equals(chatMessage.Sender));
            if (messagesLeftByScreenName >= _settings.MessagesPerScreenName)
            {
                _output.Formatted("You can't leave anymore messages, {0} ({1}/{2})", chatMessage.Sender.Formatted, messagesLeftByScreenName, _settings.MessagesPerScreenName);
                return;
            }

            var message = new Message(chatMessage.Sender, chatMessage.Message.Substring(messageTrigger.Length));
            _settings.Messages.Add(message);
            _settingsWriter.SaveSettings(_settings);
            _output.Formatted("Your message has been saved, {0} ({1}/{2})", chatMessage.Sender.Formatted, ++messagesLeftByScreenName, _settings.MessagesPerScreenName);
        }
    }
}