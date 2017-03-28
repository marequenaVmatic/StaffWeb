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
    public partial class task : _classes.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string userid = Request["userid"];
            DataSet dbPending = DBConn.RunSelectQuery("SELECT * FROM [pendingTask]"); 
            DataSet dbCompleted = DBConn.RunSelectQuery("SELECT * FROM [completedTask]");
            DataSet dbTin = DBConn.RunSelectQuery("SELECT taskid,tasktype,cus,quantity,rutaabastecimiento, nus FROM TaskDetail GROUP BY taskid,tasktype,cus,quantity,rutaabastecimiento, nus");
            DataSet dbDetail = DBConn.RunSelectQuery("SELECT * FROM [Detail_Counter]");
            DataSet dbError = DBConn.RunSelectQuery("SELECT * FROM [spinner_tipo_error]");

            Response.Clear();
            Response.ContentType = "text/json";

            
            string strJsonPending = "[";
            string strSpliter = "";
            string strJson = "";

            if (Int32.Parse(DateTime.Now.Hour.ToString())>21)
            {
                strJson = string.Format("{{\"result\": \"{0}\"}}", "Fuerade Horario de sincronización");
                Response.Write(strJson);
            }
            else
            {
                if (DataSetUtil.RowCount(dbPending) >= 0)
                {
                    for (int i = 0; i < DataSetUtil.RowCount(dbPending); i++)
                    {
                        if (DataSetUtil.RowStringValue(dbPending, "userid", i) == userid)
                        {
                            string strTaskid = DataSetUtil.RowStringValue(dbPending, "TaskID", i);
                            string strDate = DataSetUtil.RowStringValue(dbPending, "date", i);
                            string strTaskType = DataSetUtil.RowStringValue(dbPending, "TaskType", i);
                            string strRutaAbastecimiento = DataSetUtil.RowStringValue(dbPending, "RutaAbastecimiento", i);
                            string strTaskBusinessKey = DataSetUtil.RowStringValue(dbPending, "TaskBusinessKey", i);
                            string strCustomer = DataSetUtil.RowStringValue(dbPending, "Customer", i);
                            string strAdress = DataSetUtil.RowStringValue(dbPending, "Adress", i);
                            string strLocationDec = DataSetUtil.RowStringValue(dbPending, "LocationDesc", i);
                            string strModel = DataSetUtil.RowStringValue(dbPending, "Model", i);
                            string strLatitude = DataSetUtil.RowStringValue(dbPending, "Latitude", i);
                            string strLongitude = DataSetUtil.RowStringValue(dbPending, "longitude", i);
                            string strEPV = DataSetUtil.RowStringValue(dbPending, "EPV", i);
                            string strMachinType = DataSetUtil.RowStringValue(dbPending, "MachineType", i);
                            string strAux_valor1 = DataSetUtil.RowStringValue(dbPending, "Aux_valor1", i);
                            string strAux_valor2 = DataSetUtil.RowStringValue(dbPending, "Aux_valor2", i);
                            string strAux_valor3 = DataSetUtil.RowStringValue(dbPending, "Aux_valor3", i);
                            string strAux_valor4 = DataSetUtil.RowStringValue(dbPending, "Aux_valor4", i);
                            string strAux_valor5 = DataSetUtil.RowStringValue(dbPending, "Aux_valor5", i);
                            string strAux_valor6 = DataSetUtil.RowStringValue(dbPending, "Aux_valor6", i);
                            strJsonPending += strSpliter + string.Format("{{\"TaskID\": \"{0}\", \"date\": \"{1}\", \"TaskType\": \"{2}\", \"RutaAbastecimiento\": \"{3}\", \"TaskBusinessKey\": \"{4}\", \"Customer\": \"{5}\", \"Adress\": \"{6}\", \"LocationDesc\": \"{7}\", \"Model\": \"{8}\", \"Latitude\": \"{9}\", \"Longitude\": \"{10}\", \"EPV\": \"{11}\", \"MachineType\": \"{12}\", \"userid\": \"{13}\", \"Aux_valor1\": \"{14}\", \"Aux_valor2\": \"{15}\", \"Aux_valor3\": \"{16}\", \"Aux_valor4\": \"{17}\", \"Aux_valor5\": \"{18}\", \"Aux_valor6\": \"{19}\"}}", strTaskid, strDate, strTaskType, strRutaAbastecimiento, strTaskBusinessKey, strCustomer, strAdress, strLocationDec, strModel, strLatitude, strLongitude, strEPV, strMachinType, userid, strAux_valor1, strAux_valor2, strAux_valor3, strAux_valor4, strAux_valor5, strAux_valor6);
                            if (strSpliter == "") strSpliter = ",";

                        }
                    }
                    strJsonPending += "]";
                }
                else
                {
                    strJsonPending += string.Format("{{\"resultpending\": \"{0}\"}}", "fail");
                    strJsonPending += "]";
                }
                strSpliter = "";
                string strJsonCompleted = "[";
                string strSpliterCompleted = "";
                List<string> arrCompletedTaskID = new List<string>();
                if (DataSetUtil.RowCount(dbCompleted) >= 0)
                {
                    for (int i = 0; i < DataSetUtil.RowCount(dbCompleted); i++)
                    {
                        if (DataSetUtil.RowStringValue(dbCompleted, "userid", i) == userid)
                        {
                            string strTaskid = DataSetUtil.RowStringValue(dbCompleted, "TaskID", i);
                            arrCompletedTaskID.Add(strTaskid);
                            string strDate = DataSetUtil.RowStringValue(dbCompleted, "date", i);
                            string strTaskType = DataSetUtil.RowStringValue(dbCompleted, "TaskType", i);
                            string strRutaAbastecimiento = DataSetUtil.RowStringValue(dbCompleted, "RutaAbastecimiento", i);
                            string strTaskBusinessKey = DataSetUtil.RowStringValue(dbCompleted, "TaskBusinessKey", i);
                            string strCustomer = DataSetUtil.RowStringValue(dbCompleted, "Customer", i);
                            string strAdress = DataSetUtil.RowStringValue(dbCompleted, "Adress", i);
                            string strLocationDec = DataSetUtil.RowStringValue(dbCompleted, "LocationDesc", i);
                            string strModel = DataSetUtil.RowStringValue(dbCompleted, "Model", i);
                            string strLatitude = DataSetUtil.RowStringValue(dbCompleted, "Latitude", i);
                            string strLongitude = DataSetUtil.RowStringValue(dbCompleted, "longitude", i);
                            string strEPV = DataSetUtil.RowStringValue(dbCompleted, "EPV", i);
                            string strLogLatitude = DataSetUtil.RowStringValue(dbCompleted, "logLatitude", i);
                            string strLogLongitude = DataSetUtil.RowStringValue(dbCompleted, "logLongitude", i);
                            string ActionDate = DataSetUtil.RowStringValue(dbCompleted, "ActionDate", i);
                            string image1 = DataSetUtil.RowStringValue(dbCompleted, "image1", i);
                            string image2 = DataSetUtil.RowStringValue(dbCompleted, "image2", i);
                            string image3 = DataSetUtil.RowStringValue(dbCompleted, "image3", i);
                            string image4 = DataSetUtil.RowStringValue(dbCompleted, "image4", i);
                            string image5 = DataSetUtil.RowStringValue(dbCompleted, "image5", i);
                            string strMachinType = DataSetUtil.RowStringValue(dbCompleted, "MachineType", i);
                            string signature = DataSetUtil.RowStringValue(dbCompleted, "Signature", i);
                            string NumeroGuia = DataSetUtil.RowStringValue(dbCompleted, "NumeroGuia", i);
                            string Glosa = DataSetUtil.RowStringValue(dbCompleted, "Glosa", i);
                            string Aux_valor1 = DataSetUtil.RowStringValue(dbCompleted, "Aux_valor1", i);
                            string Aux_valor2 = DataSetUtil.RowStringValue(dbCompleted, "Aux_valor2", i);
                            string Aux_valor3 = DataSetUtil.RowStringValue(dbCompleted, "Aux_valor3", i);
                            string Aux_valor4 = DataSetUtil.RowStringValue(dbCompleted, "Aux_valor4", i);
                            string Aux_valor5 = DataSetUtil.RowStringValue(dbCompleted, "Aux_valor5", i);
                            string Aux_valor6 = DataSetUtil.RowStringValue(dbCompleted, "Aux_valor6", i);
                            int iCompleted = DataSetUtil.RowIntValue(dbCompleted, "Completed", i);
                            string strComment = DataSetUtil.RowStringValue(dbCompleted, "Comment", i);
                            string strQuantityResumen = DataSetUtil.RowStringValue(dbCompleted, "QuantityResumen", i);
                            string tipo_error_captura = DataSetUtil.RowStringValue(dbCompleted, "tipo_error_captura", i);
                            strJsonCompleted += strSpliterCompleted + string.Format("{{\"TaskID\": \"{0}\", \"date\": \"{1}\", \"TaskType\": \"{2}\", \"RutaAbastecimiento\": \"{3}\", \"TaskBusinessKey\": \"{4}\", \"Customer\": \"{5}\", \"Adress\": \"{6}\", \"LocationDesc\": \"{7}\", \"Model\": \"{8}\", \"Latitude\": \"{9}\", \"Longitude\": \"{10}\", \"EPV\": \"{11}\", \"logLatitude\": \"{12}\", \"logLongitude\": \"{13}\", \"ActionDate\": \"{14}\", \"image1\": \"{15}\", \"image2\": \"{16}\", \"image3\": \"{17}\", \"image4\": \"{18}\", \"image5\": \"{19}\", \"MachineType\": \"{20}\", \"Signature\": \"{21}\", \"NumeroGuia\": \"{22}\", \"Glosa\": \"{23}\", \"Aux_valor1\": \"{24}\", \"Aux_valor2\": \"{25}\", \"Aux_valor3\": \"{26}\", \"Aux_valor4\": \"{27}\", \"Aux_valor5\": \"{28}\", \"Completed\": {29}, \"Comment\": \"{30}\", \"Aux_valor6\": \"{31}\", \"QuantityResumen\": \"{32}\", \"tipo_error_captura\": \"{33}\"}}", strTaskid, strDate, strTaskType, strRutaAbastecimiento, strTaskBusinessKey, strCustomer, strAdress, strLocationDec, strModel, strLatitude, strLongitude, strEPV, strLogLatitude, strLogLongitude, ActionDate, image1, image2, image3, image4, image5, strMachinType, signature, NumeroGuia, Glosa, Aux_valor1, Aux_valor2, Aux_valor3, Aux_valor4, Aux_valor5, iCompleted, strComment, Aux_valor6, strQuantityResumen, tipo_error_captura);
                            if (strSpliterCompleted == "") strSpliterCompleted = ",";
                        }
                    }
                    strJsonCompleted += "]";
                }
                else
                {
                    strJsonCompleted += string.Format("{{\"resultcomplete\": \"{0}\"}}", "fail");
                    strJsonCompleted += "]";
                }

                string strJsonTin = "[";
                strSpliter = "";
                if (DataSetUtil.RowCount(dbTin) < 0)
                {
                    strJsonTin += string.Format("{{\"resultTin\": \"{0}\"}}", "fail");
                    strJsonTin += "]";
                }
                if (DataSetUtil.RowCount(dbTin) >= 0)
                {
                    for (int i = 0; i < DataSetUtil.RowCount(dbTin); i++)
                    {
                        for (int j = 0; j < arrCompletedTaskID.Count; j++)
                        {
                            if (arrCompletedTaskID[j] == DataSetUtil.RowStringValue(dbTin, "Taskid", i))
                            {

                                string strTaskid = DataSetUtil.RowStringValue(dbTin, "Taskid", i);
                                string strTaskType = DataSetUtil.RowStringValue(dbTin, "TaskType", i);
                                string strRutaAbastecimiento = DataSetUtil.RowStringValue(dbTin, "RutaAbastecimiento", i);
                                string strCus = DataSetUtil.RowStringValue(dbTin, "CUS", i);
                                string strNus = DataSetUtil.RowStringValue(dbTin, "NUS", i);
                                string strQuantity = DataSetUtil.RowStringValue(dbTin, "Quantity", i);
                                strJsonTin += strSpliter + string.Format("{{\"Taskid\": \"{0}\", \"TaskType\": \"{1}\", \"RutaAbastecimiento\": \"{2}\", \"CUS\": \"{3}\", \"NUS\": \"{4}\", \"Quantity\": \"{5}\"}}", strTaskid, strTaskType, strRutaAbastecimiento, strCus, strNus, strQuantity);
                                if (strSpliter == "") strSpliter = ",";
                            }
                        }
                    }
                    strJsonTin += "]";
                }
                else
                {
                    strJsonTin += string.Format("{{\"resultError\": \"{0}\"}}", "fail");
                    strJsonTin += "]";
                }

                string strJsonError = "[";
                strSpliter = "";
                if (DataSetUtil.RowCount(dbError) < 0)
                {
                    strJsonError += string.Format("{{\"resultError\": \"{0}\"}}", "fail");
                    strJsonError += "]";
                }
                if (DataSetUtil.RowCount(dbError) >= 0)
                {
                    for (int i = 0; i < DataSetUtil.RowCount(dbError); i++)
                    {
                        string strID = DataSetUtil.RowStringValue(dbError, "ID", i);
                        string strError = DataSetUtil.RowStringValue(dbError, "Error", i);
                        strJsonError += strSpliter + string.Format("{{\"ID\": \"{0}\", \"Error\": \"{1}\"}}", strID, strError);
                        if (strSpliter == "") strSpliter = ",";
                    }
                    strJsonError += "]";
                }
                else
                {
                    strJsonError += string.Format("{{\"resultError\": \"{0}\"}}", "fail");
                    strJsonError += "]";
                }


                string strJsonDetail = "[";
                strSpliter = "";
                if (DataSetUtil.RowCount(dbDetail) < 0)
                {
                    strJsonDetail += string.Format("{{\"resultDetail\": \"{0}\"}}", "fail");
                    strJsonDetail += "]";
                }
                if (DataSetUtil.RowCount(dbDetail) >= 0)
                {
                    for (int i = 0; i < DataSetUtil.RowCount(dbDetail); i++)
                    {
                        for (int j = 0; j < arrCompletedTaskID.Count; j++)
                        {
                            if (arrCompletedTaskID[j] == DataSetUtil.RowStringValue(dbDetail, "Taskid", i))
                            {

                                string strTaskid = DataSetUtil.RowStringValue(dbDetail, "Taskid", i);
                                string strCodCounter = DataSetUtil.RowStringValue(dbDetail, "CodCounter", i);
                                string strQuantity = DataSetUtil.RowStringValue(dbDetail, "Quantity", i);
                                strJsonDetail += strSpliter + string.Format("{{\"Taskid\": \"{0}\", \"CodCounter\": \"{1}\", \"Quantity\": \"{2}\"}}", strTaskid, strCodCounter, strQuantity);
                                if (strSpliter == "") strSpliter = ",";
                            }
                        }
                    }
                    strJsonDetail += "]";
                    strJson = string.Format("{{\"result\": \"{0}\", \"task\": {1}, \"complete\": {2}, \"tin\": {3}, \"detail\": {4}, \"error\": {5}}}", "success", strJsonPending, strJsonCompleted, strJsonTin, strJsonDetail, strJsonError);
                }
                else
                {
                    strJsonDetail += string.Format("{{\"resultTin\": \"{0}\"}}", "fail");
                    strJsonDetail += "]";
                    strJson = string.Format("{{\"result\": \"{0}\", \"task\": {1}, \"complete\": {2}, \"tin\": {3},  \"detail\": {4}, \"error\": {5}}}", "success", strJsonPending, strJsonCompleted, strJsonTin, strJsonDetail, strJsonError);
                }

                Response.Write(strJson);
            }
        }
    }
}