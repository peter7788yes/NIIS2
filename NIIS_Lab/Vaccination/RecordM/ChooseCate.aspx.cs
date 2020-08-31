using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.UI;

public partial class Vaccination_RecordM_ChooseCate : BasePage
{
    public int CaseUserID = 0;
    public int RecordDataID = 0;
    public string SystemRecordVaccineCode = "";
    public int SystemRecordVaccineID = 0;
    public string AppointmentDate = "";

    public Vaccination_RecordM_ChooseCate()
    {
        base.AddPower("/Vaccination/RecrodM/RegisterData.aspx", MyPowerEnum.瀏覽);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);

        if (Request.HttpMethod.Equals("POST"))
        {
            CaseUserID = GetNumber<int>("c");
            RecordDataID = GetNumber<int>("i");
            SystemRecordVaccineCode = GetString("r");
            SystemRecordVaccineID = GetNumber<int>("ri");
            AppointmentDate = GetString("a");
            DateTime date = default(DateTime);
            bool success = DateTime.TryParse(AppointmentDate, out date);
            //AppointmentDate = date.ToShortTaiwanDate();

            if (success==false || CaseUserID == 0)
            {
                string script = "<script>alert('資料取得失敗');window.close();</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
                return;
            }


 
        

            bool HasInsertSystemRecordVaccine = false;
            int Chk = 0;
       

            if (CaseUserID > 0 && SystemRecordVaccineCode.Length > 0 && RecordDataID == 0 )
            {
                var user = AuthServer.GetLoginUser();

                Dictionary<string, object> OutDict = new Dictionary<string, object>()
                                                     {
                                                        { "@RecordDataID", RecordDataID },
                                                        { "@SystemRecordVaccineID",SystemRecordVaccineID},
                                                        { "@HasInsertSystemRecordVaccine", HasInsertSystemRecordVaccine },
                                                        { "@Chk", Chk }
                                                     };

                MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_RecordM_xAddRecordData"
                                                 , ref OutDict
                                                 , new Dictionary<string, object>()
                                                 {
                                                     { "@CaseUserID", CaseUserID },
                                                     { "@SystemRecordVaccineCode", SystemRecordVaccineCode},
                                                     { "@OrgID", user.OrgID },
                                                     { "@CreatedUserID", user.ID }
                                                });

                RecordDataID = (int)OutDict["@RecordDataID"];
                SystemRecordVaccineID = (int)OutDict["@SystemRecordVaccineID"];
                HasInsertSystemRecordVaccine = (bool)OutDict["@HasInsertSystemRecordVaccine"];
                Chk = (int)OutDict["@Chk"];

                if (HasInsertSystemRecordVaccine == true)
                    SystemRecordVaccine.Update();

                if (Chk < 1 || RecordDataID == 0)
                {
                    lblScript.Text = "<script>alert('資料取得失敗');window.close();</script>";
                }
                else
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    dict.Add("SCode", SystemRecordVaccineCode);
                    dict.Add("RID", RecordDataID);
                    lblScript.Text = "<script>window.opener.getRecordDataID(" + JsonConvert.SerializeObject(dict) + ");document.getElementById('formid').submit();</script>";
                }

            }
        }
        else
        {
            Response.Write("");
            Response.End();
        }
    }
}