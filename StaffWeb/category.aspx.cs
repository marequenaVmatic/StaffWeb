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
    public partial class category : _classes.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string userid = Request["userid"];

            string strJson = "";
            if (Int32.Parse(DateTime.Now.Hour.ToString())>21)
            {
                strJson = string.Format("{{\"result\": \"{0}\"}}", "Fuerade Horario de sincronización");
                Response.Write(strJson);
            }
            else
            {
                DataSet dsCategory = DBConn.RunSelectQuery("select * from [SelectOptions]");
                DataSet dsProducto = DBConn.RunSelectQuery("select * from [Producto]");
                //DataSet dsProducto_Ruta = DBConn.RunSelectQuery("select * from [Producto_Rutaabastecimiento]");
                DataSet dsProducto_Ruta = DBConn.RunSelectQuery("SP_Producto_RutaAbastecimiento @userid",
                    new string[]{
                    "@userid"
                    },
                    new object[]{
                userid
                    });
                /*DataSet dsProducto_Ruta = DBConn.RunSelectQuery("select pr.* from producto_Rutaabastecimiento pr inner join PendingTask pt on pt.RutaAbastecimiento=pr.RutaAbastecimiento and pt.TaskType=pr.TaskType and pr.TaskBusinessKey=pt.TaskBusinessKey inner join Producto p on p.Cus=pr.Cus where pt.userid=@userid", 
                    new string[] {
                        "@userid"
                    }, 
                    new object[] {
                        userid
                    });*/
                DataSet dsUser = DBConn.RunSelectQuery("select * from [User]");
                DataSet dbTaskType = DBConn.RunSelectQuery("select * from [Task]");
                //DataSet dbMachine = DBConn.RunSelectQuery("select * from [Machine_Counter]");
                /*DataSet dbMachine = DBConn.RunSelectQuery("select * from [Machine_Counter] where TaskBusinessKey=@task",
                     new string[] {
                        "@task"
                    },
                    new object[] {
                        "MVL1300-7"
                    });*/
                Response.Clear();
                Response.ContentType = "text/json";
                string strJsonCategory = "[";
                string strSpliter = "";

                for (int i = 0; i < DataSetUtil.RowCount(dsCategory); i++)
                {

                    string strid = DataSetUtil.RowStringValue(dsCategory, "idOption", i);
                    string strCategory = DataSetUtil.RowStringValue(dsCategory, "OptionName", i);
                    strJsonCategory += strSpliter + string.Format("{{\"id\": \"{0}\", \"category\": \"{1}\"}}", strid, strCategory);
                    if (strSpliter == "") strSpliter = ",";

                }
                strJsonCategory += "]";

                string strJsonProducto = "[";
                strSpliter = "";

                for (int i = 0; i < DataSetUtil.RowCount(dsProducto); i++)
                {

                    string strCUS = DataSetUtil.RowStringValue(dsProducto, "CUS", i);
                    string strNUS = DataSetUtil.RowStringValue(dsProducto, "NUS", i);
                    string strMAXP = DataSetUtil.RowStringValue(dsProducto, "MAXP", i);
                    strJsonProducto += strSpliter + string.Format("{{\"CUS\": \"{0}\", \"NUS\": \"{1}\", \"MAXP\": \"{2}\"}}", strCUS, strNUS,strMAXP);
                    if (strSpliter == "") strSpliter = ",";

                }
                strJsonProducto += "]";

                string strJsonProducto_Ruta = "[";
                strSpliter = "";

                for (int i = 0; i < DataSetUtil.RowCount(dsProducto_Ruta); i++)
                {

                    string strCUS = DataSetUtil.RowStringValue(dsProducto_Ruta, "CUS", i);
                    string strRuta = DataSetUtil.RowStringValue(dsProducto_Ruta, "RutaAbastecimiento", i);
                    string strTaskBusinessKey = DataSetUtil.RowStringValue(dsProducto_Ruta, "TaskBusinessKey", i);
                    string strTaskType = DataSetUtil.RowStringValue(dsProducto_Ruta, "TaskType", i);
                    strJsonProducto_Ruta += strSpliter + string.Format("{{\"CUS\": \"{0}\", \"RutaAbastecimiento\": \"{1}\", \"TaskBusinessKey\": \"{2}\", \"TaskType\": \"{3}\"}}", strCUS, strRuta, strTaskBusinessKey, strTaskType);
                    if (strSpliter == "") strSpliter = ",";

                }
                strJsonProducto_Ruta += "]";

                string strJsonUser = "[";
                strSpliter = "";

                for (int i = 0; i < DataSetUtil.RowCount(dsUser); i++)
                {

                    string strUserid = DataSetUtil.RowStringValue(dsUser, "userid", i);
                    string strPassword = DataSetUtil.RowStringValue(dsUser, "password", i);
                    strJsonUser += strSpliter + string.Format("{{\"userid\": \"{0}\", \"password\": \"{1}\", \"first_name\": \"{2}\", \"last_name\": \"{3}\"}}", strUserid, strPassword,
                        DataSetUtil.RowStringValue(dsUser, "first_name", i),
                        DataSetUtil.RowStringValue(dsUser, "last_name", i));
                    if (strSpliter == "") strSpliter = ",";

                }
                strJsonUser += "]";

                string strJsonType = "[";
                strSpliter = "";

                for (int i = 0; i < DataSetUtil.RowCount(dbTaskType); i++)
                {

                    string strtype = DataSetUtil.RowStringValue(dbTaskType, "TaskType", i);
                    string strName = DataSetUtil.RowStringValue(dbTaskType, "TaskName", i);
                    strJsonType += strSpliter + string.Format("{{\"type\": \"{0}\", \"name\": \"{1}\"}}", strtype, strName);
                    if (strSpliter == "") strSpliter = ",";

                }
                strJsonType += "]";
                /*
                string strJsonMachine = "[";
                strSpliter = "";

                for (int i = 0; i < DataSetUtil.RowCount(dbMachine); i++)
                {

                    string strTaskBusinessKey = DataSetUtil.RowStringValue(dbMachine, "TaskBusinessKey", i);
                    string strCodContador = DataSetUtil.RowStringValue(dbMachine, "CodContador", i);
                    string strStartValue = DataSetUtil.RowIntValue(dbMachine, "StartValue", i).ToString();
                    string strEndValue = DataSetUtil.RowIntValue(dbMachine, "EndValue", i).ToString();
                    string strStartDate = DataSetUtil.RowDateTime(dbMachine, "StartDate", i).ToString();
                    string strEndDate = DataSetUtil.RowDateTime(dbMachine, "EndDate", i).ToString();

                    strJsonMachine += strSpliter + string.Format("{{\"TaskBusinessKey\": \"{0}\", \"CodContador\": \"{1}\", \"StartValue\": \"{2}\", \"EndValue\": \"{3}\", \"StartDate\": \"{4}\", \"EndDate\": \"{5}\"}}", strTaskBusinessKey, strCodContador, strStartValue, strEndValue, strStartDate, strEndDate);
                    if (strSpliter == "") strSpliter = ",";

                }
                strJsonMachine += "]";*/

                //strJson = string.Format("{{\"result\": \"{0}\", \"category\": {1}, \"producto\": {2}, \"producto_ruta\": {3}, \"user\": {4}, \"tasktype\": {5}, \"machine\": {6}}}", "success", strJsonCategory, strJsonProducto, strJsonProducto_Ruta, strJsonUser, strJsonType, strJsonMachine);
                strJson = string.Format("{{\"result\": \"{0}\", \"category\": {1}, \"producto\": {2}, \"producto_ruta\": {3}, \"user\": {4}, \"tasktype\": {5}}}", "success", strJsonCategory, strJsonProducto, strJsonProducto_Ruta, strJsonUser, strJsonType);

                Response.Write(strJson);
            }
        }
    }
}