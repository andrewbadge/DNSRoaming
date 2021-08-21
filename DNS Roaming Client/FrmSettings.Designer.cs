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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSettings));
            this.listViewRules = new System.Windows.Forms.ListView();
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColWhen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNetworkID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colThen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRuleEdit = new System.Windows.Forms.Button();
            this.btnRuleNew = new System.Windows.Forms.Button();
            this.btnRuleRemove = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
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
            this.listViewRules.Location = new System.Drawing.Point(6, 19);
            this.listViewRules.MultiSelect = false;
            this.listViewRules.Name = "listViewRules";
            this.listViewRules.Size = new System.Drawing.Size(489, 201);
            this.listViewRules.TabIndex = 2;
            this.listViewRules.UseCompatibleStateImageBehavior = false;
            this.listViewRules.View = System.Windows.Forms.View.Details;
            this.listViewRules.DoubleClick += new System.EventHandler(this.listViewRules_DoubleClick);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRuleEdit);
            this.groupBox1.Controls.Add(this.btnRuleNew);
            this.groupBox1.Controls.Add(this.btnRuleRemove);
            this.groupBox1.Controls.Add(this.listViewRules);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(516, 270);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rules";
            // 
            // btnRuleEdit
            // 
            this.btnRuleEdit.Location = new System.Drawing.Point(328, 226);
            this.btnRuleEdit.Name = "btnRuleEdit";
            this.btnRuleEdit.Size = new System.Drawing.Size(75, 23);
            this.btnRuleEdit.TabIndex = 5;
            this.btnRuleEdit.Text = "Edit";
            this.btnRuleEdit.UseVisualStyleBackColor = true;
            this.btnRuleEdit.Click += new System.EventHandler(this.btnRuleEdit_Click);
            // 
            // btnRuleNew
            // 
            this.btnRuleNew.Location = new System.Drawing.Point(247, 226);
            this.btnRuleNew.Name = "btnRuleNew";
            this.btnRuleNew.Size = new System.Drawing.Size(75, 23);
            this.btnRuleNew.TabIndex = 4;
            this.btnRuleNew.Text = "New";
            this.btnRuleNew.UseVisualStyleBackColor = true;
            this.btnRuleNew.Click += new System.EventHandler(this.btnRuleNew_Click);
            // 
            // btnRuleRemove
            // 
            this.btnRuleRemove.Location = new System.Drawing.Point(409, 226);
            this.btnRuleRemove.Name = "btnRuleRemove";
            this.btnRuleRemove.Size = new System.Drawing.Size(86, 24);
            this.btnRuleRemove.TabIndex = 3;
            this.btnRuleRemove.Text = "Remove";
            this.btnRuleRemove.UseVisualStyleBackColor = true;
            this.btnRuleRemove.Click += new System.EventHandler(this.btnRuleRemove_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(350, 299);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(431, 299);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 332);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DNS Roaming Settings";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView listViewRules;
        private System.Windows.Forms.ColumnHeader ColWhen;
        private System.Windows.Forms.ColumnHeader colNetworkID;
        private System.Windows.Forms.ColumnHeader colThen;
        private System.Windows.Forms.ColumnHeader colID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRuleEdit;
        private System.Windows.Forms.Button btnRuleNew;
        private System.Windows.Forms.Button btnRuleRemove;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}

