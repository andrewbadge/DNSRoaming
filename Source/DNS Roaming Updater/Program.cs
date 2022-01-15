using DNS_Roaming_Common;
using System;
using System.Reflection;

namespace DNS_Roaming_Updater
{
    class Program
    {
        static bool autoUpdate = true;
        static int autoUpdateHours = 72;
        static DateTime autoUpdateLastCheck = DateTime.Now.AddDays(-30);


        static void Main(string[] args)
        {
            try
            {
                Logger.Info("--------------------------");
                Logger.Info(String.Format("Starting ({0})", Assembly.GetExecutingAssembly().GetName().Version.ToString()));

                bool forceCheck = false;
                bool forceDownloadandInstall = false;

                LoadOptions();
                ParseArgs(args, out forceCheck, out forceDownloadandInstall);

                if (autoUpdate || forceCheck)
                {
                    LoadUpdateData();
                    CheckforUpdates(forceCheck, forceDownloadandInstall);
                }
                else
                {
                    Logger.Info("Auto update is not enabled.");
                }

#if DEBUG
                Console.WriteLine("----------");
                Console.WriteLine("Press ENTER to exit");
                Console.ReadLine();
#endif 
                Logger.Info("Exiting.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        static private void ParseArgs(string[] args, out bool forceCheck, out bool forceDownloadandInstall)
        {
            Logger.Debug("ParseArgs");

            bool argForceCheck = false;
            bool argForceDownloadandInstall = false;

            try
            {
                if (args.Length > 0)
                {
                    foreach (string arg in args)
                    {
                        if (arg.ToLower().Trim() == "forcecheck") argForceCheck = true;
                        if (arg.ToLower().Trim() == "forcedownloadandinstall")
                        {
                            argForceCheck = true;
                            argForceDownloadandInstall = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            finally
            {
                forceCheck = argForceCheck;
                forceDownloadandInstall = argForceDownloadandInstall;
            }

        }

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
                autoUpdate = newOption.AutoUpdate;
                autoUpdateHours = newOption.AutoUpdateHours;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

        }

        /// <summary>
        /// Load Options from the Options File
        /// </summary>
        static private void LoadUpdateData()
        {
            Logger.Debug("LoadUpdateData");

            try
            {
                UpdateData updateData = new UpdateData();
                updateData.Load();
                autoUpdateLastCheck = updateData.AutoUpdateLastCheck;

                Logger.Info(string.Format("Last update check was ({0})", autoUpdateLastCheck.ToString("yyyy-MMM-dd HH:mm")));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

        }

        /// <summary>
        /// Checks GitHub for a new version. If found downloads and installs
        /// </summary>
        /// <returns></returns>
        private static bool CheckforUpdates(bool forceCheck, bool forceDownloadandInstall)
        {
            bool updatesFound = false;

            try
            {
                if (!autoUpdate && !forceCheck) return false;

                Logger.Debug("CheckforUpdates");

                if (autoUpdateLastCheck.AddHours(autoUpdateHours) < DateTime.Now || forceCheck)
                {
                    if (forceCheck)
                        Logger.Debug("Forcing an Update Check");
                    else
                        Logger.Debug("Update Check is due");

                    //Save the new "last checked" date
                    autoUpdateLastCheck = DateTime.Now;

                    Logger.Debug("Saving last update check date");
                    UpdateData updateData = new UpdateData();
                    updateData.Load();
                    updateData.AutoUpdateLastCheck = autoUpdateLastCheck;
                    updateData.Save();

                    //Check for updates
                    clsUpdates updates = new clsUpdates();
                    if (updates.NewerVersionAvailable() || forceDownloadandInstall)
                    {
                        if (forceDownloadandInstall)
                            Logger.Info("Forcing to download a new version");
                        else
                            Logger.Info("GitHub Version is newer");
                        updatesFound = updates.DownloadandExecuteLatestVersion();
                    }
                    else
                    {
                        Logger.Info("No newer Version is available");
                        updatesFound = false;
                    }
                }
                else
                {
                    Logger.Debug("Update Check is not due yet");
                    updatesFound = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return updatesFound;
        }
    }
}
