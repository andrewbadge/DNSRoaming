using DNS_Roaming_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace DNS_Roaming_Client
{
    public partial class FrmSettings : Form
    {
        
        IList<DNSRoamingRule> ruleList = new List<DNSRoamingRule>();
        
        public FrmSettings()
        {
            InitializeComponent();
            PathsandData pathsandData = new PathsandData();
            InitialiseRules(pathsandData.BaseSettingsPath);
            ListRules();
        }

        /// <summary>
        /// Load Rules from the Settings Files
        /// </summary>
        /// <param name="settingPath"></param>
        private void InitialiseRules(string settingPath)
        {

            string[] settingFiles = Directory.GetFiles(settingPath);

            try
            {
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
                ListViewItem lvItem = new ListViewItem();
                lvItem.Text = thisRule.ID;

                if (thisRule.UseNetworkType)
                    lvItem.SubItems.Add(String.Format("Type is {0}", thisRule.NetworkType));
                else
                    lvItem.SubItems.Add(String.Format("Name is {0}",thisRule.NetworkName));

                if (thisRule.AddressSpecific)
                    lvItem.SubItems.Add(String.Format("{0}/{1}", thisRule.AddressIP, thisRule.AddressSubnet));
                else
                    lvItem.SubItems.Add("Any Subnet");

                if (thisRule.DNSPreferred == String.Empty && thisRule.DNSAlternative == String.Empty)
                    lvItem.SubItems.Add(thisRule.DNSSet);
                else
                    lvItem.SubItems.Add(String.Format("{0},{1}", thisRule.DNSPreferred, thisRule.DNSAlternative).Trim());
                
                listViewRules.Items.Add(lvItem);
            }



        }

        /// <summary>
        /// New Rule action
        /// </summary>
        private void ListRuleNew()
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

        /// <summary>
        /// Edit a Rule action
        /// </summary>
        private void ListRuleEdit()
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

        /// <summary>
        /// Remove a Rule action
        /// </summary>
        private void ListRuleRemove()
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
            foreach (DNSRoamingRule thisRule in ruleList)
            {
                thisRule.Save();
            }
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
    }
}
