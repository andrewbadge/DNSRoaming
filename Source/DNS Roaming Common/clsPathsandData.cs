using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace DNS_Roaming_Common
{
    public class PathsandData
    {

        private string baseApplicationPath = string.Empty;
        private string baseSettingsPath = string.Empty;
        private string baseOptionsPath = string.Empty;
        private string baseDownloadsPath = string.Empty;

        public string BaseApplicationPath
        {
            get { return baseApplicationPath; }
        }

        public string BaseSettingsPath
        {
            get { return baseSettingsPath; }
        }

        public string BaseOptionsPath
        {
            get { return baseOptionsPath; }
        }

        public string BaseDownloadsPath
        {
            get { return baseDownloadsPath; }
        }

        public PathsandData()
        {
            PopulateDataPaths();
        }

        /// <summary>
        /// Set the paths used in the app. If they don't exist then create
        /// </summary>
        private void PopulateDataPaths()
        {
            Logger.Debug("PopulateDataPaths");

            try
            {
                string commonApplicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                baseApplicationPath = Path.Combine(commonApplicationDataPath, "DNSRoaming");
                baseSettingsPath = Path.Combine(baseApplicationPath, "Settings");
                baseOptionsPath = Path.Combine(baseApplicationPath, "Options");
                baseDownloadsPath = Path.Combine(baseApplicationPath, "Downloads");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        /// <summary>
        /// Does the Settings Path Exist?
        /// </summary>
        public bool SettingsPathExist()
        {
            Logger.Debug("SettingsPathExist");

            bool settingsPathExist = false;

            try
            {
                settingsPathExist = System.IO.Directory.Exists(baseSettingsPath);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return settingsPathExist;
        }

        /// <summary>
        /// Does the Options Path Exist?
        /// </summary>
        public bool OptionsPathExist()
        {
            Logger.Debug("OptionsPathExist");

            bool optionsPathExist = false;

            try
            {
                optionsPathExist = System.IO.Directory.Exists(baseOptionsPath);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return optionsPathExist;
        }

        /// <summary>
        /// Set the paths used in the app. If they don't exist then create
        /// </summary>
        public void CreateDataPaths(bool RunningAsAService = false)
        {
            Logger.Debug("ValidateDataPaths");

            try
            {
                PopulateDataPaths();

                if (!System.IO.Directory.Exists(baseApplicationPath))
                {
                    Logger.Info("Creating Log Folder");
                    System.IO.Directory.CreateDirectory(baseApplicationPath);
                }

                if (!System.IO.Directory.Exists(baseSettingsPath))
                {
                    Logger.Info("Creating Setting Folder");
                    System.IO.Directory.CreateDirectory(baseSettingsPath);
                }

                if (!System.IO.Directory.Exists(baseOptionsPath))
                {
                    Logger.Info("Creating Options Folder");
                    System.IO.Directory.CreateDirectory(baseOptionsPath);
                }

                if (!System.IO.Directory.Exists(baseDownloadsPath))
                {
                    Logger.Info("Creating Downloads Folder");
                    System.IO.Directory.CreateDirectory(baseDownloadsPath);
                }

                if (RunningAsAService) SetDirectoryPermissions(baseApplicationPath);
                if (RunningAsAService) SetDirectoryPermissions(baseSettingsPath);
                if (RunningAsAService) SetDirectoryPermissions(baseOptionsPath);
                if (RunningAsAService) SetDirectoryPermissions(baseDownloadsPath);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        /// <summary>
        /// Set the paths used in the app. If they don't exist then create
        /// </summary>
        public void CreateDataPaths()
        {
            CreateDataPaths(false);
        }

        /// <summary>
        /// Allow user to modify the Settings and log folder (as the service is most likely to be the firts to start) 
        /// Otherwise the client can't save logs or settings
        /// </summary>
        /// <param name="directoryPath"></param>
        private void SetDirectoryPermissions(string directoryPath)
        {
            Logger.Debug(String.Format("SetDirectoryPermissions for {0}", directoryPath));

            try
            {
                // Get directory access info
                DirectoryInfo dinfo = new DirectoryInfo(directoryPath);
                DirectorySecurity dSecurity = dinfo.GetAccessControl(AccessControlSections.All);


                Logger.Debug("Setting new ACL");

                // Add the FileSystemAccessRule to the security settings. 
                dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null), FileSystemRights.Modify, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.InheritOnly, AccessControlType.Allow));
                dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null), FileSystemRights.CreateFiles, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.InheritOnly, AccessControlType.Allow));
                dinfo.SetAccessControl(dSecurity);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

    }
}
