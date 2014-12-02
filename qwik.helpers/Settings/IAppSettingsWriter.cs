namespace qwik.helpers.Settings
{
    public interface IAppSettingsWriter
    {
        void SaveSettings(IAppSettings appSettings);
    }
}