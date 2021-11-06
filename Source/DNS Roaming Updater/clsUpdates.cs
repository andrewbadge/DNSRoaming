using Microsoft.Win32;
using Octokit;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace DNS_Roaming_Common
{
    public class clsUpdates
    {
        Release latestRelease;

        private Version GetInstalledMSIVersion()
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

        private string GetInstalledMSIType()
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

        private async Task<Version> GetGitHubLatestVersionAsync()
        {
            Logger.Debug("GetGitHubLatestVersionAsync");

            Version returnVersion = null;

            try
            {
                var client = new GitHubClient(new ProductHeaderValue("DNS-Roaming-Updates"));
                var releases = await client.Repository.Release.GetAll("andrewbadge", "DNSRoaming");
                latestRelease = releases[0];
                string releaseName = latestRelease.Name;

                if (releaseName.StartsWith("v"))
                {
                    releaseName = releaseName.Substring(1, releaseName.Length - 1);
                }

                if (releaseName.EndsWith("-Beta"))
                {
                    releaseName = releaseName.Substring(0, releaseName.Length - 5);
                }

                returnVersion = Version.Parse(releaseName);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return returnVersion;

        }

        public bool NewerVersionAvailable()
        {
            Logger.Debug("NewerVersionAvailable");

            bool newerVersionAvailable = false;

            try
            {
                Version installedversion = GetInstalledMSIVersion();
                Logger.Info(String.Format("Installed Version is ({0})", installedversion.ToString()));

                Task<Version> githubResult = GetGitHubLatestVersionAsync();
                githubResult.Wait();

                Version githubVersion = githubResult.Result;

                Logger.Info(String.Format("Latest GitHub Version is ({0})", githubVersion.ToString()));

                var versionComparison = githubVersion.CompareTo(installedversion);
                newerVersionAvailable = versionComparison > 0;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return newerVersionAvailable;
                
        }

        private string GetDownloadURL()
        {
            Logger.Debug("GetDownloadURL");

            string downloadURL = string.Empty;
            bool serviceandClient = false;

            //CheckAnnotationLevel to see if the Client is installed (or just the service)
            Logger.Debug("Getting MSI Type");
            serviceandClient = (GetInstalledMSIType() == "ServiceandClient");
            //Loop each Asset in the release
            foreach (ReleaseAsset asset in latestRelease.Assets)
            {
                //If Service and Client is installed and the MSI matches
                if (serviceandClient && asset.Name == "DNSRoaming-ServiceAndClient.msi")
                {
                    downloadURL = asset.BrowserDownloadUrl;
                    Logger.Debug(String.Format("ServiceAndClient URL ({0})", downloadURL));
                    break;
                }

                //If only the Service is installed and the MSI matches
                if (!serviceandClient && asset.Name == "DNSRoaming-ServiceOnly.msi")
                {
                    downloadURL = asset.BrowserDownloadUrl;
                    Logger.Debug(String.Format("ServiceOnly URL ({0})", downloadURL));
                    break;
                }

            }

            return downloadURL;
        }

        public bool DownloadandExecuteLatestVersion()
        {
            Logger.Debug("DownloadandExecuteLatestVersion");

            bool downloadedandExecuted = false;

            try
            {
                if (latestRelease == null)
                {
                    Logger.Warn("Unexpected. Release information is missing");
                }
                else
                {
                    PathsandData pathsandData = new PathsandData();

                    string downloadURL = GetDownloadURL();
                    if (downloadURL != string.Empty)
                    {
                        string downloadedFile = Path.Combine(pathsandData.BaseDownloadsPath, "DNSRoaming.msi");

                        Logger.Debug(String.Format("Downloading from ({0})", downloadURL));
                        Logger.Debug(String.Format("Downloading to ({0})", downloadedFile));

                        //Remove the MSI is it already exists
                        if (File.Exists(downloadedFile)) File.Delete(downloadedFile);

                        //Download a new version from GitHub
                        using (var webClient = new WebClient())
                        {
                            Logger.Info("Downloading the update");
                            webClient.DownloadFile(downloadURL, downloadedFile);
                        }

                        if (File.Exists(downloadedFile))
                        {
                            Logger.Info("File downloaded sucessfully. Executing update.");

                            //Remove the MSI is it already exists
                            string logFile = Path.Combine(pathsandData.BaseDownloadsPath, "MSIInstallation.log");
                            if (File.Exists(logFile)) File.Delete(logFile);

                            //Execute MSI
                            Process process = new Process();
                            process.StartInfo.FileName = "msiexec";
                            process.StartInfo.WorkingDirectory = pathsandData.BaseDownloadsPath;
                            //process.StartInfo.Arguments = String.Format(" /i \"{0}\" /QN /L*V \"{1}\" ", downloadedFile,logFile);
                            process.StartInfo.Arguments = String.Format(" /i \"{0}\" /QN /L*V \"{1}\" ", downloadedFile, logFile);
                            process.StartInfo.Verb = "runas";
                            process.Start();

                            downloadedandExecuted = true;
                        }
                        else
                            Logger.Warn("Update failed to download.");
                    }
                    else
                        Logger.Warn("Download URL was empty.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return downloadedandExecuted;
        }

    }
}
