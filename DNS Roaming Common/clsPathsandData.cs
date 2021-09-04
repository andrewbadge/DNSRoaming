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

        public string BaseApplicationPath
        {
            get { return baseApplicationPath; }
        }

        public string BaseSettingsPath
        {
            get { return baseSettingsPath; }
        }

        public PathsandData()
        {
            ValidateDataPaths();
        }

        /// <summary>
        /// Set the paths used int he app. If they don't exist then create
        /// </summary>
        private void ValidateDataPaths()
        {
            Logger.Debug("ValidateDataPaths");

            try
            {
                string commonApplicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

                baseApplicationPath = Path.Combine(commonApplicationDataPath, "DNSRoaming");
                if (!System.IO.Directory.Exists(baseApplicationPath))
                {
                    Logger.Info("Creating Log and Setting Folder");
                    System.IO.Directory.CreateDirectory(baseApplicationPath);
                }

                baseSettingsPath = Path.Combine(baseApplicationPath, "Settings");
                if (!System.IO.Directory.Exists(baseSettingsPath)) System.IO.Directory.CreateDirectory(baseSettingsPath);

                SetDirectoryPermissions(baseApplicationPath);
                SetDirectoryPermissions(baseSettingsPath);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private void SetDirectoryPermissions(string directoryPath)
        {
            Logger.Debug("SetDirectoryPermissions");

            try
            {
                // Get directory access info
                DirectoryInfo dinfo = new DirectoryInfo(directoryPath);
                DirectorySecurity dSecurity = dinfo.GetAccessControl();

                // Add the FileSystemAccessRule to the security settings. 
                dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));

                // Set the access control
                dinfo.SetAccessControl(dSecurity);
            }
            catch { }
        }

    }
}
