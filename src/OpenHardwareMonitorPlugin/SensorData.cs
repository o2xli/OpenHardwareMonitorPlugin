namespace Loupedeck.OpenHardwareMonitorPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class SensorData
    {
        private String min;
        private String max;
        private String value;
        private String name;

        public String Identifier { get; set; }
        public String SensorType { get; set; }

        public String Name { get; set; }
        //public String Name { get => this.GetName(); }
        public String Min { get => this.FormatValue(this.min); set => this.min = value; }
        public String Max { get => this.FormatValue(this.max); set => this.max = value; }
        public String Value { get => this.FormatValue(this.value); set => this.value = value; }

        private String FormatValue(String value)
        {
            var prefix = $"{this.Name}\n \n{ this.Cut(value)}";
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

       

        private String Cut(String value)
        {
            return value.Split(',', '.').First();
        }
    }
}
