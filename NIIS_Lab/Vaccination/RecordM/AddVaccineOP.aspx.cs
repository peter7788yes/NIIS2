using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public partial class Vaccination_RecordM_AddVaccineOP : BasePage
{
    public Vaccination_RecordM_AddVaccineOP()
    {
        base.AddPower("/Vaccination/RecordM/RegisterData.aspx", MyPowerEnum.新增);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(false);

        var user = AuthServer.GetLoginUser();


        int CaseUserID = 0;
        string SystemRecordVaccineCode = "";
        int SystemRecordVaccineID = 0;
        int RecordDataID = 0;
        bool HasInsertSystemRecordVaccine = false;
        int Chk = 0;
        CaseUserID = GetNumber<int>("c");
        SystemRecordVaccineCode = GetString("r");

        if (CaseUserID > 0 || SystemRecordVaccineCode.Length > 0)
        {

            Dictionary<string, object> OutDict = new Dictionary<string, object>() {
                                                                                    { "@RecordDataID", RecordDataID },
                                                                                    { "@HasInsertSystemRecordVaccine", HasInsertSystemRecordVaccine },
                                                                                    { "@SystemRecordVaccineID",SystemRecordVaccineID},
                                                                                    { "@Chk", Chk }
                                                                                  };

            MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_RecordM_xAddRecordData"
                                             , ref OutDict
                                             , new Dictionary<string, object>()
                                             {
                                                     { "@CaseUserID", CaseUserID },
                                                     { "@SystemRecordVaccineCode", SystemRecordVaccineCode },
                                                     { "@OrgID", user.OrgID },
                                                     { "@CreatedUserID", user.ID }
                                            });

            RecordDataID = (int)OutDict["@RecordDataID"];
            HasInsertSystemRecordVaccine = (bool)OutDict["@HasInsertSystemRecordVaccine"];
            Chk = (int)OutDict["@Chk"];

            if (HasInsertSystemRecordVaccine == true)
                SystemRecordVaccine.Update();
            OPVM VM = new OPVM();
            VM.message = RecordDataID.ToString();
            VM.chk = Chk;

            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(JsonConvert.SerializeObject(VM));
            Response.End();
        }
    }
}