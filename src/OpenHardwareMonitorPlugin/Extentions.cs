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
            => sensors?.FirstOrDefault(s => s.Identifier.Equals(identifier, StringComparison.OrdinalIgnoreCase));

        public static BitmapColor GetBitmapColor(this SensorData sensor,Single value = -1)
        {
            var sensorValue = sensor.Value;
            if (value != -1)
                sensorValue = value;

            if (sensor?.SensorType == null)
                return BitmapColor.Black;

            switch (sensor.SensorType)
            {                
                case SensorType.Load:
                case SensorType.Temperature:
                    if(sensorValue < 70)
                        return new BitmapColor(0, 139, 0);
                    else if(sensorValue < 80)
                        return new BitmapColor(255, 140, 0);
                    else
                        return new BitmapColor(139,0,0);
                case SensorType.Fan:
                    if (sensorValue < 900)
                        return new BitmapColor(0, 139, 0);
                    else if (sensorValue < 1200)
                        return new BitmapColor(255, 140, 0);
                    else
                        return new BitmapColor(139, 0, 0);                
                case SensorType.Power:
                    var percentageForward = sensor.GetPercentageAgainstMax(sensorValue);
                    if (percentageForward < 50)
                        return new BitmapColor(0, 139, 0);
                    else if (percentageForward < 70)
                        return new BitmapColor(255, 140, 0);
                    else
                        return new BitmapColor(139, 0, 0);
                case SensorType.Clock:
                case SensorType.Data:
                case SensorType.SmallData:
                case SensorType.Throughput:
                case SensorType.Flow:
                    var percentage = sensor.GetPercentageAgainstMax(sensorValue);
                    if (percentage < 50)
                        return new BitmapColor(139, 0, 0);
                    else if (percentage < 70)
                        return new BitmapColor(255, 140, 0);
                    else
                        return new BitmapColor(0, 139, 0);
                case SensorType.Control:
                case SensorType.Level:
                case SensorType.Voltage:
                case SensorType.Factor:
                case SensorType.None:
                default:
                    return BitmapColor.Black;
            }

            
        }
        public static List<Single> GetHistory(this SensorData sensor) => Monitor.GetInstance().GetHistoryValues(sensor.Identifier);

        public static Single GetPercentageAgainstMax(this SensorData sensor, Single sensorValue) => sensorValue / sensor.Max * 100;
    }
}
