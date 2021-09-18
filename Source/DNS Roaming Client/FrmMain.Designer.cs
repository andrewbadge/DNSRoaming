namespace DNS_Roaming_Client
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLogs = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuLogs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuLogsClient = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLogsService = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLogsFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuServiceStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStopAndClose = new System.Windows.Forms.ToolStripMenuItem();
            this.btnForceClose = new System.Windows.Forms.Button();
            this.timerCheckServiceStatus = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip.SuspendLayout();
            this.contextMenuLogs.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "DNS Roaming Client";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "This form shouldn\'t be visible";
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "DNS Roaming";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSettings,
            this.menuLogs,
            this.menuAbout,
            this.toolStripSeparator2,
            this.menuServiceStatus,
            this.toolStripSeparator1,
            this.menuStopAndClose});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(134, 126);
            this.contextMenuStrip.DoubleClick += new System.EventHandler(this.contextMenuStrip_DoubleClick);
            // 
            // menuSettings
            // 
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Size = new System.Drawing.Size(133, 22);
            this.menuSettings.Text = "Settings";
            this.menuSettings.Click += new System.EventHandler(this.menuSettings_Click);
            // 
            // menuLogs
            // 
            this.menuLogs.DropDown = this.contextMenuLogs;
            this.menuLogs.Name = "menuLogs";
            this.menuLogs.Size = new System.Drawing.Size(133, 22);
            this.menuLogs.Text = "Logs";
            // 
            // contextMenuLogs
            // 
            this.contextMenuLogs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuLogsClient,
            this.menuLogsService,
            this.menuLogsFolder});
            this.contextMenuLogs.Name = "contextMenuLogs";
            this.contextMenuLogs.OwnerItem = this.menuLogs;
            this.contextMenuLogs.Size = new System.Drawing.Size(191, 70);
            // 
            // menuLogsClient
            // 
            this.menuLogsClient.Name = "menuLogsClient";
            this.menuLogsClient.Size = new System.Drawing.Size(190, 22);
            this.menuLogsClient.Text = "View latest Client log";
            this.menuLogsClient.Click += new System.EventHandler(this.menuLogsClient_Click);
            // 
            // menuLogsService
            // 
            this.menuLogsService.Name = "menuLogsService";
            this.menuLogsService.Size = new System.Drawing.Size(190, 22);
            this.menuLogsService.Text = "View latest Service log";
            this.menuLogsService.Click += new System.EventHandler(this.menuLogsService_Click);
            // 
            // menuLogsFolder
            // 
            this.menuLogsFolder.Name = "menuLogsFolder";
            this.menuLogsFolder.Size = new System.Drawing.Size(190, 22);
            this.menuLogsFolder.Text = "Open Logs Folder";
            this.menuLogsFolder.Click += new System.EventHandler(this.menuLogsFolder_Click);
            // 
            // menuAbout
            // 
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size(133, 22);
            this.menuAbout.Text = "About";
            this.menuAbout.Click += new System.EventHandler(this.menuAbout_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(130, 6);
            // 
            // menuServiceStatus
            // 
            this.menuServiceStatus.Name = "menuServiceStatus";
            this.menuServiceStatus.Size = new System.Drawing.Size(133, 22);
            this.menuServiceStatus.Text = "Checking...";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(130, 6);
            // 
            // menuStopAndClose
            // 
            this.menuStopAndClose.Name = "menuStopAndClose";
            this.menuStopAndClose.Size = new System.Drawing.Size(133, 22);
            this.menuStopAndClose.Text = "Exit Client";
            this.menuStopAndClose.Click += new System.EventHandler(this.menuStopAndClose_Click);
            // 
            // btnForceClose
            // 
            this.btnForceClose.Location = new System.Drawing.Point(221, 67);
            this.btnForceClose.Name = "btnForceClose";
            this.btnForceClose.Size = new System.Drawing.Size(75, 23);
            this.btnForceClose.TabIndex = 2;
            this.btnForceClose.Text = "Force Close";
            this.btnForceClose.UseVisualStyleBackColor = true;
            this.btnForceClose.Click += new System.EventHandler(this.btnForceClose_Click);
            // 
            // timerCheckServiceStatus
            // 
            this.timerCheckServiceStatus.Tick += new System.EventHandler(this.timerCheckServiceStatus_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 102);
            this.ControlBox = false;
            this.Controls.Add(this.btnForceClose);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FrmMain";
            this.ShowInTaskbar = false;
            this.Text = "DNS Roaming";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.contextMenuStrip.ResumeLayout(false);
            this.contextMenuLogs.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Button btnForceClose;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuServiceStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuStopAndClose;
        private System.Windows.Forms.ToolStripMenuItem menuSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Timer timerCheckServiceStatus;
        private System.Windows.Forms.ToolStripMenuItem menuLogs;
        private System.Windows.Forms.ContextMenuStrip contextMenuLogs;
        private System.Windows.Forms.ToolStripMenuItem menuLogsClient;
        private System.Windows.Forms.ToolStripMenuItem menuLogsService;
        private System.Windows.Forms.ToolStripMenuItem menuLogsFolder;
        private System.Windows.Forms.ToolStripMenuItem menuAbout;
    }
}