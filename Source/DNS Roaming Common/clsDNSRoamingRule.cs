using System;
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

        public string ID { get; set; }
        
        public bool UseNetworkType { get; set; }
        
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
                if (networkNameIs != String.Empty)
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

        public int AddressByType { get; set; }
        
        public string AddressIP { get; set; }
        public string AddressSubnet { get; set; }
        
        public string DNSSet { get; set; }
        
        public string DNSPreferred { get; set; }
        
        public string DNSAlternative { get; set; }
        
        public string DNS2ndAlternative { get; set; }
        
        public string DNS3rdAlternative { get; set; }
        
        public bool ResetToDHCP { get; set; }
        
        private int delaySeconds = 5;
        public int DelaySeconds
        {
            get { return delaySeconds; }
            set { delaySeconds = value; }
        }

        public bool RuleWasDownloaded { get; set; }
        public string RuleDownloadURL { get; set; }

        public int PingType { get; set; }
        public string PingAddress { get; set; }
        
        // 0 = Do not Query, 1 = Query for Success, 2 = Query for Fail
        public int DNSQueryType { get; set; }
        public string DNSQueryDomainName { get; set; }
        public string DNSQueryServer { get; set; }
        public string DNSQueryRecordType { get; set; }

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
                if (ID == null)
                {
                    ID = System.Guid.NewGuid().ToString();
                }

                //Determine the path for the settings
                //Creates the folder if missing
                PathsandData pathsandData = new PathsandData();
                ruleFileNameFullPath = String.Format(@"{0}\Rule-{1}.xml", pathsandData.BaseSettingsPath, ID);

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
                if (ID == null) return;

                PathsandData pathsandData = new PathsandData();
                ruleFileNameFullPath = String.Format(@"{0}\Rule-{1}.xml", pathsandData.BaseSettingsPath, ID);

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
                newRule.AddressByType = AddressByType;
                newRule.AddressIP = AddressIP;
                newRule.AddressIsNotSpecific = addressIsNotSpecific;
                newRule.AddressIsSpecific = addressIsSpecific;
                newRule.AddressSubnet = AddressSubnet;
                newRule.DelaySeconds = delaySeconds;
                newRule.DNS2ndAlternative = DNS2ndAlternative;
                newRule.DNS3rdAlternative = DNS3rdAlternative;
                newRule.DNSAlternative = DNSAlternative;
                newRule.DNSPreferred = DNSPreferred;
                newRule.DNSSet = DNSSet;
                newRule.NetworkNameIs = networkNameIs;
                newRule.NetworkNameIsNot = networkNameIsNot;
                newRule.NetworkType = networkType;
                newRule.UseNetworkType = UseNetworkType;

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