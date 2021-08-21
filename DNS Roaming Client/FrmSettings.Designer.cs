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
            this.listViewRules = new System.Windows.Forms.ListView();
            this.ColWhen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNetworkID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colThen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRuleRemove = new System.Windows.Forms.Button();
            this.btnRuleNew = new System.Windows.Forms.Button();
            this.btnRuleEdit = new System.Windows.Forms.Button();
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
            this.listViewRules.Location = new System.Drawing.Point(6, 36);
            this.listViewRules.MultiSelect = false;
            this.listViewRules.Name = "listViewRules";
            this.listViewRules.Size = new System.Drawing.Size(489, 201);
            this.listViewRules.TabIndex = 2;
            this.listViewRules.UseCompatibleStateImageBehavior = false;
            this.listViewRules.View = System.Windows.Forms.View.Details;
            this.listViewRules.DoubleClick += new System.EventHandler(this.listViewRules_DoubleClick);
            // 
            // ColWhen
            // 
            this.ColWhen.Text = "When";
            this.ColWhen.Width = 62;
            // 
            // colNetworkID
            // 
            this.colNetworkID.Text = "and Network";
            // 
            // colThen
            // 
            this.colThen.Text = "Then set";
            // 
            // colID
            // 
            this.colID.Text = "ID";
            this.colID.Width = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRuleEdit);
            this.groupBox1.Controls.Add(this.btnRuleNew);
            this.groupBox1.Controls.Add(this.btnRuleRemove);
            this.groupBox1.Controls.Add(this.listViewRules);
            this.groupBox1.Location = new System.Drawing.Point(12, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(516, 314);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rules";
            // 
            // btnRuleRemove
            // 
            this.btnRuleRemove.Location = new System.Drawing.Point(409, 243);
            this.btnRuleRemove.Name = "btnRuleRemove";
            this.btnRuleRemove.Size = new System.Drawing.Size(86, 24);
            this.btnRuleRemove.TabIndex = 3;
            this.btnRuleRemove.Text = "Remove";
            this.btnRuleRemove.UseVisualStyleBackColor = true;
            this.btnRuleRemove.Click += new System.EventHandler(this.btnRuleRemove_Click);
            // 
            // btnRuleNew
            // 
            this.btnRuleNew.Location = new System.Drawing.Point(247, 243);
            this.btnRuleNew.Name = "btnRuleNew";
            this.btnRuleNew.Size = new System.Drawing.Size(75, 23);
            this.btnRuleNew.TabIndex = 4;
            this.btnRuleNew.Text = "New";
            this.btnRuleNew.UseVisualStyleBackColor = true;
            this.btnRuleNew.Click += new System.EventHandler(this.btnRuleNew_Click);
            // 
            // btnRuleEdit
            // 
            this.btnRuleEdit.Location = new System.Drawing.Point(328, 243);
            this.btnRuleEdit.Name = "btnRuleEdit";
            this.btnRuleEdit.Size = new System.Drawing.Size(75, 23);
            this.btnRuleEdit.TabIndex = 5;
            this.btnRuleEdit.Text = "Edit";
            this.btnRuleEdit.UseVisualStyleBackColor = true;
            this.btnRuleEdit.Click += new System.EventHandler(this.btnRuleEdit_Click);
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 472);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
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
    }
}

