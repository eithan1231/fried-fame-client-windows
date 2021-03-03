using fried_fame_client_windows.Classes;
using fried_fame_client_windows.Classes.AutoAPI;
using fried_fame_client_windows.Classes.OpenVPN;
using fried_fame_client_windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fried_fame_client_windows
{
    static class Program
    {
        

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if DEBUG
            // Attaching console for debugging purposes.
            Logging.AllocConsole();
#endif

            Logging.Info("Process Started");
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            StartupProcedureAsync().Wait();
            Shutdown();
        }

        /// <summary>
        /// Startup procedure to handle asynchronous actions.
        /// </summary>
        /// <returns></returns>
        private static async Task StartupProcedureAsync()
        {
            Logging.Info("Startup procedure has started");

            Logging.Info("Startup - Relaunch check");
            if (!Environment.GetCommandLineArgs().Contains("--relaunch"))
            {
                if (Utilities.IsInstanceRunning)
                {
                    Logging.Info("Startup - Instance already running");
                    MessageBox.Show(
                        "VPN Instance is already running. Navigate to task-bar to " +
                        "access the VPN",
                        "Instance running",
                        MessageBoxButtons.RetryCancel,
                        MessageBoxIcon.Information);
                    return;
                }
            }


            try
            {
                Logging.Info("Startup - Checking internet connectivity with context");
                await Context.Initialize(true);
            }
            catch (HttpRequestException ex)
            {
                Logging.Error(ex);

                var result = MessageBox.Show(
                    "We are unable to establish a secure connection " +
                    "with the VPN server. Would you like to retry?",
                    "Internet Connectivity",
                    MessageBoxButtons.RetryCancel,
                    MessageBoxIcon.Error);

                // recursive retry
                if(result == DialogResult.Retry)
                    await StartupProcedureAsync();

                return;
            }


            // UAC Relaunch check
            Logging.Info("Startup - UAC Elevation check");
            if (!Utilities.IsUACElevated)
            {
                Logging.Info("Startup - UAC Elevation check failed. Reloading.");
                Utilities.RelaunchWithUACElevation();

                return;
            }


            // TAP Driver check
            Logging.Info("Startup - TAP Driver check");
            if (Utilities.IsUACElevated && !TAPDriver.IsTapInstalled)
            {
                Logging.Info("Startup - TAP Driver is not installed");

                var tapInstallResult = MessageBox.Show(
                    "The system TAP Driver is missing. It is required in order to" +
                    " establish a secure connection the the VPN server. Would you like" +
                    " to install the TAP Driver?", "TAP Driver",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                // They dont wish to install it...
                if (tapInstallResult != DialogResult.Yes)
                {
                    Logging.Info("Startup - TAP Driver, user does not want to install");
                    return;
                }

                await TAPDriver.Install();

                Logging.Info("Startup - TAP Driver, install procedure completed");

                // Install procedure has completed. Was it successful?
                if (!TAPDriver.IsTapInstalled)
                {
                    Logging.Info("Startup - TAP Driver, not installed after running installation. User cancelled installation? did it fail?");
                    MessageBox.Show(
                        "We detected the TAP Driver installation failed. If you " +
                        "successfully installed TAP Driver but we are displaying " +
                        "this message in error, please create a support ticket " +
                        "for further assistance.\r\n\r\nIf you would like to retry" +
                        " the installation, try opening this VPN again", "TAP Failure",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


            // Loading the correct form
            // Attempting to login user from saved session
            Logging.Info("Startup - Session Loading");
            var sessionResult = await Session.LoadSession();
            if (sessionResult.isAuthenticated)
            {
                Logging.Info("Startup - Session, is already authenticated. Loading home page.");
                FormManager.DisplayForm(FormManager.FORMS.HOME, true, true);
            }
            else
            {
                Logging.Info("Startup - Session not found or invalid, loading login.");
                FormManager.DisplayForm(FormManager.FORMS.LOGIN, true, true);
            }
        }

        public static async void Shutdown()
        {
            
            try
            {
                Logging.Info("Shutdown - Shutdown procedure started.. Goodbye");

                Logging.Save();

                await OpenVPN.Close();
            }
            finally
            {
                Environment.Exit(0);
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Classes.Logging.Error((Exception)e.ExceptionObject);
            Shutdown();
        }
    }
}
