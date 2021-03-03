using fried_fame_client_windows.Classes;
using fried_fame_client_windows.Classes.AutoAPI;
using fried_fame_client_windows.Classes.OpenVPN;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static fried_fame_client_windows.Classes.AutoAPI.Node;

namespace fried_fame_client_windows.Forms
{
    public partial class FormHome : MaterialForm
    {
        public FormHome()
        {
            InitializeComponent();
            this.Enabled = false;

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey700, Primary.Grey800, Primary.Grey600, Accent.Cyan200, TextShade.WHITE);
        }

        private VPNNode[] nodes;

        private async void FormHome_Load(object sender, EventArgs e)
        {
            // Notifcation tray
            await NotificationTray.Initialize();

            // Version check
            if (Application.ProductVersion != await Context.Get("newest-version"))
                this.buttonUpdate.Visible = true;

            this.Text = await Context.Get("project-name");
            this.Icon = await Assets.LoadIconAsync("icon.ico");

            OpenVPNManagement.OnBandwidthEvent += OpenVPNManagement_OnBandwidthEvent;
            OpenVPNManagement.OnCloseEvent += OpenVPNManagement_OnCloseEvent;
            OpenVPNManagement.OnFailureEvent += OpenVPNManagement_OnFailureEvent;
            OpenVPNManagement.OnStateChangeEvent += OpenVPNManagement_OnStateChangeEvent;

            this.listViewServers.Columns.Add("", 40);
            this.listViewServers.Columns.Add("Country", 90);
            this.listViewServers.Columns.Add("City", 90);
            this.listViewServers.Columns.Add("Hostname", 190);
            this.listViewServers.Columns.Add("Latency", 80);
            this.listViewServers.Columns.Add("Protocols", 80);

            await this.RefreshServerList();  

            this.Enabled = true;

            // Now that we have loaded, get server list WITH ping/latency
            await this.RefreshServerList(true);
        }

        private void OpenVPNManagement_OnStateChangeEvent(OpenVPNManagement.State state, string localIP, string publicIP, uint unixTimestamp)
        {
            this.labelConnectedIPAddress.Text = publicIP;

            switch (state)
            {
                case OpenVPNManagement.State.CONNECTING:
                    this.labelStatus.Text = "Connecting";
                    break;

                case OpenVPNManagement.State.CONNECTED:
                    this.labelStatus.Text = "Connected";
                    break;

                case OpenVPNManagement.State.AUTH:
                    this.labelStatus.Text = "Authenticating";
                    break;

                case OpenVPNManagement.State.ASSIGN_IP:
                    this.labelStatus.Text = "Assigning IP address";
                    break;

                case OpenVPNManagement.State.ADD_ROUTES:
                    this.labelStatus.Text = "Adding Routes";
                    break;

                case OpenVPNManagement.State.EXITING:
                    this.labelStatus.Text = "Exiting";
                    break;

                case OpenVPNManagement.State.GET_CONFIG:
                    this.labelStatus.Text = "Getting Configuration";
                    break;

                case OpenVPNManagement.State.RECONNECTING:
                    this.labelStatus.Text = "Reconnecting";
                    break;

                case OpenVPNManagement.State.UNKNOWN:
                    this.labelStatus.Text = "Unknown";
                    break;

                case OpenVPNManagement.State.WAIT:
                    this.labelStatus.Text = "Waiting for OpenVPN";
                    break;

                default:
                    break;
            }
        }

        private void OpenVPNManagement_OnFailureEvent(OpenVPNManagement.FailureReason reason)
        {
            if(reason == OpenVPNManagement.FailureReason.AUTHENTICATION)
                this.labelStatus.Text = "Authentication Failed";
        }

        private void OpenVPNManagement_OnCloseEvent()
        {
            this.labelStatus.Text = "Disconnected";
            this.labelConnectedServer.Text = string.Empty;
            this.labelConnectedIPAddress.Text = string.Empty;
            this.labelConnectedLocation.Text = string.Empty;
            this.labelConnectedBandwidth.Text = string.Empty;
        }

        /// <summary>
        /// Event that handles bandwidth usage reports
        /// </summary>
        /// <param name="bytesIn">Byte count we have received</param>
        /// <param name="bytesOut">byte count we have sent</param>
        private void OpenVPNManagement_OnBandwidthEvent(ulong bytesIn, ulong bytesOut)
        {
            this.labelConnectedBandwidth.Text = string.Format(
                "{0} / {1}",
                Utilities.GetSizeAsVisual(bytesIn),
                Utilities.GetSizeAsVisual(bytesOut));
        }

        /// <summary>
        /// Designed to cancel resizing of columns.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewServers_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = this.listViewServers.Columns[e.ColumnIndex].Width;
        }

        /// <summary>
        /// Connect button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonConnect_Click(object sender, EventArgs e)
        {
            var selectedNode = this.SelectedNode;
            if(selectedNode.id == 0)
            {
                this.labelStatus.Text = "Please select valid node.";
                return;
            }

            if(OpenVPN.IsRunnnig)
            {
                this.labelStatus.Text = "Closing current connection.";
                await OpenVPN.Close();
            }

            this.labelStatus.Text = "Checking Connectability.";
            var connectability = await selectedNode.CanConnect();
            if(!connectability.permit)
            {
                this.labelStatus.Text = connectability.message;
                return;
            }

            this.labelStatus.Text = "Starting new session.";
            if(await OpenVPN.StartOpenVPN(selectedNode))
            {
                this.labelConnectedServer.Text = selectedNode.hostname;
                this.labelConnectedLocation.Text = this.GetNodeLocation(selectedNode);
            }
            else
            {
                this.labelStatus.Text = "Failed to start";
            }
        }

        /// <summary>
        /// disconnect button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonDisconnect_Click(object sender, EventArgs e)
        {
            await OpenVPN.Close();
        }

        private async void buttonLogout_Click(object sender, EventArgs e)
        {
            await Session.Deauthenticate();
            await Session.SaveSession();
            await OpenVPN.Close();
            FormManager.DisplayForm(FormManager.FORMS.LOGIN, true, true);
        }

        private async void buttonReload_Click(object sender, EventArgs e)
        {
            buttonReload.Enabled = false;
            await RefreshServerList(true);
            buttonReload.Enabled = true;
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            Process.Start(await Context.Get("download-page-url"));
        }

        private void labelStatus_DoubleClick(object sender, EventArgs e)
        {
            Logging.AllocConsole();
        }

        private async void listViewServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedNode = this.SelectedNode;

            if(selectedNode.id == 0)
            {
                this.labelSelectedLatency.Text = string.Empty;
                this.labelSelectedLocation.Text = string.Empty;
                this.labelSelectedServer.Text = string.Empty;
            }
            else
            {
                int ping = await selectedNode.GetPing();
                this.labelSelectedLocation.Text = this.GetNodeLocation(selectedNode);
                this.labelSelectedServer.Text = selectedNode.hostname;
                this.labelSelectedLatency.Text = string.Format("{0}ms", ping);
            }
        }

        private VPNNode SelectedNode
        {
            get
            {
                if (listViewServers.SelectedItems.Count == 0)
                    return new VPNNode() { id = 0 };

                int id = int.Parse(listViewServers.SelectedItems[0].SubItems[0].Text);
                foreach(var node in this.nodes)
                {
                    if(node.id == id)
                    {
                        return node;
                    }
                }

                return new VPNNode() { id = 0 };
            }
        }

        private string GetNodeLocation(VPNNode node)
        {
            return string.Format("{0}, {1}", CountryISO2ToDisplayName(node.country), node.city);
        }

        private async Task RefreshServerList(bool getPing = false)
        {
            listViewServers.BeginUpdate();
            listViewServers.Items.Clear();

            this.nodes = await Node.GetNodes();
            foreach (var node in this.nodes)
            {
                string ping = string.Empty;
                if (getPing)
                {
                    int numericPing = await node.GetPing();
                    if (numericPing > 0)
                        ping = numericPing.ToString() + "ms";
                }

                // protocols
                string protocols = string.Empty;
                if (node.ovpn && node.pptp)
                    protocols = "OVPN/PPTP";
                else if (node.ovpn)
                    protocols = "OpenVPN";
                else if (node.pptp)
                    protocols = "PPTP";

                string[] listViewItems = new string[]
                {
                    node.id.ToString(),
                    CountryISO2ToDisplayName(node.country),
                    node.city,
                    node.hostname,
                    ping,
                    protocols
                };

                listViewServers.Items.Add(new ListViewItem(listViewItems));
            }

            listViewServers.EndUpdate();
        }

        private string CountryISO2ToDisplayName(string s)
        {
            RegionInfo regionInfo = new RegionInfo(s);
            return regionInfo.EnglishName;
        }
    }
}
