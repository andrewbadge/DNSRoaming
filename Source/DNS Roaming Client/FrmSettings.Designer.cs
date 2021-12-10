namespace DNS_Roaming_Client
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
            this.tabOptions = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.autoUpdateDays = new System.Windows.Forms.NumericUpDown();
            this.chkAutoupdate = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.retainLogDays = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupIPV6 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkIPV6Disable = new System.Windows.Forms.CheckBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.ruleSetUpdateDays = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRuleSetURL = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControlSettings.SuspendLayout();
            this.tabRules.SuspendLayout();
            this.tabOptions.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.autoUpdateDays)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.retainLogDays)).BeginInit();
            this.groupIPV6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ruleSetUpdateDays)).BeginInit();
            this.SuspendLayout();
            // 
            // listViewRules
            // 
            this.listViewRules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colImage,
            this.colID,
            this.ColWhen,
            this.colNetworkID,
            this.colThen});
            this.listViewRules.FullRowSelect = true;
            this.listViewRules.HideSelection = false;
            this.listViewRules.LargeImageList = this.imageList;
            this.listViewRules.Location = new System.Drawing.Point(17, 15);
            this.listViewRules.MultiSelect = false;
            this.listViewRules.Name = "listViewRules";
            this.listViewRules.Size = new System.Drawing.Size(479, 221);
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
            this.btnRuleEdit.Location = new System.Drawing.Point(175, 250);
            this.btnRuleEdit.Name = "btnRuleEdit";
            this.btnRuleEdit.Size = new System.Drawing.Size(103, 23);
            this.btnRuleEdit.TabIndex = 5;
            this.btnRuleEdit.Text = "Edit";
            this.btnRuleEdit.UseVisualStyleBackColor = true;
            this.btnRuleEdit.Click += new System.EventHandler(this.btnRuleEdit_Click);
            // 
            // btnRuleNew
            // 
            this.btnRuleNew.Location = new System.Drawing.Point(66, 250);
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
            this.btnRuleRemove.Location = new System.Drawing.Point(393, 249);
            this.btnRuleRemove.Name = "btnRuleRemove";
            this.btnRuleRemove.Size = new System.Drawing.Size(103, 24);
            this.btnRuleRemove.TabIndex = 3;
            this.btnRuleRemove.Text = "Remove";
            this.btnRuleRemove.UseVisualStyleBackColor = true;
            this.btnRuleRemove.Click += new System.EventHandler(this.btnRuleRemove_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(290, 333);
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
            this.btnCancel.Location = new System.Drawing.Point(399, 333);
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
            this.tabControlSettings.Controls.Add(this.tabOptions);
            this.tabControlSettings.Location = new System.Drawing.Point(2, 2);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            this.tabControlSettings.Size = new System.Drawing.Size(520, 316);
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
            this.tabRules.Size = new System.Drawing.Size(512, 290);
            this.tabRules.TabIndex = 0;
            this.tabRules.Text = "Rules";
            this.tabRules.UseVisualStyleBackColor = true;
            // 
            // btnRuleCopy
            // 
            this.btnRuleCopy.Enabled = false;
            this.btnRuleCopy.Location = new System.Drawing.Point(284, 250);
            this.btnRuleCopy.Name = "btnRuleCopy";
            this.btnRuleCopy.Size = new System.Drawing.Size(103, 23);
            this.btnRuleCopy.TabIndex = 6;
            this.btnRuleCopy.Text = "Copy";
            this.btnRuleCopy.UseVisualStyleBackColor = true;
            this.btnRuleCopy.Click += new System.EventHandler(this.btnRuleCopy_Click);
            // 
            // tabOptions
            // 
            this.tabOptions.Controls.Add(this.groupBox2);
            this.tabOptions.Controls.Add(this.groupBox1);
            this.tabOptions.Controls.Add(this.groupIPV6);
            this.tabOptions.Location = new System.Drawing.Point(4, 22);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.Size = new System.Drawing.Size(512, 290);
            this.tabOptions.TabIndex = 1;
            this.tabOptions.Text = "Options";
            this.tabOptions.UseVisualStyleBackColor = true;
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
            this.groupBox2.Location = new System.Drawing.Point(16, 166);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(479, 106);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Updates";
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
            this.chkAutoupdate.CheckedChanged += new System.EventHandler(this.chkAutoupdate_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.retainLogDays);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(17, 96);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(479, 60);
            this.groupBox1.TabIndex = 1;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Remove log files older than ";
            // 
            // groupIPV6
            // 
            this.groupIPV6.Controls.Add(this.label1);
            this.groupIPV6.Controls.Add(this.chkIPV6Disable);
            this.groupIPV6.Location = new System.Drawing.Point(16, 16);
            this.groupIPV6.Name = "groupIPV6";
            this.groupIPV6.Size = new System.Drawing.Size(479, 74);
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "URL:";
            // 
            // txtRuleSetURL
            // 
            this.txtRuleSetURL.Location = new System.Drawing.Point(65, 75);
            this.txtRuleSetURL.Name = "txtRuleSetURL";
            this.txtRuleSetURL.Size = new System.Drawing.Size(408, 20);
            this.txtRuleSetURL.TabIndex = 10;
            this.txtRuleSetURL.WordWrap = false;
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
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(528, 364);
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
            this.tabOptions.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.autoUpdateDays)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.retainLogDays)).EndInit();
            this.groupIPV6.ResumeLayout(false);
            this.groupIPV6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ruleSetUpdateDays)).EndInit();
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
        private System.Windows.Forms.TabPage tabOptions;
        private System.Windows.Forms.GroupBox groupIPV6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkIPV6Disable;
        private System.Windows.Forms.Button btnRuleCopy;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown retainLogDays;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkAutoupdate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown autoUpdateDays;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ColumnHeader colImage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown ruleSetUpdateDays;
        private System.Windows.Forms.TextBox txtRuleSetURL;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}

