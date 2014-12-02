using System.Management;

namespace qwik.helpers.System
{
    public class SystemInformationRetriever
    {
        public static SystemInformation GetSystemInformation()
        {
            var systemInfo = new SystemInformation();
                //win32CompSys = new ManagementObjectSearcher("select * from Win32_ComputerSystem"),
                //win32Memory = new ManagementObjectSearcher("select * from Win32_PhysicalMemory")
            using (var processor = new ManagementObjectSearcher("select * from Win32_Processor"))
            {
                foreach (ManagementObject obj in processor.Get())
                {
                    systemInfo.ClockSpeed = obj["CurrentClockSpeed"].ToString();
                    systemInfo.MaxClockSpeed = obj["MaxClockSpeed"].ToString();
                    systemInfo.ProcessorName = obj["Name"].ToString();
                    systemInfo.Manufacturer = obj["Manufacturer"].ToString();
                    systemInfo.Version = obj["Version"].ToString();
                }
            }

            return systemInfo;
        }
    }

    public class SystemInformation
    {
        public string ClockSpeed { get; set; }
        public string MaxClockSpeed { get; set; }
        public string ProcessorName { get; set; }
        public string Manufacturer { get; set; }
        public string Version { get; set; }
    }
}