using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace fried_fame_client_windows.Classes
{
    class Utilities
    {
        /// <summary>
        /// Checks whether client is already running.
        /// </summary>
        public static bool IsInstanceRunning
        {
            get
            {
#if DEBUG
                return false;
#endif
                // Returns true if more than once process with this processses' name is running.
                return Process.GetProcessesByName(
                    AppDomain.CurrentDomain.FriendlyName.Replace(".exe", "")).Length > 1;
            }
        }

        /// <summary>
        /// Is running with UAV elevation
        /// </summary>
        public static bool IsUACElevated
        {
            get
            {
#if DEBUG
                return true;
#endif
                bool isAdmin;
                try
                {
                    isAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent())
                        .IsInRole(WindowsBuiltInRole.Administrator);
                }
                catch (Exception)
                {
                    isAdmin = false;
                }
                return isAdmin;
            }
        }

        /// <summary>
        /// Relaunches this application with UAC
        /// </summary>
        public static void RelaunchWithUACElevation()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(Environment.CurrentDirectory,
                Process.GetCurrentProcess().ProcessName.Replace(".exe", ""));
            startInfo.Verb = "runas";
            startInfo.Arguments = "--relaunch";

            Process.Start(startInfo);

            Program.Shutdown();
        }

        /// <summary>
        /// credits to stackoverflow
        /// </summary>
        public static string GetSizeAsVisual(ulong size)
        {
            string[] unit = new string[] { "B", "KB", "MB", "GB", "TB", "PB" };
            uint i = (uint)Math.Floor(Math.Log(size, 1024));
            return Math.Round(size / Math.Pow(1024, i), 2).ToString() + unit[i];
        }
    }
}
