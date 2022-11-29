using System;
using System.Configuration;
using System.IO;
using static System.Net.WebRequestMethods;

namespace A06Service
{
    public static class Logger
    {
        public static void Log(string message)
        {
            Guid guid = Guid.NewGuid();
            string logFilePath = ConfigurationManager.AppSettings["Directory"];
            StreamWriter HiLoLog = null;
            
            try
            {
                HiLoLog = new StreamWriter(logFilePath, true);
                HiLoLog.Write("\r\nLog Entry:\n" + DateTime.Now.ToString() + ": " + message);
            }
            catch(Exception e)
            {
                HiLoLog.Write("\r\nLog Entry:\n" + DateTime.Now.ToString() + ": " + e.ToString());
            }
            finally
            {
                if (HiLoLog != null)
                {
                    HiLoLog.Close();
                    HiLoLog = null;
                }
            }
        }
    }
}
