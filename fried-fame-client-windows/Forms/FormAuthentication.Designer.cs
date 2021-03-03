
namespace fried_fame_client_windows
{
    partial class FormAuthentication
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textUsername = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.textPassword = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabelUsername = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabelPassword = new MaterialSkin.Controls.MaterialLabel();
            this.buttonLogin = new MaterialSkin.Controls.MaterialRaisedButton();
            this.buttonRegister = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialLabelStatus = new MaterialSkin.Controls.MaterialLabel();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.materialLabelUpdate = new MaterialSkin.Controls.MaterialLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // textUsername
            // 
            this.textUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textUsername.Depth = 0;
            this.textUsername.Hint = "Username";
            this.textUsername.Location = new System.Drawing.Point(38, 278);
            this.textUsername.MouseState = MaterialSkin.MouseState.HOVER;
            this.textUsername.Name = "textUsername";
            this.textUsername.PasswordChar = '\0';
            this.textUsername.SelectedText = "";
            this.textUsername.SelectionLength = 0;
            this.textUsername.SelectionStart = 0;
            this.textUsername.Size = new System.Drawing.Size(250, 23);
            this.textUsername.TabIndex = 0;
            this.textUsername.TabStop = false;
            this.textUsername.UseSystemPasswordChar = false;
            this.textUsername.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textUsername_KeyUp);
            // 
            // textPassword
            // 
            this.textPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textPassword.Depth = 0;
            this.textPassword.Hint = "Password";
            this.textPassword.Location = new System.Drawing.Point(38, 356);
            this.textPassword.MouseState = MaterialSkin.MouseState.HOVER;
            this.textPassword.Name = "textPassword";
            this.textPassword.PasswordChar = '\0';
            this.textPassword.SelectedText = "";
            this.textPassword.SelectionLength = 0;
            this.textPassword.SelectionStart = 0;
            this.textPassword.Size = new System.Drawing.Size(250, 23);
            this.textPassword.TabIndex = 1;
            this.textPassword.TabStop = false;
            this.textPassword.UseSystemPasswordChar = true;
            this.textPassword.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textPassword_KeyUp);
            // 
            // materialLabelUsername
            // 
            this.materialLabelUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialLabelUsername.AutoSize = true;
            this.materialLabelUsername.Depth = 0;
            this.materialLabelUsername.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabelUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabelUsername.Location = new System.Drawing.Point(21, 250);
            this.materialLabelUsername.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabelUsername.Name = "materialLabelUsername";
            this.materialLabelUsername.Size = new System.Drawing.Size(77, 19);
            this.materialLabelUsername.TabIndex = 2;
            this.materialLabelUsername.Text = "Username";
            // 
            // materialLabelPassword
            // 
            this.materialLabelPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialLabelPassword.AutoSize = true;
            this.materialLabelPassword.Depth = 0;
            this.materialLabelPassword.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabelPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabelPassword.Location = new System.Drawing.Point(22, 328);
            this.materialLabelPassword.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabelPassword.Name = "materialLabelPassword";
            this.materialLabelPassword.Size = new System.Drawing.Size(75, 19);
            this.materialLabelPassword.TabIndex = 3;
            this.materialLabelPassword.Text = "Password";
            // 
            // buttonLogin
            // 
            this.buttonLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonLogin.Depth = 0;
            this.buttonLogin.Location = new System.Drawing.Point(24, 395);
            this.buttonLogin.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Primary = true;
            this.buttonLogin.Size = new System.Drawing.Size(122, 31);
            this.buttonLogin.TabIndex = 3;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // buttonRegister
            // 
            this.buttonRegister.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRegister.Depth = 0;
            this.buttonRegister.Location = new System.Drawing.Point(178, 395);
            this.buttonRegister.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Primary = true;
            this.buttonRegister.Size = new System.Drawing.Size(122, 31);
            this.buttonRegister.TabIndex = 4;
            this.buttonRegister.Text = "Register";
            this.buttonRegister.UseVisualStyleBackColor = true;
            this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);
            // 
            // materialLabelStatus
            // 
            this.materialLabelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialLabelStatus.Depth = 0;
            this.materialLabelStatus.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabelStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabelStatus.Location = new System.Drawing.Point(20, 445);
            this.materialLabelStatus.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabelStatus.Name = "materialLabelStatus";
            this.materialLabelStatus.Size = new System.Drawing.Size(280, 19);
            this.materialLabelStatus.TabIndex = 5;
            this.materialLabelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxLogo.Location = new System.Drawing.Point(12, 75);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(300, 145);
            this.pictureBoxLogo.TabIndex = 6;
            this.pictureBoxLogo.TabStop = false;
            // 
            // materialLabelUpdate
            // 
            this.materialLabelUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialLabelUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.materialLabelUpdate.Depth = 0;
            this.materialLabelUpdate.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabelUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabelUpdate.Location = new System.Drawing.Point(22, 225);
            this.materialLabelUpdate.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabelUpdate.Name = "materialLabelUpdate";
            this.materialLabelUpdate.Size = new System.Drawing.Size(280, 19);
            this.materialLabelUpdate.TabIndex = 7;
            this.materialLabelUpdate.Text = "Pending Update";
            this.materialLabelUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.materialLabelUpdate.Visible = false;
            this.materialLabelUpdate.Click += new System.EventHandler(this.materialLabelUpdate_Click);
            // 
            // FormAuthentication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 481);
            this.Controls.Add(this.materialLabelUpdate);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.materialLabelStatus);
            this.Controls.Add(this.buttonRegister);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.materialLabelPassword);
            this.Controls.Add(this.materialLabelUsername);
            this.Controls.Add(this.textPassword);
            this.Controls.Add(this.textUsername);
            this.MaximizeBox = false;
            this.Name = "FormAuthentication";
            this.Sizable = false;
            this.Load += new System.EventHandler(this.FormAuthentication_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialSingleLineTextField textUsername;
        private MaterialSkin.Controls.MaterialSingleLineTextField textPassword;
        private MaterialSkin.Controls.MaterialLabel materialLabelUsername;
        private MaterialSkin.Controls.MaterialLabel materialLabelPassword;
        private MaterialSkin.Controls.MaterialRaisedButton buttonLogin;
        private MaterialSkin.Controls.MaterialRaisedButton buttonRegister;
        private MaterialSkin.Controls.MaterialLabel materialLabelStatus;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private MaterialSkin.Controls.MaterialLabel materialLabelUpdate;
    }
}

