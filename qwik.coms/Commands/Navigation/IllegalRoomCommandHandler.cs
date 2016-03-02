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

        public override IEnumerable<string> Commands => new[] { "ir" };
        protected override string AolKeyword => "aol://2719:10-10-";
        protected override string RoomType => "illegal room";
    }
}