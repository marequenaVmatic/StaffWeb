using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using DataAccess;
namespace StaffWeb
{
    public partial class login : _classes.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strJson = "";
            if (Int32.Parse(DateTime.Now.Hour.ToString()) >8)
            {
                strJson = string.Format("{{\"result\": \"{0}\"}}", "Fuerade Horario de sincronización");
                Response.Write(strJson);
            }
            else
            {
                string userid = Request["userid"];
                string password = Request["pwd"];
                DataSet dbUser = DBConn.RunSelectQuery("select * from [User] where userid=@userid and password=@password",
                    new string[] {
                    "@userid",
                    "@password"
                    },
                    new object[] {
                    userid,
                    password
                    });

                //string query = "select * from [User] where userid='";
                //query += userid;
                //query += "' and password='" + password + "'";
                //DataSet dbPending = DBConn.RunSelectQuery("select * from [User] where userid='userid' and password = 'password'");
                //DataSet dbUser = DBConn.RunSelectQuery(query);
                Response.Clear();
                Response.ContentType = "text/json";

                strJson = "";

                if (DataSetUtil.RowCount(dbUser) > 0)
                {
                    strJson += string.Format("{{\"result\": \"{0}\", \"userid\": \"{1}\", \"first_name\": \"{2}\", \"last_name\": \"{3}\"}}", "success", userid, DataSetUtil.RowStringValue(dbUser, "first_name", 0), DataSetUtil.RowStringValue(dbUser, "last_name", 0));
                }
                else
                {
                    strJson += string.Format("{{\"result\": \"{0}\"}}", "fail");
                }
                Response.Write(strJson);
            }
        }
    }
}