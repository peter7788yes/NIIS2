using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public partial class Vaccination_RecordM_RegisterData_Detail_StoolOP : BasePage
{
    public Vaccination_RecordM_RegisterData_Detail_StoolOP()
    {
        base.AddPower("/Vaccination/RecordM/RegisterData.aspx", MyPowerEnum.瀏覽);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");

        int CaseUserID = GetNumber<int>("c");
        int StoolCard = GetNumber<int>("s");
        int Chk = 0;

        var user = AuthServer.GetLoginUser();

        Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

        MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_RecordM_xAddRegisterDataByStoolCard"
                                         , ref OutDict
                                         , new Dictionary<string, object>()
                                         {
                                                    { "@UserID", user.ID },
                                                    { "@CaseUserID", CaseUserID },
                                                    { "@StoolCard", StoolCard }
                                        });

        Chk = (int)OutDict["@Chk"];

        OPVM VM = new OPVM();
        VM.chk = Chk;
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(JsonConvert.SerializeObject(VM));
        Response.End();
    }

}