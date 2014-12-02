using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace qwik.helpers.Settings
{
    public class AppSettingsBuilder : IAppSettingsBuilder
    {
        public AppSettings ReadSettings()
        {
            try
            {
                var settingsText = File.ReadAllText("settings.json", Encoding.UTF8);
                return JsonConvert.DeserializeObject<AppSettings>(settingsText);
            }
            catch (FileNotFoundException)
            {
                return new AppSettings();
            }
        }
    }
}