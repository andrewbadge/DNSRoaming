using DNS_Roaming_Common;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;

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
            Logger.Debug("FrmRule Initialize");

            InitializeComponent();
            PopulateNetworkType();
            PopulateNetworkName();
            PopulateDNSset();
        }

        /// <summary>
        /// LLoad the list of NetwokrTypes from the enum
        /// </summary>
        private void PopulateNetworkType()
        {
            Logger.Debug("PopulateNetworkType");

            listNetworkType.Items.Clear();

            var values = Enum.GetValues(typeof(NetworkInterfaceType)).Cast<NetworkInterfaceType>();
            foreach (var v in values)
            {
                listNetworkType.Items.Add(v.ToString());
            }
        }

        /// <summary>
        /// Load the list of NICs on the PC
        /// </summary>
        private void PopulateNetworkName()
        {
            Logger.Debug("PopulateNetworkName");

            cmbNetworkName.Items.Clear();

            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface n in adapters)
            {
                cmbNetworkName.Items.Add(n.Name.ToString());
            }
        }

        /// <summary>
        /// Add the options for DNS sets
        /// </summary>
        private void PopulateDNSset()
        {
            Logger.Debug("PopulateDNSset");

            cmbDNSset.Items.Clear();

            cmbDNSset.Items.Add("Quad9 + CloudFlare - No Malware");
            cmbDNSset.Items.Add("Quad9");
            cmbDNSset.Items.Add("Cloudflare");
            cmbDNSset.Items.Add("Cloudflare - No Malware");
            cmbDNSset.Items.Add("Cloudflare - No Malware or Adult");
            cmbDNSset.Items.Add("Google");
            cmbDNSset.Items.Add("AdGuard");
            cmbDNSset.Items.Add("Alternate DNS");
            cmbDNSset.Items.Add("CleanBrowsing - Adult");
            cmbDNSset.Items.Add("CleanBrowsing - Family");
            cmbDNSset.Items.Add("CleanBrowsing - Security");
            cmbDNSset.Items.Add("FourthEstate");
        }

        /// <summary>
        /// Load the content of the Rule object to the form
        /// </summary>
        private void LoadRule()
        {
            Logger.Debug("LoadRule");

            int index;
            if (thisRule == null)
            {
                this.Text = string.Format("New Rule");
                thisRule = DNSRoamingRuleDefault.GetDefaultRule();
            }
            else
            {
                this.Text = string.Format("Edit Rule (ID:{0})", thisRule.ID);
            }

            if (thisRule.NetworkType != String.Empty && listNetworkType.Items.Count>0)
            {
                string[] networkTypes = thisRule.NetworkType.Split(',');
                foreach (string networkType in networkTypes)
                {
                    index = listNetworkType.Items.IndexOf(networkType);
                    if (index != -1) listNetworkType.SetItemChecked(index, true);
                }
            }

            radioAddressIsSpecific.Checked = thisRule.AddressIsSpecific;
            radioAddressIsNotSpecific.Checked = thisRule.AddressIsNotSpecific;
            
            txtAddressIP.Text = thisRule.AddressIP;
            txtAddressSubnet.Text = thisRule.AddressSubnet;

            txtPreferredDNS.Text = thisRule.DNSPreferred;
            txtAlternateDNS.Text = thisRule.DNSAlternative;

            index = cmbDNSset.Items.IndexOf(thisRule.DNSSet);
            if (index != -1) cmbDNSset.SelectedIndex = index;

            if (thisRule.NetworkNameIs== String.Empty && thisRule.NetworkNameIsNot == String.Empty)
            {
                radioNetworkType.Checked = true;
            }
            else
            {
                if (thisRule.NetworkNameIs == String.Empty )
                {
                    radioNetworkNameIsNot.Checked = true;
                    cmbNetworkName.Text = thisRule.NetworkNameIsNot;
                }
                else
                {
                    radioNetworkNameIs.Checked = true;
                    cmbNetworkName.Text = thisRule.NetworkNameIs;
                }
            }
                   

            
            Logger.Info("Rule loaded");

        }

        /// <summary>
        /// Validate the form content is ready to save. if not an error prompt will be displayed
        /// </summary>
        /// <returns>True if ok to save</returns>
        private bool ValidateForm()
        {
            Logger.Debug("ValidateForm");

            bool isFormValid = true;
            IPAddress ipAddress;

            errorProvider.Clear();

            if (radioNetworkType.Checked && listNetworkType.CheckedItems.Count == 0)
            {
                errorProvider.SetError(listNetworkType, "Please choose a Network Type");
                listNetworkType.Focus();
                isFormValid = false;
            }

            if (!radioNetworkType.Checked && cmbNetworkName.Text.Trim() == String.Empty)
            {
                errorProvider.SetError(cmbNetworkName, "Please choose a Network Name");
                cmbNetworkName.Focus();
                isFormValid = false;
            }

            if (radioAddressIsSpecific.Checked)
            {
                if (!IPAddress.TryParse(txtAddressIP.Text, out ipAddress))
                {
                    errorProvider.SetError(txtAddressIP, "IP Address is not valid");
                    txtAddressIP.Focus();
                    isFormValid = false;
                }
                else
                {
                    txtAddressIP.Text = IPAddress.Parse(txtAddressIP.Text).ToString();
                }
                
                if (!IPAddress.TryParse(txtAddressSubnet.Text, out ipAddress))
                {
                    errorProvider.SetError(txtAddressSubnet, "Subnet is not valid");
                    txtAddressSubnet.Focus();
                    isFormValid = false;
                }
                else
                {
                    txtAddressSubnet.Text = IPAddress.Parse(txtAddressSubnet.Text).ToString();
                }
                
            }

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

            if (txtPreferredDNS.Text.Trim() == String.Empty && txtAlternateDNS.Text.Trim() == String.Empty && cmbDNSset.SelectedIndex == -1)
            {
                errorProvider.SetError(cmbDNSset, "Select a DNS Set");
                cmbDNSset.Focus();
                isFormValid = false;
            }



            return isFormValid;
        }

        /// <summary>
        /// Save the form details to the Rule object
        /// </summary>
        private void SaveRule()
        {
            Logger.Debug("SaveRule");

            if (thisRule == null) thisRule = new DNSRoamingRule();

            if (thisRule.ID == null)
            {
                thisRule.ID = System.Guid.NewGuid().ToString();
            }

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

            thisRule.AddressIsSpecific = radioAddressIsSpecific.Checked;
            thisRule.AddressIsNotSpecific = radioAddressIsNotSpecific.Checked;

            thisRule.AddressIP = txtAddressIP.Text;
            thisRule.AddressSubnet = txtAddressSubnet.Text;

            thisRule.DNSPreferred = txtPreferredDNS.Text;
            if (txtAlternateDNS.Text.Trim() == ".   .   .") thisRule.DNSAlternative = String.Empty; else thisRule.DNSAlternative = txtAlternateDNS.Text;

            thisRule.DNSSet = (cmbDNSset.SelectedItem == null) ? String.Empty: cmbDNSset.SelectedItem.ToString();

            if (radioNetworkNameIs.Checked) thisRule.NetworkNameIs = cmbNetworkName.Text;
            if (radioNetworkNameIsNot.Checked) thisRule.NetworkNameIsNot = cmbNetworkName.Text;

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
            Logger.Debug("btnCancel_Click");

            this.formCancelled = true;
            this.Close();
        }

        private void txtPreferredDNS_TextChanged(object sender, EventArgs e)
        {
            if (txtPreferredDNS.Text != String.Empty) cmbDNSset.SelectedIndex = -1;
        }

        private void txtAlternateDNS_TextChanged(object sender, EventArgs e)
        {
            if (txtAlternateDNS.Text != String.Empty) cmbDNSset.SelectedIndex = -1;
        }

        private void cmbDNSset_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPreferredDNS.Text = String.Empty;
            txtAlternateDNS.Text = String.Empty;
        }

        private void radioAddressAny_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// If you choose any Network address, then clear the specific address entered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioAddressAny_CheckedChanged(object sender, EventArgs e)
        {
            if (radioAddressAny.Checked)
            {
                txtAddressIP.Text = string.Empty;
                txtAddressSubnet.Text = string.Empty;
            }
        }

        /// <summary>
        /// Get your current NIC IP and Sunet and add it as a suggestion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetIPInfo_Click(object sender, EventArgs e)
        {
            Logger.Debug("btnGetIPInfo_Click");

            string returnIP = string.Empty;
            string returnSubnet = string.Empty;
            NetworkingExtensions.GetCurrentIPandSubNet(out returnIP,out returnSubnet);

            if (returnIP != String.Empty && returnSubnet != String.Empty)
            {
                txtAddressIP.Text = returnIP;
                txtAddressSubnet.Text = returnSubnet;

                if (radioAddressAny.Checked) radioAddressIsSpecific.Checked = true;
            }

        }
        /// <summary>
        /// Copy the addresses from the DNS set and add the addresses to the manual IP entries
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDNSsetCopy_Click(object sender, EventArgs e)
        {
            Logger.Debug("btnDNSsetCopy_Click");

            if (cmbDNSset.SelectedIndex != -1)
            {
                string returnIPPreferred = string.Empty;
                string returnIPAlternative = string.Empty;
                string dnsSet = cmbDNSset.SelectedItem.ToString();
                NetworkingExtensions.GetDNSSetIPAddress(dnsSet, out returnIPPreferred, out returnIPAlternative);

                cmbDNSset.SelectedIndex = -1;
                txtPreferredDNS.Text = returnIPPreferred;
                txtAlternateDNS.Text = returnIPAlternative;
            }
        }

        #region Tooltips

        private void btnGetIPInfo_MouseHover(object sender, EventArgs e)
        {
            toolTip.Show("Get your current Network's IP and Subnet", btnGetIPInfo);
        }

        private void btnDNSsetCopy_MouseHover(object sender, EventArgs e)
        {
            toolTip.Show("Copy the selected DNS Set's IPs to the fields", btnDNSsetCopy);
        }

        

        private void cmbNetworkName_MouseHover(object sender, EventArgs e)
        {
            toolTip.Show("Pick from the list of Network Interfaces on your PC", cmbNetworkName);
        }

        #endregion

        private void cmbNetworkName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (radioNetworkType.Checked) radioNetworkNameIs.Checked = true;
        }
    }
}
