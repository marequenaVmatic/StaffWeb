using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using DataAccess;
using System.IO;

namespace StaffWeb
{
    public partial class logfile : _classes.PageBase
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
                    string taskid = "";
                    string capture_file = "";
                    string file_name = "";

                    if (!string.IsNullOrEmpty(Request.Form["taskid"]))
                    {
                        taskid = Request.Form["taskid"];
                    }
                    if (!string.IsNullOrEmpty(Request.Form["capture_file"]))
                    {
                        capture_file = Request.Form["capture_file"];
                    }
                    if (!string.IsNullOrEmpty(Request.Form["file_name"]))
                    {
                        file_name = Request.Form["file_name"];
                    }

                    DataSet dsCheck = DBConn.RunSelectQuery("SELECT * FROM log_file WHERE taskid={0} AND capture_file={1}",
                        new string[] { "@taskid", "@capture_file" }, new object[] { taskid, capture_file });
                    if (DataSetUtil.IsNullOrEmpty(dsCheck))
                    {
                        string strFileContent = "";
                        HttpPostedFile hpf = Request.Files["uploadedfile"];
                        if (hpf != null && hpf.ContentLength > 0)
                        {
                            string strDirPath = Request.PhysicalApplicationPath + "LogFile";
                            if (!Directory.Exists(strDirPath))
                                Directory.CreateDirectory(strDirPath);

                            string strFilePath = strDirPath + "\\" + capture_file + "_" + taskid + ".logfile";
                            hpf.SaveAs(strFilePath);

                            file_name = "/LogFile/" + capture_file + "_" + taskid + ".logfile";

                            using (StreamReader sr = new StreamReader(strFilePath))
                            {
                                strFileContent = sr.ReadToEnd();
                                if (capture_file.Contains("Spengler_ADM28"))
                                {
                                    string[] strPart = strFileContent.Split(new char[] { '\n' });
                                    for (int i = 0; i < strPart.Length; i++)
                                    {
                                        string strPartElement = strPart[i];
                                        string[] strArray = strPart[i].Split(new char[] { ' ' });
                                        if (taskid != "")
                                        {
                                            if (strArray.Length != 0)
                                            {
                                                String first = "";
                                                if (strArray.Length > 0) first = strArray[0];
                                                String second = "";
                                                if (strArray.Length > 1) second = strArray[1];
                                                String third = "";
                                                if (strArray.Length > 2) third = strArray[2];
                                                String forth = "";
                                                if (strArray.Length > 3) forth = strArray[3];
                                                long dbAdm28 = DBConn.RunInsertQuery("insert into [ADM28](position, category, counter, value, taskId) values (@position, @category, @counter, @value, @taskid)",
                                                    new string[] {
                                                    "@position",
                                                    "@category",
                                                    "@counter",
                                                    "@value",
                                                    "@taskid"
                                                    },
                                                    new object[] {
                                                    first,
                                                    second,
                                                    third,
                                                    forth,
                                                    taskid
                                                });
                                            }
                                        }
                                    }
                                }
                                if (capture_file.Contains("Spengler_OPN"))
                                {
                                    try
                                    {
                                        _classes.Logger.Log(string.Format("logfile params: {0}, {1}, {2}, {3}", taskid, capture_file, file_name, strFileContent));
                                        DBConn.RunStoreProcedure("SP_Parse_Parseo", new string[] { "@ISBN1", "@taskid" }, new object[] { strFileContent, taskid });
                                    }
                                    catch (Exception ex)
                                    {
                                        _classes.Logger.Log("logfile err: " + ex.Message);
                                        _classes.Logger.Log(ex.StackTrace);
                                    }
                                }
                                sr.Close();
                            }
                        }
                        /*
                        string taskid = Request["taskid"];
                        string capture_file = Request["capture_file"];
                        string file_name = Request["file_name"];
                        */
                        Response.Clear();
                        Response.ContentType = "text/json";
                        if (taskid != "")
                        {
                            long dbComplete = DBConn.RunInsertQuery("insert into [log_file](capture_file, file_name, taskid, file_content) values (@capture_file, @file_name, @taskid, @file_content)",
                                new string[] {
                                    "@capture_file",
                                    "@file_name",
                                    "@taskid",
                                    "@file_content"
                                    },
                                new object[] {
                                    capture_file,
                                    file_name,
                                    taskid,
                                    strFileContent
                                    });
                            strJson = string.Format("{{\"result\": \"{0}\"}}", "success");
                        }
                        else
                        {
                            strJson = string.Format("{{\"result\": \"{0}\"}}", "fail");
                        }
                    }
                    else
                    {
                        strJson = string.Format("{{\"result\": \"{0}\"}}", "success");
                    }
                    Response.Write(strJson);

                }
                catch (Exception ex)
                {
                    _classes.Logger.Log("logfile err: " + ex.Message);
                    _classes.Logger.Log(ex.StackTrace);
                }
            }   
        }
    }
}