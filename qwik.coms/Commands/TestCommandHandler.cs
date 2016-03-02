using qwik.chatscan;
using qwik.coms.Output;
using qwik.helpers.Navigation;
using System.Collections.Generic;
using System.Diagnostics;

namespace qwik.coms.Commands
{
    public class TestCommandHandler : BaseCommandHandler
    {
        private readonly IKeywordBrowser _keywordBrowser;
        private readonly IOutput _output;

        public TestCommandHandler(IOutput output, IKeywordBrowser keywordBrowser)
        {
            _output = output;
            _keywordBrowser = keywordBrowser;
        }

        public override IEnumerable<string> Commands => new[] {"test"};

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            var stopwatch = Stopwatch.StartNew();
            _keywordBrowser.Browse("welcome");
            stopwatch.Stop();
            _output.Output($"Command Completed in {stopwatch.ElapsedMilliseconds}ms");
        }
    }
}