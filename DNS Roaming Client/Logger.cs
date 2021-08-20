using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using NLog;

namespace DNS_Roaming_Client
{
    public static class Logger
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public enum eventID
        {
            DispenseNotFound = 2000,
            DispenseOutOfDate = 2010,
            DispenseOk = 2100,

            RMSNotFound = 3000,
            RMSOutOfDate = 3010,
            RMSOk = 3100,

            FredOfficeNotFound = 4000,
            FredOfficeOutOfDate = 4010,
            FredOfficeOk = 4100,

            LOTSAccessNotFound = 5000,
            LOTSAccessOutOfDate = 5010,
            LOTSAccessOk = 5100,

            ErrorVFPProvider = 1010,
            OtherError = 1000
        }

        public static void Info(string message)
        {
            logger.Info(message);
        }

        public static void Warn(string message)
        {
            logger.Warn("Warning:{0}", message);
        }

        public static void Error(string message)
        {
            logger.Error("Error:{0}", message);
            LogEvent(message, EventLogEntryType.Error, (int)eventID.OtherError); 
        }

        public static void Info(string message, Int32 eventID)
        {
            logger.Info(message);
            LogEvent(message, EventLogEntryType.Information, eventID); 
        }

        public static void Warn(string message, Int32 eventID)
        {
            logger.Warn("Warning:{0}", message);
            LogEvent(message, EventLogEntryType.Warning, eventID); 
        }

        public static void Error(string message, Int32 eventID)
        {
            logger.Error("Error:{0}",message);
            LogEvent(message, EventLogEntryType.Error, eventID); 
        }


        private static void LogEvent(string message, EventLogEntryType entryType, Int32 eventID)
        {
            try
            {
                string sSource = "FredVerifyData";
                string sLog = "Application";

                if (!EventLog.SourceExists(sSource))
                    EventLog.CreateEventSource(sSource, sLog);

                EventLog.WriteEntry(sSource, message, entryType, eventID);
            }
            catch { }
        }
                
    }
}
