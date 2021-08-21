using DNS_Roaming_Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.ServiceProcess;
using DNS_Roaming_Common;

namespace DNS_Roaming_Service
{
    public partial class svcMain : ServiceBase
    {
        static IList<DNSRoamingRule> ruleList = new List<DNSRoamingRule>();

        public svcMain()
        {
            try
            {
                
                InitializeComponent();

                PathsandData pathsandData = new PathsandData();
                registerEvents();

                LoadDNSRules();
                CheckCurrentNetworktoRules();
            }
            catch (Exception exception)
            {
                Logger.Error(exception.Message);
            }
        }

        protected override void OnStart(string[] args)
        {
            Logger.Info("Service Starting");
        }

        protected override void OnStop()
        {
            Logger.Info("Service Stopping");
        }

        private void registerEvents()
        {
            NetworkChange.NetworkAddressChanged += new NetworkAddressChangedEventHandler(AddressChangedCallback);
            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(AvailabilityChangedCallback);
        }

        static void AddressChangedCallback(object sender, EventArgs e)
        {
            Logger.Info(String.Format("Address change detected"));
            CheckCurrentNetworktoRules();
        }

        static void AvailabilityChangedCallback(object sender, EventArgs e)
        {
            Logger.Info(String.Format("Availability change detected"));
            CheckCurrentNetworktoRules();
        }

        private void LoadDNSRules()
        {
            PathsandData pathsandData = new PathsandData();
            string[] settingFiles = Directory.GetFiles(pathsandData.BaseSettingsPath);
            foreach (string settingFilename in settingFiles)
            {
                DNSRoamingRule newRule = new DNSRoamingRule();
                newRule.Load(settingFilename);
                ruleList.Add(newRule);
            }
        }

        private static void CheckCurrentNetworktoRules()
        {
            //Wait for 10 seconds for Network or DHCP to settle
            System.Threading.Thread.Sleep(5000);

            if (ruleList.Count > 0)
            {
                Logger.Info(String.Format("Checking {0} rules", ruleList.Count));

                string currentIP = string.Empty;
                string currentSubnet = string.Empty;
                string networkName = string.Empty;
                IList<NetworkInterface> currentNICs = new List<NetworkInterface>();
                NetworkInterfaceType networkInterfaceType;


                NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface n in adapters)
                {
                    if (n.OperationalStatus == OperationalStatus.Up)
                    {
                        if (n.Supports(NetworkInterfaceComponent.IPv4))
                        {
                            currentNICs.Add(n);
                        }
                    }
                }

                Logger.Info(String.Format("{0} active Networks", currentNICs.Count));

                foreach (NetworkInterface currentNIC in currentNICs)
                {

                    networkName = currentNIC.Name;
                    networkInterfaceType = currentNIC.NetworkInterfaceType;
                    if (currentNIC.Supports(NetworkInterfaceComponent.IPv4))
                    {
                        foreach (UnicastIPAddressInformation ip in currentNIC.GetIPProperties().UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                currentIP = ip.Address.ToString();
                                currentSubnet = ip.IPv4Mask.ToString();
                                break;
                            }
                        }
                    }

                    foreach (DNSRoamingRule thisRule in ruleList)
                    {
                        bool ruleMatchedNetwork = false;
                        if (thisRule.UseNetworkType)
                        {
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
                            ruleMatchedNetwork = (networkName == thisRule.NetworkName);
                        }

                        bool ruleMatchedAddress = false;
                        if (thisRule.AddressSpecific)
                        {

                            IPAddress ip1 = IPAddress.Parse(currentIP);
                            IPAddress subnet1 = IPAddress.Parse(currentSubnet);

                            IPAddress ip2 = IPAddress.Parse(thisRule.AddressIP);
                            IPAddress subnet2 = IPAddress.Parse(thisRule.AddressSubnet);

                            IPAddress network1 = ip1.GetNetworkAddress(subnet1);
                            IPAddress network2 = ip2.GetNetworkAddress(subnet2);

                            ruleMatchedAddress = network1.Equals(network2);

                        }
                        else
                            ruleMatchedAddress = true;

                        if (ruleMatchedNetwork && ruleMatchedAddress)
                        {
                            string dns1 = string.Empty;
                            string dns2 = string.Empty;
                            string dnsString = string.Empty;

                            if (thisRule.DNSSet == String.Empty)
                            {
                                dns1 = thisRule.DNSPreferred;
                                dns2 = thisRule.DNSAlternative;

                                if (dns1 == String.Empty) dnsString = dns2;
                                if (dns2 == String.Empty) dnsString = dns1;
                                if (dnsString == String.Empty) dnsString = String.Format("{0},{1}", dns1, dns2);
                            }
                            else
                            {
                                NetworkingExtensions.GetDNSSetIPAddress(thisRule.DNSSet, out dns1, out dns2);
                                dnsString = String.Format("{0},{1}", dns1, dns2);
                            }

                            Logger.Info(String.Format("Setting DNS for {0} to {1}",networkName, dnsString));
                            NetworkingExtensions.SetDNSforActiveNetwork(currentNIC,dnsString);

                        }
                    }
                }
            }

            Logger.Info(String.Format("Rule check complete"));

        }

    }
}
