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
    public partial class posttintask : _classes.PageBase
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
                try
                {
                    /*
                    string userid = "";
                    string taskid = "";
                    string tasktype = "";
                    string RutaAbastecimiento = "";
                    string cus = "";
                    string nus = "";
                    string quantity = "";*/
                    string userid = Request["userid"];
                    string taskid = Request["taskid"];
                    string tasktype = Request["tasktype"];
                    string RutaAbastecimiento = Request["RutaAbastecimiento"];
                    string cus = Request["cus"];
                    string nus = Request["nus"];
                    string quantity = Request["quantity"];
                    /*
                    if (!string.IsNullOrEmpty(Request.Form["userid"]))
                    {
                        userid = Request.Form["userid"];
                    }
                    if (!string.IsNullOrEmpty(Request.Form["taskid"]))
                    {
                        taskid = Request.Form["taskid"];
                    }
                    if (!string.IsNullOrEmpty(Request.Form["tasktype"]))
                    {
                        tasktype = Request.Form["tasktype"];
                    }
                    if (!string.IsNullOrEmpty(Request.Form["RutaAbastecimiento"]))
                    {
                        RutaAbastecimiento = Request.Form["RutaAbastecimiento"];
                    }
                    if (!string.IsNullOrEmpty(Request.Form["cus"]))
                    {
                        cus = Request.Form["cus"];
                    }
                    if (!string.IsNullOrEmpty(Request.Form["nus"]))
                    {
                        nus = Request.Form["nus"];
                    }
                    if (!string.IsNullOrEmpty(Request.Form["quantity"]))
                    {
                        quantity = Request.Form["quantity"];
                    }
                    */
                    strJson = "";
                    Response.Clear();
                    Response.ContentType = "text/json";
                    //if (userid != "")
                    //{

                    if (tasktype == "1")
                    {
                        DataSet dbValue = DBConn.RunSelectQuery("select * from [SelectOptions] where Cus=@cus and OptionName=@option",
                            new string[] {
                    "@cus",
                    "@option"
                    },
                            new object[] {
                    cus,
                    nus
                    });
                        if (DataSetUtil.RowCount(dbValue) > 0)
                            quantity = DataSetUtil.RowStringValue(dbValue, "Value", 0);

                    }
                    DataSet dbduplicated = DBConn.RunSelectQuery("select * from [TaskDetail] where TaskType=@tasktype and TaskID=@taskid and CUS=@cus",
                        new string[]{
                        "@tasktype",
                        "@taskid",
                        "@cus"
                        },
                        new object[] {
                        tasktype,
                        taskid,
                        cus
                        });

                    if (DataSetUtil.RowCount(dbduplicated) == 0)
                    {

                        long dbComplete = DBConn.RunInsertQuery("insert into [TaskDetail](Taskid, TaskType, CUS, Quantity, RutaAbastecimiento, NUS) values (@taskid, @tasktype, @cus, @quantity, @RutaAbastecimiento, @nus)",
                            new string[] {
                                "@taskid",
                                "@tasktype",
                                "@cus",
                                "@quantity",
                                "@RutaAbastecimiento",
                                "@nus"
                                },
                            new object[] {
                                taskid,
                                tasktype,
                                cus,
                                quantity,
                                RutaAbastecimiento,
                                nus
                                });
                    }
                    strJson = string.Format("{{\"result\": \"{0}\"}}", "success");

                    //}else
                    //strJson = string.Format("{{\"result\": \"{0}\"}}", "fail");
                    Response.Write(strJson);

                }
                catch (Exception ex)
                {
                    _classes.Logger.Log("posttintask err: " + ex.Message);
                    _classes.Logger.Log(ex.StackTrace);
                }
            }
        }
    }
}