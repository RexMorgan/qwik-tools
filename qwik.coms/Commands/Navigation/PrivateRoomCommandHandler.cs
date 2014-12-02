using qwik.coms.Output;
using qwik.helpers.Navigation;
using qwik.helpers.Settings;
using System.Collections.Generic;

namespace qwik.coms.Commands.Navigation
{
    public class PrivateRoomCommandHandler : BaseChatChangingCommandHandler
    {
        public PrivateRoomCommandHandler(IOutput output, IAppSettings settings, IRoomNavigator roomNavigator) 
            : base(output, settings, roomNavigator)
        {
        }

        public override IEnumerable<string> Commands
        {
            get { return new[] {"pr"}; }
        }

        protected override string AolKeyword
        {
            get { return "aol://2719:2-2-"; }
        }

        protected override string RoomType
        {
            get { return "private room"; }
        }
    }
}