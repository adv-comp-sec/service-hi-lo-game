using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A06Service
{
    public static class Logger
    {
        
        public static void Log(string message)
        {
            Guid guid = Guid.NewGuid();
            //string directoryPath = ConfigurationManager.AppSettings["Directory"];
            string logFilePath = guid.ToString() + ".txt";
            StreamWriter HiLoLog;
            
            try
            {
                HiLoLog = new StreamWriter(logFilePath);
                //fi
            }
            catch
            {

            }

            //EventLog serviceEventLog = new EventLog();

            //if(!EventLog.SourceExists("ServiceEventSource"))
            //{
            //    EventLog.CreateEventSource("ServiceEventSource", "ServiceEventLog");
            //}

            //serviceEventLog.Source = "ServiceEventSource";
            //serviceEventLog.Log = "ServiceEventLog";
            //serviceEventLog.WriteEntry(message);
        }
    }
}
