using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanDotNetUtils.Helpers
{
    public class LoggerHelper
    {
        public static void Log(string msg, string filePre = "")
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = string.IsNullOrWhiteSpace(filePre) ? DateTime.Now.ToString("yyyy-MM-dd") + ".log" : filePre + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";

            try
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(path, fileName),true))
                {
                    sw.WriteLine(DateTime.Now.ToString() + ": " + msg);
                }
            }
            catch { }
        }
    }
}
