using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNS_Roaming_Client
{
    public class Rule
    {
        
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
    }
}
