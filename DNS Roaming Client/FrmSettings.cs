using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using DNS_Roaming_Common;

namespace DNS_Roaming_Client
{
    public partial class FrmSettings : Form
    {
        static string logs="";
        IList<DNSRoamingRule> ruleList = new List<DNSRoamingRule>();
        
        public FrmSettings()
        {
            InitializeComponent();
            NetworkChange.NetworkAddressChanged += new NetworkAddressChangedEventHandler(AddressChangedCallback);
            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(AvailabilityChangedCallback);

            InitialiseRules();
            ListRules();
        }

        static void AddressChangedCallback(object sender, EventArgs e)
        {

            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface n in adapters)
            {

                logs += string.Format("   {0} is {1}", n.Name, n.OperationalStatus);
            }
        }

        static void AvailabilityChangedCallback(object sender, EventArgs e)
        {

            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface n in adapters)
            {

                logs += string.Format("   {0} is {1}", n.Name, n.OperationalStatus);
            }
        }

        private void InitialiseRules()
        {
            if (ruleList.Count==0)
            {


                DNSRoamingRule newRule = new DNSRoamingRule();
                newRule.UseNetworkType = true;
                newRule.NetworkType = "Ethernet,Wireless80211,Banana";
                newRule.DNSSet = "Quad9 + CloudFlare - No Malware";
                newRule.DNSPreferred = string.Empty;
                newRule.DNSAlternative = string.Empty;

                ruleList.Add(newRule);
            }
        }
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


                lvItem.SubItems.Add("Any Subnet");

                if (thisRule.DNSPreferred == String.Empty && thisRule.DNSAlternative == String.Empty)
                    lvItem.SubItems.Add(thisRule.DNSSet);
                else
                    lvItem.SubItems.Add(String.Format("{0},{1}", thisRule.DNSPreferred, thisRule.DNSAlternative).Trim());
                
                listViewRules.Items.Add(lvItem);
            }



        }

        private void ListRuleNew()
        {
            FrmRule frmRule = new FrmRule();
            frmRule.ShowDialog();

            if (!frmRule.FormCancelled)
            {
                DNSRoamingRule returnRule = frmRule.ThisRule;
                //thisRule = returnRule;
                ListRules();
            }
        }

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
                        //do something
                    }
                }
            }
        }

        /*
        private void button1_Click(object sender, EventArgs e)
        {
            
            FrmRule frmRule = new FrmRule();
            frmRule.ThisRule = newRule;
            frmRule.ShowDialog();

            if (!frmRule.FormCancelled)
            {
                newRule = frmRule.ThisRule;
                ListRules();
            }
        }
        */

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
    }
}
