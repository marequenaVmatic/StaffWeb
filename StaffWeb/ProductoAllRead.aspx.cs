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
    public partial class ProductoAllRead : _classes.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strJson = "";
            if (Int32.Parse(DateTime.Now.Hour.ToString())>21)
            {
                strJson = string.Format("{{\"result\": \"{0}\"}}", "Fuerade Horario de sincronización");
                Response.Write(strJson);
            }
            else
            {
                DataSet dsCategory = DBConn.RunSelectQuery("select * from [SelectOptions]");

                Response.Clear();
                Response.ContentType = "text/json";

                strJson = "[";
                string strSpliter = "";

                for (int i = 0; i < DataSetUtil.RowCount(dsCategory); i++)
                {

                    string strid = DataSetUtil.RowStringValue(dsCategory, "idOption", i);
                    string strCategory = DataSetUtil.RowStringValue(dsCategory, "OptionName", i);
                    strJson += strSpliter + string.Format("{{\"id\": \"{0}\", \"category\": \"{1}\"}}", strid, strCategory);
                    if (strSpliter == "") strSpliter = ",";

                }
                strJson += "]";
                Response.Write(strJson);
            }
        }
    }
}