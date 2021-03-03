using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace fried_fame_client_windows.Classes.OpenVPN
{
    class TAPDriver
    {
        /// <summary>
        /// Checks if TAP driver is installed
        /// </summary>
        public static bool IsTapInstalled
        {
            get
            {
                Classes.Logging.Info("Checking for TAP Driver");

                // First lets check for driver.
                var query = new SelectQuery("Win32_SystemDriver");
                query.Condition = "Name like 'tap%'";
                var searcher = new ManagementObjectSearcher(query);
                if (searcher.Get().Count == 0)
                {
                    Classes.Logging.Info("TAP Driver missing");
                    return false;
                }

                // now lets check if the tap is operational
                var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (var networkInterface in networkInterfaces)
                {
                    if (networkInterface.Description.Contains("TAP-Windows Adapter"))
                    {
                        Classes.Logging.Info("TAP Driver found");
                        return true;
                    }
                }

                Classes.Logging.Info("TAP Driver missing");
                return false;
            }
        }

        /// <summary>
        /// Installs TAP driver
        /// </summary>
        /// <returns></returns>
        public static async Task Install()
        {
            await Task.Run(delegate ()
            {
                Classes.Logging.Info("TAP Driver installing...");

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = Path.Combine(Environment.CurrentDirectory, "installers/tap-windows.exe");
                startInfo.Verb = "runas";
                startInfo.Arguments = "/SD";
                Process proc = Process.Start(startInfo);
                proc.WaitForExit();

                Classes.Logging.Info("TAP Driver exited with code " + proc.ExitCode);
            });
        }
    }
}
