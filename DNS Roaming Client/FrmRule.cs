﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DNS_Roaming_Common;

namespace DNS_Roaming_Client
{
    public partial class FrmRule : Form
    {
        private DNSRoamingRule thisRule;
        public DNSRoamingRule ThisRule
        {
            get { return thisRule; }
            set {
                thisRule = value;
                LoadRule();
            }
        }

        private bool formCancelled = false;
        public bool FormCancelled
        {
            get { return formCancelled; }
        }

        public FrmRule()
        {
            InitializeComponent();
            PopulateNetworkType();
            PopulateNetworkName();
            PopulateDNSset();
            LoadRule();
        }

        private void PopulateNetworkType()
        {
            listNetworkType.Items.Clear();

            var values = Enum.GetValues(typeof(NetworkInterfaceType)).Cast<NetworkInterfaceType>();
            foreach (var v in values)
            {
                listNetworkType.Items.Add(v.ToString());
            }
        }

        private void PopulateNetworkName()
        {
            cmbNetworkName.Items.Clear();

            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface n in adapters)
            {
                cmbNetworkName.Items.Add(n.Name.ToString());
            }
        }

        private void PopulateDNSset()
        {
            cmbDNSset.Items.Clear();

            cmbDNSset.Items.Add("Quad9 + CloudFlare - No Malware");
            cmbDNSset.Items.Add("Quad9");
            cmbDNSset.Items.Add("Cloudflare");
            cmbDNSset.Items.Add("Cloudflare - No Malware");
            cmbDNSset.Items.Add("Cloudflare - No Malware or Adult");
            cmbDNSset.Items.Add("Google");
        }

        private void LoadRule()
        {
            int index;
            if (thisRule == null)
            {

                this.Text = string.Format("New Rule");

                radioNetworkType.Checked = true;
                radioNetworkName.Checked = false;

                index = listNetworkType.Items.IndexOf("Ethernet");
                if (index != -1) listNetworkType.SetItemChecked(index, true);

                index = listNetworkType.Items.IndexOf("Wireless80211");
                if (index != -1) listNetworkType.SetItemChecked(index, true);


            }
            else
            {
                this.Text = string.Format("Edit Rule (ID:{0})", thisRule.ID);

                radioNetworkType.Checked = thisRule.UseNetworkType;
                radioNetworkName.Checked = !thisRule.UseNetworkType;

                if (thisRule.NetworkType != String.Empty && listNetworkType.Items.Count>0)
                {
                    string[] networkTypes = thisRule.NetworkType.Split(',');
                    foreach (string networkType in networkTypes)
                    {
                        index = listNetworkType.Items.IndexOf(networkType);
                        if (index != -1) listNetworkType.SetItemChecked(index, true);
                    }
                }

                txtPreferredDNS.Text = thisRule.DNSPreferred;
                txtAlternateDNS.Text = thisRule.DNSAlternative;

                index = cmbDNSset.Items.IndexOf(thisRule.DNSSet);
                if (index != -1) cmbDNSset.SelectedIndex = index;

                cmbNetworkName.Text = thisRule.NetworkName;

            }
            Logger.Info("Rule loaded");

        }

        private bool ValidateForm()
        {
            bool isFormValid = true;

            if (radioNetworkType.Checked && listNetworkType.CheckedItems.Count == 0)
            {
                errorProvider.SetError(listNetworkType, "Please choose a Network Type");
                listNetworkType.Focus();
                isFormValid = false;
            }

            if (radioNetworkName.Checked && cmbNetworkName.Text.Trim() == String.Empty)
            {
                errorProvider.SetError(cmbNetworkName, "Please choose a Network Name");
                cmbNetworkName.Focus();
                isFormValid = false;
            }

            IPAddress ipAddress;
            if (txtPreferredDNS.Text.Trim() != String.Empty)
            {
                if (!IPAddress.TryParse(txtPreferredDNS.Text, out ipAddress))
                {
                    errorProvider.SetError(txtPreferredDNS, "IP Address is not valid");
                    txtPreferredDNS.Focus();
                    isFormValid = false;
                }
                else
                {
                    txtPreferredDNS.Text = IPAddress.Parse(txtPreferredDNS.Text).ToString();
                }
            }

            if (txtAlternateDNS.Text.Trim() != String.Empty)
            {
                if (!IPAddress.TryParse(txtAlternateDNS.Text, out ipAddress))
                {
                    errorProvider.SetError(txtAlternateDNS, "IP Address is not valid");
                    txtAlternateDNS.Focus();
                    isFormValid = false;
                }
                else
                {
                    txtAlternateDNS.Text = IPAddress.Parse(txtAlternateDNS.Text).ToString();
                }
            }

            if (txtPreferredDNS.Text.Trim() == String.Empty && txtAlternateDNS.Text.Trim() == String.Empty && cmbDNSset.SelectedItem.ToString() == String.Empty)
            {
                errorProvider.SetError(cmbDNSset, "Select a DNS Set");
                cmbDNSset.Focus();
                isFormValid = false;
            }



            return isFormValid;
        }
        private void SaveRule()
        {
            if (thisRule == null) thisRule = new DNSRoamingRule();
            thisRule.UseNetworkType = radioNetworkType.Checked;

            thisRule.NetworkType = string.Empty;
            if (listNetworkType.CheckedItems.Count>0)
            {
                for (int x = 0; x < listNetworkType.CheckedItems.Count; x++)
                {
                    if (thisRule.NetworkType != String.Empty) thisRule.NetworkType += ",";
                    thisRule.NetworkType += listNetworkType.CheckedItems[x].ToString();
                }
            }

            thisRule.DNSPreferred = txtPreferredDNS.Text;
            if (txtAlternateDNS.Text.Trim() == ".   .   .") thisRule.DNSAlternative = String.Empty; else thisRule.DNSAlternative = txtAlternateDNS.Text;
            thisRule.DNSSet = cmbDNSset.SelectedItem.ToString();
            thisRule.NetworkName = cmbNetworkName.Text;

            Logger.Info("Rule saved");
        }

            private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                SaveRule();
                this.formCancelled = false;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.formCancelled = true;
            this.Close();
        }
    }
}
