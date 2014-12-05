using qwik.chatscan;
using qwik.helpers.Settings;

namespace qwik.coms.Listener
{
    public class CommandListener : ICommandListener
    {
        private readonly IAppSettings _settings;
        private readonly ICommandExecutor _commandExecutor;

        public CommandListener(IAppSettings settings, ICommandExecutor commandExecutor)
        {
            _settings = settings;
            _commandExecutor = commandExecutor;
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
            _commandExecutor.Execute(message);
        }
    }
}