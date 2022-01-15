using DNS_Roaming_Common;
using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace DNS_Roaming_Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Mutex mutex = new System.Threading.Mutex(false, "DNSRoamingClientMutex");
            try
            {
                Logger.Info("--------------------------");
                Logger.Info(String.Format("Starting ({0})", Assembly.GetExecutingAssembly().GetName().Version.ToString()));

                if (mutex.WaitOne(0, false))
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FrmMain());
                }
                else
                    Logger.Info("Exiting as another instance is already running");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            finally
            {
                if (mutex != null)
                {
                    mutex.Close();
                    mutex = null;
                }
            }
        }
    }
}
