using DNS_Roaming_Common;
using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Windows.Forms;
using System.Linq;

namespace DNS_Roaming_Client
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();

            PathsandData pathsandData = new PathsandData();
            ConfigureTimer();
        }

        private void btnForceClose_Click(object sender, EventArgs e)
        {
            ForceClose();
        }

        private void ForceClose()
        {
            this.Close();
        }

        private void menuPause_Click(object sender, EventArgs e)
        {

        }

        private void menuSettings_Click(object sender, EventArgs e)
        {
            FrmSettings frmSettings = new FrmSettings();
            frmSettings.ShowDialog();
        }

        private void menuStopAndClose_Click(object sender, EventArgs e)
        {
            ForceClose();
        }

        private void contextMenuStrip_DoubleClick(object sender, EventArgs e)
        {
            FrmSettings frmSettings = new FrmSettings();
            frmSettings.ShowDialog();
        }

        private void ConfigureTimer()
        {
            timerCheckServiceStatus.Interval = 5000;
            timerCheckServiceStatus.Enabled = true;

        }

        private void CheckServiceStatus()
        {

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

        private void menuLogsClient_Click(object sender, EventArgs e)
        {
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
    }
}
