﻿using qwik.coms.Output;
using qwik.helpers.Navigation;
using qwik.helpers.Settings;
using System.Collections.Generic;

namespace qwik.coms.Commands.Navigation
{
    public class AimPrivateRoomCommandHandler : BaseChatChangingCommandHandler
    {
        public AimPrivateRoomCommandHandler(IOutput output, IAppSettings settings, IRoomNavigator roomNavigator) 
            : base(output, settings, roomNavigator)
        {
        }

        public override IEnumerable<string> Commands => new[] {"apr"};
        protected override string AolKeyword => "aol://2719:10-4-";
        protected override string RoomType => "aim chat";
    }
}