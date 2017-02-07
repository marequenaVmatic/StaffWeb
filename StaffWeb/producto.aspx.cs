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
    public partial class producto : _classes.PageBase
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
                string RutaAbastecimiento = Request["RutaAbastecimiento"];
                string Taskbusinesskey = Request["Taskbusinesskey"];
                string TaskType = Request["TaskType"];
                //int taskType = int.Parse(TaskType);
                //string strquery = "select NUS from Producto_RutaAbastecimiento pr inner join producto r on pr.cus=r.cus where RutaAbastecimiento='$pendingTask.RutaAbastecimiento' where $pendingTask.RutaAbastecimiento = pendingTask.Rutaabastecimiento";
                //DataSet dsProducto = DBConn.RunSelectQuery("select * from [Producto]");
                /*DataSet dsProducto = DBConn.RunSelectQuery("select NUS from [Producto_RutaAbastecimiento] pr inner join [Producto] r on pr.cus=r.cus where RutaAbastecimiento=@RutaAbastecimiento1 and TaskBusinessKey=@Taskbusinesskey1 and TaskType=@TaskType1", 
                    new string[] {
                        "@RutaAbastecimiento1",
                        "@Taskbusinesskey1",
                        "@TaskType1"
                    }, 
                    new object[] {
                    RutaAbastecimiento, 
                    Taskbusinesskey, 
                    TaskType
                    });*/
                DataSet dsProducto = DBConn.RunSelectQuery("select r.NUS, r.CUS from [Producto_RutaAbastecimiento] pr inner join [Producto] r on pr.cus=r.cus where RutaAbastecimiento=@RutaAbastecimiento1 and TaskBusinessKey=@Taskbusinesskey1 and TaskType=@TaskType1 ORDER BY r.CUS",
                    new string[] {
                    "@RutaAbastecimiento1",
                    "@Taskbusinesskey1",
                    "@TaskType1"
                    },
                    new object[] {
                RutaAbastecimiento,
                Taskbusinesskey,
                TaskType,
                    });
                Response.Clear();
                Response.ContentType = "text/json";

                strJson = "[";
                string strSpliter = "";

                for (int i = 0; i < DataSetUtil.RowCount(dsProducto); i++)
                {

                    string strCus = DataSetUtil.RowStringValue(dsProducto, "CUS", i);
                    string strNus = DataSetUtil.RowStringValue(dsProducto, "NUS", i);
                    strJson += strSpliter + string.Format("{{\"CUS\": \"{0}\", \"NUS\": \"{1}\"}}", strCus, strNus);
                    if (strSpliter == "") strSpliter = ",";

                }
                strJson += "]";
                Response.Write(strJson);
            }
        }
    }
}