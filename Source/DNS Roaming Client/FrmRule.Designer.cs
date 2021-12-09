namespace DNS_Roaming_Client
{
    partial class FrmRule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRule));
            this.listNetworkType = new System.Windows.Forms.CheckedListBox();
            this.radioNetworkNameIs = new System.Windows.Forms.RadioButton();
            this.radioNetworkType = new System.Windows.Forms.RadioButton();
            this.cmbNetworkName = new System.Windows.Forms.ComboBox();
            this.cmbDNSset = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtPreferredDNS = new System.Windows.Forms.TextBox();
            this.txtAlternateDNS = new System.Windows.Forms.TextBox();
            this.groupNetworks = new System.Windows.Forms.GroupBox();
            this.radioNetworkNameIsNot = new System.Windows.Forms.RadioButton();
            this.groupAddressSpecific = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioAddressIsNotSpecific = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGetIPInfo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAddressIP = new System.Windows.Forms.TextBox();
            this.txtAddressSubnet = new System.Windows.Forms.TextBox();
            this.radioAddressIsSpecific = new System.Windows.Forms.RadioButton();
            this.groupDNSValues = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt3rdAlternateDNS = new System.Windows.Forms.TextBox();
            this.txt2ndAlternateDNS = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.upDownDelaySeconds = new System.Windows.Forms.NumericUpDown();
            this.btnDNSsetCopy = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupAddressOptions = new System.Windows.Forms.GroupBox();
            this.radioAddressByWAN = new System.Windows.Forms.RadioButton();
            this.radioAddressByLAN = new System.Windows.Forms.RadioButton();
            this.radioAddressByAny = new System.Windows.Forms.RadioButton();
            this.lblRuleDownloaded = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupNetworks.SuspendLayout();
            this.groupAddressSpecific.SuspendLayout();
            this.groupDNSValues.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownDelaySeconds)).BeginInit();
            this.groupAddressOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // listNetworkType
            // 
            this.listNetworkType.FormattingEnabled = true;
            this.listNetworkType.Location = new System.Drawing.Point(21, 42);
            this.listNetworkType.Name = "listNetworkType";
            this.listNetworkType.Size = new System.Drawing.Size(235, 109);
            this.listNetworkType.Sorted = true;
            this.listNetworkType.TabIndex = 0;
            // 
            // radioNetworkNameIs
            // 
            this.radioNetworkNameIs.AutoSize = true;
            this.radioNetworkNameIs.Location = new System.Drawing.Point(273, 19);
            this.radioNetworkNameIs.Name = "radioNetworkNameIs";
            this.radioNetworkNameIs.Size = new System.Drawing.Size(75, 17);
            this.radioNetworkNameIs.TabIndex = 3;
            this.radioNetworkNameIs.Text = "or Name is";
            this.radioNetworkNameIs.UseVisualStyleBackColor = true;
            // 
            // radioNetworkType
            // 
            this.radioNetworkType.AutoSize = true;
            this.radioNetworkType.Checked = true;
            this.radioNetworkType.Location = new System.Drawing.Point(6, 19);
            this.radioNetworkType.Name = "radioNetworkType";
            this.radioNetworkType.Size = new System.Drawing.Size(95, 17);
            this.radioNetworkType.TabIndex = 4;
            this.radioNetworkType.TabStop = true;
            this.radioNetworkType.Text = "Type is one of:";
            this.radioNetworkType.UseVisualStyleBackColor = true;
            // 
            // cmbNetworkName
            // 
            this.cmbNetworkName.FormattingEnabled = true;
            this.cmbNetworkName.Location = new System.Drawing.Point(288, 65);
            this.cmbNetworkName.Name = "cmbNetworkName";
            this.cmbNetworkName.Size = new System.Drawing.Size(235, 21);
            this.cmbNetworkName.Sorted = true;
            this.cmbNetworkName.TabIndex = 5;
            this.cmbNetworkName.SelectionChangeCommitted += new System.EventHandler(this.cmbNetworkName_SelectionChangeCommitted);
            this.cmbNetworkName.MouseHover += new System.EventHandler(this.cmbNetworkName_MouseHover);
            // 
            // cmbDNSset
            // 
            this.cmbDNSset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDNSset.FormattingEnabled = true;
            this.cmbDNSset.Location = new System.Drawing.Point(6, 19);
            this.cmbDNSset.Name = "cmbDNSset";
            this.cmbDNSset.Size = new System.Drawing.Size(325, 21);
            this.cmbDNSset.Sorted = true;
            this.cmbDNSset.TabIndex = 8;
            this.cmbDNSset.SelectedIndexChanged += new System.EventHandler(this.cmbDNSset_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "or";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Preferred DNS";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Alternate DNS";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(455, 521);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(101, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(348, 521);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(101, 23);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Save and Close";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // txtPreferredDNS
            // 
            this.txtPreferredDNS.Location = new System.Drawing.Point(113, 52);
            this.txtPreferredDNS.Name = "txtPreferredDNS";
            this.txtPreferredDNS.Size = new System.Drawing.Size(156, 20);
            this.txtPreferredDNS.TabIndex = 18;
            this.txtPreferredDNS.TextChanged += new System.EventHandler(this.txtPreferredDNS_TextChanged);
            // 
            // txtAlternateDNS
            // 
            this.txtAlternateDNS.Location = new System.Drawing.Point(113, 81);
            this.txtAlternateDNS.Name = "txtAlternateDNS";
            this.txtAlternateDNS.Size = new System.Drawing.Size(156, 20);
            this.txtAlternateDNS.TabIndex = 19;
            this.txtAlternateDNS.TextChanged += new System.EventHandler(this.txtAlternateDNS_TextChanged);
            // 
            // groupNetworks
            // 
            this.groupNetworks.Controls.Add(this.radioNetworkNameIsNot);
            this.groupNetworks.Controls.Add(this.listNetworkType);
            this.groupNetworks.Controls.Add(this.radioNetworkNameIs);
            this.groupNetworks.Controls.Add(this.radioNetworkType);
            this.groupNetworks.Controls.Add(this.cmbNetworkName);
            this.groupNetworks.Location = new System.Drawing.Point(12, 12);
            this.groupNetworks.Name = "groupNetworks";
            this.groupNetworks.Size = new System.Drawing.Size(544, 182);
            this.groupNetworks.TabIndex = 20;
            this.groupNetworks.TabStop = false;
            this.groupNetworks.Text = "When active network is";
            // 
            // radioNetworkNameIsNot
            // 
            this.radioNetworkNameIsNot.AutoSize = true;
            this.radioNetworkNameIsNot.Location = new System.Drawing.Point(273, 42);
            this.radioNetworkNameIsNot.Name = "radioNetworkNameIsNot";
            this.radioNetworkNameIsNot.Size = new System.Drawing.Size(93, 17);
            this.radioNetworkNameIsNot.TabIndex = 6;
            this.radioNetworkNameIsNot.Text = "or Name is not";
            this.radioNetworkNameIsNot.UseVisualStyleBackColor = true;
            // 
            // groupAddressSpecific
            // 
            this.groupAddressSpecific.Controls.Add(this.panel1);
            this.groupAddressSpecific.Controls.Add(this.radioAddressIsNotSpecific);
            this.groupAddressSpecific.Controls.Add(this.label2);
            this.groupAddressSpecific.Controls.Add(this.btnGetIPInfo);
            this.groupAddressSpecific.Controls.Add(this.label1);
            this.groupAddressSpecific.Controls.Add(this.txtAddressIP);
            this.groupAddressSpecific.Controls.Add(this.txtAddressSubnet);
            this.groupAddressSpecific.Controls.Add(this.radioAddressIsSpecific);
            this.groupAddressSpecific.Location = new System.Drawing.Point(12, 260);
            this.groupAddressSpecific.Name = "groupAddressSpecific";
            this.groupAddressSpecific.Size = new System.Drawing.Size(544, 80);
            this.groupAddressSpecific.TabIndex = 21;
            this.groupAddressSpecific.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(123, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2, 52);
            this.panel1.TabIndex = 25;
            // 
            // radioAddressIsNotSpecific
            // 
            this.radioAddressIsNotSpecific.AutoSize = true;
            this.radioAddressIsNotSpecific.Location = new System.Drawing.Point(8, 41);
            this.radioAddressIsNotSpecific.Name = "radioAddressIsNotSpecific";
            this.radioAddressIsNotSpecific.Size = new System.Drawing.Size(99, 17);
            this.radioAddressIsNotSpecific.TabIndex = 24;
            this.radioAddressIsNotSpecific.Text = "not in IP Range";
            this.radioAddressIsNotSpecific.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Address";
            // 
            // btnGetIPInfo
            // 
            this.btnGetIPInfo.Image = global::DNS_Roaming_Client.Properties.Resources.upload_file_16x16_1214209;
            this.btnGetIPInfo.Location = new System.Drawing.Point(357, 19);
            this.btnGetIPInfo.Name = "btnGetIPInfo";
            this.btnGetIPInfo.Size = new System.Drawing.Size(19, 23);
            this.btnGetIPInfo.TabIndex = 22;
            this.btnGetIPInfo.UseVisualStyleBackColor = true;
            this.btnGetIPInfo.Click += new System.EventHandler(this.btnGetIPInfo_Click);
            this.btnGetIPInfo.MouseHover += new System.EventHandler(this.btnGetIPInfo_MouseHover);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(133, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Subnet";
            // 
            // txtAddressIP
            // 
            this.txtAddressIP.Location = new System.Drawing.Point(192, 19);
            this.txtAddressIP.Name = "txtAddressIP";
            this.txtAddressIP.Size = new System.Drawing.Size(156, 20);
            this.txtAddressIP.TabIndex = 20;
            // 
            // txtAddressSubnet
            // 
            this.txtAddressSubnet.Location = new System.Drawing.Point(192, 45);
            this.txtAddressSubnet.Name = "txtAddressSubnet";
            this.txtAddressSubnet.Size = new System.Drawing.Size(156, 20);
            this.txtAddressSubnet.TabIndex = 19;
            // 
            // radioAddressIsSpecific
            // 
            this.radioAddressIsSpecific.AutoSize = true;
            this.radioAddressIsSpecific.Checked = true;
            this.radioAddressIsSpecific.Location = new System.Drawing.Point(8, 19);
            this.radioAddressIsSpecific.Name = "radioAddressIsSpecific";
            this.radioAddressIsSpecific.Size = new System.Drawing.Size(81, 17);
            this.radioAddressIsSpecific.TabIndex = 1;
            this.radioAddressIsSpecific.TabStop = true;
            this.radioAddressIsSpecific.Text = "in IP Range";
            this.radioAddressIsSpecific.UseVisualStyleBackColor = true;
            // 
            // groupDNSValues
            // 
            this.groupDNSValues.Controls.Add(this.label10);
            this.groupDNSValues.Controls.Add(this.label9);
            this.groupDNSValues.Controls.Add(this.label8);
            this.groupDNSValues.Controls.Add(this.txt3rdAlternateDNS);
            this.groupDNSValues.Controls.Add(this.txt2ndAlternateDNS);
            this.groupDNSValues.Controls.Add(this.label7);
            this.groupDNSValues.Controls.Add(this.label6);
            this.groupDNSValues.Controls.Add(this.upDownDelaySeconds);
            this.groupDNSValues.Controls.Add(this.btnDNSsetCopy);
            this.groupDNSValues.Controls.Add(this.cmbDNSset);
            this.groupDNSValues.Controls.Add(this.label3);
            this.groupDNSValues.Controls.Add(this.label4);
            this.groupDNSValues.Controls.Add(this.txtAlternateDNS);
            this.groupDNSValues.Controls.Add(this.label5);
            this.groupDNSValues.Controls.Add(this.txtPreferredDNS);
            this.groupDNSValues.Location = new System.Drawing.Point(12, 346);
            this.groupDNSValues.Name = "groupDNSValues";
            this.groupDNSValues.Size = new System.Drawing.Size(544, 156);
            this.groupDNSValues.TabIndex = 22;
            this.groupDNSValues.TabStop = false;
            this.groupDNSValues.Text = "Then set the DNS server to";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label10.Location = new System.Drawing.Point(9, 130);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(203, 13);
            this.label10.TabIndex = 31;
            this.label10.Text = "*Advanced: use a long delay with caution";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(303, 84);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "3rd Alt.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(303, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "2nd Alt.";
            // 
            // txt3rdAlternateDNS
            // 
            this.txt3rdAlternateDNS.Location = new System.Drawing.Point(357, 81);
            this.txt3rdAlternateDNS.Name = "txt3rdAlternateDNS";
            this.txt3rdAlternateDNS.Size = new System.Drawing.Size(156, 20);
            this.txt3rdAlternateDNS.TabIndex = 28;
            this.txt3rdAlternateDNS.TextChanged += new System.EventHandler(this.txt3rdAlternateDNS_TextChanged);
            // 
            // txt2ndAlternateDNS
            // 
            this.txt2ndAlternateDNS.Location = new System.Drawing.Point(357, 55);
            this.txt2ndAlternateDNS.Name = "txt2ndAlternateDNS";
            this.txt2ndAlternateDNS.Size = new System.Drawing.Size(156, 20);
            this.txt2ndAlternateDNS.TabIndex = 27;
            this.txt2ndAlternateDNS.TextChanged += new System.EventHandler(this.txt2ndAlternateDNS_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 109);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "after a delay of";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(143, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "seconds";
            // 
            // upDownDelaySeconds
            // 
            this.upDownDelaySeconds.Location = new System.Drawing.Point(89, 107);
            this.upDownDelaySeconds.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.upDownDelaySeconds.Name = "upDownDelaySeconds";
            this.upDownDelaySeconds.Size = new System.Drawing.Size(48, 20);
            this.upDownDelaySeconds.TabIndex = 24;
            this.upDownDelaySeconds.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // btnDNSsetCopy
            // 
            this.btnDNSsetCopy.Image = global::DNS_Roaming_Client.Properties.Resources.upload_file_16x16_1214209;
            this.btnDNSsetCopy.Location = new System.Drawing.Point(519, 55);
            this.btnDNSsetCopy.Name = "btnDNSsetCopy";
            this.btnDNSsetCopy.Size = new System.Drawing.Size(19, 23);
            this.btnDNSsetCopy.TabIndex = 23;
            this.btnDNSsetCopy.UseVisualStyleBackColor = true;
            this.btnDNSsetCopy.Click += new System.EventHandler(this.btnDNSsetCopy_Click);
            this.btnDNSsetCopy.MouseHover += new System.EventHandler(this.btnDNSsetCopy_MouseHover);
            // 
            // groupAddressOptions
            // 
            this.groupAddressOptions.Controls.Add(this.radioAddressByWAN);
            this.groupAddressOptions.Controls.Add(this.radioAddressByLAN);
            this.groupAddressOptions.Controls.Add(this.radioAddressByAny);
            this.groupAddressOptions.Location = new System.Drawing.Point(12, 200);
            this.groupAddressOptions.Name = "groupAddressOptions";
            this.groupAddressOptions.Size = new System.Drawing.Size(544, 54);
            this.groupAddressOptions.TabIndex = 23;
            this.groupAddressOptions.TabStop = false;
            this.groupAddressOptions.Text = "And IP Address In";
            // 
            // radioAddressByWAN
            // 
            this.radioAddressByWAN.AutoSize = true;
            this.radioAddressByWAN.Location = new System.Drawing.Point(228, 19);
            this.radioAddressByWAN.Name = "radioAddressByWAN";
            this.radioAddressByWAN.Size = new System.Drawing.Size(106, 17);
            this.radioAddressByWAN.TabIndex = 3;
            this.radioAddressByWAN.Text = "By WAN address";
            this.radioAddressByWAN.UseVisualStyleBackColor = true;
            // 
            // radioAddressByLAN
            // 
            this.radioAddressByLAN.AutoSize = true;
            this.radioAddressByLAN.Location = new System.Drawing.Point(111, 19);
            this.radioAddressByLAN.Name = "radioAddressByLAN";
            this.radioAddressByLAN.Size = new System.Drawing.Size(101, 17);
            this.radioAddressByLAN.TabIndex = 2;
            this.radioAddressByLAN.Text = "By LAN address";
            this.radioAddressByLAN.UseVisualStyleBackColor = true;
            // 
            // radioAddressByAny
            // 
            this.radioAddressByAny.AutoSize = true;
            this.radioAddressByAny.Checked = true;
            this.radioAddressByAny.Location = new System.Drawing.Point(6, 19);
            this.radioAddressByAny.Name = "radioAddressByAny";
            this.radioAddressByAny.Size = new System.Drawing.Size(83, 17);
            this.radioAddressByAny.TabIndex = 1;
            this.radioAddressByAny.TabStop = true;
            this.radioAddressByAny.Text = "Any address";
            this.radioAddressByAny.UseVisualStyleBackColor = true;
            this.radioAddressByAny.CheckedChanged += new System.EventHandler(this.radioAddressByAny_CheckedChanged);
            // 
            // lblRuleDownloaded
            // 
            this.lblRuleDownloaded.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblRuleDownloaded.Location = new System.Drawing.Point(12, 505);
            this.lblRuleDownloaded.Name = "lblRuleDownloaded";
            this.lblRuleDownloaded.Size = new System.Drawing.Size(330, 42);
            this.lblRuleDownloaded.TabIndex = 32;
            this.lblRuleDownloaded.Text = "*Warning: this rule was downloaded and will be overwritten periodically. Manual c" +
    "hanges may be lost.";
            this.lblRuleDownloaded.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblRuleDownloaded.Visible = false;
            // 
            // FrmRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(568, 556);
            this.ControlBox = false;
            this.Controls.Add(this.lblRuleDownloaded);
            this.Controls.Add(this.groupAddressOptions);
            this.Controls.Add(this.groupDNSValues);
            this.Controls.Add(this.groupAddressSpecific);
            this.Controls.Add(this.groupNetworks);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Rule";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupNetworks.ResumeLayout(false);
            this.groupNetworks.PerformLayout();
            this.groupAddressSpecific.ResumeLayout(false);
            this.groupAddressSpecific.PerformLayout();
            this.groupDNSValues.ResumeLayout(false);
            this.groupDNSValues.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownDelaySeconds)).EndInit();
            this.groupAddressOptions.ResumeLayout(false);
            this.groupAddressOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox listNetworkType;
        private System.Windows.Forms.RadioButton radioNetworkNameIs;
        private System.Windows.Forms.RadioButton radioNetworkType;
        private System.Windows.Forms.ComboBox cmbNetworkName;
        private System.Windows.Forms.ComboBox cmbDNSset;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TextBox txtAlternateDNS;
        private System.Windows.Forms.TextBox txtPreferredDNS;
        private System.Windows.Forms.GroupBox groupDNSValues;
        private System.Windows.Forms.GroupBox groupAddressSpecific;
        private System.Windows.Forms.GroupBox groupNetworks;
        private System.Windows.Forms.RadioButton radioAddressIsSpecific;
        private System.Windows.Forms.TextBox txtAddressSubnet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAddressIP;
        private System.Windows.Forms.Button btnGetIPInfo;
        private System.Windows.Forms.Button btnDNSsetCopy;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.RadioButton radioAddressIsNotSpecific;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioNetworkNameIsNot;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupAddressOptions;
        private System.Windows.Forms.RadioButton radioAddressByWAN;
        private System.Windows.Forms.RadioButton radioAddressByLAN;
        private System.Windows.Forms.RadioButton radioAddressByAny;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown upDownDelaySeconds;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt3rdAlternateDNS;
        private System.Windows.Forms.TextBox txt2ndAlternateDNS;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblRuleDownloaded;
    }
}