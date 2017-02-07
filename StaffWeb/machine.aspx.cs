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
    public partial class machine : _classes.PageBase
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
                DataSet dbMachine = DBConn.RunSelectQuery("select mc.* from pendingTask p inner join Machine_Counter mc on mc.TaskBusinessKey=p.TaskBusinessKey where userid=@userid",
                     new string[] {
                    "@userid"
                    },
                    new object[] {
                    userid
                    });
                Response.Clear();
                Response.ContentType = "text/json";

                string strJsonMachine = "[";
                string strSpliter = "";

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
                strJsonMachine += "]";
                strJson = "";

                strJson = string.Format("{{\"result\": \"{0}\", \"machine\": {1}}}", "success", strJsonMachine);

                Response.Write(strJson);
            }
        }
    }
}