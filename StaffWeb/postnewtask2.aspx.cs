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
    public partial class postnewtask2 : _classes.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string userid = "1099".ToString();
            string date = "2016-12-07".ToString();
            string tasktype = "4";
            string RutaAbastecimiento = "120";
            string TaskBusinessKey = "Paula Tomate un armonil";
            string Customer ="";
            string Adress = "";
            string LocationDesc = "";
            string Model = "";
            decimal latitude = 0;
            decimal longitude = 0;
            string epv = "";
            string MachineType = "";
            string Aux_valor1 = "";
            string Aux_valor2 = "";
            string Aux_valor3 = "";
            string Aux_valor4 = "";
            string Aux_valor5 = "";
            string Aux_valor6 = "";

            string strJson = string.Format("{{\"result\": \"{0}\"}}", "failed");
            DataSet lNewTaskID =   DBConn.RunStoreProcedure("sp_Post_New_Task", new string[] {
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