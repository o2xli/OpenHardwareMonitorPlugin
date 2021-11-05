namespace Loupedeck.OpenHardwareMonitorPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class SensorData
    {
        public String Identifier { get; set; }
        public SensorType SensorType { get; set; }

        public String Name { get; set; }
        public String MinFormatted => this.FormatValue(this.Min);
        public String MaxFormatted => this.FormatValue(this.Max);
        public String ValueFormatted => this.FormatValue(this.Value);
        public Single Min { get; set; }
        public Single Max { get; set; }
        public Single Value { get; set; }

        private String FormatValue(Single value)
        {
            var prefix = $"{this.Name}\n \n{ this.Round(value)}";
            switch (this.SensorType)
            {               
                case SensorType.Voltage:
                    return $"{prefix} V";
                case SensorType.Clock:
                     return $"{prefix} MHz";
                case SensorType.Temperature:
                     return $"{prefix} °C";
                case SensorType.Load:
                     return $"{prefix} %";
                case SensorType.Fan:
                     return $"{prefix} RPM";
                case SensorType.Flow:
                     return $"{prefix} L/h";
                case SensorType.Control:
                     return $"{prefix} %";
                case SensorType.Level:
                     return $"{prefix} %";
                case SensorType.Power:
                     return $"{prefix} W";
                case SensorType.Data:
                     return $"{prefix} GB";
                case SensorType.SmallData:
                     return $"{prefix} MB";
                case SensorType.Throughput:
                     return $"{prefix} MB/s";
                case SensorType.Factor:
                case SensorType.None:
                default:
                     return $"{prefix}";
            }
        }

        private Int16 Round(Single value) => (Int16)Math.Round(value, 0, MidpointRounding.AwayFromZero);

        
    }
    internal enum SensorType
    {
        None,
        Voltage, // V
        Clock, // MHz
        Temperature, // °C
        Load, // %
        Fan, // RPM
        Flow, // L/h
        Control, // %
        Level, // %
        Factor, // 1
        Power, // W
        Data, // GB = 2^30 Bytes    
        SmallData, // MB = 2^20 Bytes
        Throughput, // MB/s = 2^20 Bytes/s
    }
}
