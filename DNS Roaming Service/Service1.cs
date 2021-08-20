using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.Configuration;
using System.Reflection;
using System.Threading;
using System.Net;
using System.Net.NetworkInformation;
using System.Management;

namespace DNS_Roaming_Service
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            try
            {
                Logger.Info("--------------------------");
                Logger.Info(String.Format("Starting ({0})", Assembly.GetExecutingAssembly().GetName().Version.ToString()));
                InitializeComponent();

                string logPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "DNSRoaming");
                if (!System.IO.Directory.Exists(logPath)) System.IO.Directory.CreateDirectory(logPath);

                registerEvents();
                GetActiveNetworkinterface();

                SetDNS("1.1.1.1");

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

            GetActiveNetworkinterface();
        }

        static void AvailabilityChangedCallback(object sender, EventArgs e)
        {

            GetActiveNetworkinterface();
        }

        static void GetActiveNetworkinterface()
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface n in adapters)
            {
                if (n.NetworkInterfaceType == NetworkInterfaceType.Ethernet || n.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                {
                    if (n.OperationalStatus == OperationalStatus.Up)
                    {
                        if (n.Supports(NetworkInterfaceComponent.IPv4))
                        {
                            Logger.Info(string.Format("Active NIC is: {0} is {1}", n.Name, n.OperationalStatus));

                        }
                    }
                }


            }
        }

        static NetworkInterface GetActiveEthernetOrWifiNetworkInterface()
        {
            var Nic = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(
                a => a.OperationalStatus == OperationalStatus.Up &&
                (a.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || a.NetworkInterfaceType == NetworkInterfaceType.Ethernet) &&
                a.GetIPProperties().GatewayAddresses.Any(g => g.Address.AddressFamily.ToString() == "InterNetwork"));

            return Nic;
        }

        static void SetDNS(string DnsString)
        {
            string[] Dns = { DnsString };
            var CurrentInterface = GetActiveEthernetOrWifiNetworkInterface();
            if (CurrentInterface == null) return;

            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();
            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool)objMO["IPEnabled"])
                {
                    if (objMO["Description"].ToString().Equals(CurrentInterface.Description))
                    {
                        ManagementBaseObject objdns = objMO.GetMethodParameters("SetDNSServerSearchOrder");
                        if (objdns != null)
                        {
                            objdns["DNSServerSearchOrder"] = Dns;
                            objMO.InvokeMethod("SetDNSServerSearchOrder", objdns, null);
                        }
                    }
                }
            }
        }
    }
}
