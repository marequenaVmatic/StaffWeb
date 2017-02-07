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
    public partial class report : _classes.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string userId = Request["userId"];
            string strJson = "";
            if (Int32.Parse(DateTime.Now.Hour.ToString()) >8)
            {
                strJson = string.Format("{{\"result\": \"{0}\"}}", "Fuerade Horario de sincronización");
                Response.Write(strJson);
            }
            else
            {
                DataSet dsProducto = DBConn.RunSelectQuery("select NUS, Quantity=sum(1*quantity) from [taskdetail] td inner join completedtask c on c.taskid=td.taskid where c.userid=@userid and quantity>@quantity GROUP BY nus ORDER BY sum(1*quantity)  desc",
                new string[] {
                    "@userid",
                    "@quantity"
                },
                new object[] {
                userId,
                "0"
                });
                Response.Clear();
                Response.ContentType = "text/json";

                strJson = "[";
                string strSpliter = "";

                for (int i = 0; i < DataSetUtil.RowCount(dsProducto); i++)
                {

                    string strNus = DataSetUtil.RowStringValue(dsProducto, "NUS", i);
                    string strQuantity = DataSetUtil.RowStringValue(dsProducto, "Quantity", i);
                    strJson += strSpliter + string.Format("{{\"NUS\": \"{0}\", \"Quantity\": \"{1}\"}}", strNus, strQuantity);
                    if (strSpliter == "") strSpliter = ",";

                }
                strJson += "]";
                Response.Write(strJson);
            }
        }
    }
}