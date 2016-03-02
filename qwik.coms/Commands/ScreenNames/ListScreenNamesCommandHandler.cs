using qwik.chatscan;
using qwik.coms.Output;
using qwik.helpers.Settings;
using System.Collections.Generic;
using System.Linq;

namespace qwik.coms.Commands.ScreenNames
{
    public class ListScreenNamesCommandHandler : BaseCommandHandler
    {
        private readonly IAppSettings _settings;
        private readonly IOutput _output;

        public ListScreenNamesCommandHandler(IAppSettings settings, IOutput output)
        {
            _settings = settings;
            _output = output;
        }

        public override IEnumerable<string> Commands => new[] { "sns?" };

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            var screennames = string.Join(", ", _settings.ScreenNames.Select(x => x.Readable).ToArray());
            _output.Output($"ScreenNames: {screennames}");
        }
    }
}