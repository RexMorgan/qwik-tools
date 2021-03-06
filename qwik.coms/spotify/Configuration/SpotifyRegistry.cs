﻿using qwik.coms.spotify.NextTrackStrategies;
using qwik.coms.spotify.Sessions;
using StructureMap;
using StructureMap.Graph;

namespace qwik.coms.spotify.Configuration
{
    public class SpotifyRegistry : Registry
    {
        public SpotifyRegistry()
        {
            For<IPlayer>().Singleton().Use<Player>();
            For<ISession>().Singleton().Use<Session>();
            For<ISettings>().Singleton().Use<Settings>();

            Scan(_ =>
            {
                _.TheCallingAssembly();
                _.AddAllTypesOf<INextTrackStrategy>();
            });
        }
    }
}