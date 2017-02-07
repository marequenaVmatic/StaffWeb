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
    public partial class postnewtask : _classes.PageBase
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
                string date = Request["date"];
                string tasktype = Request["tasktype"];
                string RutaAbastecimiento = Request["RutaAbastecimiento"];
                string TaskBusinessKey = Request["TaskBusinessKey"];
                string Customer = Request["Customer"];
                string Adress = Request["Adress"];
                string LocationDesc = Request["LocationDesc"];
                string Model = Request["Model"];
                string latitude = Request["latitude"];
                string longitude = Request["longitude"];
                string epv = Request["epv"];
                string MachineType = Request["MachineType"];
                string Aux_valor1 = Request["Aux_valor1"];
                string Aux_valor2 = Request["Aux_valor2"];
                string Aux_valor3 = Request["Aux_valor3"];
                string Aux_valor4 = Request["Aux_valor4"];
                string Aux_valor5 = Request["Aux_valor5"];
                string Aux_valor6 = Request["Aux_valor6"];

                strJson = string.Format("{{\"result\": \"{0}\"}}", "failed");
                DataSet lNewTaskID = DBConn.RunStoreProcedure("sp_Post_New_Task", new string[] {
                                "@userid",
                                "@date",
                                "@tasktype",
                                "@RutaAbastecimiento",
                                "@TaskBusinessKey",
                                "@Customer",
                                "@Adress",
                                "@LocationDesc",
                                "@Model",
                                "@Latitude",
                                "@Longitude",
                                "@epv",
                                "@MachineType",
                                "@Aux_valor1",
                                "@Aux_valor2",
                                "@Aux_valor3",
                                "@Aux_valor4",
                                "@Aux_valor5",
                                "@Aux_valor6"
                            },
                                new object[] {
                                userid,
                                date,
                                tasktype,
                                RutaAbastecimiento,
                                TaskBusinessKey,
                                Customer,
                                Adress,
                                LocationDesc,
                                Model,
                                latitude,
                                longitude,
                                epv,
                                MachineType,
                                Aux_valor1,
                                Aux_valor2,
                                Aux_valor3,
                                Aux_valor4,
                                Aux_valor5,
                                Aux_valor6
                                });

                if (lNewTaskID.Tables[0].Rows[0][0].ToString() != "")
                {
                    strJson = string.Format("{{\"result\": \"{0}\", \"taskid\": {1}}}", "success", lNewTaskID.Tables[0].Rows[0][0].ToString());
                }

                Response.Write(strJson);
            }
        }
    }
}