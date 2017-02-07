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
    public partial class detailcounter : _classes.PageBase
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
                    string taskid = Request["taskid"];
                    string codcounter = Request["codcounter"];
                    string quantity = Request["quantity"];
                    int nTaskID = 0;
                    int nQuantity = 0;
                    int.TryParse(taskid, out nTaskID);
                    int.TryParse(quantity, out nQuantity);
                    strJson = "";
                    Response.Clear();
                    Response.ContentType = "text/json";

                    long dbDetail = DBConn.RunInsertQuery("insert into [Detail_Counter](Taskid, CodCounter, Quantity) values (@taskid, @codcounter, @quantity)",
                        new string[] {
                            "@taskid",
                            "@codcounter",
                            "@quantity"
                            },
                        new object[] {
                            nTaskID,
                            codcounter,
                            nQuantity
                            });
                    strJson = string.Format("{{\"result\": \"{0}\"}}", "success");
                    Response.Write(strJson);
                }
                catch (Exception ex)
                {
                    _classes.Logger.Log("detailcounter err: " + ex.Message);
                    _classes.Logger.Log(ex.StackTrace);
                }
            }
        }
    }
}