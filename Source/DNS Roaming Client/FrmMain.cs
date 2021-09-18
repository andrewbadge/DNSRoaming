using DNS_Roaming_Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Windows.Forms;

namespace DNS_Roaming_Client
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            Logger.Debug("FrmMain Initialize");

            InitializeComponent();

            PathsandData pathsandData = new PathsandData();
            ConfigureTimer();
        }

        #region Form and Actions

        private void btnForceClose_Click(object sender, EventArgs e)
        {
            ForceClose();
        }

        private void ForceClose()
        {
            Logger.Debug("ForceClose");

            this.Close();
        }

        private void OpenSettingsForm()
        {
            Logger.Debug("OpenSettingsForm");

            if (Application.OpenForms.OfType<FrmSettings>().Count() == 0)
            {
                FrmSettings frmSettings = new FrmSettings();
                frmSettings.ShowDialog();
            }
        }

        #endregion

        #region Service Status

        private void ConfigureTimer()
        {
            Logger.Debug("ConfigureTimer");

            timerCheckServiceStatus.Interval = 5000;
            timerCheckServiceStatus.Enabled = true;
        }

        private void CheckServiceStatus()
        {
            Logger.Debug("CheckServiceStatus");

            string serviceStatus = "Error";

            try
            {
                ServiceController sc = new ServiceController("DNSRoamingService");
                switch (sc.Status)
                {
                    case ServiceControllerStatus.Running:
                        serviceStatus = "Running";
                        break;
                    case ServiceControllerStatus.Stopped:
                        serviceStatus = "Stopped";
                        break;
                    case ServiceControllerStatus.Paused:
                        serviceStatus = "Paused";
                        break;
                    case ServiceControllerStatus.StopPending:
                        serviceStatus = "Stopping";
                        break;
                    case ServiceControllerStatus.StartPending:
                        serviceStatus = "Starting";
                        break;
                    default:
                        serviceStatus = "Status Changing";
                        break;
                }
                menuServiceStatus.Text = String.Format("Service is {0}", serviceStatus);
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message == "The specified service does not exist as an installed service")
                {

                    Logger.Warn("The DNS Roaming Service is not installed. Please rerun the installer to repair this or manually install the service.");
                    menuServiceStatus.Text = String.Format("Error: Service is not installed");
                }
                else
                {
                    Logger.Error(ex.Message);
                    menuServiceStatus.Text = String.Format("Error: cannot get service state");
                }
            }


        }

        private void timerCheckServiceStatus_Tick(object sender, EventArgs e)
        {
            Logger.Info(String.Format("Checking service state"));
            CheckServiceStatus();

            //Reschedule the next Timer to a random internal 
            //between 30secs and 5mins
            Random randomNumber = new Random();
            int timerDelay = randomNumber.Next(30, 600) * 1000;
            timerCheckServiceStatus.Interval = timerDelay;
        }

#endregion

        #region Menus and Actions

        private void menuSettings_Click(object sender, EventArgs e)
        {
            Logger.Debug("Menu: Open FrmSettings");

            OpenSettingsForm();
        }

        private void menuStopAndClose_Click(object sender, EventArgs e)
        {
            ForceClose();
        }

        private void contextMenuStrip_DoubleClick(object sender, EventArgs e)
        {
            Logger.Debug("DoubleClick: Open FrmSettings");

            OpenSettingsForm();
        }

        private void menuLogsClient_Click(object sender, EventArgs e)
        {
            Logger.Debug("Menu: Open Client logs");

            try
            {
                PathsandData pathsandData = new PathsandData();

                string pattern = "Client*.txt";
                var dirInfo = new DirectoryInfo(pathsandData.BaseApplicationPath);
                var file = (from f in dirInfo.GetFiles(pattern) orderby f.LastWriteTime descending select f).First();

                if (file != null) Process.Start(file.FullName);
            }
            catch (Exception ex)
            {
                if (ex.Message != "Sequence contains no elements") Logger.Error(ex.Message);
            }
        }

        private void menuLogsService_Click(object sender, EventArgs e)
        {
            Logger.Debug("Menu: Open Service logs");

            try
            {
                PathsandData pathsandData = new PathsandData();

                string pattern = "Service*.txt";
                var dirInfo = new DirectoryInfo(pathsandData.BaseApplicationPath);
                var file = (from f in dirInfo.GetFiles(pattern) orderby f.LastWriteTime descending select f).First();

                if (file != null) Process.Start(file.FullName);
            }
            catch (Exception ex)
            {
                if (ex.Message != "Sequence contains no elements") Logger.Error(ex.Message);
            }
        }

        private void menuLogsFolder_Click(object sender, EventArgs e)
        {
            Logger.Debug("Menu: Open Log Folder");

            try
            {
                PathsandData pathsandData = new PathsandData();
                Process.Start(pathsandData.BaseApplicationPath);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private void menuAbout_Click(object sender, EventArgs e)
        {
            Logger.Debug("Menu: Open About");

            try
            {
                Process.Start(@"https://github.com/andrewbadge/DNSRoaming");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            menuSettings_Click(sender, e);
        }

        #endregion
    }
}
