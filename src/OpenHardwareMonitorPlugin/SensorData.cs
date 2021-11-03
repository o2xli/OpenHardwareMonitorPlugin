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
        public String SensorType { get; set; }

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
                case "Voltage":
                    return $" V";
                case "Clock":
                    return $"{prefix} MHz";
                case "Temperature":
                    return $"{prefix} °C";
                case "Load":
                    return $"{prefix} %";
                case "Fan":
                    return $"{prefix} RPM";
                case "Flow":
                    return $"{prefix} L/h";
                case "Control":
                    return $"{prefix} %";
                case "Level":
                    return $"{prefix} %";
                case "Factor":
                    return $"{prefix} ";
                case "Power":
                    return $"{prefix} W";
                case "Data":
                    return $"{prefix} GB";
                case "SmallData":
                    return $"{prefix} MB";
                case "Throughput":
                    return $"{prefix} MB/s";
            }

            return value.ToString();
        }

        private Int16 Round(Single value) => (Int16)Math.Round(value, 0, MidpointRounding.AwayFromZero);
    }
}
