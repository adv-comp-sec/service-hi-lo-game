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
            string directoryPath = ConfigurationManager.AppSettings["Directory"];
            string logFilePath = directoryPath + "\\" + guid.ToString() + ".txt";
            StreamWriter HiLoLog = null;
            
            try
            {
                HiLoLog = new StreamWriter(logFilePath);
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
