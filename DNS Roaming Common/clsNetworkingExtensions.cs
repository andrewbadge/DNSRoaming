using System;
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

        public static bool IsInSameSubnet(this IPAddress address2, IPAddress address, IPAddress subnetMask)
        {
            IPAddress network1 = address.GetNetworkAddress(subnetMask);
            IPAddress network2 = address2.GetNetworkAddress(subnetMask);

            return network1.Equals(network2);
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
                    ipAlternative = "1.0.0.2";
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

    }
}
