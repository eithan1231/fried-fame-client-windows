
namespace fried_fame_client_windows.Forms
{
    partial class FormHome
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
            this.listViewServers = new System.Windows.Forms.ListView();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.buttonConnect = new MaterialSkin.Controls.MaterialRaisedButton();
            this.buttonDisconnect = new MaterialSkin.Controls.MaterialRaisedButton();
            this.buttonLogout = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialDivider2 = new MaterialSkin.Controls.MaterialDivider();
            this.materialDivider3 = new MaterialSkin.Controls.MaterialDivider();
            this.buttonReload = new MaterialSkin.Controls.MaterialRaisedButton();
            this.labelSelectedServer = new System.Windows.Forms.Label();
            this.labelSelectedLocation = new System.Windows.Forms.Label();
            this.labelSelectedLatency = new System.Windows.Forms.Label();
            this.labelConnectedIPAddress = new System.Windows.Forms.Label();
            this.labelConnectedLocation = new System.Windows.Forms.Label();
            this.labelConnectedServer = new System.Windows.Forms.Label();
            this.labelConnectedBandwidth = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonUpdate = new MaterialSkin.Controls.MaterialRaisedButton();
            this.SuspendLayout();
            // 
            // listViewServers
            // 
            this.listViewServers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewServers.AutoArrange = false;
            this.listViewServers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewServers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewServers.FullRowSelect = true;
            this.listViewServers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewServers.HideSelection = false;
            this.listViewServers.Location = new System.Drawing.Point(12, 74);
            this.listViewServers.MultiSelect = false;
            this.listViewServers.Name = "listViewServers";
            this.listViewServers.Size = new System.Drawing.Size(581, 217);
            this.listViewServers.TabIndex = 0;
            this.listViewServers.UseCompatibleStateImageBehavior = false;
            this.listViewServers.View = System.Windows.Forms.View.Details;
            this.listViewServers.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.listViewServers_ColumnWidthChanging);
            this.listViewServers.SelectedIndexChanged += new System.EventHandler(this.listViewServers_SelectedIndexChanged);
            // 
            // materialDivider1
            // 
            this.materialDivider1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialDivider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider1.Depth = 0;
            this.materialDivider1.Location = new System.Drawing.Point(12, 297);
            this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            this.materialDivider1.Size = new System.Drawing.Size(581, 1);
            this.materialDivider1.TabIndex = 1;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonConnect.Depth = 0;
            this.buttonConnect.Location = new System.Drawing.Point(12, 304);
            this.buttonConnect.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Primary = true;
            this.buttonConnect.Size = new System.Drawing.Size(122, 25);
            this.buttonConnect.TabIndex = 2;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDisconnect.Depth = 0;
            this.buttonDisconnect.Location = new System.Drawing.Point(12, 335);
            this.buttonDisconnect.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Primary = true;
            this.buttonDisconnect.Size = new System.Drawing.Size(122, 25);
            this.buttonDisconnect.TabIndex = 3;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // buttonLogout
            // 
            this.buttonLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLogout.Depth = 0;
            this.buttonLogout.Location = new System.Drawing.Point(530, 32);
            this.buttonLogout.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Primary = true;
            this.buttonLogout.Size = new System.Drawing.Size(63, 23);
            this.buttonLogout.TabIndex = 4;
            this.buttonLogout.Text = "logout";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // materialDivider2
            // 
            this.materialDivider2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.materialDivider2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider2.Depth = 0;
            this.materialDivider2.Location = new System.Drawing.Point(360, 304);
            this.materialDivider2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider2.Name = "materialDivider2";
            this.materialDivider2.Size = new System.Drawing.Size(1, 91);
            this.materialDivider2.TabIndex = 6;
            this.materialDivider2.Text = "materialDivider2";
            // 
            // materialDivider3
            // 
            this.materialDivider3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.materialDivider3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider3.Depth = 0;
            this.materialDivider3.Location = new System.Drawing.Point(140, 301);
            this.materialDivider3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider3.Name = "materialDivider3";
            this.materialDivider3.Size = new System.Drawing.Size(1, 94);
            this.materialDivider3.TabIndex = 8;
            this.materialDivider3.Text = "materialDivider3";
            // 
            // buttonReload
            // 
            this.buttonReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReload.Depth = 0;
            this.buttonReload.Location = new System.Drawing.Point(461, 32);
            this.buttonReload.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonReload.Name = "buttonReload";
            this.buttonReload.Primary = true;
            this.buttonReload.Size = new System.Drawing.Size(63, 23);
            this.buttonReload.TabIndex = 23;
            this.buttonReload.Text = "Reload";
            this.buttonReload.UseVisualStyleBackColor = true;
            this.buttonReload.Click += new System.EventHandler(this.buttonReload_Click);
            // 
            // labelSelectedServer
            // 
            this.labelSelectedServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSelectedServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSelectedServer.Location = new System.Drawing.Point(211, 304);
            this.labelSelectedServer.Name = "labelSelectedServer";
            this.labelSelectedServer.Size = new System.Drawing.Size(143, 23);
            this.labelSelectedServer.TabIndex = 24;
            this.labelSelectedServer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelSelectedLocation
            // 
            this.labelSelectedLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSelectedLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSelectedLocation.Location = new System.Drawing.Point(211, 327);
            this.labelSelectedLocation.Name = "labelSelectedLocation";
            this.labelSelectedLocation.Size = new System.Drawing.Size(143, 23);
            this.labelSelectedLocation.TabIndex = 25;
            this.labelSelectedLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelSelectedLatency
            // 
            this.labelSelectedLatency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSelectedLatency.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSelectedLatency.Location = new System.Drawing.Point(211, 350);
            this.labelSelectedLatency.Name = "labelSelectedLatency";
            this.labelSelectedLatency.Size = new System.Drawing.Size(143, 23);
            this.labelSelectedLatency.TabIndex = 26;
            this.labelSelectedLatency.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelConnectedIPAddress
            // 
            this.labelConnectedIPAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelConnectedIPAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConnectedIPAddress.Location = new System.Drawing.Point(446, 351);
            this.labelConnectedIPAddress.Name = "labelConnectedIPAddress";
            this.labelConnectedIPAddress.Size = new System.Drawing.Size(147, 23);
            this.labelConnectedIPAddress.TabIndex = 29;
            this.labelConnectedIPAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelConnectedLocation
            // 
            this.labelConnectedLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelConnectedLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConnectedLocation.Location = new System.Drawing.Point(446, 328);
            this.labelConnectedLocation.Name = "labelConnectedLocation";
            this.labelConnectedLocation.Size = new System.Drawing.Size(147, 23);
            this.labelConnectedLocation.TabIndex = 28;
            this.labelConnectedLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelConnectedServer
            // 
            this.labelConnectedServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelConnectedServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConnectedServer.Location = new System.Drawing.Point(446, 305);
            this.labelConnectedServer.Name = "labelConnectedServer";
            this.labelConnectedServer.Size = new System.Drawing.Size(147, 23);
            this.labelConnectedServer.TabIndex = 27;
            this.labelConnectedServer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelConnectedBandwidth
            // 
            this.labelConnectedBandwidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelConnectedBandwidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConnectedBandwidth.Location = new System.Drawing.Point(446, 374);
            this.labelConnectedBandwidth.Name = "labelConnectedBandwidth";
            this.labelConnectedBandwidth.Size = new System.Drawing.Size(147, 23);
            this.labelConnectedBandwidth.TabIndex = 30;
            this.labelConnectedBandwidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(12, 370);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(122, 23);
            this.labelStatus.TabIndex = 31;
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelStatus.DoubleClick += new System.EventHandler(this.labelStatus_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(147, 308);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 16);
            this.label1.TabIndex = 32;
            this.label1.Text = "Server:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(147, 330);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 16);
            this.label2.TabIndex = 33;
            this.label2.Text = "Location:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(147, 353);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 16);
            this.label3.TabIndex = 34;
            this.label3.Text = "Latency:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(367, 354);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 16);
            this.label4.TabIndex = 37;
            this.label4.Text = "IP Address:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(367, 331);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 16);
            this.label5.TabIndex = 36;
            this.label5.Text = "Location:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(367, 308);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 16);
            this.label6.TabIndex = 35;
            this.label6.Text = "Server:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(367, 377);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 16);
            this.label7.TabIndex = 38;
            this.label7.Text = "Bandwidth:";
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpdate.Depth = 0;
            this.buttonUpdate.Location = new System.Drawing.Point(392, 32);
            this.buttonUpdate.MouseState = MaterialSkin.MouseState.HOVER;
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Primary = true;
            this.buttonUpdate.Size = new System.Drawing.Size(63, 23);
            this.buttonUpdate.TabIndex = 39;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Visible = false;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // FormHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 404);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.labelConnectedBandwidth);
            this.Controls.Add(this.labelConnectedIPAddress);
            this.Controls.Add(this.labelConnectedLocation);
            this.Controls.Add(this.labelConnectedServer);
            this.Controls.Add(this.labelSelectedLatency);
            this.Controls.Add(this.labelSelectedLocation);
            this.Controls.Add(this.labelSelectedServer);
            this.Controls.Add(this.buttonReload);
            this.Controls.Add(this.materialDivider3);
            this.Controls.Add(this.materialDivider2);
            this.Controls.Add(this.buttonLogout);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.materialDivider1);
            this.Controls.Add(this.listViewServers);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "FormHome";
            this.Sizable = false;
            this.Load += new System.EventHandler(this.FormHome_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewServers;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private MaterialSkin.Controls.MaterialRaisedButton buttonConnect;
        private MaterialSkin.Controls.MaterialRaisedButton buttonDisconnect;
        private MaterialSkin.Controls.MaterialRaisedButton buttonLogout;
        private MaterialSkin.Controls.MaterialDivider materialDivider2;
        private MaterialSkin.Controls.MaterialDivider materialDivider3;
        private MaterialSkin.Controls.MaterialRaisedButton buttonReload;
        private System.Windows.Forms.Label labelSelectedServer;
        private System.Windows.Forms.Label labelSelectedLocation;
        private System.Windows.Forms.Label labelSelectedLatency;
        private System.Windows.Forms.Label labelConnectedIPAddress;
        private System.Windows.Forms.Label labelConnectedLocation;
        private System.Windows.Forms.Label labelConnectedServer;
        private System.Windows.Forms.Label labelConnectedBandwidth;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private MaterialSkin.Controls.MaterialRaisedButton buttonUpdate;
    }
}