using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNS_Roaming_Common
{
    public class DNSRoamingRule
    {

        private string ruleID;
        public string ID
        {
            get { return ruleID; }
        }

        private bool useNetworkType;
        public bool UseNetworkType
        {
            get { return useNetworkType; }
            set { useNetworkType = value; }
        }

        private string networkType;
        public string NetworkType
        {
            get { return networkType; }
            set { networkType = value; }
        }

        private string networkName;
        public string NetworkName
        {
            get { return networkName; }
            set { networkName = value; }
        }

        private bool addressSpecific;
        public bool AddressSpecific
        {
            get { return addressSpecific; }
            set { addressSpecific = value; }
        }

        private string addressIP;
        public string AddressIP
        {
            get { return addressIP; }
            set { addressIP = value; }
        }

        private string addressSubnet;
        public string AddressSubnet
        {
            get { return addressSubnet; }
            set { addressSubnet = value; }
        }

        private string dnsSet;
        public string DNSSet
        {
            get { return dnsSet; }
            set { dnsSet = value; }
        }

        private string dnsPreferred;
        public string DNSPreferred
        {
            get { return dnsPreferred; }
            set { dnsPreferred = value; }
        }

        private string dnsAlternative;
        public string DNSAlternative
        {
            get { return dnsAlternative; }
            set { dnsAlternative = value; }
        }

        public DNSRoamingRule()
        {
            if (ruleID == null)
            {
                ruleID = System.Guid.NewGuid().ToString();
            }
        }
    }

    public static class DNSRoamingRuleDefault
    {
        public static DNSRoamingRule GetDefaultRule()
        {
            DNSRoamingRule newRule = new DNSRoamingRule();
            newRule.UseNetworkType = true;
            newRule.NetworkType = "Ethernet,Wireless80211";
            newRule.DNSSet = "Quad9 + CloudFlare - No Malware";
            newRule.DNSPreferred = string.Empty;
            newRule.DNSAlternative = string.Empty;
            newRule.AddressSpecific = false;

            return newRule;
        }
    }
}