using Microsoft.Win32;
using System;

namespace DNS_Roaming_Common
{
    public class InstallerInfo
    {
        public Version GetInstalledMSIVersion()
        {
            Logger.Debug("GetInstalledMSIVersion");

            string registryKeyName = @"SOFTWARE\WOW6432Node\DNSRoaming";
            const string registryValueName = "Version";
            Version returnVersion = null;

            try
            {

                RegistryKey regKey = Registry.LocalMachine.OpenSubKey(registryKeyName);
                if (regKey == null)
                {
                    registryKeyName = @"SOFTWARE\DNSRoaming";
                    regKey = Registry.LocalMachine.OpenSubKey(registryKeyName);
                }

                //Not installed?
                if (regKey != null)
                {
                    var regValue = regKey.GetValue(registryValueName);
                    if (regValue != null)
                        returnVersion = Version.Parse((string)regValue);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return returnVersion;
        }

        public string GetInstalledMSIType()
        {
            Logger.Debug("GetInstalledMSIType");

            string registryKeyName = @"SOFTWARE\WOW6432Node\DNSRoaming";
            const string registryValueName = "Type";
            string returnType = "ServiceandClient";

            try
            {

                RegistryKey regKey = Registry.LocalMachine.OpenSubKey(registryKeyName);
                if (regKey == null)
                {
                    registryKeyName = @"SOFTWARE\DNSRoaming";
                    regKey = Registry.LocalMachine.OpenSubKey(registryKeyName);
                }

                //Not installed?
                if (regKey != null)
                {
                    var regValue = regKey.GetValue(registryValueName);
                    if (regValue != null)
                        returnType = (string)regValue;
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return returnType;

        }
    }
}
