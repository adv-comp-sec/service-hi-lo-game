using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W11Service
{
    public static class Logger
    {
        public static void Log(string message)
        {
            EventLog serviceEventLog = new EventLog();

            if(!EventLog.SourceExists("ServiceEventSource"))
            {
                EventLog.CreateEventSource("ServiceEventSource", "ServiceEventLog");
            }

            serviceEventLog.Source = "ServiceEventSource";
            serviceEventLog.Log = "ServiceEventLog";
            serviceEventLog.WriteEntry(message);
        }
    }
}
