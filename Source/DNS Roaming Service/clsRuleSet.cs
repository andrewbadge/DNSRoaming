using DNS_Roaming_Common;
using Microsoft.Win32;
using System;
using System.IO;
using System.Net;

namespace DNS_Roaming_Service
{
    class clsRuleSet
    {
        public void CheckRegistryForRuleSet()
        {
            Logger.Debug("CheckRegistryForRuleSet");

            string registryKeyName = @"SOFTWARE\WOW6432Node\DNSRoaming";
            const string registryValueName = "RuleSetURL";
            string ruleSetURL = string.Empty;

            try
            {

                RegistryKey regKey = Registry.LocalMachine.OpenSubKey(registryKeyName);
                if (regKey == null)
                {
                    registryKeyName = @"SOFTWARE\DNSRoaming";
                    regKey = Registry.LocalMachine.OpenSubKey(registryKeyName);
                }

                //Not installed?
                if (regKey != null)
                {
                    var regValue = regKey.GetValue(registryValueName);
                    if (regValue != null)
                        ruleSetURL = (string)regValue;
                }

                Uri ruleSetUri;
                bool isValidURL = Uri.TryCreate(ruleSetURL, UriKind.Absolute, out ruleSetUri) && (ruleSetUri.Scheme == Uri.UriSchemeHttp || ruleSetUri.Scheme == Uri.UriSchemeHttps);

                if (isValidURL)
                {
                    string ruleSetFilename = DownloadRuleSet(ruleSetUri);
                    if (ruleSetFilename != String.Empty)
                    {
                        ParseRuleSet(ruleSetFilename);
                    }
                }


            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private string DownloadRuleSet(Uri ruleSetUri)
        {
            Logger.Debug("DownloadRuleSet");

            string ruleSetFilename = string.Empty;

            try
            {
                Logger.Info("Downloading an updated rule set");

                //Initialise Paths and set Permissions if neccessary
                PathsandData pathsandData = new PathsandData();
                pathsandData.CreateDataPaths(true);
                string localFilename = Path.Combine(pathsandData.BaseDownloadsPath, "dnsset.txt");

                //Remove the download File is it already exists
                if (File.Exists(localFilename)) File.Delete(localFilename);

                using (var client = new WebClient())
                {
                    client.DownloadFile(ruleSetUri.ToString(), localFilename);
                }

                if (File.Exists(localFilename)) ruleSetFilename = localFilename;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return ruleSetFilename;
        }

        private void ParseRuleSet(string ruleSetFilename)
        {
            Logger.Debug("ParseRuleSet");

            try
            {
                //Check if less than 10KB. That should cover 50+ rules.
                //It would be weird if larger so ignore the file
                FileInfo fi = new FileInfo(ruleSetFilename);
                if (fi.Length < (10 * 1024))
                {
                    foreach (string ruleLine in System.IO.File.ReadLines(ruleSetFilename))
                    {
                        string[] ruleParts = ruleLine.Split(',');

                        //Each line must be the localfilename,url
                        if (ruleParts.Length == 2)
                        {
                            Uri ruleUri;
                            bool isValidURL = Uri.TryCreate(ruleParts[1], UriKind.Absolute, out ruleUri) && (ruleUri.Scheme == Uri.UriSchemeHttp || ruleUri.Scheme == Uri.UriSchemeHttps);

                            if (isValidURL)
                            {
                                DownloadandReplaceRule(ruleUri.ToString(), ruleParts[0]);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        public void DownloadandReplaceRule(string ruleURL, string ruleFileName)
        {
            Logger.Debug("DownloadandReplaceRule");

            string localFilename = string.Empty;

            try
            {
                Logger.Info(String.Format("Downloading a new version of rule {0} from {1}", ruleFileName, ruleURL));

                PathsandData pathsandData = new PathsandData();
                localFilename = Path.Combine(pathsandData.BaseSettingsPath, ruleFileName);

                //Remove the download File is it already exists
                if (File.Exists(localFilename)) File.Delete(localFilename);

                //Download the new Rule
                using (var client = new WebClient())
                {
                    client.DownloadFile(ruleURL, localFilename);
                }

                //Load the rule to Ensure it can parse
                DNSRoamingRule newRule = new DNSRoamingRule();
                if (newRule.Load(localFilename))
                {
                    newRule.RuleWasDownloaded = true;
                    newRule.Save();
                }
                else
                    //Remove the download File if there is an exception. Eg. Parsing failed
                    File.Delete(localFilename);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

    }
}
