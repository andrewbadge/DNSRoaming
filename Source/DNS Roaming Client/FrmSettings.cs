using DNS_Roaming_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace DNS_Roaming_Client
{
    public partial class FrmSettings : Form
    {
        
        IList<DNSRoamingRule> ruleList = new List<DNSRoamingRule>();
        bool settingsPathExist = false;
        bool optionsPathExist = false;

        public FrmSettings()
        {
            InitializeComponent();

            this.Text = String.Format("DNS Roaming Settings (v{0})", Assembly.GetExecutingAssembly().GetName().Version.ToString());

            //Check Paths in case there is a permissions issue
            //or the client is before the service is ever run
            PathsandData pathsandData = new PathsandData();
            settingsPathExist = pathsandData.SettingsPathExist();
            optionsPathExist = pathsandData.OptionsPathExist();

            //Init all the details
            InitialiseForm();
            InitialiseOptions();
            InitialiseRules(pathsandData.BaseSettingsPath);
            ListRules();
        }

        #region Initialisation

        private void InitialiseForm()
        {
            btnRuleNew.Enabled = settingsPathExist;

            errorProvider.Clear();
            if (!settingsPathExist) errorProvider.SetError(btnSave, "Settings Folder does not exist. Check if the Service is running");
        }

        /// <summary>
        /// Load Options from the Options File
        /// </summary>
        private void InitialiseOptions()
        {

            if (!optionsPathExist) return;

            try
            {
                DNSRoamingOption newOption = new DNSRoamingOption();
                newOption.Load();
                chkIPV6Disable.Checked = newOption.DisableIPV6;

                retainLogDays.Value = newOption.DaysToRetainLogs;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

        }

        /// <summary>
        /// Load Rules from the Settings Files
        /// </summary>
        /// <param name="settingPath"></param>
        private void InitialiseRules(string settingPath)
        {

            if (!settingsPathExist) return;

            try
            {
                string[] settingFiles = Directory.GetFiles(settingPath, "*.xml", SearchOption.TopDirectoryOnly);
                foreach (string settingFilename in settingFiles)
                {
                    //Catch an exception for a specific file but continue to process the next
                    try
                    {
                        DNSRoamingRule newRule = new DNSRoamingRule();
                        newRule.Load(settingFilename);
                        ruleList.Add(newRule);
                    }
                    catch
                    {
                        Logger.Error(String.Format("Error loading rule {0}", settingFilename));
                    }
                
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

        }

        /// <summary>
        /// Show the List of Rules on screen. ID (GUID) of the Rule is the ID
        /// </summary>
        private void ListRules()
        {
            listViewRules.FullRowSelect = true;
            listViewRules.View = View.Details;
            listViewRules.Items.Clear();

            foreach (DNSRoamingRule thisRule in ruleList)
            {
                try {

                    if (thisRule.ID != String.Empty)
                    {
                        ListViewItem lvItem = new ListViewItem();
                        lvItem.Text = thisRule.ID;

                        if (thisRule.UseNetworkType)
                            lvItem.SubItems.Add(String.Format("Type is {0}", thisRule.NetworkType));
                        else
                        {
                            if (thisRule.NetworkNameIs == String.Empty)
                                lvItem.SubItems.Add(String.Format("Name is not {0}", thisRule.NetworkNameIsNot));
                            else
                                lvItem.SubItems.Add(String.Format("Name is {0}", thisRule.NetworkNameIs));
                        }


                        string addressTypePrefix = String.Empty;
                        switch (thisRule.AddressByType)
                        {
                            case 1:
                                addressTypePrefix = "LAN";
                                break;
                            case 2:
                                addressTypePrefix = "WAN";
                                break;
                            default:
                                //In case any other malformed value inthe Settings file
                                if (thisRule.AddressByType != 0) thisRule.AddressByType = 0;
                                break;
                        }

                        if (thisRule.AddressByType == 0)
                            lvItem.SubItems.Add("Any Subnet");
                        else
                        {
                            if (thisRule.AddressIsSpecific)
                                lvItem.SubItems.Add(String.Format("{0} In {1}/{2}", addressTypePrefix, thisRule.AddressIP, thisRule.AddressSubnet));

                            if (thisRule.AddressIsNotSpecific)
                                lvItem.SubItems.Add(String.Format("{0} Not in {1}/{2}", addressTypePrefix, thisRule.AddressIP, thisRule.AddressSubnet));
                        }

                        if (thisRule.DNSPreferred == String.Empty && thisRule.DNSAlternative == String.Empty)
                            lvItem.SubItems.Add(thisRule.DNSSet);
                        else
                            lvItem.SubItems.Add(String.Format("{0}", NetworkingExtensions.ExpandIPString(thisRule.DNSPreferred, thisRule.DNSAlternative, thisRule.DNS2ndAlternative, thisRule.DNS3rdAlternative)));

                        listViewRules.Items.Add(lvItem);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message);
                }
            }
        }

        #endregion

        #region Button Actions

        /// <summary>
        /// New Rule action
        /// </summary>
        private void ListRuleNew()
        {
            try
            {
                FrmRule frmRule = new FrmRule();
                frmRule.ShowDialog();

                if (!frmRule.FormCancelled)
                {
                    DNSRoamingRule returnRule = frmRule.ThisRule;
                    ruleList.Add(returnRule);
                    ListRules();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        /// <summary>
        /// Edit a Rule action
        /// </summary>
        private void ListRuleEdit()
        {
            try
            {
                if (listViewRules.SelectedItems.Count != 0)
                {
                    string lvID = listViewRules.SelectedItems[0].Text;

                    if (ruleList.Any(x => x.ID == lvID))
                    {
                        var thisRule = ruleList.FirstOrDefault(x => x.ID == lvID);
                        FrmRule frmRule = new FrmRule();
                        frmRule.ThisRule = thisRule;
                        frmRule.ShowDialog();

                        if (!frmRule.FormCancelled)
                        {
                            DNSRoamingRule returnRule = frmRule.ThisRule;
                            thisRule = returnRule;
                            ListRules();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        /// <summary>
        /// Copy a Rule action
        /// </summary>
        private void ListRuleCopy()
        {
            try
            {
                if (listViewRules.SelectedItems.Count != 0)
                {
                    string lvID = listViewRules.SelectedItems[0].Text;

                    if (ruleList.Any(x => x.ID == lvID))
                    {
                        var thisRule = ruleList.FirstOrDefault(x => x.ID == lvID);

                        //Copy the Rule and Set a new GUID
                        DNSRoamingRule newRule = thisRule.Clone();
                        ruleList.Add(newRule);

                        //Refresh the Rules List
                        ListRules();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
        }
            }

        /// <summary>
        /// Remove a Rule action
        /// </summary>
        private void ListRuleRemove()
        {
            try
            {
                if (listViewRules.SelectedItems.Count != 0)
                {
                    string lvID = listViewRules.SelectedItems[0].Text;

                    if (ruleList.Any(x => x.ID == lvID))
                    {
                        var thisRule = ruleList.FirstOrDefault(x => x.ID == lvID);

                        DialogResult dialogResult = MessageBox.Show("Are you sure you want to remove this rule?", string.Format("Remove Rule (ID:{0})", thisRule.ID), MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            thisRule.RemoveSaved();
                            ruleList.Remove(thisRule);
                            ListRules();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private void SaveOptions()
        {

            if (!optionsPathExist) return;

            try
            {
                DNSRoamingOption newOption = new DNSRoamingOption();
                newOption.Load();
                newOption.DisableIPV6 = chkIPV6Disable.Checked;
                newOption.DaysToRetainLogs = (int)retainLogDays.Value;
                newOption.Save();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

        }

        #endregion

        #region Form Events

        private void listViewRules_DoubleClick(object sender, EventArgs e)
        {
            ListRuleEdit();
        }

        private void btnRuleEdit_Click(object sender, EventArgs e)
        {
            ListRuleEdit();
        }

        private void btnRuleNew_Click(object sender, EventArgs e)
        {
            ListRuleNew();
        }

        private void btnRuleRemove_Click(object sender, EventArgs e)
        {
            ListRuleRemove();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool saveFailed = false;
            errorProvider.Clear();

            SaveOptions();

            foreach (DNSRoamingRule thisRule in ruleList)
            {
                if (!thisRule.Save())
                    saveFailed = true;
            }

            if (saveFailed)
                errorProvider.SetError(btnSave, "Error Saving. Check the Client Logs");
            else
                this.Close();
        }

        private void linkGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(@"https://github.com/andrewbadge/DNSRoaming");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private void listViewRules_MouseHover(object sender, EventArgs e)
        {
            if (listViewRules.SelectedItems.Count != 0)
            {
                string lvID = listViewRules.SelectedItems[0].Text;

                toolTip.Show(String.Format("Rule ID {0}", lvID), listViewRules);
            }
        }

        private void listViewRules_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRuleEdit.Enabled = (settingsPathExist && listViewRules.SelectedItems.Count != 0);
            btnRuleCopy.Enabled = (settingsPathExist && listViewRules.SelectedItems.Count != 0);
            btnRuleRemove.Enabled = (settingsPathExist && listViewRules.SelectedItems.Count != 0);
        }

        private void btnRuleCopy_Click(object sender, EventArgs e)
        {
            ListRuleCopy();
        }


        #endregion
    }
}
