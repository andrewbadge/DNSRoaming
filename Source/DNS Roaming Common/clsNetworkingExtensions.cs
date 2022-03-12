using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web.Script.Serialization;

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
        /// Gets the Current WAN IP from CHECKIP.DYNDNS.ORG, IFCONFIG.IO or IPINFO.ORG
        /// </summary>
        /// <param name="currentIP"></param>
        /// <param name="currentSubnet"></param>
        public static void GetWANIPandSubnet(out String currentIP, out String currentSubnet)
        {
            string returnIP = string.Empty;
            string returnSubnet = "255.255.255.255";

            bool queryDoAttempt = true;
            int queryAttempts = 0;

            while (queryDoAttempt)
            {
                //Handle if the external query times out
                queryAttempts += 1;

                if (queryAttempts == 1)
                {
                    returnIP = string.Empty;
                    queryDoAttempt = true;
                    try
                    {
                        string url = "http://ifconfig.io/ip";
                        System.Net.WebRequest req = System.Net.WebRequest.Create(url);
                        System.Net.WebResponse resp = req.GetResponse();
                        System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                        string response = sr.ReadToEnd().Trim();

                        if (IsValidIPAddress(response))
                        {
                            returnIP = response;
                            queryDoAttempt = false;
                        }
                        else
                        {
                            returnIP = string.Empty;
                            queryDoAttempt = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Debug(ex.Message);
                        returnIP = string.Empty;
                        queryDoAttempt = true;
                    }
                }

                if (queryAttempts == 2)
                {
                    returnIP = string.Empty;
                    queryDoAttempt = true;
                    try
                    {
                        string url = "http://ipinfo.io/ip";
                        System.Net.WebRequest req = System.Net.WebRequest.Create(url);
                        System.Net.WebResponse resp = req.GetResponse();
                        System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                        string response = sr.ReadToEnd().Trim();
                        returnIP = response;

                        if (IsValidIPAddress(response))
                        {
                            returnIP = response;
                            queryDoAttempt = false;
                        }
                        else
                        {
                            returnIP = string.Empty;
                            queryDoAttempt = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Debug(ex.Message);
                        returnIP = string.Empty;
                        queryDoAttempt = true;
                    }
                }

                if (queryAttempts == 3)
                {
                    returnIP = string.Empty;
                    queryDoAttempt = true;
                    try
                    {
                        string url = "http://checkip.dyndns.org";
                        System.Net.WebRequest req = System.Net.WebRequest.Create(url);
                        System.Net.WebResponse resp = req.GetResponse();
                        System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                        string response = sr.ReadToEnd().Trim();
                        string[] a = response.Split(':');
                        string a2 = a[1].Substring(1);
                        string[] a3 = a2.Split('<');

                        if (IsValidIPAddress(a3[0]))
                        {
                            returnIP = a3[0];
                            queryDoAttempt = false;
                        }
                        else
                        {
                            returnIP = string.Empty;
                            queryDoAttempt = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Debug(ex.Message);
                        returnIP = string.Empty;
                        queryDoAttempt = true;
                    }
                }

                if (queryAttempts > 3)
                {
                    throw new Exception("Too many attempts to get WAN IP");
                }

                //If a failed attempt then wait a few seconds incase the network if still initialising
                if (queryDoAttempt) System.Threading.Thread.Sleep(5000);

            }

            currentIP = returnIP;
            currentSubnet = returnSubnet;

        }

        static public bool IsValidURL(string URL)
        {
            bool isValidURL = false;
            Uri returnURI = null;

            try
            {
                if (URL != string.Empty)
                {
                    isValidURL = Uri.TryCreate(URL, UriKind.Absolute, out returnURI) && (returnURI.Scheme == Uri.UriSchemeHttp || returnURI.Scheme == Uri.UriSchemeHttps);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return isValidURL;
        }

        static public bool IsValidIPAddress(string ipAddress)
        {
            bool isValidIP = false;
            IPAddress returnIPAddress;

            try
            {
                if (ipAddress != string.Empty)
                {
                    isValidIP = IPAddress.TryParse(ipAddress, out returnIPAddress);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return isValidIP;
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
        public static string ExpandIPString(string ip1, string ip2, string ip3, string ip4, bool wrapInQuotes = false)
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

        /// <summary>
        /// Pings a address (URL or IP) and return
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static bool PingAddress(string address)
        {
            bool pingSuccessful = false;
            int timeout = 5000;

            try
            {
                Ping pingSender = new Ping();

                // Send the request.
                PingReply reply = pingSender.Send(address, timeout);
                pingSuccessful = (reply.Status == IPStatus.Success);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex.Message);
            }

            return pingSuccessful;
        }

        /// <summary>
        /// Gets a List of ServerAddress (IP) to DNS over HTTP addresses
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetDoHList()
        {
            var doHList = new Dictionary<string, string>()
            {
                //Quad9
                { "9.9.9.9","https://dns.quad9.net/dns-query" },
                { "149.112.112.112","https://dns.quad9.net/dns-query" },
                
                //CloudFlare 
                { "1.1.1.1","https://1.1.1.1/dns-query" },
                { "1.0.0.1","https://1.0.0.1/dns-query" },
                
                //CloudFlare - No Malware
                { "1.1.1.2","https://1.1.1.2/dns-query" },
                { "1.0.0.2","https://1.0.0.2/dns-query" },
                
                //Google
                { "8.8.8.8","https://dns.google/dns-query" },
                { "8.8.4.4","https://dns.google/dns-query" },

                //CleanBrowsing Guide
                //https://cleanbrowsing.org/guides/dnsoverhttps/

                //CleanBrowsing - Security 
                { "185.228.168.9","https://doh.cleanbrowsing.org/doh/security-filter" },
                { "185.228.169.9","https://doh.cleanbrowsing.org/doh/security-filter" },
                //CleanBrowsing - Family
                { "185.228.168.168","https://doh.cleanbrowsing.org/doh/family-filter" },
                { "185.228.169.168","https://doh.cleanbrowsing.org/doh/family-filter" },
                //CleanBrowsing - Adult
                { "185.228.168.10","https://doh.cleanbrowsing.org/doh/adult-filter" },
                { "185.228.168.11","https://doh.cleanbrowsing.org/doh/adult-filter" },

                //https://kb.adguard.com/en/general/dns-providers
                //AdGuard - Default
                { "94.140.14.14","https://dns.adguard.com/dns-query" },
                { "94.140.15.15","https://dns.adguard.com/dns-query" }

            };

            return doHList;
        }

        /// <summary>
        /// Gets all DOH Adddresses currently configured on the PC
        /// </summary>
        /// <returns></returns>
        public static DoHResult[] GetAllDoHAddresses()
        {
            Logger.Debug("GetAllDoHAddresses");

            try
            {
                //Build the Powershell Command
                string argument = string.Format(@"-NoProfile -ExecutionPolicy unrestricted & {{Get-DnsClientDohServerAddress | Select-Object -Property ServerAddress,AllowFallbackToUdp,AutoUpgrade,DohTemplate | ConvertTo-Json }}");

                //Execute the Powershell command
                var startInfo = new ProcessStartInfo()
                {
                    FileName = "powershell.exe",
                    Arguments = argument,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };
                var process = new Process();
                process.StartInfo = startInfo;
                process.Start();
                string result = process.StandardOutput.ReadToEnd();

                if (result != string.Empty)
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    DoHResult[] dohResults = js.Deserialize<DoHResult[]>(result);
                    return dohResults;
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Compares the List of DOH addresess, the current ones on the PC and inserts the missing ones
        /// </summary>
        public static void InsertMissingDoHAddresses()
        {
            Logger.Debug("InsertMissingDoHAddresses");

            //Get the List of New DoH Addresses
            Dictionary<string, string> newDoHList = GetDoHList();

            //Get the List of Current DoH Addresses in the PC
            DoHResult[] currentDoHList = GetAllDoHAddresses();

            //Loop through each. If missing then Insert
            foreach (KeyValuePair<string, string> doHEntry in newDoHList)
            {
                bool doHAddressFound = false;
                foreach (DoHResult result in currentDoHList)
                {
                    //If the ServerAddress matches
                    if (doHEntry.Key ==  result.ServerAddress)
                    {
                        doHAddressFound = true;
                        break;
                    }
                }

                //If not found; Add
                if (!doHAddressFound) AddDoHAddress(doHEntry.Key, doHEntry.Value, false, false);
            }

            
        }

        /// <summary>
        /// Configures all current DoH address on the PC to be used automatically or not
        /// </summary>
        /// <param name="allowFallbackToUdp"></param>
        /// <param name="autoUpgrade"></param>
        public static void ModifyDoHAddresses(bool allowFallbackToUdp, bool autoUpgrade)
        {
            Logger.Debug("ModifyDoHAddresses");

            DoHResult[] dohResults = GetAllDoHAddresses();
            if (dohResults != null)
            {
                foreach(DoHResult result in dohResults)
                {
                    if (result.AllowFallbackToUdp != allowFallbackToUdp || result.AutoUpgrade != autoUpgrade )
                    {
                        UpsertDoHAddress(result.ServerAddress, result.DohTemplate, allowFallbackToUdp, autoUpgrade);
                    }
                }
            }
        }

        /// <summary>
        /// Sets the configuration of a DoH server on the PC
        /// </summary>
        /// <param name="serverAddress"></param>
        /// <param name="allowFallbackToUdp"></param>
        /// <param name="autoUpgrade"></param>
        /// <returns></returns>
        private static bool SetDoHAddress(string serverAddress, bool allowFallbackToUdp, bool autoUpgrade)
        {
            Logger.Debug(String.Format("SetDoHAddress for [{0}]",serverAddress));

            bool success = false;

            try
            {
                //Build the Powershell Command
                string argument = string.Format(@"-NoProfile -ExecutionPolicy unrestricted & {{Set-DnsClientDohServerAddress -ServerAddress {0} -AllowFallbackToUdp {1} -AutoUpgrade {2} }}", serverAddress, FormatPowershellBoolean(allowFallbackToUdp), FormatPowershellBoolean(autoUpgrade));

                //Execute the Powershell command
                var startInfo = new ProcessStartInfo()
                {
                    FileName = "powershell.exe",
                    Arguments = argument,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };
                var process = new Process();
                process.StartInfo = startInfo;
                process.Start();
                string result = process.StandardOutput.ReadToEnd();

                success = (result == string.Empty);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return success;
        }

        /// <summary>
        /// Adds a configuration of a DoH server on the PC
        /// </summary>
        /// <param name="serverAddress"></param>
        /// <param name="dohTemplate"></param>
        /// <param name="allowFallbackToUdp"></param>
        /// <param name="autoUpgrade"></param>
        private static void AddDoHAddress(string serverAddress, string dohTemplate, bool allowFallbackToUdp, bool autoUpgrade)
        {
            Logger.Debug(String.Format("AddDoHAddress for [{0}]", serverAddress));

            try
            {
                //Build the Powershell Command
                string argument = string.Format(@"-NoProfile -ExecutionPolicy unrestricted & {{Add-DnsClientDohServerAddress -ServerAddress {0} -DohTemplate {1} -AllowFallbackToUdp {2} -AutoUpgrade {3} }}", serverAddress, dohTemplate, FormatPowershellBoolean(allowFallbackToUdp), FormatPowershellBoolean(autoUpgrade));

                //Execute the Powershell command
                var startInfo = new ProcessStartInfo()
                {
                    FileName = "powershell.exe",
                    Arguments = argument,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                var process = new Process();
                process.StartInfo = startInfo;
                process.Start();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        /// <summary>
        /// Sets the configuration of a DoH server on the PC.
        /// If missing then Adds it
        /// </summary>
        /// <param name="serverAddress"></param>
        /// <param name="dohTemplate"></param>
        /// <param name="allowFallbackToUdp"></param>
        /// <param name="autoUpgrade"></param>
        public static void UpsertDoHAddress(string serverAddress, string dohTemplate, bool allowFallbackToUdp, bool autoUpgrade)
        {
            Logger.Debug(String.Format("UpsertDoHAddress for [{0}]", serverAddress));

            try
            {
                if (!SetDoHAddress(serverAddress, allowFallbackToUdp, autoUpgrade))
                {
                    AddDoHAddress(serverAddress, dohTemplate, allowFallbackToUdp, autoUpgrade);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        /// <summary>
        /// Formats a boolean as a PowerShell compatible string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string FormatPowershellBoolean(bool value)
        {
            return value ? "$true" : "$false";  
        }

    }

    public class DoHResult
    {
        public string ServerAddress { get; set; }

        public bool AllowFallbackToUdp { get; set; }

        public bool AutoUpgrade { get; set; }

        public string DohTemplate { get; set; }
    } 
}
