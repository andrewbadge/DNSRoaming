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

        public static void GetCurrentIPandSubNet(out String currentIP, out String currentSubnet)
        {
            string returnIP = string.Empty;
            string returnSubnet = string.Empty;

            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface n in adapters)
            {
                if (n.NetworkInterfaceType == NetworkInterfaceType.Ethernet || n.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
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

        public static void SetStaticDNSusingPowershell(string networkName, string preferredDNS, string alternateDNS)
        {
            try { 
                //Build the DNS address list
                string dnsString = string.Empty;
                if (preferredDNS == String.Empty) dnsString = String.Format(@"""{0}""", alternateDNS);
                if (alternateDNS == String.Empty) dnsString = String.Format(@"""{0}""", preferredDNS);
                if (dnsString == String.Empty) dnsString = String.Format(@"""{0}"",""{1}""", preferredDNS, alternateDNS);

                //Build the Powershell Command
                string argument = string.Format(@"-NoProfile -ExecutionPolicy unrestricted & {{Set-DnsClientServerAddress -InterfaceAlias (""{0}"") -ServerAddresses ({1}) }}", networkName, dnsString);

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
        public static void GetNetworkAttributes(NetworkInterface currentNIC, out string currentIP, out string currentSubnet, out string networkName, out NetworkInterfaceType networkInterfaceType, out string dnsAddress1, out string dnsAddress2)
        {
            //Get name, Type, IP and Subnet
            networkName = currentNIC.Name;
            networkInterfaceType = currentNIC.NetworkInterfaceType;
            currentIP = string.Empty;
            currentSubnet = string.Empty;
            dnsAddress1 = string.Empty;
            dnsAddress2 = string.Empty;


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
                    if (dnsAddress1 == String.Empty)
                        dnsAddress1 = dnsAddress.ToString();
                    else
                    {
                        if (dnsAddress2 == String.Empty)
                            dnsAddress2 = dnsAddress.ToString();
                        else
                            break;
                    }
                }

            }
        }

    }
}
