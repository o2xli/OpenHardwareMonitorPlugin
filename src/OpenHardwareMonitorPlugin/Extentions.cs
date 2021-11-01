namespace Loupedeck.OpenHardwareMonitorPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal static class Extentions
    {
        public static SensorData SelectSensorData(this List<SensorData> sensors, String identifier)
        {
            return sensors?.FirstOrDefault(s => s.Identifier.Equals(identifier, StringComparison.OrdinalIgnoreCase));
        }
    }
}
