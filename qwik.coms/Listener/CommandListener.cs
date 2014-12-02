using qwik.chatscan;
using qwik.coms.Commands;
using qwik.helpers.Settings;
using System.Collections.Generic;

namespace qwik.coms.Listener
{
    public class CommandListener : ICommandListener
    {
        private readonly IDictionary<string, ICommandHandler> _handlerLookup = new Dictionary<string, ICommandHandler>();
        private readonly IAppSettings _settings;

        public CommandListener(IEnumerable<ICommandHandler> commandHandlers, IAppSettings settings)
        {
            _settings = settings;
            foreach (var handler in commandHandlers)
            {
                foreach (var command in handler.Commands)
                {
                    _handlerLookup.Add(command, handler);
                }
            }
        }

        public void Start()
        {
            ChatScanner.Instance.NewMessage += MessageHandler;
            ChatScanner.Instance.Start();
        }

        public void Stop()
        {
            ChatScanner.Instance.NewMessage -= MessageHandler;
        }

        private void MessageHandler(ChatMessage message)
        {
            if (!message.FromScreenNames(_settings.ScreenNames)) return;

            var trigger = _settings.Trigger;
            if (!message.Trigger(trigger)) return;
            var command = message.GetCommand(trigger);
            if (string.IsNullOrWhiteSpace(command)) return;
            if (!_handlerLookup.ContainsKey(command)) return;
            var handler = _handlerLookup[command];
            var arguments = message.GetArguments(command, trigger);
            if (handler.RequiresArguments && string.IsNullOrWhiteSpace(arguments)) return;
            try
            {
                handler.Execute(arguments, command, message);
            }
            catch
            {
            }
        }
    }
}