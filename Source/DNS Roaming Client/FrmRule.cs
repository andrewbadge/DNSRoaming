using DNS_Roaming_Common;
using System;
using System.Collections.Generic;
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

        #region Initialisation

        /// <summary>
        /// LLoad the list of NetwokrTypes from the enum
        /// </summary>
        private void PopulateNetworkType()
        {
            Logger.Debug("PopulateNetworkType");

            listNetworkType.Items.Clear();

            //Add the standard .Net Types
            var values = Enum.GetValues(typeof(NetworkInterfaceType)).Cast<NetworkInterfaceType>();
            foreach (var v in values)
            {
                listNetworkType.Items.Add(v.ToString());
            }

            //Add the custom Types (used by VPNs and 3rd Party software)
            IList<DNSRoamingNetworkInterfaceType> interfaceTypes = DNSRoamingNetworkInterfaces.GetNetworkInterfaceTypes();
            foreach (DNSRoamingNetworkInterfaceType interfaceType in interfaceTypes)
            {
                listNetworkType.Items.Add(interfaceType.Name);
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

            try
            {
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

                if (thisRule.NetworkType != String.Empty && listNetworkType.Items.Count > 0)
                {
                    string[] networkTypes = thisRule.NetworkType.Split(',');
                    foreach (string networkType in networkTypes)
                    {
                        index = listNetworkType.Items.IndexOf(networkType);
                        if (index != -1)
                        {
                            //Check the Item
                            listNetworkType.SetItemChecked(index, true);
                            //Select it so its visible on screen
                            listNetworkType.SelectedIndex = index;
                        }
                    }
                }

                radioAddressIsSpecific.Checked = thisRule.AddressIsSpecific;
                radioAddressIsNotSpecific.Checked = thisRule.AddressIsNotSpecific;
                if (!(radioAddressIsSpecific.Checked || radioAddressIsNotSpecific.Checked)) radioAddressIsSpecific.Checked = true;

                switch (thisRule.AddressByType)
                {
                    case 1:
                        radioAddressByLAN.Checked = true;
                        break;
                    case 2:
                        radioAddressByWAN.Checked = true;
                        break;
                    default:
                        radioAddressByAny.Checked = true;
                        break;
                }

                txtAddressIP.Text = thisRule.AddressIP;
                txtAddressSubnet.Text = thisRule.AddressSubnet;

                txtPreferredDNS.Text = thisRule.DNSPreferred;
                txtAlternateDNS.Text = thisRule.DNSAlternative;
                txt2ndAlternateDNS.Text = thisRule.DNS2ndAlternative;
                txt3rdAlternateDNS.Text = thisRule.DNS3rdAlternative;

                index = cmbDNSset.Items.IndexOf(thisRule.DNSSet);
                if (index != -1) cmbDNSset.SelectedIndex = index;

                if (thisRule.NetworkNameIs == String.Empty && thisRule.NetworkNameIsNot == String.Empty)
                {
                    radioNetworkType.Checked = true;   
                }
                else
                {
                    if (thisRule.NetworkNameIs == String.Empty)
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

                chkResetToDHCP.Checked = thisRule.ResetToDHCP;
                upDownDelaySeconds.Value = thisRule.DelaySeconds;
                lblRuleDownloaded.Visible = thisRule.RuleWasDownloaded;

                switch (thisRule.PingType)
                {
                    case 1:
                        radioPINGSuccess.Checked = true;
                        txtPINGAddress.Text = thisRule.PingAddress;
                        break;
                    case 2:
                        radioPINGFail.Checked = true;
                        txtPINGAddress.Text = thisRule.PingAddress;
                        break;
                    default:
                        radioDoNotPING.Checked = true;
                        txtPINGAddress.Text = string.Empty;
                        break;
                }

                Logger.Info("Rule loaded");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

        }

        #endregion

        #region Saving and Validation

        /// <summary>
        /// Validate the form content is ready to save. if not an error prompt will be displayed
        /// </summary>
        /// <returns>True if ok to save</returns>
        private bool ValidateForm()
        {
            Logger.Debug("ValidateForm");

            bool isFormValid = true;

            try
            {
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

                if (radioAddressByLAN.Checked || radioAddressByWAN.Checked)
                {
                    if (!IPAddress.TryParse(txtAddressIP.Text, out ipAddress))
                    {
                        errorProvider.SetError(txtAddressIP, "IP Address is not valid");
                        txtAddressIP.Focus();
                        isFormValid = false;
                    }
                    else
                    {
                        txtAddressIP.Text = IPAddressFormat(txtAddressIP.Text);
                    }

                    if (!IPAddress.TryParse(txtAddressSubnet.Text, out ipAddress))
                    {
                        errorProvider.SetError(txtAddressSubnet, "Subnet is not valid");
                        txtAddressSubnet.Focus();
                        isFormValid = false;
                    }
                    else
                    {
                        txtAddressSubnet.Text = IPAddressFormat(txtAddressSubnet.Text);
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
                        txtPreferredDNS.Text = IPAddressFormat(txtPreferredDNS.Text);
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
                        txtAlternateDNS.Text = IPAddressFormat(txtAlternateDNS.Text);
                    }
                }

                if (txt2ndAlternateDNS.Text.Trim() != String.Empty)
                {
                    if (!IPAddress.TryParse(txt2ndAlternateDNS.Text, out ipAddress))
                    {
                        errorProvider.SetError(txt2ndAlternateDNS, "IP Address is not valid");
                        txt2ndAlternateDNS.Focus();
                        isFormValid = false;
                    }
                    else
                    {
                        txt2ndAlternateDNS.Text = IPAddressFormat(txt2ndAlternateDNS.Text);
                    }
                }

                if (txt3rdAlternateDNS.Text.Trim() != String.Empty)
                {
                    if (!IPAddress.TryParse(txt3rdAlternateDNS.Text, out ipAddress))
                    {
                        errorProvider.SetError(txt3rdAlternateDNS, "IP Address is not valid");
                        txt3rdAlternateDNS.Focus();
                        isFormValid = false;
                    }
                    else
                    {
                        txt3rdAlternateDNS.Text = IPAddressFormat(txt3rdAlternateDNS.Text);
                    }
                }

                if (!chkResetToDHCP.Checked && txtPreferredDNS.Text.Trim() == String.Empty && txtAlternateDNS.Text.Trim() == String.Empty && cmbDNSset.SelectedIndex == -1)
                {
                    errorProvider.SetError(cmbDNSset, "Select a DNS Set");
                    cmbDNSset.Focus();
                    isFormValid = false;
                }

                if (!radioDoNotPING.Checked && txtPINGAddress.Text.Trim() == string.Empty)
                {
                    errorProvider.SetError(txtPINGAddress, "Set an IP to PING");
                    txtPINGAddress.Focus();
                    isFormValid = false;
                }
                else
                    txtPINGAddress.Text = IPAddressFormat(txtPINGAddress.Text);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                isFormValid = false;
                errorProvider.SetError(btnSave, "Error Validating");
            }

            return isFormValid;
        }

        private string IPAddressFormat(string ipaddress)
        {
            string returnIPAddress = string.Empty;
            try
            {
                returnIPAddress = IPAddress.Parse(ipaddress).ToString();
            }            
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return returnIPAddress;

        }

        /// <summary>
        /// Save the form details to the Rule object
        /// </summary>
        private bool SaveRule()
        {
            Logger.Debug("SaveRule");

            bool wasSaveSucessful = true;

            try
            {

                if (thisRule == null) thisRule = new DNSRoamingRule();

                if (thisRule.ID == null)
                {
                    thisRule.ID = System.Guid.NewGuid().ToString();
                }

                thisRule.UseNetworkType = radioNetworkType.Checked;

                thisRule.NetworkType = string.Empty;
                if (listNetworkType.CheckedItems.Count > 0)
                {
                    for (int x = 0; x < listNetworkType.CheckedItems.Count; x++)
                    {
                        if (thisRule.NetworkType != String.Empty) thisRule.NetworkType += ",";
                        thisRule.NetworkType += listNetworkType.CheckedItems[x].ToString();
                    }
                }

                if (radioAddressByAny.Checked) thisRule.AddressByType = 0;
                if (radioAddressByLAN.Checked) thisRule.AddressByType = 1;
                if (radioAddressByWAN.Checked) thisRule.AddressByType = 2;

                thisRule.AddressIsSpecific = radioAddressIsSpecific.Checked;
                thisRule.AddressIsNotSpecific = radioAddressIsNotSpecific.Checked;

                thisRule.AddressIP = txtAddressIP.Text;
                thisRule.AddressSubnet = txtAddressSubnet.Text;

                //Add addresses to a list to clear the empty values
                IList<string> dNSAddressesList = new List<string>();
                if (txtPreferredDNS.Text != String.Empty) dNSAddressesList.Add(txtPreferredDNS.Text);
                if (txtAlternateDNS.Text != String.Empty) dNSAddressesList.Add(txtAlternateDNS.Text);
                if (txt2ndAlternateDNS.Text != String.Empty) dNSAddressesList.Add(txt2ndAlternateDNS.Text);
                if (txt3rdAlternateDNS.Text != String.Empty) dNSAddressesList.Add(txt3rdAlternateDNS.Text);

                //Loop the list and add to the Rule
                int dNSAddressIndex = 0;
                foreach (string dNSAddress in dNSAddressesList)
                {
                    switch (dNSAddressIndex)
                    {
                        case 0:
                            thisRule.DNSPreferred = dNSAddress;
                            break;
                        case 1:
                            thisRule.DNSAlternative = dNSAddress;
                            break;
                        case 2:
                            thisRule.DNS2ndAlternative = dNSAddress;
                            break;
                        case 3:
                            thisRule.DNS3rdAlternative = dNSAddress;
                            break;
                    }
                    dNSAddressIndex += 1;
                }

                //Clear the Remaining values
                for (var i = dNSAddressIndex; i < 4; i++)
                {
                    switch (i)
                    {
                        case 0:
                            thisRule.DNSPreferred = String.Empty;
                            break;
                        case 1:
                            thisRule.DNSAlternative = String.Empty;
                            break;
                        case 2:
                            thisRule.DNS2ndAlternative = String.Empty;
                            break;
                        case 3:
                            thisRule.DNS3rdAlternative = String.Empty;
                            break;
                    }
                }

                thisRule.ResetToDHCP = chkResetToDHCP.Checked;
                thisRule.DNSSet = (cmbDNSset.SelectedItem == null) ? String.Empty : cmbDNSset.SelectedItem.ToString();

                if (radioNetworkNameIs.Checked) thisRule.NetworkNameIs = cmbNetworkName.Text;
                if (radioNetworkNameIsNot.Checked) thisRule.NetworkNameIsNot = cmbNetworkName.Text;

                thisRule.DelaySeconds = (int)upDownDelaySeconds.Value;

                if (radioDoNotPING.Checked)
                {
                    thisRule.PingType = 0;
                    thisRule.PingAddress = string.Empty;
                }
                else
                {
                    thisRule.PingAddress = txtPINGAddress.Text;
                    thisRule.PingType = radioPINGSuccess.Checked ? 1 : 2;
                }

                Logger.Info("Rule saved");
                wasSaveSucessful = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                wasSaveSucessful = false;
                errorProvider.SetError(btnSave, "Error Saving");
            }

            return wasSaveSucessful;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                if (SaveRule())
                {
                    this.formCancelled = false;
                    this.Close();
                }
            }
        }

        #endregion

        #region Form and Events

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Logger.Debug("btnCancel_Click");

            this.formCancelled = true;
            this.Close();
        }

        private void txtPreferredDNS_TextChanged(object sender, EventArgs e)
        {
            if (txtPreferredDNS.Text != String.Empty)
            {
                cmbDNSset.SelectedIndex = -1;
                chkResetToDHCP.Checked = false;
            }
        }

        private void txtAlternateDNS_TextChanged(object sender, EventArgs e)
        {
            if (txtAlternateDNS.Text != String.Empty)
            {
                cmbDNSset.SelectedIndex = -1;
                chkResetToDHCP.Checked = false;
            }
        }

        private void cmbDNSset_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPreferredDNS.Text = String.Empty;
            txtAlternateDNS.Text = String.Empty;
            txt2ndAlternateDNS.Text = String.Empty;
            txt3rdAlternateDNS.Text = String.Empty;
            chkResetToDHCP.Checked = false;
        }

        /// <summary>
        /// If you choose any Network address, then clear the specific address entered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioAddressByAny_CheckedChanged(object sender, EventArgs e)
        {
            if (radioAddressByAny.Checked)
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

            if (radioAddressByAny.Checked)
            {
                radioAddressByLAN.Checked = true;
                radioAddressIsSpecific.Checked = true;
            }

            if (radioAddressByLAN.Checked)
            {
                NetworkingExtensions.GetLANIPandSubnet(out returnIP, out returnSubnet);
                if (returnIP != String.Empty && returnSubnet != String.Empty)
                {
                    txtAddressIP.Text = returnIP;
                    txtAddressSubnet.Text = returnSubnet;
                }
            }

            if (radioAddressByWAN.Checked)
            {
                NetworkingExtensions.GetWANIPandSubnet(out returnIP, out returnSubnet);
                if (returnIP != String.Empty && returnSubnet != String.Empty)
                {
                    txtAddressIP.Text = returnIP;
                    txtAddressSubnet.Text = returnSubnet;
                }
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

        private void cmbNetworkName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (radioNetworkType.Checked) radioNetworkNameIs.Checked = true;
        }

        private void txt2ndAlternateDNS_TextChanged(object sender, EventArgs e)
        {
            if (txt2ndAlternateDNS.Text != String.Empty)
            {
                cmbDNSset.SelectedIndex = -1;
                chkResetToDHCP.Checked = false;
            }
        }

        private void txt3rdAlternateDNS_TextChanged(object sender, EventArgs e)
        {
            if (txt3rdAlternateDNS.Text != String.Empty)
            {
                cmbDNSset.SelectedIndex = -1;
                chkResetToDHCP.Checked = false;
            }
        }

        private void chkResetToDHCP_CheckedChanged(object sender, EventArgs e)
        {
            if (chkResetToDHCP.Checked)
            {
                cmbDNSset.SelectedIndex = -1;
                txtPreferredDNS.Text = String.Empty;
                txtAlternateDNS.Text = String.Empty;
                txt2ndAlternateDNS.Text = String.Empty;
                txt3rdAlternateDNS.Text = String.Empty;
            }
        }

        #endregion

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

        private void radioDoNotPING_CheckedChanged(object sender, EventArgs e)
        {
            if (radioDoNotPING.Checked) txtPINGAddress.Text = string.Empty;
        }

        #endregion


    }
}
