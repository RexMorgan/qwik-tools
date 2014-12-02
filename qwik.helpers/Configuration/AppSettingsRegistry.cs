using qwik.helpers.Settings;
using StructureMap.Configuration.DSL;

namespace qwik.helpers.Configuration
{
    public class AppSettingsRegistry : Registry
    {
        public AppSettingsRegistry()
        {
            For<IAppSettingsBuilder>().Singleton().Use<AppSettingsBuilder>();
            For<IAppSettingsWriter>().Singleton().Use<AppSettingsWriter>();

            For<IAppSettings>().Singleton().UseSpecial(c => c.ConstructedBy("Building up settings", ctx =>
            {
                var builder = ctx.GetInstance<IAppSettingsBuilder>();
                return builder.ReadSettings();
            }));
        }
    }
}