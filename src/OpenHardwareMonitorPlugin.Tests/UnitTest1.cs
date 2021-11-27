using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Management;

namespace OpenHardwareMonitorPlugin.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\openhardwaremonitor", "select identifier, value from sensor where identifier = '/amdcpu/0/temperature/4'");
            foreach (ManagementObject service in searcher.Get())
            {
                var value = service.GetPropertyValue("value");
                // show the service
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
