using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace DNS_Roaming_Common
{
    /// <summary>
    /// Rule Class with all the properties saved for each setting
    /// </summary>
    [Serializable]
    public class DNSRoamingRule
    {

        private string ruleFileNameFullPath;

        #region Properties

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
            set 
            { 
                networkType = value; 
                if (networkType != String.Empty)
                {
                    networkNameIs = string.Empty;
                    networkNameIsNot = string.Empty;
                }
            }
        }

        private string networkNameIs;
        public string NetworkNameIs
        {
            get { return networkNameIs; }
            set 
            { 
                networkNameIs = value;
                if (networkNameIs!= String.Empty)
                {
                    networkType = string.Empty;
                    networkNameIsNot = string.Empty;
                }
            }
        }

        private string networkNameIsNot;
        public string NetworkNameIsNot
        {
            get { return networkNameIsNot; }
            set
            {
                networkNameIsNot = value;
                if (networkNameIsNot != String.Empty)
                {
                    networkType = string.Empty;
                    networkNameIs = string.Empty;
                }
            }
        }

        private bool addressIsSpecific;
        public bool AddressIsSpecific
        {
            get { return addressIsSpecific; }
            set 
            { 
                addressIsSpecific = value;
                if (addressIsSpecific)
                {
                    addressIsNotSpecific = false;
                }
            }
        }

        private bool addressIsNotSpecific;
        public bool AddressIsNotSpecific
        {
            get { return addressIsNotSpecific; }
            set
            {
                addressIsNotSpecific = value;
                if (addressIsNotSpecific)
                {
                    addressIsSpecific = false;
                }
            }
        }

        private int addressByType;
        public int AddressByType
        {
            get { return addressByType; }
            set
            {
                addressByType = value;
            }
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

        private string dns2ndAlternative;
        public string DNS2ndAlternative
        {
            get { return dns2ndAlternative; }
            set { dns2ndAlternative = value; }
        }

        private string dns3rdAlternative;
        public string DNS3rdAlternative
        {
            get { return dns3rdAlternative; }
            set { dns3rdAlternative = value; }
        }

        private int delaySeconds = 5;
        public int DelaySeconds
        {
            get { return delaySeconds; }
            set { delaySeconds = value; }
        }

        private bool ruleWasDownloaded = false;
        public bool RuleWasDownloaded
        {
            get { return ruleWasDownloaded; }
            set { ruleWasDownloaded = value; }
        }

        #endregion

        public DNSRoamingRule()
        {
            //Do not set a new GUID on intialisation.
            //Otherwise when loading a saved rules, the GUID will be wrong
        }

        #region Methods

        /// <summary>
        /// Save the objeect as a settings file in the Settings folder
        /// </summary>
        public virtual bool Save()
        {
            bool SaveSuccessful = false;
            try
            {
                //Set a New GUID if saving and the GUID is missing.
                //Should only occur for a new Rule
                if (ruleID == null)
                {
                    ruleID = System.Guid.NewGuid().ToString();
                }

                //Determine the path for the settings
                //Creates the folder if missing
                PathsandData pathsandData = new PathsandData();
                ruleFileNameFullPath = String.Format(@"{0}\Rule-{1}.xml", pathsandData.BaseSettingsPath, ruleID);

                //Write the object to the file
                StreamWriter w = new StreamWriter(ruleFileNameFullPath);
                XmlSerializer s = new XmlSerializer(GetType());
                s.Serialize(w, this);
                w.Close();

                SaveSuccessful = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return SaveSuccessful;

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
                Logger.Error(ex.Message);
            }

        }

        public virtual bool Load(string settingFiletoLoad)
        {
            bool loadSuccessful = false;
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

                    loadSuccessful = true;
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                loadSuccessful = false;
            }
            return loadSuccessful;
        }

        /// <summary>
        /// Clone the existing object without reference and a new GUID
        /// </summary>
        /// <returns></returns>
        public virtual DNSRoamingRule Clone()
        {
            DNSRoamingRule newRule = new DNSRoamingRule();

            try
            {
                newRule.AddressByType = addressByType;
                newRule.AddressIP = addressIP;
                newRule.AddressIsNotSpecific = addressIsNotSpecific;
                newRule.AddressIsSpecific = addressIsSpecific;
                newRule.AddressSubnet = addressSubnet;
                newRule.DelaySeconds = delaySeconds;
                newRule.DNS2ndAlternative = dns2ndAlternative;
                newRule.DNS3rdAlternative = dns3rdAlternative;
                newRule.DNSAlternative = dnsAlternative;
                newRule.DNSPreferred = dnsPreferred;
                newRule.DNSSet = dnsSet;
                newRule.NetworkNameIs = networkNameIs;
                newRule.NetworkNameIsNot = networkNameIsNot;
                newRule.NetworkType = networkType;
                newRule.UseNetworkType = useNetworkType;

                Guid newGUID = Guid.NewGuid();
                newRule.ID = newGUID.ToString();

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return newRule;
        }

        #endregion
    }

    public static class DNSRoamingRuleDefault
    {
        /// <summary>
        /// Default Rule if no other rules exist.
        /// </summary>
        /// <returns></returns>
        public static DNSRoamingRule GetDefaultRule()
        {
            DNSRoamingRule newRule = new DNSRoamingRule();
            newRule.UseNetworkType = true;
            newRule.NetworkType = "Wireless80211";
            newRule.DNSSet = "Quad9 + CloudFlare - No Malware";
            newRule.DNSPreferred = string.Empty;
            newRule.DNSAlternative = string.Empty;
            newRule.AddressIsSpecific = false;

            return newRule;
        }
    }
}