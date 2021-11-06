using System;
using System.Reflection;
using DNS_Roaming_Common;

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

                LoadOptions();
                if (autoUpdate)
                {
                    LoadUpdateData();
                    CheckforUpdates();
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
                DNSRoamingUpdateData updateData = new DNSRoamingUpdateData();
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
        private static bool CheckforUpdates()
        {
            if (!autoUpdate) return false;

            Logger.Debug("CheckforUpdates");

            if (autoUpdateLastCheck.AddHours(autoUpdateHours) < DateTime.Now)
            {
                Logger.Debug("Update Check is due");

                //Save the new "last checked" date
                autoUpdateLastCheck = DateTime.Now;

                Logger.Debug("Saving last update check date");
                DNSRoamingUpdateData updateData = new DNSRoamingUpdateData();
                updateData.Load();
                updateData.AutoUpdateLastCheck = autoUpdateLastCheck;
                updateData.Save();

                //Check for updates
                clsUpdates updates = new clsUpdates();
                if (updates.NewerVersionAvailable())
                {
                    Logger.Info("GitHub Version is newer");
                    return updates.DownloadandExecuteLatestVersion();
                }
                else
                {
                    Logger.Info("No newer Version is available");
                    return false;
                }
            }
            else
            {
                Logger.Debug("Update Check is not due yet");
                return false;
            }
        }
    }
}
