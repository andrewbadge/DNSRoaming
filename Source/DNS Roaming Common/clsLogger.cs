using System;
using System.Diagnostics;

namespace DNS_Roaming_Common
{
    public static class Logger
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public enum eventID
        {
            EventA = 2000,
            EventB = 2010,

            OtherError = 1000
        }

        public static void Debug(string message)
        {
            logger.Debug(message);
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
                string sSource = "DNS Roaming";
                string sLog = "Application";

                if (!EventLog.SourceExists(sSource))
                    EventLog.CreateEventSource(sSource, sLog);

                EventLog.WriteEntry(sSource, message, entryType, eventID);
            }
            catch { }
        }
                
    }
}
