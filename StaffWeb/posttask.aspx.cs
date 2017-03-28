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
    public partial class posttask : _classes.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                /*
                string userid = "";
                string taskid = "";
                string date = "";
                string tasktype = "";
                string RutaAbastecimiento = "";
                string TaskBusinessKey = "";
                string Customer = "";
                string Adress = "";
                string LocationDesc = "";
                string Model = "";
                string latitude = "";
                string longitude = "";
                string epv = "";
                string logLatitude = "";
                string logLongitude = "";
                string estMaq = "";
                string moned = "";
                string billeter = "";
                string tarjet = "";
                string nivAb = "";
                string higEx = "";
                string higIn = "";
                string atrSm = "";
                string atrSen = "";
                string atrIlu = "";
                string ActionDate = "";
                string MachineType = "";
                string count = "";
                int cn = 0;
                int.TryParse(count, out cn);
                string file1 = "";
                string file2 = "";
                string file3 = "";
                string file4 = "";
                string file5 = "";

                if (!string.IsNullOrEmpty(Request.Form["userid"]))
                {
                    userid = Request.Form["userid"];
                }
                if (!string.IsNullOrEmpty(Request.Form["taskid"]))
                {
                    taskid = Request.Form["taskid"];
                }
                if (!string.IsNullOrEmpty(Request.Form["date"]))
                {
                    date = Request.Form["date"];
                }
                if (!string.IsNullOrEmpty(Request.Form["tasktype"]))
                {
                    tasktype = Request.Form["tasktype"];
                }
                if (!string.IsNullOrEmpty(Request.Form["RutaAbastecimiento"]))
                {
                    RutaAbastecimiento = Request.Form["RutaAbastecimiento"];
                }
                if (!string.IsNullOrEmpty(Request.Form["TaskBusinessKey"]))
                {
                    TaskBusinessKey = Request.Form["TaskBusinessKey"];
                }
                if (!string.IsNullOrEmpty(Request.Form["Customer"]))
                {
                    Customer = Request.Form["Customer"];
                }
                if (!string.IsNullOrEmpty(Request.Form["Adress"]))
                {
                    Adress = Request.Form["Adress"];
                }
                if (!string.IsNullOrEmpty(Request.Form["LocationDesc"]))
                {
                    LocationDesc = Request.Form["LocationDesc"];
                }
                if (!string.IsNullOrEmpty(Request.Form["Model"]))
                {
                    Model = Request.Form["Model"];
                }
                if (!string.IsNullOrEmpty(Request.Form["latitude"]))
                {
                    latitude = Request.Form["latitude"];
                }
                if (!string.IsNullOrEmpty(Request.Form["longitude"]))
                {
                    longitude = Request.Form["longitude"];
                }
                if (!string.IsNullOrEmpty(Request.Form["epv"]))
                {
                    epv = Request.Form["epv"];
                }
                if (!string.IsNullOrEmpty(Request.Form["logLatitude"]))
                {
                    logLatitude = Request.Form["logLatitude"];
                }
                if (!string.IsNullOrEmpty(Request.Form["logLongitude"]))
                {
                    logLongitude = Request.Form["logLongitude"];
                }
                if (!string.IsNullOrEmpty(Request.Form["estMaq"]))
                {
                    estMaq = Request.Form["estMaq"];
                }
                if (!string.IsNullOrEmpty(Request.Form["moned"]))
                {
                    moned = Request.Form["moned"];
                }
                if (!string.IsNullOrEmpty(Request.Form["billeter"]))
                {
                    billeter = Request.Form["billeter"];
                }
                if (!string.IsNullOrEmpty(Request.Form["tarjet"]))
                {
                    tarjet = Request.Form["tarjet"];
                }
                if (!string.IsNullOrEmpty(Request.Form["nivAb"]))
                {
                    nivAb = Request.Form["nivAb"];
                }
                if (!string.IsNullOrEmpty(Request.Form["higEx"]))
                {
                    higEx = Request.Form["higEx"];
                }
                if (!string.IsNullOrEmpty(Request.Form["higIn"]))
                {
                    higIn = Request.Form["higIn"];
                }
                if (!string.IsNullOrEmpty(Request.Form["atrSm"]))
                {
                    atrSm = Request.Form["atrSm"];
                }
                if (!string.IsNullOrEmpty(Request.Form["atrSen"]))
                {
                    atrSen = Request.Form["atrSen"];

                }
                if (!string.IsNullOrEmpty(Request.Form["atrIlu"]))
                {
                    atrIlu = Request.Form["atrIlu"];
                }
                if (!string.IsNullOrEmpty(Request.Form["ActionDate"]))
                {
                    ActionDate = Request.Form["ActionDate"];
                }
                if (!string.IsNullOrEmpty(Request.Form["MachineType"]))
                {
                    MachineType = Request.Form["MachineType"];
                }
                if (!string.IsNullOrEmpty(Request.Form["count"]))
                {
                    count = Request.Form["count"];
                    int.TryParse(count, out cn);
                }
                if (!string.IsNullOrEmpty(Request.Form["atrSen"]))
                {
                    atrSen = Request.Form["atrSen"];

                }

                if(cn > 0 ){
                    if (!string.IsNullOrEmpty(Request.Form["file1"]))
                    {
                        file1 = Request.Form["file1"];
                    }
                }
                if (cn > 1){
                    if (!string.IsNullOrEmpty(Request.Form["file2"]))
                    {
                        file2 = Request.Form["file2"];
                    }
                }
                if (cn > 2){
                    if(!string.IsNullOrEmpty(Request.Form["file3"]))
                    {
                        file3 = Request.Form["file3"];
                    }
                }
                if (cn > 3){
                    if(!string.IsNullOrEmpty(Request.Form["file4"]))
                    {
                        file4 = Request.Form["file4"];
                    }
                }
                if (cn > 4){
                    if(!string.IsNullOrEmpty(Request.Form["file5"]))
                    {
                        file5 = Request.Form["file5"];
                    }
                }*/
                System.DateTime moment = new System.DateTime();
                string strJson = "";
                if (Int32.Parse(DateTime.Now.Hour.ToString()) >21)
                {
                    strJson = string.Format("{{\"result\": \"{0}\"}}", "Fuerade Horario de sincronización");
                    Response.Write(strJson);
                }

                string userid = Request["userid"];
                string taskid = Request["taskid"];
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
                string logLatitude = Request["logLatitude"];
                string logLongitude = Request["logLongitude"];
                string ActionDate = Request["ActionDate"];
                string MachineType = Request["MachineType"];
                string count = Request["count"];
                int cn = 0;
                int.TryParse(count, out cn);
                string file1 = Request["file1"];
                string file2 = Request["file2"];
                string file3 = Request["file3"];
                string file4 = Request["file4"];
                string file5 = Request["file5"];
                 strJson = "";
                string signature = Request["Signature"];
                string NumeroGuia = Request["NumeroGuia"];
                string Glosa = Request["Glosa"];
                string Aux_valor1 = Request["Aux_valor1"];
                string Aux_valor2 = Request["Aux_valor2"];
                string Aux_valor3 = Request["Aux_valor3"];
                string Aux_valor4 = Request["Aux_valor4"];
                string Aux_valor5 = Request["Aux_valor5"];
                string Aux_valor6 = Request["Aux_valor6"];
                string QuantityResumen = Request["QuantityResumen"];
                string tipo_error_captura = Request["tipo_error_captura"];
                int iCompleted = 1;
                if (string.IsNullOrEmpty(Request["Completed"]) || !int.TryParse(Request["Completed"], out iCompleted) || iCompleted < 0 || iCompleted > 1)
                    iCompleted = 1;
                string strComment = Request["Comment"];
                
                Response.Clear();
                Response.ContentType = "text/json";
                DataSet dbCompleted = DBConn.RunSelectQuery("select * from [completedTask] where userid=@userid and TaskID=@taskid",
                    new string[]{
                        "@userid", 
                        "@taskid"
                    },
                    new object[] {
                        userid, 
                        taskid
                    });

                if (DataSetUtil.RowCount(dbCompleted) == 0) { 
                    DBConn.RunStoreProcedure("SP_Complete_Task", new string[] {
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
                        "@logLatitude", 
                        "@logLongitude",
                        "@ActionDate",
                        "@file1",
                        "@file2", 
                        "@file3",
                        "@file4",
                        "@file5",
                        "@MachineType",
                        "@taskid", 
                        "@signature", 
                        "@NumeroGuia", 
                        "@Glosa", 
                        "@Aux_valor1", 
                        "@Aux_valor2", 
                        "@Aux_valor3", 
                        "@Aux_valor4", 
                        "@Aux_valor5",
                        "@Aux_valor6", 
                        "@Completed",
                        "@Comment", 
                        "@QuantityResumen", 
                        "@tipo_error_captura"
                    }, new object[] {
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
                        logLatitude,
                        logLongitude, 
                        ActionDate,
                        file1,
                        file2, 
                        file3,
                        file4, 
                        file5,
                        MachineType,
                        taskid, 
                        signature, 
                        NumeroGuia, 
                        Glosa, 
                        Aux_valor1, 
                        Aux_valor2, 
                        Aux_valor3, 
                        Aux_valor4, 
                        Aux_valor5,
                        Aux_valor6,
                        iCompleted,
                        strComment, 
                        QuantityResumen, 
                        tipo_error_captura
                    });

                    DBConn.RunDeleteQuery("delete from [pendingTask] where userid=@userid and TaskID=@taskid",
                            new string[] {
                            "@userid",
                            "@taskid"
                        },
                            new object[] {
                            userid,
                            taskid
                        });
                }
                strJson += string.Format("{{\"result\": \"{0}\"}}", "success");
                // 업로드 성공
                Response.Write(strJson);
            }
            catch (Exception ex)
            {
                _classes.Logger.Log("posttask err: " + ex.Message);
                _classes.Logger.Log(ex.StackTrace);
            }
        }
    }
}