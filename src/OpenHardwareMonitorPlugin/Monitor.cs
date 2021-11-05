namespace Loupedeck.OpenHardwareMonitorPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Management;
    using System.Text;
    using System.Threading.Tasks;

    internal class Monitor
    {
        private static Monitor _instance;
        private readonly System.Timers.Timer timer;
        private readonly ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\openhardwaremonitor", "select identifier,value,SensorType,Max,Min,name from sensor");
        public event EventHandler OnRefreshValues;

        private List<SensorData> list;

        public String[] Identifiers { get; private set; } = new String[0];
        private Monitor()
        {
            this.timer = new System.Timers.Timer(1000);
            this.timer.Elapsed += (sender, e) =>
            {
                OnRefreshValues?.Invoke(this.GetSensorData(), e);
            };
            this.timer.AutoReset = true;
            this.timer.Start();
        }
        public static Monitor GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Monitor();
            }

            return _instance;
        }

        public List<SensorData> GetSensorData()
        {
            this.list = this.managementObjectSearcher.Get().OfType<ManagementObject>()
            .Select(t =>
            new SensorData
            {
                Identifier = t.GetPropertyValue("identifier").ToString(),
                SensorType = (SensorType)Enum.Parse(typeof(SensorType),t.GetPropertyValue("SensorType").ToString(),true),
                Name = t.GetPropertyValue("Name").ToString(),
                Value = (Single)t.GetPropertyValue("Value"),
                Max = (Single)t.GetPropertyValue("Max"),
                Min = (Single)t.GetPropertyValue("Min"),

            }).ToList();

            this.Identifiers = this.list.Select(l=> l.Identifier).Distinct().OrderBy(o=>o).ToArray();
            return this.list;

        }

        public SensorData GetSensor(String identifier)
        {
            lock (this.list)
            {
                return this.list.FirstOrDefault(l => l.Identifier.Equals(identifier, StringComparison.OrdinalIgnoreCase));
            }
        }
    


    }
}
