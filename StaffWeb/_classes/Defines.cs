using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace StaffWeb._classes
{
    public class Defines
    {
        public static string DB_HOST
        {
            get { return ConfigurationManager.AppSettings["DB_HOST"]; }
        }
        public static string DB_PORT
        {
            get { return ConfigurationManager.AppSettings["DB_PORT"]; }
        }
        public static string DB_NAME
        {
            get { return ConfigurationManager.AppSettings["DB_NAME"]; }
        }
        public static string DB_USER
        {
            get { return ConfigurationManager.AppSettings["DB_USER"]; }
        }
        public static string DB_PASS
        {
            get { return ConfigurationManager.AppSettings["DB_PASS"]; }
        }
    }
}