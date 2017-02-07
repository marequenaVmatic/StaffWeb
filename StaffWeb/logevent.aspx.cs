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
    public partial class logevent : _classes.PageBase
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
                    string userid = Request["userid"];
                    string taskid = Request["taskid"];
                    string datetime = Request["datetime"];
                    string description = Request["description"];
                    string latitude = Request["latitude"];
                    string longitude = Request["longitude"];

                    string batteryPercent = Request["batteryPercent"];
                    string freespace = Request["freespace"];
                    string isChargingUSB = Request["isChargingUSB"];
                    string isChargingOther = Request["isChargingOther"];
                    int iIsChargingUSB = 0;
                    int iIsChargingOther = 0;
                    if (string.IsNullOrEmpty(isChargingUSB) || !int.TryParse(isChargingUSB, out iIsChargingUSB))
                        iIsChargingUSB = 0;
                    if (string.IsNullOrEmpty(isChargingOther) || !int.TryParse(isChargingOther, out iIsChargingOther))
                        iIsChargingOther = 0;
                    if (string.IsNullOrEmpty(batteryPercent))
                        batteryPercent = "";
                    if (string.IsNullOrEmpty(freespace))
                        freespace = "";

                    //_classes.Logger.Log(string.Format("logevent params: {0}, {1}, {2}, {3}, {4}, {5}", userid, taskid, datetime, description, latitude, longitude));

                    strJson = "";
                    Response.Clear();
                    Response.ContentType = "text/json";

                    long dbComplete = DBConn.RunInsertQuery("insert into [log_event](userid, datetime, description, latitude, longitude, taskid, phn_battery_level, phn_freespace, phn_is_oncharge_usb, phn_is_oncharge_other) values (@userid, @datetime, @description, @latitude, @longitude, @taskid, @phn_battery_level, @phn_freespace, @phn_is_oncharge_usb, @phn_is_oncharge_other)",
                        new string[] {
                                "@userid",
                                "@datetime",
                                "@description",
                                "@latitude",
                                "@longitude",
                                "@taskid",
                                "@phn_battery_level",
                                "@phn_freespace",
                                "@phn_is_oncharge_usb",
                                "@phn_is_oncharge_other"
                                },
                        new object[] {
                                userid,
                                datetime,
                                description,
                                latitude,
                                longitude,
                                taskid,
                                batteryPercent,
                                freespace,
                                iIsChargingUSB,
                                iIsChargingOther
                                });
                    strJson = string.Format("{{\"result\": \"{0}\"}}", "success");

                    Response.Write(strJson);
                }
                catch (Exception ex)
                {
                    _classes.Logger.Log("logevent err: " + ex.Message);
                    _classes.Logger.Log(ex.StackTrace);
                }
            }
        }
    }
}