using qwik.helpers.Settings;
using StructureMap;

namespace qwik.helpers.Configuration
{
    public class AppSettingsRegistry : Registry
    {
        public AppSettingsRegistry()
        {
            For<IAppSettingsBuilder>().Singleton().Use<AppSettingsBuilder>();
            For<IAppSettingsWriter>().Singleton().Use<AppSettingsWriter>();

            For<IAppSettings>().Singleton().Use(ctx => ctx.GetInstance<IAppSettingsBuilder>().ReadSettings());
        }
    }
}