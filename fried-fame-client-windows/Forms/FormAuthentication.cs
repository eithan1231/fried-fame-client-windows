using fried_fame_client_windows.Classes;
using fried_fame_client_windows.Classes.AutoAPI;
using fried_fame_client_windows.Classes.OpenVPN;
using fried_fame_client_windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fried_fame_client_windows
{
    public partial class FormAuthentication : MaterialForm
    {
        public FormAuthentication()
        {
            InitializeComponent();

            this.Enabled = false;

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey700, Primary.Grey800, Primary.Grey600, Accent.Cyan200, TextShade.WHITE);
        }

        private async void FormAuthentication_Load(object sender, EventArgs e)
        {
            // Notifcation tray
            await NotificationTray.Initialize();

            // UI stuff
            this.Text = string.Format("{0} Login", await Context.Get("project-name"));
            this.Icon = await Assets.LoadIconAsync("icon.ico");
            this.pictureBoxLogo.Image = await Assets.LoadImageAsync("evpn.png");

            // Version check
            if (Application.ProductVersion != await Context.Get("newest-version"))
                this.materialLabelUpdate.Visible = true;

            this.Enabled = true;
        }

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                this.materialLabelStatus.Text = "Authorizing...";
                this.buttonLogin.Enabled = false;

                string username = this.textUsername.Text;
                string password = this.textPassword.Text;
                this.textPassword.ResetText();

                Session.AuthenticationStatus status = await Session.Authenticate(username, password);

                switch(status.message)
                {
                    case "try-again-later":
                        this.materialLabelStatus.Text = "Try again later.";
                        break;
                    case "missing-param":
                        this.materialLabelStatus.Text = "Missing input.";
                        break;
                    case "bad-username":
                        this.materialLabelStatus.Text = "Username not found";
                        break;
                    case "bad-password":
                        this.materialLabelStatus.Text = "Password mismatch";
                        break;
                    case "autoapi-error":
                        this.materialLabelStatus.Text = "Internal API Error";
                        break;
                    case "no-subscription":
                        this.materialLabelStatus.Text = "No Active Subscription";
                        break;
                    case "okay":
                        this.materialLabelStatus.Text = "Authenticated.. Please wait";
                        break;
                    default:
                        this.materialLabelStatus.Text = string.Format("Message: ", status.message);
                        break;
                }

                if(status.isAuthenticated)
                {
                    await Session.SaveSession();
                    FormManager.DisplayForm(FormManager.FORMS.HOME, true, true);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                this.buttonLogin.Enabled = true;
            }
        }

        private async void materialLabelUpdate_Click(object sender, EventArgs e)
        {
            Process.Start(await Context.Get("download-page-url"));
        }

        private async void buttonRegister_Click(object sender, EventArgs e)
        {
            Process.Start(await Context.Get("registration-url"));
        }

        private void textPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.buttonLogin_Click(null, null);
            }
        }

        private void textUsername_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buttonLogin_Click(null, null);
            }
        }
    }
}
