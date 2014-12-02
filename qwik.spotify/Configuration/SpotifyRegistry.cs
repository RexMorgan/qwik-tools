using qwik.spotify.Sessions;
using StructureMap.Configuration.DSL;

namespace qwik.spotify.Configuration
{
    public class SpotifyRegistry : Registry
    {
        public SpotifyRegistry()
        {
            For<IPlayer>().Singleton().Use<Player>();
            For<ISession>().Singleton().Use<Session>();
            For<ISettings>().Singleton().Use<Settings>();
        }
    }
}