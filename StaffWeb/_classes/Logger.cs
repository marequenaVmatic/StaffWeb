using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace StaffWeb._classes
{
    public class Logger
    {
        private const string LOGFOLDER = "/logs/";
        private const string LOGFILE = "{0:yyyy-MM-dd}.log";

        public static void Log(string strMsg)
        {
            try
            {
                string strFolder = HttpContext.Current.Server.MapPath(LOGFOLDER);
                if (!Directory.Exists(strFolder))
                    Directory.CreateDirectory(strFolder);

                string strLogFilePath = strFolder + string.Format(LOGFILE, DateTime.Now);

                using (StreamWriter writer = new StreamWriter(strLogFilePath, true))
                {
                    writer.WriteLine(string.Format("{0:HH:mm:ss} ===> {1}", DateTime.Now, strMsg));
                    writer.Close();
                }
            }
            catch { }
        }
    }
}