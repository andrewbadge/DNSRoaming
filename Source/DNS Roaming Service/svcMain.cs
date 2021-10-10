using DNS_Roaming_Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

        //Options
        static bool disableIPV6 = true;
        static int daysToRetainLogs = 14;

        static System.Timers.Timer serviceTimer;
        static System.Timers.Timer logTimer;
        static System.ComponentModel.BackgroundWorker backgroundWorker;

        public svcMain()
        {
            Logger.Debug("svcMain Initialize");

            try
            {
                InitializeComponent();
                InitializeBackgroundWorker();
                LoadOptions();
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

            Logger.Info(String.Format("Settings file change detected"));
            LoadDNSRules();
            FireEvent();
        }

        private static void SettingsFileCreated(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Created)
            {
                return;
            }

            Logger.Info(String.Format("Settings file create detected"));
            LoadDNSRules();
            FireEvent();
        }

        private static void SettingsFileDeleted(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Deleted)
            {
                return;
            }

            Logger.Info(String.Format("Settings file delete detected"));
            LoadDNSRules();
            FireEvent();
        }

        private static void SettingsFileRenamed(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Renamed)
            {
                return;
            }

            Logger.Info(String.Format("Settings file rename detected"));
            LoadDNSRules();
            FireEvent();
        }

        private static void OptionsFileChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }

            Logger.Info(String.Format("Options file change detected"));
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

            Logger.Info(String.Format("Options file delete detected"));
            LoadOptions();
        }

        private static void OptionsFileRenamed(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Renamed)
            {
                return;
            }

            Logger.Info(String.Format("Options file rename detected"));
            LoadOptions();
        }

        static void AddressChangedCallback(object sender, EventArgs e)
        {
            Logger.Info(String.Format("Address change detected"));
            FireEvent();
        }

        static void AvailabilityChangedCallback(object sender, EventArgs e)
        {
            Logger.Info(String.Format("Availability change detected"));
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
            CompareNetworkToRules();
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

            //In 30 mins
            serviceTimer = new System.Timers.Timer(1800000);
            serviceTimer.Elapsed += ServiceTimerEvent;
            serviceTimer.AutoReset = true;
            serviceTimer.Enabled = true;

            //In nearly 6 hours. Deliberately offset from an exact hour
            logTimer = new System.Timers.Timer(21000000);
            logTimer.Elapsed += LogTimerEvent;
            logTimer.AutoReset = true;
            logTimer.Enabled = true;

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
        /// Periodically check for old log files and remove them
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        static void LogTimerEvent(Object source, ElapsedEventArgs e)
        {
            Logger.Debug("LogTimerEvent");

            try
            {
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
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
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
                //If an event hasn't been actioned for more than 120 seconds
                //or we already skipped 5 events
                if (eventDelay.TotalSeconds > 120 || countEventSkipped > 5)
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
                DNSRoamingOption newOption = new DNSRoamingOption();
                newOption.Load();
                disableIPV6 = newOption.DisableIPV6;

                daysToRetainLogs = newOption.DaysToRetainLogs;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

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
                        newRule.Load(settingFilename);
                        ruleList.Add(newRule);
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

        /// <summary>
        /// Checks each active network and compares to the rules. If matched then set the static DNS
        /// This is the Core activity for the DNS Roaming Service
        /// </summary>
        private static void CompareNetworkToRules()
        {
            Logger.Debug("CompareNetworkToRules");

            if (isServicePaused) return;
            int rulesMatched = 0;

            try
            {

                if (ruleList.Count > 0)
                {
                    Logger.Info(String.Format("Checking {0} rules", ruleList.Count));

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
                            //Loop through each rule
                            foreach (DNSRoamingRule thisRule in ruleList)
                            {
                                Logger.Debug(String.Format("Processing Rule [{0}]", thisRule.ID));

                                //Compare if the network type matches the rule
                                bool ruleMatchedNetwork = false;
                                if (thisRule.UseNetworkType)
                                {
                                    Logger.Debug("Rule uses Network Type");

                                    string[] networkTypes = thisRule.NetworkType.Split(',');
                                    foreach (string networkType in networkTypes)
                                    {
                                        //Match the Type name if a standard .NET Interface type
                                        //Or If the Customer Type matched the Type ID from the NIC
                                        if ((networkInterfaceType.ToString() == networkType) || (DNSRoamingNetworkInterfaces.MatchCustomNetworkInterfaceType(networkInterfaceType.ToString(),networkType)))
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

                                //Compare if the network address matches the rule
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

                                //If all the conditions match; then get the DNS settings and set a static address
                                if (ruleMatchedNetwork && ruleMatchedAddress)
                                {
                                    Logger.Debug("All Rules Match");

                                    rulesMatched += 1;

                                    if (thisRule.DelaySeconds > 0)
                                    {
                                        Logger.Debug(String.Format("Pausing for {0} seconds", thisRule.DelaySeconds));
                                        System.Threading.Thread.Sleep(thisRule.DelaySeconds * 1000);
                                    }

                                    if (isIPV6Enabled && disableIPV6) NetworkingExtensions.DisableIPV6onNetworkInterface(networkName);

                                    //Check if the current DNS and new DNS match
                                    string currentDNSString = NetworkingExtensions.ExpandCurrentDNS(currentDNSAddresses);
                                    string newDNSString = NetworkingExtensions.GetNewDNSString(thisRule,false);

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
                        else
                            Logger.Warn(String.Format("No IP info for {0}", networkName));
                    }
                }

                Logger.Info(String.Format("Check complete. Actioned {0} Rules.", rulesMatched));

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        
    }
}
