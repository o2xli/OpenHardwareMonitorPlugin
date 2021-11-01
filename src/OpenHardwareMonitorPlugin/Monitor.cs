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
        private readonly ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\openhardwaremonitor", "select identifier,value,SensorType,Max,Min from sensor");
        public event EventHandler OnRefreshValues;
        
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
            return this.managementObjectSearcher.Get().OfType<ManagementObject>().Select(t =>
            new SensorData
            {
                Identifier = t.GetPropertyValue("identifier").ToString(),
                SensorType = t.GetPropertyValue("SensorType").ToString(),
                //Name = t.GetPropertyValue("Name").ToString(),
                Value = t.GetPropertyValue("Value").ToString(),
                Max = t.GetPropertyValue("Max").ToString(),
                Min = t.GetPropertyValue("Min").ToString(),

            }).ToList();
        }


    }
}
