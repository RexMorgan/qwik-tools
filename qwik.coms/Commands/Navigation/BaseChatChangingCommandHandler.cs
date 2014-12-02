using qwik.chatscan;
using qwik.coms.Output;
using qwik.helpers.Navigation;
using qwik.helpers.Settings;

namespace qwik.coms.Commands.Navigation
{
    public abstract class BaseChatChangingCommandHandler : BaseCommandHandler
    {
        private readonly IOutput _output;
        private readonly IRoomNavigator _roomNavigator;
        private readonly IAppSettings _settings;

        protected BaseChatChangingCommandHandler(IOutput output, IAppSettings settings, IRoomNavigator roomNavigator)
        {
            _output = output;
            _settings = settings;
            _roomNavigator = roomNavigator;
        }

        protected abstract string AolKeyword { get; }
        protected abstract string RoomType { get; }

        public override bool RequiresArguments
        {
            get { return true; }
        }

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            _output.Formatted("Leaving for {1}: {0}", arguments, RoomType);
            _roomNavigator.Navigate(string.Format("{0}{1}", AolKeyword, arguments));
            _output.Formatted("[{0}] joined {1}: {2}", _settings.Handle, RoomType, arguments);
        }
    }
}