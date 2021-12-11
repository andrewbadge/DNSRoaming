using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace DNS_Roaming_Common
{
    public static class NetworkingExtensions
    {
        public static IPAddress GetBroadcastAddress(this IPAddress address, IPAddress subnetMask)
        {
            byte[] ipAdressBytes = address.GetAddressBytes();
            byte[] subnetMaskBytes = subnetMask.GetAddressBytes();

            if (ipAdressBytes.Length != subnetMaskBytes.Length)
                throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

            byte[] broadcastAddress = new byte[ipAdressBytes.Length];
            for (int i = 0; i < broadcastAddress.Length; i++)
            {
                broadcastAddress[i] = (byte)(ipAdressBytes[i] | (subnetMaskBytes[i] ^ 255));
            }
            return new IPAddress(broadcastAddress);
        }

        public static IPAddress GetNetworkAddress(this IPAddress address, IPAddress subnetMask)
        {
            byte[] ipAdressBytes = address.GetAddressBytes();
            byte[] subnetMaskBytes = subnetMask.GetAddressBytes();

            if (ipAdressBytes.Length != subnetMaskBytes.Length)
                throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

            byte[] broadcastAddress = new byte[ipAdressBytes.Length];
            for (int i = 0; i < broadcastAddress.Length; i++)
            {
                broadcastAddress[i] = (byte)(ipAdressBytes[i] & (subnetMaskBytes[i]));
            }
            return new IPAddress(broadcastAddress);
        }

        public static void GetLANIPandSubnet(out String currentIP, out String currentSubnet)
        {
            string returnIP = string.Empty;
            string returnSubnet = string.Empty;

            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface n in adapters)
            {
                if (n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                    if (n.OperationalStatus == OperationalStatus.Up)
                    {
                        if (n.Supports(NetworkInterfaceComponent.IPv4))
                        {
                            foreach (UnicastIPAddressInformation ip in n.GetIPProperties().UnicastAddresses)
                            {
                                if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                {
                                    returnIP = ip.Address.ToString();
                                    returnSubnet = ip.IPv4Mask.ToString();
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            currentIP = returnIP;
            currentSubnet = returnSubnet;

        }

        /// <summary>
        /// Gets the Current WAN IP from DYNDNS.ORG
        /// </summary>
        /// <param name="currentIP"></param>
        /// <param name="currentSubnet"></param>
        public static void GetWANIPandSubnet(out String currentIP, out String currentSubnet)
        {
            string returnIP = string.Empty;
            string returnSubnet = "255.255.255.255";

            bool queryDoAttempt = true;
            int queryAttempts = 0;
            int queryAttemptsMax = 2;

            while (queryDoAttempt)
            {
                try
                {
                    //Handle if the external query times out
                    queryAttempts += 1;

                    string url = "http://checkip.dyndns.org";
                    System.Net.WebRequest req = System.Net.WebRequest.Create(url);
                    System.Net.WebResponse resp = req.GetResponse();
                    System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                    string response = sr.ReadToEnd().Trim();
                    string[] a = response.Split(':');
                    string a2 = a[1].Substring(1);
                    string[] a3 = a2.Split('<');
                    returnIP = a3[0];

                    queryDoAttempt = false;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message);

                    if (queryAttempts >= queryAttemptsMax)
                        queryDoAttempt = false;
                    else
                        System.Threading.Thread.Sleep(2000);
                }
            }

            currentIP = returnIP;
            currentSubnet = returnSubnet;

        }

        /// <summary>
        /// Gets the DNS Addresses foir the states set name
        /// </summary>
        /// <param name="dnsSet"></param>
        /// <param name="ipPreferred"></param>
        /// <param name="ipAlternative"></param>
        public static void GetDNSSetIPAddress(string dnsSet, out string ipPreferred, out string ipAlternative)
        {
            switch (dnsSet)
            {
                case "Quad9":
                    ipPreferred = "9.9.9.9";
                    ipAlternative = "149.112.112.112";
                    break;
                case "Cloudflare":
                    ipPreferred = "1.1.1.1";
                    ipAlternative = "1.0.0.1";
                    break;
                case "Cloudflare - No Malware":
                    ipPreferred = "1.1.1.2";
                    ipAlternative = "1.0.0.2";
                    break;
                case "Cloudflare - No Malware or Adult":
                    ipPreferred = "1.1.1.3";
                    ipAlternative = "1.0.0.3";
                    break;
                case "Google":
                    ipPreferred = "8.8.8.8";
                    ipAlternative = "8.8.4.4";
                    break;
                case "AdGuard":
                    ipPreferred = "94.140.14.14";
                    ipAlternative = "94.140.15.15";
                    break;
                case "Alternate DNS":
                    ipPreferred = "76.76.19.19";
                    ipAlternative = "76.223.122.150";
                    break;
                case "CleanBrowsing - Adult":
                    ipPreferred = "185.228.168.10";
                    ipAlternative = "185.228.169.11";
                    break;
                case "CleanBrowsing - Family":
                    ipPreferred = "185.228.168.168";
                    ipAlternative = "185.228.169.168";
                    break;
                case "CleanBrowsing - Security":
                    ipPreferred = "185.228.168.9";
                    ipAlternative = "185.228.169.9";
                    break;
                case "FourthEstate":
                    ipPreferred = "45.77.165.194";
                    ipAlternative = "45.32.36.36";
                    break;

                default: //"Quad9 + CloudFlare - No Malware"
                    ipPreferred = "9.9.9.9";
                    ipAlternative = "1.1.1.2";
                    break;
            }

        }

        public static NetworkInterface GetActiveNetworkInterface()
        {
            var Nic = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(
                a => a.OperationalStatus == OperationalStatus.Up &&
                //(a.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || a.NetworkInterfaceType == NetworkInterfaceType.Ethernet) &&
                a.GetIPProperties().GatewayAddresses.Any(g => g.Address.AddressFamily.ToString() == "InterNetwork"));

            return Nic;
        }

        /// <summary>
        /// Uses Powershell to Set the DNS Addresses for a NIC
        /// </summary>
        /// <param name="networkName"></param>
        /// <param name="rule"></param>
        public static void SetStaticDNSusingPowershell(string networkName, DNSRoamingRule thisRule)
        {
            try
            {
                //Build the DNS address list
                string dnsString = NetworkingExtensions.GetNewDNSString(thisRule, true);

                //Build the Powershell Command
                string argument = string.Format(@"-NoProfile -ExecutionPolicy unrestricted & {{Set-DnsClientServerAddress -InterfaceAlias ('{0}') -ServerAddresses ({1}) }}", networkName, dnsString);

                //Execute the Powershell command
                var startInfo = new ProcessStartInfo()
                {
                    FileName = "powershell.exe",
                    Arguments = argument,
                    UseShellExecute = false

                };
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

        }

        /// <summary>
        /// Uses Powershell to Reset (to default) the DNS Addresses for a NIC
        /// </summary>
        /// <param name="networkName"></param>
        public static void SetDefaultDNSusingPowershell(string networkName)
        {
            try
            {
                //Build the Powershell Command
                string argument = string.Format(@"-NoProfile -ExecutionPolicy unrestricted & {{Set-DnsClientServerAddress -InterfaceAlias ('{0}') -ResetServerAddresses }}", networkName);

                //Execute the Powershell command
                var startInfo = new ProcessStartInfo()
                {
                    FileName = "powershell.exe",
                    Arguments = argument,
                    UseShellExecute = false

                };
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

        }

        /// <summary>
        /// Uses Powershell to Disbale IPV6 on the NIC
        /// </summary>
        /// <param name="networkName"></param>
        public static void DisableIPV6onNetworkInterface(string networkName)
        {
            Logger.Debug("DisableIPV6onNetworkInterface");

            try
            {
                //Build the Powershell Command
                string argument = string.Format(@"-NoProfile -ExecutionPolicy unrestricted & {{disable-NetAdapterBinding -Name '{0}' -ComponentID ms_tcpip6 }}", networkName);

                //Execute the Powershell command
                var startInfo = new ProcessStartInfo()
                {
                    FileName = "powershell.exe",
                    Arguments = argument,
                    UseShellExecute = false

                };
                Process.Start(startInfo);

                Logger.Info(String.Format("IPV6 Disabled for {0}", networkName));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        /// <summary>
        /// Return a list Network Interfaces that are active with IPV4
        /// </summary>
        /// <returns></returns>
        public static IList<NetworkInterface> GetActiveNetworks()
        {
            IList<NetworkInterface> currentNICs = new List<NetworkInterface>();

            //Build a list of active Networks
            try
            {
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
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return currentNICs;

        }

        /// <summary>
        /// For a Network Interface; return attributes of interest
        /// </summary>
        /// <param name="currentNIC"></param>
        /// <param name="currentIP"></param>
        /// <param name="currentSubnet"></param>
        /// <param name="networkName"></param>
        /// <param name="networkInterfaceType"></param>
        /// <param name="dnsAddress1"></param>
        /// <param name="dnsAddress2"></param>
        public static void GetNetworkAttributes(NetworkInterface currentNIC, out string currentIP, out string currentSubnet, out string networkName, out NetworkInterfaceType networkInterfaceType, out IList<string> currentDNSAddresses, out bool isIPV6Enabled)
        {
            //Get name, Type, IP and Subnet
            networkName = currentNIC.Name;
            networkInterfaceType = currentNIC.NetworkInterfaceType;
            currentIP = string.Empty;
            currentSubnet = string.Empty;
            currentDNSAddresses = new List<string>();

            isIPV6Enabled = currentNIC.Supports(NetworkInterfaceComponent.IPv6);

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

                IPInterfaceProperties ipProperties = currentNIC.GetIPProperties();
                IPAddressCollection dnsAddresses = ipProperties.DnsAddresses;

                foreach (IPAddress dnsAddress in dnsAddresses)
                {
                    currentDNSAddresses.Add(dnsAddress.ToString());
                }

            }
        }

        public static bool IPIsInRange(string currentIP, string ruleIP, string ruleSubnet)
        {
            bool isInRange = false;

            try
            {
                IPAddress currentIPaddress = IPAddress.Parse(currentIP);
                var ruleIPRange = NetTools.IPAddressRange.Parse(String.Format("{0}/{1}", ruleIP, ruleSubnet));

                isInRange = ruleIPRange.Contains(currentIPaddress);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return isInRange;
        }

        /// <summary>
        /// Convert the list of IP Address to a single string delimited with commas
        /// </summary>
        /// <param name="currentDNSAddresses"></param>
        /// <returns></returns>
        public static string ExpandCurrentDNS(IList<string> currentDNSAddresses)
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

        /// <summary>
        /// Takes a list of IPs and creates a delimited string (ignoring blanks)
        /// </summary>
        /// <param name="ip1"></param>
        /// <param name="ip2"></param>
        /// <param name="ip3"></param>
        /// <param name="ip4"></param>
        /// <param name="wrapInQuotes"></param>
        /// <returns></returns>
        public static string ExpandIPString(string ip1, string ip2, string ip3, string ip4, bool wrapInQuotes=false)
        {
            string expandedIPString = string.Empty;

            IList<string> dNSAddressesList = new List<string>();
            if (ip1 != String.Empty) dNSAddressesList.Add(ip1);
            if (ip2 != String.Empty) dNSAddressesList.Add(ip2);
            if (ip3 != String.Empty) dNSAddressesList.Add(ip3);
            if (ip4 != String.Empty) dNSAddressesList.Add(ip4);

            foreach (string dNSAddress in dNSAddressesList)
            {
                if (expandedIPString == String.Empty)
                {
                    if (wrapInQuotes) expandedIPString = String.Format(@"'{0}'", dNSAddress); else expandedIPString = dNSAddress;
                }
                else
                {
                    if (wrapInQuotes) expandedIPString += "," + String.Format(@"'{0}'", dNSAddress); else expandedIPString += "," + dNSAddress; 
                }
            }

            return expandedIPString;
        }

        /// <summary>
        /// Based on the Rule returns either the static DNS IPs or IPs based on the predefined Set
        /// </summary>
        /// <param name="thisRule"></param>
        /// <param name="wrapInQuotes"></param>
        /// <returns></returns>
        public static string GetNewDNSString(DNSRoamingRule thisRule, bool wrapInQuotes = false)
        {
            string newDNSString = string.Empty;

            string dns1 = string.Empty;
            string dns2 = string.Empty;
            string dns3 = string.Empty;
            string dns4 = string.Empty;

            if (thisRule.DNSSet == String.Empty)
            {
                //Use manual DNS addresses
                Logger.Debug("Setting specific DNS addresses");
                dns1 = thisRule.DNSPreferred;
                dns2 = thisRule.DNSAlternative;
                dns3 = thisRule.DNS2ndAlternative;
                dns4 = thisRule.DNS3rdAlternative;
            }
            else
            {
                //Use one of the predefined sets of DNS addresses
                Logger.Debug(String.Format("Setting a DNS Set [{0}]", thisRule.DNSSet));
                NetworkingExtensions.GetDNSSetIPAddress(thisRule.DNSSet, out dns1, out dns2);
            }

            newDNSString = NetworkingExtensions.ExpandIPString(dns1, dns2, dns3, dns4, wrapInQuotes);

            return newDNSString;
        }
    }
}
