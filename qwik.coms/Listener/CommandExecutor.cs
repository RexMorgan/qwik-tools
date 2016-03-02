using qwik.chatscan;
using qwik.coms.Commands;
using qwik.coms.Output;
using qwik.helpers.Settings;
using System;
using System.Collections.Generic;

namespace qwik.coms.Listener
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly IDictionary<string, ICommandHandler> _handlerLookup = new Dictionary<string, ICommandHandler>();
        private readonly IAppSettings _settings;
#if DEBUG
        private readonly IOutput _output;

        public CommandExecutor(IEnumerable<ICommandHandler> commandHandlers, IAppSettings settings, IOutput output)
        {
            _settings = settings;
            _output = output;
            foreach (var handler in commandHandlers)
            {
                foreach (var command in handler.Commands)
                {
                    _handlerLookup.Add(command, handler);
                }
            }
        }
#else
        public CommandExecutor(IEnumerable<ICommandHandler> commandHandlers, IAppSettings settings)
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
#endif



        public void Execute(ChatMessage message)
        {
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
            catch(Exception ex)
            {
#if DEBUG
                _output.Output(ex.Message);
#endif
            }
        }
    }
}