using qwik.coms.Output;
using qwik.helpers.Navigation;
using qwik.helpers.Settings;
using System.Collections.Generic;

namespace qwik.coms.Commands.Navigation
{
    public class IllegalRoomCommandHandler : BaseChatChangingCommandHandler
    {
        public IllegalRoomCommandHandler(IOutput output, IAppSettings settings, IRoomNavigator roomNavigator) 
            : base(output, settings, roomNavigator)
        {
        }

        public override IEnumerable<string> Commands
        {
            get { return new[] { "ir" }; }
        }

        protected override string AolKeyword
        {
            get { return "aol://2719:10-10-"; }
        }

        protected override string RoomType
        {
            get { return "illegal room"; }
        }
    }
}