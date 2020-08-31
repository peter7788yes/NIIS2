using System;
using System.Collections.Generic;
using System.Web.UI;
using Newtonsoft.Json;

public partial class Vaccination_ParameterM_BatchSetting_AddOP : BasePage
{
    public Vaccination_ParameterM_BatchSetting_AddOP()
    {
        base.AddPower("/Vaccination/ParameterM/BatchSetting.aspx",MyPowerEnum.新增);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(true);

        int VaccineBatchID = GetNumber<int>("b");
        int VaccineDataID = GetNumber<int>("d");

        if (VaccineBatchID == 0 || VaccineDataID == 0)
        {
            string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }

        var user = AuthServer.GetLoginUser();
        int Chk = 0;

        Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

        MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_ParameterM_xAddOrUpdateDefaultVaccine"
                                         , ref OutDict
                                         , new Dictionary<string, object>()
                                         {
                                                    { "@VaccineBatchID", VaccineBatchID },
                                                    { "@VaccineDataID", VaccineDataID },
                                                    { "@OrgID", user.OrgID },
                                                    { "@CreatedUserID", user.ID }
                                        });

        Chk = (int)OutDict["@Chk"];

        OPVM VM = new OPVM();
        VM.chk = Chk;
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(JsonConvert.SerializeObject(VM));
        Response.End();
    }

}