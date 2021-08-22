using System;
using System.IO;

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
            string commonApplicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

            baseApplicationPath = Path.Combine(commonApplicationDataPath, "DNSRoaming");
            if (!System.IO.Directory.Exists(baseApplicationPath)) System.IO.Directory.CreateDirectory(baseApplicationPath);

            baseSettingsPath = Path.Combine(baseApplicationPath, "Settings");
            if (!System.IO.Directory.Exists(baseSettingsPath)) System.IO.Directory.CreateDirectory(baseSettingsPath);
        }

    }
}
