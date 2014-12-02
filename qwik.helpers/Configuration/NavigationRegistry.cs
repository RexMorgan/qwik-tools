using qwik.helpers.Navigation;
using StructureMap.Configuration.DSL;

namespace qwik.helpers.Configuration
{
    public class NavigationRegistry : Registry
    {
        public NavigationRegistry()
        {
            For<IKeywordBrowser>().Use<KeywordBrowser>();
            For<IRoomNavigator>().Use<RoomNavigator>();

            For<IScreenNameRetriever>().Use<ScreenNameRetriever>();
        }
    }
}