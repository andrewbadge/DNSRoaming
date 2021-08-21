﻿namespace DNS_Roaming_Client
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
            this.listNetworkType = new System.Windows.Forms.CheckedListBox();
            this.radioNetworkName = new System.Windows.Forms.RadioButton();
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // listNetworkType
            // 
            this.listNetworkType.FormattingEnabled = true;
            this.listNetworkType.Location = new System.Drawing.Point(35, 48);
            this.listNetworkType.Name = "listNetworkType";
            this.listNetworkType.Size = new System.Drawing.Size(325, 109);
            this.listNetworkType.TabIndex = 0;
            // 
            // radioNetworkName
            // 
            this.radioNetworkName.AutoSize = true;
            this.radioNetworkName.Location = new System.Drawing.Point(15, 177);
            this.radioNetworkName.Name = "radioNetworkName";
            this.radioNetworkName.Size = new System.Drawing.Size(78, 17);
            this.radioNetworkName.TabIndex = 3;
            this.radioNetworkName.TabStop = true;
            this.radioNetworkName.Text = "or Name is:";
            this.radioNetworkName.UseVisualStyleBackColor = true;
            // 
            // radioNetworkType
            // 
            this.radioNetworkType.AutoSize = true;
            this.radioNetworkType.Location = new System.Drawing.Point(15, 25);
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
            this.cmbNetworkName.Location = new System.Drawing.Point(35, 200);
            this.cmbNetworkName.Name = "cmbNetworkName";
            this.cmbNetworkName.Size = new System.Drawing.Size(325, 21);
            this.cmbNetworkName.TabIndex = 5;
            // 
            // cmbDNSset
            // 
            this.cmbDNSset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDNSset.FormattingEnabled = true;
            this.cmbDNSset.Location = new System.Drawing.Point(15, 30);
            this.cmbDNSset.Name = "cmbDNSset";
            this.cmbDNSset.Size = new System.Drawing.Size(333, 21);
            this.cmbDNSset.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "or";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(52, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Preferred DNS";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(52, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Alternate DNS";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(339, 518);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(258, 518);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // txtPreferredDNS
            // 
            this.txtPreferredDNS.Location = new System.Drawing.Point(146, 72);
            this.txtPreferredDNS.Name = "txtPreferredDNS";
            this.txtPreferredDNS.Size = new System.Drawing.Size(156, 20);
            this.txtPreferredDNS.TabIndex = 18;
            // 
            // txtAlternateDNS
            // 
            this.txtAlternateDNS.Location = new System.Drawing.Point(146, 99);
            this.txtAlternateDNS.Name = "txtAlternateDNS";
            this.txtAlternateDNS.Size = new System.Drawing.Size(156, 20);
            this.txtAlternateDNS.TabIndex = 19;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listNetworkType);
            this.groupBox1.Controls.Add(this.radioNetworkName);
            this.groupBox1.Controls.Add(this.radioNetworkType);
            this.groupBox1.Controls.Add(this.cmbNetworkName);
            this.groupBox1.Location = new System.Drawing.Point(24, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(390, 248);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "When active network is";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(24, 266);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(390, 100);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "And";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbDNSset);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtAlternateDNS);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txtPreferredDNS);
            this.groupBox3.Location = new System.Drawing.Point(24, 372);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(390, 140);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "then set the DNS server to";
            // 
            // FrmRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(434, 552);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Name = "FrmRule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DNS Rule";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox listNetworkType;
        private System.Windows.Forms.RadioButton radioNetworkName;
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
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}