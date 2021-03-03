using fried_fame_client_windows.Classes.AutoAPI;
using fried_fame_client_windows.Classes.OpenVPN;
using fried_fame_client_windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fried_fame_client_windows.Classes
{
    class NotificationTray
    {
        private static NotifyIcon notifyIcon = null;

        public static async Task Initialize()
        {
            if (notifyIcon != null) return;
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = await Assets.LoadIconAsync("icon.ico");
            notifyIcon.Text = await Context.Get("project-name");
            notifyIcon.Visible = true;
            notifyIcon.Click += NotifyIcon_Click;

            MenuItem menuItemExit = new MenuItem();
            menuItemExit.Name = "Exit";
            menuItemExit.Text = "Exit";
            menuItemExit.Click += MenuItemExit_Click;
            menuItemExit.Visible = true;

            notifyIcon.ContextMenu = new ContextMenu();
            notifyIcon.ContextMenu.MenuItems.Add(menuItemExit);

            OpenVPNManagement.OnStateChangeEvent += OpenVPNManagement_OnStateChangeEvent;
        }

        private static void OpenVPNManagement_OnStateChangeEvent(OpenVPNManagement.State state, string localIP, string publicIP, uint unixTimestamp)
        {
            switch (state)
            {
                case OpenVPNManagement.State.RECONNECTING:
                    notifyIcon.ShowBalloonTip(2000, "Reconnecting", "Reconnecting to VPN.", ToolTipIcon.Warning);
                    break;

                case OpenVPNManagement.State.CONNECTED:
                    notifyIcon.ShowBalloonTip(2000, "Connected", "Successfully connected to VPN.", ToolTipIcon.Info);
                    break;

                case OpenVPNManagement.State.EXITING:
                    notifyIcon.ShowBalloonTip(2000, "Disconnecting", "Disconnecting from VPN.", ToolTipIcon.Info);
                    break;
            }
        }

        private static void MenuItemExit_Click(object sender, EventArgs e)
        {
            Program.Shutdown();
        }

        private static void NotifyIcon_Click(object sender, EventArgs e)
        {
            if(Session.Authenticated)
            {
                FormManager.DisplayForm(FormManager.FORMS.HOME, false, false);
            }
            else
            {
                FormManager.DisplayForm(FormManager.FORMS.LOGIN, false, false);
            }
        }
    }
}
