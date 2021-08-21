using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace DNS_Roaming_Common
{
    [Serializable]
    public class DNSRoamingRule
    {

        private string ruleFileNameFullPath;

        private string ruleID;
        public string ID
        {
            get { return ruleID; }
            set { ruleID = value; }
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
            
        }

        #region Methods

        public virtual void Save()
        {
            try
            {
                if (ruleID == null)
                {
                    ruleID = System.Guid.NewGuid().ToString();
                }

                PathsandData pathsandData = new PathsandData();
                ruleFileNameFullPath = String.Format(@"{0}\Rule-{1}.xml", pathsandData.BaseSettingsPath, ruleID);


                StreamWriter w = new StreamWriter(ruleFileNameFullPath);
                XmlSerializer s = new XmlSerializer(GetType());
                s.Serialize(w, this);
                w.Close();
            }
            catch (Exception ex)
            {
                //FileLogger.Trace("Settings.Save", ex);
            }

        }

        public virtual void RemoveSaved()
        {
            try
            {
                if (ruleID == null) return;
                
                PathsandData pathsandData = new PathsandData();
                ruleFileNameFullPath = String.Format(@"{0}\Rule-{1}.xml", pathsandData.BaseSettingsPath, ruleID);

                File.Delete(ruleFileNameFullPath);
            }
            catch (Exception ex)
            {
                //FileLogger.Trace("Settings.Save", ex);
            }

        }

        public virtual void Load(string settingFiletoLoad)
        {
            try
            {
                bool validFile = true;

                if (File.Exists(settingFiletoLoad))
                {
                    System.IO.FileInfo fileInfo = new FileInfo(settingFiletoLoad);
                    if (fileInfo.Length == 0)
                        validFile = false;
                }
                else
                    validFile = false;

                if (validFile)
                {
                    StreamReader sr = new StreamReader(settingFiletoLoad);
                    XmlTextReader xr = new XmlTextReader(sr);
                    XmlSerializer xs = new XmlSerializer(GetType());
                    object c;
                    if (xs.CanDeserialize(xr))
                    {
                        c = xs.Deserialize(xr);
                        Type t = GetType();
                        PropertyInfo[] properties = t.GetProperties();
                        foreach (PropertyInfo p in properties)
                        {
                            p.SetValue(this, p.GetValue(c, null), null);
                        }
                    }
                    xr.Close();
                    sr.Close();
                }

            }
            catch (Exception ex)
            {
                //FileLogger.Trace("Settings.Load", ex);
            }
        }

        #endregion
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