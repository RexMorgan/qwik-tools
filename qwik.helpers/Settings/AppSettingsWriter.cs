using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace qwik.helpers.Settings
{
    public class AppSettingsWriter : IAppSettingsWriter
    {
        public void SaveSettings(IAppSettings appSettings)
        {
            var settingsText = JsonConvert.SerializeObject(appSettings, Formatting.Indented);
            File.WriteAllText("settings.json", settingsText, Encoding.UTF8);
        }
    }
}