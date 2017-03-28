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
    public partial class dayly : _classes.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strJson = "";
            if (Int32.Parse(DateTime.Now.Hour.ToString())>21)
            {
                strJson = string.Format("{{\"result\": \"{0}\"}}", "Fuera de Horario de sincronización");
                Response.Write(strJson);
            }
            else
            {
                try
                {
                    string userid = Request["userid"];

                    strJson = "";
                    Response.Clear();
                    Response.ContentType = "text/json";

                    DataSet dbValue = DBConn.RunSelectQuery("select DISTINCT RutaAbastecimiento from [completedtask] where userid=@userid",
                            new string[] {
                            "@userid"
                            },
                            new object[] {
                            userid
                     });

                    if (DataSetUtil.RowCount(dbValue) > 0)
                    {
                        long dbComplete = DBConn.RunInsertQuery("insert into [dayly_journal](RutaAbastecimiento, taskdate, tasktype, completed, logdate) values (@RutaAbastecimiento, @taskdate, @tasktype, @completed, @logdate)",
                            new string[] {
                                "@RutaAbastecimiento",
                                "@taskdate",
                                "@tasktype",
                                "@completed",
                                "@logdate"
                                },
                            new object[] {
                                DataSetUtil.RowStringValue(dbValue, "RutaAbastecimiento", 0),
                                DateTime.Now.Date.ToString(),
                                4,
                                1,
                                ""
                                });
                    }

                    strJson = string.Format("{{\"result\": \"{0}\"}}", "success");
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