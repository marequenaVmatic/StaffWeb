using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using DataAccess;

namespace StaffWeb._classes
{
    public class PageBase : System.Web.UI.Page
    {
        protected MSSqlAccess _dbconn = null;
        public MSSqlAccess DBConn
        {
            get
            {
                return _dbconn;
            }
        }

        protected virtual void Page_PreInit(object sender, EventArgs e)
        {
            _dbconn = new MSSqlAccess();
            _dbconn.DBServer = Defines.DB_HOST;
            _dbconn.DBPort = Defines.DB_PORT;
            _dbconn.DBName = Defines.DB_NAME;
            _dbconn.DBID = Defines.DB_USER;
            _dbconn.DBPwd = Defines.DB_PASS;

            _dbconn.Connect();
        }
        protected virtual void Page_Unload(object sender, EventArgs e)
        {
            if (_dbconn != null)
            {
                _dbconn.Disconnect();
                _dbconn = null;
            }
        }
    }
}