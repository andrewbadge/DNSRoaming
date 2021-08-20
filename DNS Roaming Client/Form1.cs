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

namespace DNS_Roaming_Client
{
    public partial class Form1 : Form
    {
        static string logs="";
        Rule newRule;
        public Form1()
        {
            InitializeComponent();
            NetworkChange.NetworkAddressChanged += new NetworkAddressChangedEventHandler(AddressChangedCallback);
            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(AvailabilityChangedCallback);
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (newRule == null)
            {
                newRule = new Rule();
                newRule.UseNetworkType = true;
                newRule.DNSSet = "Quad9 + CloudFlare - No Malware";
            }

            FrmRule frmRule = new FrmRule();
            frmRule.ThisRule = newRule;
            frmRule.ShowDialog();

            if (!frmRule.FormCancelled) newRule = frmRule.ThisRule;
        }
    }
}
