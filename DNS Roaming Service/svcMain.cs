using DNS_Roaming_Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.ServiceProcess;
using System.Timers;
using DNS_Roaming_Common;

namespace DNS_Roaming_Service
{
    public partial class svcMain : ServiceBase
    {
        static IList<DNSRoamingRule> ruleList = new List<DNSRoamingRule>();
        static FileSystemWatcher watcher;
        static bool isServicePaused = false;
        private static System.Timers.Timer serviceTimer;

        public svcMain()
        {
            Logger.Debug("svcMain Initialize");

            try
            {
                InitializeComponent();
                LoadDNSRules();
                registerEvents();
                ConfigureServiceTimer();
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
            isServicePaused = true;
        }

        protected override void OnStop()
        {
            Logger.Info("Service Stopping");
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
            watcher = new FileSystemWatcher(pathsandData.BaseSettingsPath);

            watcher.NotifyFilter = NotifyFilters.Attributes
                                | NotifyFilters.CreationTime
                                | NotifyFilters.DirectoryName
                                | NotifyFilters.FileName
                                | NotifyFilters.LastAccess
                                | NotifyFilters.LastWrite
                                | NotifyFilters.Security
                                | NotifyFilters.Size;

            watcher.Changed += SettingsFileChanged;
            watcher.Created += SettingsFileCreated;
            watcher.Deleted += SettingsFileDeleted;
            watcher.Renamed += SettingsFileRenamed;

            watcher.Filter = "*.xml";
            watcher.IncludeSubdirectories = false;
            watcher.EnableRaisingEvents = true;

        }

        private static void SettingsFileChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }
            Logger.Info(String.Format("Settings file change detected"));
            LoadDNSRules();
            CompareNetworkToRules();
        }

        private static void SettingsFileCreated(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Created)
            {
                return;
            }
            Logger.Info(String.Format("Settings file change detected"));
            LoadDNSRules();
            CompareNetworkToRules();
        }

        private static void SettingsFileDeleted(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Deleted)
            {
                return;
            }
            Logger.Info(String.Format("Settings file change detected"));
            LoadDNSRules();
            CompareNetworkToRules();
        }

        private static void SettingsFileRenamed(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Renamed)
            {
                return;
            }
            Logger.Info(String.Format("Settings file change detected"));
            LoadDNSRules();
            CompareNetworkToRules();
        }

        static void AddressChangedCallback(object sender, EventArgs e)
        {
            Logger.Info(String.Format("Address change detected"));
            CompareNetworkToRules();
        }

        static void AvailabilityChangedCallback(object sender, EventArgs e)
        {
            Logger.Info(String.Format("Availability change detected"));
            CompareNetworkToRules();
        }

        static void ServiceTimerEvent(Object source, ElapsedEventArgs e)
        {
            Logger.Debug("ServiceTimerEvent");

            //Reschedule the next Timer to a random internal 
            //between 5 and 60 mins
            Random randomNumber = new Random();
            int timerDelay = randomNumber.Next(600, 3600) * 1000;
            serviceTimer.Interval = timerDelay;

            Logger.Info(String.Format("Periodically checking networks"));
            CompareNetworkToRules();
        }

        #endregion

        /// <summary>
        /// Interates the Settings Folder and loads each Setting File
        /// </summary>
        private static void LoadDNSRules()
        {
            Logger.Debug("LoadDNSRules");

            ruleList = new List<DNSRoamingRule>();

            try
            {
                PathsandData pathsandData = new PathsandData();
                string[] settingFiles = Directory.GetFiles(pathsandData.BaseSettingsPath);
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
        /// <summary>
        /// Sets up the Timer which scans the network and rules periodically
        /// </summary>
        private static void ConfigureServiceTimer()
        {
            Logger.Debug("ConfigureServiceTimer");

            serviceTimer = new System.Timers.Timer(10000);
            serviceTimer.Elapsed += ServiceTimerEvent;
            serviceTimer.AutoReset = true;
            serviceTimer.Enabled = true;
        }

        /// <summary>
        /// Checks each active network and compares to the rules. If matched then set the static DNS
        /// </summary>
        private static void CompareNetworkToRules()
        {
            Logger.Debug("CompareNetworkToRules");

            if (isServicePaused) return;

            //Wait for 10 seconds for Network or DHCP to settle
            System.Threading.Thread.Sleep(5000);

            try { 

                if (ruleList.Count > 0)
                {
                    Logger.Info(String.Format("Checking {0} rules", ruleList.Count));

                    IList<NetworkInterface> currentNICs = NetworkingExtensions.GetActiveNetworks();

                    //Loop through each active network 
                    foreach (NetworkInterface currentNIC in currentNICs)
                    {
                        Logger.Debug(String.Format("Processing Network [{0}]",currentNIC.Name));

                        //Setting up Variables
                        string currentIP = string.Empty;
                        string currentSubnet = string.Empty;
                        IList<string> currentDNSAddresses = new List<string>();
                        string networkName = string.Empty;
                        NetworkInterfaceType networkInterfaceType;

                        //Get name, Type, IP and Subnet
                        NetworkingExtensions.GetNetworkAttributes(currentNIC, out currentIP, out currentSubnet, out networkName, out networkInterfaceType, out currentDNSAddresses);

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
                                        if (networkInterfaceType.ToString() == networkType)
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
                                if (thisRule.AddressIsSpecific || thisRule.AddressIsNotSpecific)
                                {
                                    Logger.Debug("Parsing current IPs");

                                    //Get the Current IP abd subnet and the Rules IP and Subnet
                                    IPAddress ip1 = IPAddress.Parse(currentIP);
                                    IPAddress subnet1 = IPAddress.Parse(currentSubnet);

                                    IPAddress ip2 = IPAddress.Parse(thisRule.AddressIP);
                                    IPAddress subnet2 = IPAddress.Parse(thisRule.AddressSubnet);

                                    //Get the NEtwork address for both and compare.
                                    Logger.Debug("Getting Network Addreses");
                                    IPAddress network1 = ip1.GetNetworkAddress(subnet1);
                                    IPAddress network2 = ip2.GetNetworkAddress(subnet2);

                                    if (thisRule.AddressIsSpecific)
                                        ruleMatchedAddress = network1.Equals(network2);
                                    else
                                        ruleMatchedAddress = !network1.Equals(network2);

                                }
                                else
                                    ruleMatchedAddress = true;

                                //If all the conditions match; then get the DNS settings and set a static address
                                if (ruleMatchedNetwork && ruleMatchedAddress)
                                {
                                    Logger.Debug("All Rules Match");

                                    string dns1 = string.Empty;
                                    string dns2 = string.Empty;

                                    if (thisRule.DNSSet == String.Empty)
                                    {
                                        //Use manual DNS addresses
                                        Logger.Debug("Setting specific DNS addresses");
                                        dns1 = thisRule.DNSPreferred;
                                        dns2 = thisRule.DNSAlternative;
                                    }
                                    else
                                    {
                                        //Use one of the predefined sets of DNS addresses
                                        Logger.Debug(String.Format("Setting a DNS Set [{0}]", thisRule.DNSSet));
                                        NetworkingExtensions.GetDNSSetIPAddress(thisRule.DNSSet, out dns1, out dns2);
                                    }

                                    //Check if the current DNS and new DNS match
                                    if (CurrentDNSMatchNewDNS(currentDNSAddresses, dns1 ,dns2))
                                    {
                                        //If so then don't do anything
                                        Logger.Info(String.Format("DNS already set for [{0}] to {1}", networkName, ExpandCurrentDNS(currentDNSAddresses)));
                                    }
                                    else
                                    {
                                        //else set the new DNS addresses
                                        Logger.Info(String.Format("Old DNS for [{0}] was {1}", networkName, ExpandCurrentDNS(currentDNSAddresses)));
                                        Logger.Info(String.Format("Setting DNS for [{0}] to {1},{2}", networkName, dns1, dns2));
                                        NetworkingExtensions.SetStaticDNSusingPowershell(networkName, dns1, dns2);
                                    }


                                }
                            }
                        }
                        else
                            Logger.Warn(String.Format("No IP info for {0}", networkName));
                    }
                }

                Logger.Info(String.Format("Rule check complete"));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private static bool CurrentDNSMatchNewDNS(IList<string> currentDNSAddresses, string newDns1, string newDns2)
        {
            //Only compare the Preferred and Alernate DNS as they're the only ones we're setting
            string currentDns1 = string.Empty;
            string currentDns2 = string.Empty;
            foreach (String dnsAddress in currentDNSAddresses)
            {
                if (dnsAddress.ToString() != String.Empty)
                {
                    if (currentDns1 == string.Empty)
                        currentDns1 = dnsAddress.ToString();
                    else
                    {
                        currentDns2 = dnsAddress.ToString();
                        break;
                    }
                }
            }

            return (currentDns1 == newDns1 && currentDns2 == newDns2);
        }

        private static string ExpandCurrentDNS(IList<string> currentDNSAddresses)
        {
            string dnsString = string.Empty;

            foreach (String dnsAddress in currentDNSAddresses)
            {
                if (dnsAddress.ToString() != String.Empty)
                {
                    if (dnsString == string.Empty)
                        dnsString += String.Format("{0}", dnsAddress.ToString());
                    else
                        dnsString += String.Format(",{0}", dnsAddress.ToString());
                }
            }

            return dnsString;

        }

    }
}
