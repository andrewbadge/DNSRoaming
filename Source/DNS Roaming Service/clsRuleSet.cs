using DNS_Roaming_Common;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace DNS_Roaming_Service
{
    class clsRuleSet
    {
        /// <summary>
        /// Check the Registry for the RuleURL to download (set by the MSI) 
        /// saves it to the config XML file and parses the file
        /// Called on service start
        /// </summary>
        public void CheckRegistryForRuleSet()
        {
            Logger.Debug("CheckRegistryForRuleSet");

            string registryKeyName = @"SOFTWARE\WOW6432Node\DNSRoaming";
            const string registryValueName = "RuleSetURL";
            string ruleSetURL = string.Empty;

            try
            {

                RegistryKey regKey = Registry.LocalMachine.OpenSubKey(registryKeyName, true);
                if (regKey == null)
                {
                    registryKeyName = @"SOFTWARE\DNSRoaming";
                    regKey = Registry.LocalMachine.OpenSubKey(registryKeyName, true);
                }

                //Not installed?
                if (regKey != null)
                {
                    var regValue = regKey.GetValue(registryValueName);
                    if (regValue != null)
                        ruleSetURL = (string)regValue;
                }

                //If the command is to clear the URL?
                if (ruleSetURL.Trim().ToLower() == "clear")
                {
                    //Clear the URI
                    RuleSetData ruleSetData = new RuleSetData();
                    ruleSetData.Load();
                    ruleSetData.RuleSetDownloadURL = String.Empty;
                    ruleSetData.Save();

                    regKey.SetValue(registryValueName, string.Empty);
                }
                else
                {
                    //Otherwise check if its a valid URI
                    Uri ruleSetUri;
                    bool isValidURL = Uri.TryCreate(ruleSetURL, UriKind.Absolute, out ruleSetUri) && (ruleSetUri.Scheme == Uri.UriSchemeHttp || ruleSetUri.Scheme == Uri.UriSchemeHttps);

                    if (isValidURL)
                    {
                        //Parse the rule the first time
                        string ruleSetFilename = DownloadRuleSet(ruleSetUri);
                        if (ruleSetFilename != String.Empty)
                        {
                            ParseRuleSet(ruleSetFilename);
                        }

                        //Save the URL and clear the registry
                        //Even if the URL is invalid as that might be a server side issue
                        RuleSetData ruleSetData = new RuleSetData();
                        ruleSetData.Load();
                        ruleSetData.RuleSetDownloadURL = ruleSetURL;
                        ruleSetData.Save();

                        regKey.SetValue(registryValueName, string.Empty);

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        /// <summary>
        /// Downloads a RuleSet defined by a URL
        /// Called when chekcing the registry and periodically if the RuleSet URL is set
        /// </summary>
        /// <param name="ruleSetUri"></param>
        /// <returns></returns>
        public string DownloadRuleSet(Uri ruleSetUri)
        {
            Logger.Debug("DownloadRuleSet");

            string ruleSetFilename = string.Empty;

            try
            {
                Logger.Info("Downloading an updated rule set");

                //Initialise Paths and set Permissions if neccessary
                PathsandData pathsandData = new PathsandData();
                pathsandData.CreateDataPaths(true);
                Guid g = Guid.NewGuid();
                string localFilename = Path.Combine(pathsandData.BaseDownloadsPath, String.Format("TmpDNSSet{0}.txt", g.ToString()));
                
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

        /// <summary>
        /// Parses a downloaded rule file
        /// </summary>
        /// <param name="ruleSetFilename"></param>
        public void ParseRuleSet(string ruleSetFilename)
        {
            Logger.Debug("ParseRuleSet");

            try
            {
                //A minVersion may be optionally set in the file.
                //If false; then the file won't be processed.
                bool minVersionOk = true;

                //Check if less than 10KB. That should cover 50+ rules.
                //It would be weird if larger so ignore the file
                FileInfo fi = new FileInfo(ruleSetFilename);
                if (fi.Length < (10 * 1024))
                {
                    bool deleteAllRulesTriggered = false;
                    IList<string> newRuleGUIDs = new List<string>();
                    string returnRuleGuid = string.Empty;

                    foreach (string ruleLine in System.IO.File.ReadLines(ruleSetFilename))
                    {
                        bool lineProcessed = false;

                        //Blank Line
                        if (!lineProcessed && ruleLine.Trim() == string.Empty) lineProcessed = true;

                        //Comment Line
                        if (!lineProcessed && ruleLine.Trim().StartsWith("//")) lineProcessed = true;
                        
                        //Delete all existing Rule Files
                        if (!lineProcessed && ruleLine.Trim().ToLower() == "deleteallrules")
                        {
                            lineProcessed = true;
                            //Perform after all lines are read so a different can be checked
                            deleteAllRulesTriggered = true;
                        }

                        //Only process the file if the service is this version or above
                        if (!lineProcessed && ruleLine.Trim().ToLower().StartsWith("minversion"))
                        {
                            lineProcessed = true;

                            //Expect the format minversion:1.1
                            string[] versionParts = ruleLine.Split(':');
                            if (versionParts.Length == 2)
                            {
                                Version minVersion = Version.Parse(versionParts[1]);

                                InstallerInfo installerInfo = new InstallerInfo();
                                Version installedversion = installerInfo.GetInstalledMSIVersion();

                                //If the installed Version is equal to or newer than the min
                                var versionComparison = installedversion.CompareTo(minVersion);
                                minVersionOk = versionComparison >= 0;

                                if (!minVersionOk)
                                {
                                    Logger.Info(String.Format("Rule file requires a minimum version ({0}) greater than the installed version ({1})",minVersion.ToString(),installedversion.ToString()));
                                    break;
                                }
                            }
                        }

                        //Line with a possible URL
                        if (!lineProcessed)
                        {
                            string[] ruleParts = ruleLine.Split(',');

                            //Expecting url
                            if (ruleParts.Length == 1)
                            {
                                Uri ruleUri;
                                bool isValidURL = Uri.TryCreate(ruleParts[0], UriKind.Absolute, out ruleUri) && (ruleUri.Scheme == Uri.UriSchemeHttp || ruleUri.Scheme == Uri.UriSchemeHttps);

                                if (isValidURL)
                                {
                                    DownloadandReplaceRule(ruleUri.ToString(), out returnRuleGuid);
                                    if (returnRuleGuid != string.Empty) newRuleGUIDs.Add(returnRuleGuid);
                                }
                            }

                            //Expecting localfilename,url
                            //This is the orignal format but now we derive the filename from the GUID in the Rule
                            if (ruleParts.Length == 2)
                            {
                                Uri ruleUri;
                                bool isValidURL = Uri.TryCreate(ruleParts[1], UriKind.Absolute, out ruleUri) && (ruleUri.Scheme == Uri.UriSchemeHttp || ruleUri.Scheme == Uri.UriSchemeHttps);

                                if (isValidURL)
                                {
                                    DownloadandReplaceRule(ruleUri.ToString(), out returnRuleGuid);
                                    if (returnRuleGuid != string.Empty) newRuleGUIDs.Add(returnRuleGuid);
                                }
                            }    
                        }

                    }

                    //Delete all existing rules at the end
                    if (minVersionOk && deleteAllRulesTriggered) DeleteAllRules(newRuleGUIDs);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        /// <summary>
        /// Downloads a rule (set in a Rule File)
        /// Parses the Rule file and retutrns the GUID
        /// </summary>
        /// <param name="ruleURL"></param>
        /// <param name="returnRuleGuid"></param>
        public void DownloadandReplaceRule(string ruleURL, out string returnRuleGuid)
        {
            Logger.Debug("DownloadandReplaceRule");

            PathsandData pathsandData = new PathsandData();
            Guid g = Guid.NewGuid();
            string localFilename = Path.Combine(pathsandData.BaseDownloadsPath, String.Format("TmpDownloadedRule{0}.xml",g.ToString()));
            returnRuleGuid = string.Empty;

            try
            {
                Logger.Info(String.Format("Downloading a new version of rule from {0}", ruleURL));

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
                    newRule.RuleDownloadURL = ruleURL;
                    newRule.Save();

                    returnRuleGuid = newRule.ID;
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            finally
            {
                //Remove the download File is it already exists
                if (File.Exists(localFilename)) File.Delete(localFilename);
            }
        }

        /// <summary>
        /// Deletes all rules except for the ones listed (recently downloaded)
        /// Called when the DeleteAllRules command is listed in a RuleSet file
        /// </summary>
        /// <param name="ruleGUIDsToRetain"></param>
        private void DeleteAllRules(IList<string> ruleGUIDsToRetain)
        {
            Logger.Debug("DeleteAllRules");

            try
            {
                Logger.Debug("Deleting all existing Rules");

                //Initialise Paths and set Permissions if neccessary
                PathsandData pathsandData = new PathsandData();
                pathsandData.CreateDataPaths(true);

                string[] settingFiles = Directory.GetFiles(pathsandData.BaseSettingsPath, "*.xml", SearchOption.TopDirectoryOnly);
                foreach (string settingFilename in settingFiles)
                {
                    //Catch an exception for a specific file but continue to process the next
                    try
                    {
                        FileInfo settingsFile = new FileInfo(settingFilename);
                        string fileNameGUID = settingsFile.Name.Replace("Rule-", "").Replace(".xml", "");
                        if (!ruleGUIDsToRetain.Contains(fileNameGUID))
                        {
                            //File was not in the list of Rules just added
                            File.Delete(settingFilename);
                        }
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

    }
}
