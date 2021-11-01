namespace Loupedeck.OpenHardwareMonitorPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Management;


    public class OpenHardwareMonitorPlugin : Plugin
    {
      
        public override void Load()
        {
            var x = String.Empty;
        }

        public override void Unload()
        {
        }

        private void OnApplicationStarted(Object sender, EventArgs e)
        {
        }

        private void OnApplicationStopped(Object sender, EventArgs e)
        {
        }

        public override void RunCommand(String commandName, String parameter)
        {
        }

        public override void ApplyAdjustment(String adjustmentName, String parameter, Int32 diff)
        {
        }
    }
    public class CPUTempCommand : PluginDynamicCommand
    {
        private List<SensorData> listSensorData;
        public CPUTempCommand() : base("Hardware Monitor", "OpenHardwareMonitor", "Hardware Monitor")
        {
            Monitor.GetInstance().OnRefreshValues += (sender, e) =>
            {
                this.listSensorData = sender as List<SensorData>;
                this.ActionImageChanged("identifier");
            };

            this.MakeProfileAction("text; Enter Identifier (e.g. /amdcpu/0/temperature/4");
            this.AddParameter("identifier", "Identifier", String.Empty);
        }

        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            var sensor = this.listSensorData.SelectSensorData(actionParameter);
            return sensor != null ? sensor.Value : String.Empty;
        }



        protected override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {
            var bitmap = new BitmapBuilder(imageSize);
            bitmap.FillRectangle(0, 0, 90, 90, BitmapColor.Black);

            bitmap.FillRectangle(0, 0, 10, 80, new BitmapColor(0,600,0));
            bitmap.FillRectangle(10, 0, 10, 30, new BitmapColor(0, 600, 0));
            bitmap.FillRectangle(20, 0, 10, 70, new BitmapColor(0, 600, 0));
            bitmap.FillRectangle(30, 0, 10, 60, new BitmapColor(0, 600, 0));
            bitmap.FillRectangle(40, 0, 10, 40, new BitmapColor(0, 600, 0));
            bitmap.FillRectangle(50, 0, 10, 60, new BitmapColor(0, 600, 0));
            bitmap.FillRectangle(60, 0, 10, 65, new BitmapColor(0, 600, 0));
            bitmap.FillRectangle(70, 0, 10, 70, new BitmapColor(0, 600, 0));
            bitmap.FillRectangle(80, 0, 10, 70, new BitmapColor(0, 600, 0));

            bitmap.DrawText(this.GetCommandDisplayName(actionParameter, imageSize), BitmapColor.White, 20);
            return bitmap.ToImage();
        }


    }
}
