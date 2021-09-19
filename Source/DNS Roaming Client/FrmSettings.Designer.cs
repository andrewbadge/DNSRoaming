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
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColWhen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNetworkID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colThen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.groupIPV6 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkIPV6Disable = new System.Windows.Forms.CheckBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabControlSettings.SuspendLayout();
            this.tabRules.SuspendLayout();
            this.tabOptions.SuspendLayout();
            this.groupIPV6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // listViewRules
            // 
            this.listViewRules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colID,
            this.ColWhen,
            this.colNetworkID,
            this.colThen});
            this.listViewRules.FullRowSelect = true;
            this.listViewRules.HideSelection = false;
            this.listViewRules.Location = new System.Drawing.Point(17, 15);
            this.listViewRules.MultiSelect = false;
            this.listViewRules.Name = "listViewRules";
            this.listViewRules.Size = new System.Drawing.Size(479, 221);
            this.listViewRules.TabIndex = 2;
            this.listViewRules.UseCompatibleStateImageBehavior = false;
            this.listViewRules.View = System.Windows.Forms.View.Details;
            this.listViewRules.SelectedIndexChanged += new System.EventHandler(this.listViewRules_SelectedIndexChanged);
            this.listViewRules.DoubleClick += new System.EventHandler(this.listViewRules_DoubleClick);
            this.listViewRules.MouseHover += new System.EventHandler(this.listViewRules_MouseHover);
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
            this.tabOptions.Controls.Add(this.groupIPV6);
            this.tabOptions.Location = new System.Drawing.Point(4, 22);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.Size = new System.Drawing.Size(512, 290);
            this.tabOptions.TabIndex = 1;
            this.tabOptions.Text = "Options";
            this.tabOptions.UseVisualStyleBackColor = true;
            // 
            // groupIPV6
            // 
            this.groupIPV6.Controls.Add(this.label1);
            this.groupIPV6.Controls.Add(this.chkIPV6Disable);
            this.groupIPV6.Location = new System.Drawing.Point(16, 16);
            this.groupIPV6.Name = "groupIPV6";
            this.groupIPV6.Size = new System.Drawing.Size(479, 100);
            this.groupIPV6.TabIndex = 0;
            this.groupIPV6.TabStop = false;
            this.groupIPV6.Text = "IPV6";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Location = new System.Drawing.Point(24, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(310, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "* Recommended if you don\'t have a mature IPV6 implementation";
            // 
            // chkIPV6Disable
            // 
            this.chkIPV6Disable.AutoSize = true;
            this.chkIPV6Disable.Location = new System.Drawing.Point(24, 30);
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
            this.groupIPV6.ResumeLayout(false);
            this.groupIPV6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
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
    }
}

