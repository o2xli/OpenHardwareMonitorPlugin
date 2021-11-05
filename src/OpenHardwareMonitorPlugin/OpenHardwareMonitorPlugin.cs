namespace Loupedeck.OpenHardwareMonitorPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Management;


    public class OpenHardwareMonitorPlugin : Plugin
    {

        public override Boolean UsesApplicationApiOnly => true;
        public override Boolean HasNoApplication => true;
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
    public class SensorMonitorCommand : PluginDynamicCommand
    {
        private List<SensorData> listSensorData;
        public SensorMonitorCommand() : base("Hardware Monitor Sensor", "OpenHardwareMonitor", "Hardware Monitor")
        {
            Monitor.GetInstance().OnRefreshValues += (sender, e) =>
            {
                this.listSensorData = sender as List<SensorData>;
                this.ActionImageChanged();
            };
            this.MakeProfileAction("tree");
        }

        protected override PluginProfileActionData GetProfileActionData()
        {
            var tree = new PluginProfileActionTree("Select Sensor");
            var sensorIds = Monitor.GetInstance().Identifiers;

            tree.AddLevel("category");
            tree.AddLevel("identifier");

            foreach (var category in sensorIds.Select(c => c.Trim('/').Split('/').First()).Distinct())
            {
                var node = tree.Root.AddNode(category);

                foreach (var id in sensorIds.Where(s => s.StartsWith($"/{category}")))
                {
                    var sensor = Monitor.GetInstance().GetSensor(id);
                    node.AddItem(id, $"{sensor.SensorType} {sensor.Name}");
                }
            }

            return tree;

        }

        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            var sensor = this.listSensorData.SelectSensorData(actionParameter);
            return sensor != null ? sensor.ValueFormatted : String.Empty;
        }



        protected override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {
            var sensor = this.listSensorData.SelectSensorData(actionParameter);

            var bitmap = new BitmapBuilder(imageSize);
            bitmap.FillRectangle(0, 0, 90, 90, sensor.GetBitmapColor());
           
            bitmap.DrawText(this.GetCommandDisplayName(actionParameter, imageSize), BitmapColor.White, 20);
            return bitmap.ToImage();
        }


    }
}
