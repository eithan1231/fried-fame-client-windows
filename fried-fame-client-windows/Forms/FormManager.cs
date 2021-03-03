using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fried_fame_client_windows.Forms
{
    class FormManager
    {
        private static bool messageLoopStarted = false;

        private static FormAuthentication formAuthentication = null;
        private static FormHome formHome = null;

        private static FORMS currentForm = 0;

        public enum FORMS: byte
        {
            LOGIN = 1,
            HOME = 2
        };

        private static void MessageLoop()
        {
            if(!messageLoopStarted)
            {
                messageLoopStarted = true;
                Application.Run();
            }
        }

        public static void DisplayForm(FORMS form, bool closeCurrent = false, bool forceNew = false)
        {
            switch(form)
            {
                case FORMS.HOME:
                    Classes.Logging.Info("Opening Form Home");

                    if (forceNew || formHome == null)
                    {
                        formHome = new FormHome();
                        formHome.FormClosing += FormGeneric_Closing;
                    }

                    formHome.Show();
                    formHome.Visible = true;
                    formHome.ShowInTaskbar = true;
                    break;

                case FORMS.LOGIN:
                    Classes.Logging.Info("Opening Form Login");

                    if (forceNew || formAuthentication == null)
                    {
                        formAuthentication = new FormAuthentication();
                        formAuthentication.FormClosing += FormGeneric_Closing;
                    }

                    formAuthentication.Show();
                    formAuthentication.Visible = true;
                    formAuthentication.ShowInTaskbar = true;
                    break;
            }

            if(closeCurrent)
            {
                CloseForm(currentForm);
            }

            currentForm = form;

            MessageLoop();
        }

        public static void CloseForm(FORMS form)
        {
            switch (form)
            {
                case FORMS.HOME:
                    Classes.Logging.Info("Closing form home");

                    formHome.Close();
                    formHome = null;
                    break;

                case FORMS.LOGIN:
                    Classes.Logging.Info("Closing form login");

                    formAuthentication.Close();
                    formAuthentication = null;
                    break;
            }
        }

        private static void FormGeneric_Closing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            ((Form)sender).Hide();
        }
    }
}
