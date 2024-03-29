﻿namespace DNS_Roaming_Client
{
    partial class FrmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSettings));
            this.listViewRules = new System.Windows.Forms.ListView();
            this.colImage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColWhen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNetworkID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPING = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colThen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.btnRuleEdit = new System.Windows.Forms.Button();
            this.btnRuleNew = new System.Windows.Forms.Button();
            this.btnRuleRemove = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.linkGithub = new System.Windows.Forms.LinkLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabRules = new System.Windows.Forms.TabPage();
            this.btnRuleCopy = new System.Windows.Forms.Button();
            this.tabNetwork = new System.Windows.Forms.TabPage();
            this.groupIPV6 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkIPV6Disable = new System.Windows.Forms.CheckBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabLogs = new System.Windows.Forms.TabPage();
            this.tabUpdates = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.retainLogDays = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRuleSetURL = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ruleSetUpdateDays = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.autoUpdateDays = new System.Windows.Forms.NumericUpDown();
            this.chkAutoupdate = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chkUpdateDoHAddresses = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbDoHFallbackToUDP = new System.Windows.Forms.ComboBox();
            this.cmbDoHAutoUpgrade = new System.Windows.Forms.ComboBox();
            this.tabControlSettings.SuspendLayout();
            this.tabRules.SuspendLayout();
            this.tabNetwork.SuspendLayout();
            this.groupIPV6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.tabLogs.SuspendLayout();
            this.tabUpdates.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.retainLogDays)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ruleSetUpdateDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.autoUpdateDays)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewRules
            // 
            this.listViewRules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colImage,
            this.colID,
            this.ColWhen,
            this.colNetworkID,
            this.colPING,
            this.colThen});
            this.listViewRules.FullRowSelect = true;
            this.listViewRules.HideSelection = false;
            this.listViewRules.LargeImageList = this.imageList;
            this.listViewRules.Location = new System.Drawing.Point(17, 15);
            this.listViewRules.MultiSelect = false;
            this.listViewRules.Name = "listViewRules";
            this.listViewRules.Size = new System.Drawing.Size(581, 221);
            this.listViewRules.SmallImageList = this.imageList;
            this.listViewRules.TabIndex = 2;
            this.listViewRules.UseCompatibleStateImageBehavior = false;
            this.listViewRules.View = System.Windows.Forms.View.Details;
            this.listViewRules.SelectedIndexChanged += new System.EventHandler(this.listViewRules_SelectedIndexChanged);
            this.listViewRules.DoubleClick += new System.EventHandler(this.listViewRules_DoubleClick);
            this.listViewRules.MouseHover += new System.EventHandler(this.listViewRules_MouseHover);
            // 
            // colImage
            // 
            this.colImage.Text = "";
            this.colImage.Width = 32;
            // 
            // colID
            // 
            this.colID.Text = "ID";
            this.colID.Width = 0;
            // 
            // ColWhen
            // 
            this.ColWhen.Text = "When";
            this.ColWhen.Width = 150;
            // 
            // colNetworkID
            // 
            this.colNetworkID.Text = "and Network";
            this.colNetworkID.Width = 150;
            // 
            // colPING
            // 
            this.colPING.Text = "and";
            this.colPING.Width = 90;
            // 
            // colThen
            // 
            this.colThen.Text = "Then set";
            this.colThen.Width = 150;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "download.png");
            // 
            // btnRuleEdit
            // 
            this.btnRuleEdit.Enabled = false;
            this.btnRuleEdit.Location = new System.Drawing.Point(277, 246);
            this.btnRuleEdit.Name = "btnRuleEdit";
            this.btnRuleEdit.Size = new System.Drawing.Size(103, 23);
            this.btnRuleEdit.TabIndex = 5;
            this.btnRuleEdit.Text = "Edit";
            this.btnRuleEdit.UseVisualStyleBackColor = true;
            this.btnRuleEdit.Click += new System.EventHandler(this.btnRuleEdit_Click);
            // 
            // btnRuleNew
            // 
            this.btnRuleNew.Location = new System.Drawing.Point(168, 247);
            this.btnRuleNew.Name = "btnRuleNew";
            this.btnRuleNew.Size = new System.Drawing.Size(103, 23);
            this.btnRuleNew.TabIndex = 4;
            this.btnRuleNew.Text = "New";
            this.btnRuleNew.UseVisualStyleBackColor = true;
            this.btnRuleNew.Click += new System.EventHandler(this.btnRuleNew_Click);
            // 
            // btnRuleRemove
            // 
            this.btnRuleRemove.Enabled = false;
            this.btnRuleRemove.Location = new System.Drawing.Point(495, 246);
            this.btnRuleRemove.Name = "btnRuleRemove";
            this.btnRuleRemove.Size = new System.Drawing.Size(103, 24);
            this.btnRuleRemove.TabIndex = 3;
            this.btnRuleRemove.Text = "Remove";
            this.btnRuleRemove.UseVisualStyleBackColor = true;
            this.btnRuleRemove.Click += new System.EventHandler(this.btnRuleRemove_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(399, 333);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(103, 23);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Save and Close";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(508, 333);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(103, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // linkGithub
            // 
            this.linkGithub.AutoSize = true;
            this.linkGithub.Location = new System.Drawing.Point(3, 333);
            this.linkGithub.Name = "linkGithub";
            this.linkGithub.Size = new System.Drawing.Size(126, 13);
            this.linkGithub.TabIndex = 18;
            this.linkGithub.TabStop = true;
            this.linkGithub.Text = "DNS Roaming on GitHub";
            this.linkGithub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkGithub_LinkClicked);
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.Controls.Add(this.tabRules);
            this.tabControlSettings.Controls.Add(this.tabNetwork);
            this.tabControlSettings.Controls.Add(this.tabLogs);
            this.tabControlSettings.Controls.Add(this.tabUpdates);
            this.tabControlSettings.Location = new System.Drawing.Point(-1, 2);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            this.tabControlSettings.Size = new System.Drawing.Size(612, 316);
            this.tabControlSettings.TabIndex = 19;
            // 
            // tabRules
            // 
            this.tabRules.Controls.Add(this.btnRuleCopy);
            this.tabRules.Controls.Add(this.btnRuleEdit);
            this.tabRules.Controls.Add(this.btnRuleNew);
            this.tabRules.Controls.Add(this.listViewRules);
            this.tabRules.Controls.Add(this.btnRuleRemove);
            this.tabRules.Location = new System.Drawing.Point(4, 22);
            this.tabRules.Name = "tabRules";
            this.tabRules.Padding = new System.Windows.Forms.Padding(3);
            this.tabRules.Size = new System.Drawing.Size(604, 290);
            this.tabRules.TabIndex = 0;
            this.tabRules.Text = "Rules";
            this.tabRules.UseVisualStyleBackColor = true;
            // 
            // btnRuleCopy
            // 
            this.btnRuleCopy.Enabled = false;
            this.btnRuleCopy.Location = new System.Drawing.Point(386, 247);
            this.btnRuleCopy.Name = "btnRuleCopy";
            this.btnRuleCopy.Size = new System.Drawing.Size(103, 23);
            this.btnRuleCopy.TabIndex = 6;
            this.btnRuleCopy.Text = "Copy";
            this.btnRuleCopy.UseVisualStyleBackColor = true;
            this.btnRuleCopy.Click += new System.EventHandler(this.btnRuleCopy_Click);
            // 
            // tabNetwork
            // 
            this.tabNetwork.Controls.Add(this.groupBox3);
            this.tabNetwork.Controls.Add(this.groupIPV6);
            this.tabNetwork.Location = new System.Drawing.Point(4, 22);
            this.tabNetwork.Name = "tabNetwork";
            this.tabNetwork.Size = new System.Drawing.Size(604, 290);
            this.tabNetwork.TabIndex = 1;
            this.tabNetwork.Text = "Network Options";
            this.tabNetwork.UseVisualStyleBackColor = true;
            // 
            // groupIPV6
            // 
            this.groupIPV6.Controls.Add(this.label1);
            this.groupIPV6.Controls.Add(this.chkIPV6Disable);
            this.groupIPV6.Location = new System.Drawing.Point(16, 16);
            this.groupIPV6.Name = "groupIPV6";
            this.groupIPV6.Size = new System.Drawing.Size(573, 74);
            this.groupIPV6.TabIndex = 0;
            this.groupIPV6.TabStop = false;
            this.groupIPV6.Text = "IPV6";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Location = new System.Drawing.Point(24, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(310, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "* Recommended if you don\'t have a mature IPV6 implementation";
            // 
            // chkIPV6Disable
            // 
            this.chkIPV6Disable.AutoSize = true;
            this.chkIPV6Disable.Location = new System.Drawing.Point(24, 24);
            this.chkIPV6Disable.Name = "chkIPV6Disable";
            this.chkIPV6Disable.Size = new System.Drawing.Size(161, 17);
            this.chkIPV6Disable.TabIndex = 0;
            this.chkIPV6Disable.Text = "Disable IPV6 for all networks";
            this.chkIPV6Disable.UseVisualStyleBackColor = true;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // tabLogs
            // 
            this.tabLogs.Controls.Add(this.groupBox1);
            this.tabLogs.Location = new System.Drawing.Point(4, 22);
            this.tabLogs.Name = "tabLogs";
            this.tabLogs.Size = new System.Drawing.Size(604, 290);
            this.tabLogs.TabIndex = 2;
            this.tabLogs.Text = "Logs";
            this.tabLogs.UseVisualStyleBackColor = true;
            // 
            // tabUpdates
            // 
            this.tabUpdates.Controls.Add(this.groupBox2);
            this.tabUpdates.Location = new System.Drawing.Point(4, 22);
            this.tabUpdates.Name = "tabUpdates";
            this.tabUpdates.Size = new System.Drawing.Size(604, 290);
            this.tabUpdates.TabIndex = 3;
            this.tabUpdates.Text = "Updates";
            this.tabUpdates.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Remove log files older than ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.retainLogDays);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(18, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(572, 60);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Logs";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(249, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "days";
            // 
            // retainLogDays
            // 
            this.retainLogDays.Location = new System.Drawing.Point(193, 26);
            this.retainLogDays.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.retainLogDays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.retainLogDays.Name = "retainLogDays";
            this.retainLogDays.Size = new System.Drawing.Size(50, 20);
            this.retainLogDays.TabIndex = 1;
            this.retainLogDays.Value = new decimal(new int[] {
            14,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtRuleSetURL);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.ruleSetUpdateDays);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.autoUpdateDays);
            this.groupBox2.Controls.Add(this.chkAutoupdate);
            this.groupBox2.Location = new System.Drawing.Point(17, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(573, 106);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Updates";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(148, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Re-download Rule Sets every";
            // 
            // txtRuleSetURL
            // 
            this.txtRuleSetURL.Location = new System.Drawing.Point(91, 75);
            this.txtRuleSetURL.Name = "txtRuleSetURL";
            this.txtRuleSetURL.Size = new System.Drawing.Size(408, 20);
            this.txtRuleSetURL.TabIndex = 10;
            this.txtRuleSetURL.WordWrap = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "From URL:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(250, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "days";
            // 
            // ruleSetUpdateDays
            // 
            this.ruleSetUpdateDays.Location = new System.Drawing.Point(194, 51);
            this.ruleSetUpdateDays.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.ruleSetUpdateDays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ruleSetUpdateDays.Name = "ruleSetUpdateDays";
            this.ruleSetUpdateDays.Size = new System.Drawing.Size(50, 20);
            this.ruleSetUpdateDays.TabIndex = 7;
            this.ruleSetUpdateDays.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(250, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "days";
            // 
            // autoUpdateDays
            // 
            this.autoUpdateDays.Location = new System.Drawing.Point(194, 22);
            this.autoUpdateDays.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.autoUpdateDays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.autoUpdateDays.Name = "autoUpdateDays";
            this.autoUpdateDays.Size = new System.Drawing.Size(50, 20);
            this.autoUpdateDays.TabIndex = 4;
            this.autoUpdateDays.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // chkAutoupdate
            // 
            this.chkAutoupdate.AutoSize = true;
            this.chkAutoupdate.Location = new System.Drawing.Point(27, 25);
            this.chkAutoupdate.Name = "chkAutoupdate";
            this.chkAutoupdate.Size = new System.Drawing.Size(153, 17);
            this.chkAutoupdate.TabIndex = 2;
            this.chkAutoupdate.Text = "Automatically update every";
            this.chkAutoupdate.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbDoHAutoUpgrade);
            this.groupBox3.Controls.Add(this.cmbDoHFallbackToUDP);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.chkUpdateDoHAddresses);
            this.groupBox3.Location = new System.Drawing.Point(16, 96);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(573, 168);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "DNS over HTTPS (DoH)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label8.Location = new System.Drawing.Point(24, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(290, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "* Adds new DoH server addresses that match the DNS Sets";
            // 
            // chkUpdateDoHAddresses
            // 
            this.chkUpdateDoHAddresses.AutoSize = true;
            this.chkUpdateDoHAddresses.Location = new System.Drawing.Point(24, 24);
            this.chkUpdateDoHAddresses.Name = "chkUpdateDoHAddresses";
            this.chkUpdateDoHAddresses.Size = new System.Drawing.Size(160, 17);
            this.chkUpdateDoHAddresses.TabIndex = 0;
            this.chkUpdateDoHAddresses.Text = "Update the DoH Address list";
            this.chkUpdateDoHAddresses.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(117, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Allow Fallback To Udp:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 118);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(118, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Use DoH automatically:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label11.Location = new System.Drawing.Point(24, 94);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(317, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "* fallback to unencrypted DNS if the DoH query fails for all servers";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label12.Location = new System.Drawing.Point(24, 141);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(274, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "*encrypt all name resolutions to all servers using the DoH";
            // 
            // cmbDoHFallbackToUDP
            // 
            this.cmbDoHFallbackToUDP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDoHFallbackToUDP.FormattingEnabled = true;
            this.cmbDoHFallbackToUDP.Items.AddRange(new object[] {
            "Leave as Default",
            "Force to Allow Fallback",
            "Force to Disallow Fallback"});
            this.cmbDoHFallbackToUDP.Location = new System.Drawing.Point(149, 69);
            this.cmbDoHFallbackToUDP.Name = "cmbDoHFallbackToUDP";
            this.cmbDoHFallbackToUDP.Size = new System.Drawing.Size(190, 21);
            this.cmbDoHFallbackToUDP.TabIndex = 6;
            this.cmbDoHFallbackToUDP.SelectedIndexChanged += new System.EventHandler(this.cmbDoHFallbackToUDP_SelectedIndexChanged);
            // 
            // cmbDoHAutoUpgrade
            // 
            this.cmbDoHAutoUpgrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDoHAutoUpgrade.FormattingEnabled = true;
            this.cmbDoHAutoUpgrade.Items.AddRange(new object[] {
            "Leave as Default",
            "Force to Automatic",
            "Force to Not Automatic"});
            this.cmbDoHAutoUpgrade.Location = new System.Drawing.Point(149, 114);
            this.cmbDoHAutoUpgrade.Name = "cmbDoHAutoUpgrade";
            this.cmbDoHAutoUpgrade.Size = new System.Drawing.Size(190, 21);
            this.cmbDoHAutoUpgrade.TabIndex = 7;
            this.cmbDoHAutoUpgrade.SelectedIndexChanged += new System.EventHandler(this.cmbDoHAutoUpgrade_SelectedIndexChanged);
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(616, 364);
            this.ControlBox = false;
            this.Controls.Add(this.tabControlSettings);
            this.Controls.Add(this.linkGithub);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DNS Roaming Settings";
            this.tabControlSettings.ResumeLayout(false);
            this.tabRules.ResumeLayout(false);
            this.tabNetwork.ResumeLayout(false);
            this.groupIPV6.ResumeLayout(false);
            this.groupIPV6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.tabLogs.ResumeLayout(false);
            this.tabUpdates.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.retainLogDays)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ruleSetUpdateDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.autoUpdateDays)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView listViewRules;
        private System.Windows.Forms.ColumnHeader ColWhen;
        private System.Windows.Forms.ColumnHeader colNetworkID;
        private System.Windows.Forms.ColumnHeader colThen;
        private System.Windows.Forms.ColumnHeader colID;
        private System.Windows.Forms.Button btnRuleEdit;
        private System.Windows.Forms.Button btnRuleNew;
        private System.Windows.Forms.Button btnRuleRemove;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.LinkLabel linkGithub;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TabControl tabControlSettings;
        private System.Windows.Forms.TabPage tabRules;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TabPage tabNetwork;
        private System.Windows.Forms.GroupBox groupIPV6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkIPV6Disable;
        private System.Windows.Forms.Button btnRuleCopy;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ColumnHeader colImage;
        private System.Windows.Forms.ColumnHeader colPING;
        private System.Windows.Forms.TabPage tabLogs;
        private System.Windows.Forms.TabPage tabUpdates;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown retainLogDays;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRuleSetURL;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown ruleSetUpdateDays;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown autoUpdateDays;
        private System.Windows.Forms.CheckBox chkAutoupdate;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkUpdateDoHAddresses;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbDoHAutoUpgrade;
        private System.Windows.Forms.ComboBox cmbDoHFallbackToUDP;
    }
}

