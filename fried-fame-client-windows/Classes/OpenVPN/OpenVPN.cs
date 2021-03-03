using fried_fame_client_windows.Classes.AutoAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static fried_fame_client_windows.Classes.AutoAPI.Node;

namespace fried_fame_client_windows.Classes.OpenVPN
{
    class OpenVPN
    {
        /// <summary>
        /// Process instance of OpenVPN
        /// </summary>
        private static Process openvpnInstance = null;

        /// <summary>
        /// Management port
        /// </summary>
        private const int port = 719;

        public static bool IsRunnnig
        {
            get
            {
                return openvpnInstance != null && !openvpnInstance.HasExited;
            }
        }

        public static async Task<bool> StartOpenVPN(VPNNode node)
        {
            Logging.Info(string.Format("OpenVPN - Start procedure {0} \"{1}\"", node.id, node.hostname));


            if (IsRunnnig)
            {
                Logging.Info("OpenVPN - Terminating existing connection.");
                await OpenVPN.Close();
                Logging.Info("OpenVPN - Terminated connection.");
            }

            if (!node.ovpn)
            {
                Logging.Info("OpenVPN - Node does not support OpenVPN.");
                throw new ArgumentException("Node expects OpenVPN support");
            }

            // tap check
            if(!TAPDriver.IsTapInstalled)
            {
                Logging.Info("OpenVPN - TAP Driver missing.");
                throw new Exception("TAP Driver missing");
            }

            if (!Utilities.IsUACElevated)
            {
                Logging.Info("OpenVPN - UAC.");
                throw new AccessViolationException("Administrative permissions required");
            }

            Logging.Info("OpenVPN - Fetching OpenVPN configuration and writing to dist.");

            string openVPNConfigContent = await node.GetOpenVPNConfig();

            string configFile = Path.Combine(
                Path.GetTempPath(),
                string.Format(
                    "OpenVPN-{0}-{1}-Config.ovpn",
                    node.country.Trim(),
                    node.city.Trim()));

            if (File.Exists(configFile))
                File.Delete(configFile);

            File.WriteAllText(configFile, openVPNConfigContent);
            Logging.Info("OpenVPN - Config saved to " + configFile);


            ProcessStartInfo startContext = new ProcessStartInfo();
            startContext.FileName = Path.Combine(Environment.CurrentDirectory, @"bin\openvpn.exe");
            startContext.Verb = "runas";
            startContext.WindowStyle = ProcessWindowStyle.Hidden;
            startContext.UseShellExecute = false;
            startContext.CreateNoWindow = true;
            startContext.Arguments = string.Format(
                "--config \"{0}\" --management 127.0.0.1 {1} --management-hold --management-query-passwords",
                configFile, OpenVPN.port);

            openvpnInstance = Process.Start(startContext);
            openvpnInstance.Exited += OpenvpnInstance_Exited;

            if (!openvpnInstance.HasExited)
            {
                OpenVPNManagement.Start(
                    System.Net.IPAddress.Loopback, OpenVPN.port,
                    Session.UserID.ToString(), Session.NodeAuthentication);
            }

            return !openvpnInstance.HasExited;
        }

        public static async Task Close()
        {
            Logging.Info("OpenVPN - Close procedure..");

            await OpenVPNManagement.Close();

            if(IsRunnnig && openvpnInstance != null)
            {
                Logging.Info("OpenVPN - Killing process");
                openvpnInstance.Kill();
            }

            openvpnInstance = null;
            Logging.Info("OpenVPN - Process nulled.");
        }

        private static async void OpenvpnInstance_Exited(object sender, EventArgs e)
        {
            Logging.Info("OpenVPN - Process exit event invoked..");
            await OpenVPNManagement.Close();
        }
    }
}
