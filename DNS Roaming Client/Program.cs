using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using DNS_Roaming_Common;

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
            try { 
                Logger.Info("--------------------------");
                Logger.Info(String.Format("Starting ({0})", Assembly.GetExecutingAssembly().GetName().Version.ToString()));

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FrmMain());
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
    }
}
