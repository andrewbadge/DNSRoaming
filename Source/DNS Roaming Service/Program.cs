using DNS_Roaming_Common;
using System;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;

namespace DNS_Roaming_Service
{
    static class Program
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            try { 
                Logger.Info("--------------------------");
                Logger.Info(String.Format("Starting ({0})", Assembly.GetExecutingAssembly().GetName().Version.ToString()));

                ServiceBase[] servicesToRun;
                servicesToRun = new ServiceBase[]
                {
                    new svcMain()
                };
                if (Environment.UserInteractive)
                {
                    RunInteractive(servicesToRun);
                }
                else
                {
                    ServiceBase.Run(servicesToRun);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        static void RunInteractive(ServiceBase[] servicesToRun)
        {
            Console.WriteLine("Services running in interactive mode.");
            Console.WriteLine();

            MethodInfo onStartMethod = typeof(ServiceBase).GetMethod("OnStart",
                BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (ServiceBase service in servicesToRun)
            {
                Console.Write("Starting {0}...", service.ServiceName);
                onStartMethod.Invoke(service, new object[] { new string[] { } });
                Console.Write("Started");
            }

            Console.WriteLine("Press any key to stop the services and end the process...");
            Console.ReadKey();
            
            MethodInfo onStopMethod = typeof(ServiceBase).GetMethod("OnStop",BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (ServiceBase service in servicesToRun)
            {
                Console.Write("Stopping {0}...", service.ServiceName);
                onStopMethod.Invoke(service, null);
                Console.WriteLine("Stopped");
            }

            Console.WriteLine("All services stopped.");
            // Keep the console alive for a second to allow the user to see the message.
            Thread.Sleep(1000);
        }
    }
}
