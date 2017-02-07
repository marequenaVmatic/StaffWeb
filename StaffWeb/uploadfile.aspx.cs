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
    public partial class uploadfile : _classes.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                HttpPostedFile fp = Request.Files["uploadedfile"];
                string targetDirectory = Request.PhysicalApplicationPath + "Upload";
                //targetDirectory = targetDirectory + "\\aaa.jpg";

                string file_name = fp.FileName;
                targetDirectory = targetDirectory + "\\" + file_name;
                try
                {
                    fp.SaveAs(targetDirectory);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                    return;
                }
                Response.Write("success");
            }
            catch (Exception ex)
            {
                _classes.Logger.Log("uploadfile err: " + ex.Message);
                _classes.Logger.Log(ex.StackTrace);
            }
        }
    }
}