using DNS_Roaming_Common;
using DnsClient;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.ServiceProcess;
using System.Timers;

namespace DNS_Roaming_Service
{
    public partial class svcMain : ServiceBase
    {
        static IList<DNSRoamingRule> ruleList = new List<DNSRoamingRule>();
        static FileSystemWatcher watcherSettings;
        static FileSystemWatcher watcherOptions;
        static bool isServicePaused = false;

        static DateTime lastEventTriggered = new DateTime();
        static int countEventSkipped = 0;

        static DateTime lastLoadOptionsTriggered = new DateTime();
        static bool optionJustSaved = false;

        //Options
        static bool disableIPV6 = true;
        static int daysToRetainLogs = 14;
        static bool autoUpdate = true;
        static int hoursToUpdateRuleSet = 72;

        //DoH
        static bool InsertNewDoHAddresses = false;
        static int ForceDoHFallbackToUdp = 0;
        static int ForceDoHAutoUpgrade = 0;

        static System.Timers.Timer serviceTimer;
        static System.Timers.Timer logandUpdateTimer;
        static System.ComponentModel.BackgroundWorker backgroundWorker;

        public svcMain()
        {
            Logger.Debug("svcMain Initialize");

            try
            {
                InitializeComponent();
                InitializeBackgroundWorker();
                LoadOptions();
                InitialiseDoH();
                CheckRegistryForRuleSet();
                LoadDNSRules();
                registerEvents();
                ConfigureTimers();

                DNSRoamingNetworkInterfaces.IntialiseNetworkInterfaceTypes();
            }
            catch (Exception exception)
            {
                Logger.Error(exception.Message);
            }
        }

        #region Service Events

        protected override void OnStart(string[] args)
        {
            Logger.Info("Service Starting");
        }

        protected virtual void OnContinue(string[] args)
        {
            Logger.Info("Service Continuing");
            isServicePaused = false;
        }

        protected virtual void OnPause(string[] args)
        {
            Logger.Info("Service Paused");
            if (backgroundWorker.IsBusy) backgroundWorker.CancelAsync();
            isServicePaused = true;
        }

        protected override void OnStop()
        {
            Logger.Info("Service Stopping");
            if (backgroundWorker.IsBusy) backgroundWorker.CancelAsync();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Registers all the events (Network changes or Setting File changes)
        /// </summary>
        private void registerEvents()
        {
            Logger.Debug("registerEvents");

            NetworkChange.NetworkAddressChanged += new NetworkAddressChangedEventHandler(AddressChangedCallback);
            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(AvailabilityChangedCallback);

            PathsandData pathsandData = new PathsandData();

            //Watch the Settings folder for changes
            watcherSettings = new FileSystemWatcher(pathsandData.BaseSettingsPath);
            watcherSettings.NotifyFilter = NotifyFilters.Attributes
                                | NotifyFilters.CreationTime
                                | NotifyFilters.DirectoryName
                                | NotifyFilters.FileName
                                | NotifyFilters.LastAccess
                                | NotifyFilters.LastWrite
                                | NotifyFilters.Security
                                | NotifyFilters.Size;

            watcherSettings.Changed += SettingsFileChanged;
            watcherSettings.Created += SettingsFileCreated;
            watcherSettings.Deleted += SettingsFileDeleted;
            watcherSettings.Renamed += SettingsFileRenamed;

            watcherSettings.Filter = "*.xml";
            watcherSettings.IncludeSubdirectories = false;
            watcherSettings.EnableRaisingEvents = true;

            //Watch the Options folder for changes
            watcherOptions = new FileSystemWatcher(pathsandData.BaseOptionsPath);

            watcherOptions.NotifyFilter = NotifyFilters.Attributes
                                | NotifyFilters.CreationTime
                                | NotifyFilters.DirectoryName
                                | NotifyFilters.FileName
                                | NotifyFilters.LastAccess
                                | NotifyFilters.LastWrite
                                | NotifyFilters.Security
                                | NotifyFilters.Size;

            watcherOptions.Changed += OptionsFileChanged;
            watcherOptions.Created += OptionsFileCreated;
            watcherOptions.Deleted += OptionsFileDeleted;
            watcherOptions.Renamed += OptionsFileRenamed;

            watcherOptions.Filter = "*.xml";
            watcherOptions.IncludeSubdirectories = false;
            watcherOptions.EnableRaisingEvents = true;

            lastEventTriggered = DateTime.Now.AddDays(-1);
            countEventSkipped = 0;

        }

        private static void SettingsFileChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }

            Logger.Debug(String.Format("Settings file change detected"));
            LoadDNSRules();
            FireEvent();
        }

        private static void SettingsFileCreated(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Created)
            {
                return;
            }

            Logger.Debug(String.Format("Settings file create detected"));
            LoadDNSRules();
            FireEvent();
        }

        private static void SettingsFileDeleted(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Deleted)
            {
                return;
            }

            Logger.Debug(String.Format("Settings file delete detected"));
            LoadDNSRules();
            FireEvent();
        }

        private static void SettingsFileRenamed(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Renamed)
            {
                return;
            }

            Logger.Debug(String.Format("Settings file rename detected"));
            LoadDNSRules();
            FireEvent();
        }

        private static void OptionsFileChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }

            Logger.Debug(String.Format("Options file change detected"));
            LoadOptions();
        }

        private static void OptionsFileCreated(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Created)
            {
                return;
            }

            Logger.Info(String.Format("Options file create detected"));
            LoadOptions();
        }

        private static void OptionsFileDeleted(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Deleted)
            {
                return;
            }

            Logger.Debug(String.Format("Options file delete detected"));
            LoadOptions();
        }

        private static void OptionsFileRenamed(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Renamed)
            {
                return;
            }

            Logger.Debug(String.Format("Options file rename detected"));
            LoadOptions();
        }

        static void AddressChangedCallback(object sender, EventArgs e)
        {
            Logger.Info(String.Format("Address change detected"));
            FireEvent();
        }

        static void AvailabilityChangedCallback(object sender, EventArgs e)
        {
            Logger.Debug(String.Format("Availability change detected"));
            FireEvent();
        }

        private void InitializeBackgroundWorker()
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_WorkCompleted);
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Logger.Debug("backgroundWorker_DoWork");
            ValidateAllRules();
        }

        private void backgroundWorker_WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Worker Completed. No action really but leaving Event Empty.
            Logger.Debug("backgroundWorker_WorkCompleted");
        }

        /// <summary>
        /// Sets up the Timer which scans the network and rules periodically
        /// </summary>
        private static void ConfigureTimers()
        {
            Logger.Debug("ConfigureTimers");

            serviceTimer = new System.Timers.Timer(28 * 60 * 1000);
            serviceTimer.Elapsed += ServiceTimerEvent;
            serviceTimer.AutoReset = true;
            serviceTimer.Enabled = true;

            logandUpdateTimer = new System.Timers.Timer(2 * 60 * 1000);
            logandUpdateTimer.Elapsed += LogandUpdateTimerEvent;
            logandUpdateTimer.AutoReset = true;
            logandUpdateTimer.Enabled = true;

        }

        /// <summary>
        /// Periodically check the network even if a network change event wasn't fired
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        static void ServiceTimerEvent(Object source, ElapsedEventArgs e)
        {
            Logger.Debug("ServiceTimerEvent");

            Logger.Info(String.Format("Periodically checking networks"));
            if (FireEvent())
            {
                //Reschedule the next Timer to a random internal 
                //between 5 and 60 mins
                Random randomNumber = new Random();
                int timerDelay = randomNumber.Next(600, 3600) * 1000;
                serviceTimer.Interval = timerDelay;
            }
        }

        /// <summary>
        /// Periodically Do checks for old logs and updates
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        static void LogandUpdateTimerEvent(Object source, ElapsedEventArgs e)
        {
            Logger.Debug("LogandUpdateTimerEvent");

            try
            {
                CleanLogFiles();
                CleanTmpDownloads();
                DownloadRuleSet();
                CheckforUpdates();

                //Reschedule the next Timer to a random internal 
                //between 60 and 120 mins
                Random randomNumber = new Random();
                int timerDelay = randomNumber.Next(3600, 7200) * 1000;
                serviceTimer.Interval = timerDelay;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

        }

        /// <summary>
        /// Check for old log files and remove them
        /// </summary>
        private static void CleanLogFiles()
        {
            Logger.Debug("CleanLogFiles");

            //Initialise Paths and set Permissions if neccessary
            PathsandData pathsandData = new PathsandData();
            pathsandData.CreateDataPaths(true);

            string[] logFiles = Directory.GetFiles(pathsandData.BaseApplicationPath, "*Log*.txt", SearchOption.TopDirectoryOnly);
            foreach (string logFilename in logFiles)
            {
                //Catch an exception for a specific file but continue to process the next
                try
                {
                    //If the log file is older than the retention days then delete
                    if (System.IO.File.GetCreationTime(logFilename) < DateTime.Now.AddDays(-daysToRetainLogs))
                    {
                        File.Delete(logFilename);
                    }
                }
                catch
                {
                    Logger.Error(String.Format("Error removing log {0}", logFilename));
                }
            }
        }

        /// <summary>
        /// Check for old tmp Downloaded files and remove them
        /// Normally they should be cleaned as part of an update but
        /// if an exception or restarted occured there might be old files
        /// </summary>
        private static void CleanTmpDownloads()
        {
            Logger.Debug("CleanTmpDownloads");

            //Initialise Paths and set Permissions if neccessary
            PathsandData pathsandData = new PathsandData();
            pathsandData.CreateDataPaths(true);

            //Remove old download files if they exist
            string localFilename = Path.Combine(pathsandData.BaseDownloadsPath, "DNSSet.txt");
            if (File.Exists(localFilename)) File.Delete(localFilename);
            localFilename = Path.Combine(pathsandData.BaseDownloadsPath, "DNSRoaming.msi");
            if (File.Exists(localFilename)) File.Delete(localFilename);

            //File any tmp files in the Downloads folder
            string[] logFiles = Directory.GetFiles(pathsandData.BaseDownloadsPath, "Tmp*.*", SearchOption.TopDirectoryOnly);
            foreach (string logFilename in logFiles)
            {
                //Catch an exception for a specific file but continue to process the next
                try
                {
                    //If the log file is older than the retention days then delete
                    if (System.IO.File.GetCreationTime(logFilename) < DateTime.Now.AddDays(-1))
                    {
                        File.Delete(logFilename);
                    }
                }
                catch
                {
                    Logger.Error(String.Format("Error removing download {0}", logFilename));
                }
            }
        }

        /// <summary>
        /// Checks GitHub for a new version. If found downloads and installs
        /// </summary>
        /// <returns></returns>
        private static bool CheckforUpdates()
        {
            if (!autoUpdate) return false;

            Logger.Debug("CheckforUpdates");

            string applicationPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            DirectoryInfo parentDirectory = System.IO.Directory.GetParent(applicationPath);
            string updaterFolder = Path.Combine(parentDirectory.FullName, "Updater");
            string updaterFilename = Path.Combine(updaterFolder, "DNS Roaming Updater.exe");
            Logger.Debug(String.Format("Updater exe ({0})", updaterFilename));

            if (File.Exists(updaterFilename))
            {
                Logger.Debug("Starting Updater Application");

                //Execute Updater Application
                Process process = new Process();
                process.StartInfo.FileName = "DNS Roaming Updater.exe";
                process.StartInfo.WorkingDirectory = updaterFolder;
                process.StartInfo.Arguments = "";
                process.StartInfo.Verb = "runas";
                process.Start();

                return true;
            }
            else
            {
                Logger.Warn("Updater application was not found");
                return false;
            }
        }

        /// <summary>
        /// Fires the event to check the network. Checks throttling settings
        /// </summary>
        /// <returns>True if the Event was Fired, False if skipped</returns>
        static bool FireEvent()
        {
            bool eventFired = false;

            if (!IsEventThrottled())
            {
                eventFired = true;
                backgroundWorker.RunWorkerAsync();
            }

            return eventFired;
        }

        /// <summary>
        /// Determines whether an event should trigger a scan. Prevents lots of events triggering too frequently
        /// </summary>
        /// <returns></returns>
        static bool IsEventThrottled()
        {
            Logger.Debug("IsEventThrottled");

            bool throttled = false;
            TimeSpan eventDelay = DateTime.Now.Subtract(lastEventTriggered);


            if (backgroundWorker.IsBusy)
            {
                countEventSkipped += 1;
                throttled = true;

                Logger.Debug(string.Format("Event already in progress ({0} skipped)", countEventSkipped));
            }
            else
            {
                //If an event hasn't been actioned for more than 5 seconds
                //or we already skipped 3 events
                if (eventDelay.TotalSeconds > 5 || countEventSkipped > 3)
                {
                    Logger.Debug("Event wasn't Throttled");

                    lastEventTriggered = DateTime.Now;
                    countEventSkipped = 0;
                    throttled = false;
                }
                else
                {
                    countEventSkipped += 1;
                    throttled = true;

                    Logger.Debug(string.Format("Event was throttled ({0} skipped)", countEventSkipped));
                }
            }

            return throttled;
        }

        #endregion

        #region Initialisation

        /// <summary>
        /// Load Options from the Options File
        /// </summary>
        static private void LoadOptions()
        {
            Logger.Debug("LoadOptions");

            try
            {
                if (IsLoadOptionsThrottled())
                {
                    return;
                }

                DNSRoamingOption newOption = new DNSRoamingOption();
                newOption.Load();
                disableIPV6 = newOption.DisableIPV6;

                autoUpdate = newOption.AutoUpdate;
                hoursToUpdateRuleSet = newOption.RuleSetUpdateHours;
                daysToRetainLogs = newOption.DaysToRetainLogs;

                //DoH Options
                InsertNewDoHAddresses = newOption.InsertNewDoHAddresses;
                ForceDoHFallbackToUdp = newOption.ForceDoHFallbackToUdp;
                ForceDoHAutoUpgrade = newOption.ForceDoHAutoUpgrade;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

        }

        static bool IsLoadOptionsThrottled()
        {
            Logger.Debug("IsLoadOptionsThrottled");

            bool throttled = false;
            TimeSpan eventDelay = DateTime.Now.Subtract(lastLoadOptionsTriggered);


            if (optionJustSaved)
            {
                optionJustSaved = false;
                throttled = true;
                Logger.Debug("Option were just saved. Load Options was throttled");
            }
            else
            {
                //If an event hasn't been actioned for more than 5 seconds
                if (eventDelay.TotalSeconds > 30)
                {
                    Logger.Debug("Load Options wasn't throttled");

                    lastLoadOptionsTriggered = DateTime.Now;
                    throttled = false;
                }
                else
                {
                    throttled = true;
                    Logger.Debug("Load Options was throttled");
                }
            }

            return throttled;
        }


        /// <summary>
        /// Interates the Settings Folder and loads each Setting File
        /// </summary>
        private static void LoadDNSRules()
        {
            Logger.Debug("LoadDNSRules");

            ruleList = new List<DNSRoamingRule>();

            try
            {
                //Initialise Paths and set Permissions if neccessary
                PathsandData pathsandData = new PathsandData();
                pathsandData.CreateDataPaths(true);

                string[] settingFiles = Directory.GetFiles(pathsandData.BaseSettingsPath, "*.xml", SearchOption.TopDirectoryOnly);
                foreach (string settingFilename in settingFiles)
                {
                    //Catch an exception for a specific file but continue to process the next
                    try
                    {
                        DNSRoamingRule newRule = new DNSRoamingRule();
                        if (newRule.Load(settingFilename)) ruleList.Add(newRule);
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

            //If no rules are loaded then load the default and save the file
            if (ruleList.Count == 0)
            {
                DNSRoamingRule newRule = DNSRoamingRuleDefault.GetDefaultRule();
                newRule.Save();
                ruleList.Add(newRule);
            }
        }

        #endregion

        private void InitialiseDoH()
        {
            Logger.Debug("InitialiseDoH");

            try
            {
                //DoH Options are for Windows 11 and above
                if (IsWindows11())
                {
                    if (InsertNewDoHAddresses) NetworkingExtensions.InsertMissingDoHAddresses();

                    if (ForceDoHFallbackToUdp == 0 || ForceDoHAutoUpgrade == 0)
                    {
                        ForceDoHFallbackToUdp = 0;
                        ForceDoHAutoUpgrade = 0;
                    }
                    else
                        NetworkingExtensions.ModifyDoHAddresses((ForceDoHFallbackToUdp == 1), (ForceDoHAutoUpgrade == 1));
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }


        /// <summary>
        /// Use the Registry to Determine if Windows 11
        /// Environment, and WinAPIs are returning compatibility results which is useless
        /// </summary>
        /// <returns></returns>
        public bool IsWindows11()
        {
            var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");

            var currentBuildStr = (string)reg.GetValue("CurrentBuild");
            var currentBuild = int.Parse(currentBuildStr);

            return currentBuild >= 22000;
        }

        private void CheckRegistryForRuleSet()
        {
            Logger.Debug("CheckRegistryForRuleSet");

            try
            {
                //Do the download
                clsRuleSet ruleSet = new clsRuleSet();
                ruleSet.CheckRegistryForRuleSet();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        static private void DownloadRuleSet()
        {
            Logger.Debug("DownloadRuleSet");

            try
            {
                bool doRuleSetCheck = false;
                bool isValidURL = false;
                Uri ruleSetUri = null;

                RuleSetData ruleSetData = new RuleSetData();
                ruleSetData.Load();

                if (ruleSetData.RuleSetDownloadURL != string.Empty)
                {
                    isValidURL = Uri.TryCreate(ruleSetData.RuleSetDownloadURL, UriKind.Absolute, out ruleSetUri) && (ruleSetUri.Scheme == Uri.UriSchemeHttp || ruleSetUri.Scheme == Uri.UriSchemeHttps);
                }

                if (isValidURL)
                {
                    doRuleSetCheck = (ruleSetData.AutoUpdateLastCheck.AddHours(hoursToUpdateRuleSet) < DateTime.Now);

                    if (doRuleSetCheck)
                    {
                        //Save the last time new rules were downloaded
                        ruleSetData.AutoUpdateLastCheck = DateTime.Now;
                        ruleSetData.Save();

                        //Do the download
                        clsRuleSet ruleSet = new clsRuleSet();
                        string ruleSetFilename = ruleSet.DownloadRuleSet(ruleSetUri);
                        if (ruleSetFilename != String.Empty)
                        {
                            ruleSet.ParseRuleSet(ruleSetFilename);
                            File.Delete(ruleSetFilename);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        /// <summary>
        /// Checks each active network and compares to the rules. If matched then set the static DNS
        /// This is the Core activity for the DNS Roaming Service
        /// </summary>
        private static void ValidateAllRules()
        {
            Logger.Debug("ValidateAllRules");

            if (isServicePaused) return;
            int rulesMatched = 0;

            try
            {

                if (ruleList.Count > 0)
                {
                    Logger.Info(String.Format("Checking {0} rules", ruleList.Count));

                    //Loop through each rule
                    foreach (DNSRoamingRule thisRule in ruleList)
                    {
                        Logger.Debug(String.Format("Processing Rule [{0}]", thisRule.ID));

                        //If a Delay is set. Useful to handle when DHCP may take a while to respond
                        if (thisRule.DelaySeconds > 0)
                        {
                            Logger.Debug(String.Format("Pausing for {0} seconds", thisRule.DelaySeconds));
                            System.Threading.Thread.Sleep(thisRule.DelaySeconds * 1000);
                        }

                        //Loop through each active network 
                        IList<NetworkInterface> currentNICs = NetworkingExtensions.GetActiveNetworks();
                        foreach (NetworkInterface currentNIC in currentNICs)
                        {
                            Logger.Debug(String.Format("Processing Network [{0}]", currentNIC.Name));

                            //Setting up Variables
                            string currentIP = string.Empty;
                            string currentSubnet = string.Empty;
                            IList<string> currentDNSAddresses = new List<string>();
                            string networkName = string.Empty;
                            NetworkInterfaceType networkInterfaceType;
                            bool isIPV6Enabled = false;

                            //Get name, Type, IP and Subnet
                            NetworkingExtensions.GetNetworkAttributes(currentNIC, out currentIP, out currentSubnet, out networkName, out networkInterfaceType, out currentDNSAddresses, out isIPV6Enabled);

                            Logger.Debug(String.Format("[{0}] has Type [{1}]", currentNIC.Name, DNSRoamingNetworkInterfaces.FormatNetworkInterfaceType(networkInterfaceType.ToString())));

                            if (currentIP != string.Empty && currentSubnet != string.Empty)
                            {
                                try
                                {
                                    //Compare if the network type matches the rule
                                    bool ruleMatchedNetwork = ValidateRuleNetworkType(thisRule, networkName, networkInterfaceType.ToString());

                                    //Compare if the network address matches the rule
                                    bool ruleMatchedAddress = ValidateRuleAddress(thisRule,currentIP,currentSubnet);
                                    
                                    //Ping the Server in a rule
                                    bool ruleMatchedPING = ValidateRulePing(thisRule);
                                    
                                    //Query a DNS Server for a Domain Name in a rule
                                    bool ruleMatchedDNSQuery = ValidateRuleDNSQuery(thisRule);
                                    
                                    //If all the conditions match; then get the DNS settings and set the new vale (stastic address or Reset)
                                    if (ruleMatchedNetwork && ruleMatchedAddress && ruleMatchedPING && ruleMatchedDNSQuery)
                                    {
                                        Logger.Debug("All Rules Match");

                                        rulesMatched += 1;

                                        if (isIPV6Enabled && disableIPV6) NetworkingExtensions.DisableIPV6onNetworkInterface(networkName);

                                        if (thisRule.ResetToDHCP)
                                        {
                                            Logger.Info(String.Format("Reset DNS for [{0}] to Automatic/DHCP", networkName));
                                            NetworkingExtensions.SetDefaultDNSusingPowershell(networkName);
                                        }
                                        else
                                        {
                                            //Check if the current DNS and new DNS match
                                            string currentDNSString = NetworkingExtensions.ExpandCurrentDNS(currentDNSAddresses);
                                            string newDNSString = NetworkingExtensions.GetNewDNSString(thisRule, false);

                                            if (currentDNSString == newDNSString)
                                            {
                                                //If unchanged then don't do anything
                                                Logger.Info(String.Format("DNS already set for [{0}] to {1}", networkName, currentDNSString));
                                            }
                                            else
                                            {
                                                //else if changed set the new DNS addresses
                                                Logger.Info(String.Format("Old DNS for [{0}] was {1}", networkName, currentDNSString));
                                                Logger.Info(String.Format("Setting DNS for [{0}] to {1}", networkName, newDNSString));
                                                NetworkingExtensions.SetStaticDNSusingPowershell(networkName, thisRule);
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    //Catch the Exception for a Single NIC and rule.
                                    //So if something raises an exception the next rule will continue to be processed
                                    Logger.Error(ex.Message);
                                }
                            }
                            else
                                Logger.Warn(String.Format("No IP info for {0}", networkName));
                        }
                    }
                }

                Logger.Info(String.Format("Check complete. Actioned {0} Rules.", rulesMatched));

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private static bool ValidateRuleNetworkType(DNSRoamingRule thisRule, string networkName, string networkInterfaceType)
        {
            bool ruleMatchedNetwork = false;
            if (thisRule.UseNetworkType)
            {
                Logger.Debug("Rule uses Network Type");

                string[] networkTypes = thisRule.NetworkType.Split(',');
                foreach (string networkType in networkTypes)
                {
                    //Match the Type name if a standard .NET Interface type
                    //Or If the Customer Type matched the Type ID from the NIC
                    if ((networkInterfaceType.ToString() == networkType) || (DNSRoamingNetworkInterfaces.MatchCustomNetworkInterfaceType(networkInterfaceType.ToString(), networkType)))
                    {
                        ruleMatchedNetwork = true;
                        break;
                    }
                }
            }
            else
            {
                //Compare if the network name matches the rule
                if (thisRule.NetworkNameIs == String.Empty)
                {
                    Logger.Debug("Rule uses Network Name Is Not");
                    ruleMatchedNetwork = (networkName != thisRule.NetworkNameIsNot);
                }
                else
                {
                    Logger.Debug("Rule uses Network Name Is");
                    ruleMatchedNetwork = (networkName == thisRule.NetworkNameIs);
                }
            }

            return ruleMatchedNetwork;
        }

        private static bool ValidateRuleAddress(DNSRoamingRule thisRule, string currentIP, string currentSubnet)
        {
            bool ruleMatchedAddress = false;
            //1=LAN, 2=WAN
            if (thisRule.AddressByType == 1 || thisRule.AddressByType == 2)
            {
                Logger.Debug("Parsing current IPs");

                if (thisRule.AddressByType == 1)
                {
                    //Parse the Current IP and subnet 
                    Logger.Debug(String.Format("Current LAN IP is [{0}]", currentIP));
                }
                else
                {
                    //Get the WANIP and Parse
                    NetworkingExtensions.GetWANIPandSubnet(out currentIP, out currentSubnet);
                    Logger.Debug(String.Format("Current WAN IP is [{0}]", currentIP));
                }

                if (thisRule.AddressIsSpecific)
                    ruleMatchedAddress = NetworkingExtensions.IPIsInRange(currentIP, thisRule.AddressIP, thisRule.AddressSubnet);
                else
                    ruleMatchedAddress = !NetworkingExtensions.IPIsInRange(currentIP, thisRule.AddressIP, thisRule.AddressSubnet);

            }
            else
                ruleMatchedAddress = true;

            return ruleMatchedAddress;
        }

        private static bool ValidateRulePing(DNSRoamingRule thisRule)
        {
            bool ruleMatchedPING = false;

            if (thisRule.PingType > 0 && thisRule.PingAddress != string.Empty)
            {
                bool pingSuccess = NetworkingExtensions.PingAddress(thisRule.PingAddress);
                if (pingSuccess)
                    Logger.Info(String.Format("PING for [{0}] was successful", thisRule.PingAddress));
                else
                    Logger.Info(String.Format("PING for [{0}] failed", thisRule.PingAddress));

                //If                PING Success (Type=1)                     or PING Failed (Type=2)
                ruleMatchedPING = ((thisRule.PingType == 1 && pingSuccess) || (thisRule.PingType == 2 && !pingSuccess));
            }
            else
            {
                //Do Not PING (Type=0)
                ruleMatchedPING = true;
            }

            return ruleMatchedPING;
        }

        private static bool ValidateRuleDNSQuery(DNSRoamingRule thisRule)
        {
            bool ruleMatchedDNSQuery = false;

            if (thisRule.DNSQueryType > 0 && thisRule.DNSQueryServer != string.Empty && thisRule.DNSQueryDomainName != string.Empty)
            {
                bool dnsQuerySuccess = TestDNSQuery(thisRule.DNSQueryServer, thisRule.DNSQueryDomainName, thisRule.DNSQueryRecordType);
                if (dnsQuerySuccess)
                    Logger.Info(String.Format("Query for [{0}] was successful", thisRule.DNSQueryDomainName));
                else
                    Logger.Info(String.Format("Query for [{0}] failed", thisRule.DNSQueryDomainName));

                //If                PING Success (Type=1)                             or PING Failed (Type=2)
                ruleMatchedDNSQuery = ((thisRule.DNSQueryType == 1 && dnsQuerySuccess) || (thisRule.DNSQueryType == 2 && !dnsQuerySuccess));
            }
            else
            {
                //Do Not Query (Type=0)
                ruleMatchedDNSQuery = true;
            }

            return ruleMatchedDNSQuery;
        }

        /// <summary>
        /// Tests resolving a DNS Query against a specific server
        /// Returns true if successful.
        /// Returns false if the query gets an error response or an exception is thrown 
        /// </summary>
        /// <param name="dnsServer"></param>
        /// <param name="domainName"></param>
        /// <returns></returns>
        public static bool TestDNSQuery(string dnsServer, string domainName, string queryType)
        {
            try
            {
                QueryType dnsQueryType;
                switch (queryType.ToLower())
                {
                    case "a":
                        dnsQueryType = QueryType.A;
                        break;
                    case "txt":
                        dnsQueryType = QueryType.TXT;
                        break;
                    default:
                        dnsQueryType = QueryType.CNAME;
                        break;
                }

                var endpoint = new IPEndPoint(IPAddress.Parse(dnsServer), 53);
                var client = new LookupClient(endpoint);
                var queryOptions = new DnsQueryOptions();
                queryOptions.UseCache = false;

                var dnsQuestion = new DnsQuestion(domainName, dnsQueryType);

                var result = client.Query(dnsQuestion);

                return !result.HasError;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return false;
            }
        }

    }
}
