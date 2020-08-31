﻿using System;
using System.Collections.Generic;
using System.Web.UI;
using Newtonsoft.Json;

public partial class Vaccination_ParameterM_BatchSetting_RemoveOP : BasePage
{
    public Vaccination_ParameterM_BatchSetting_RemoveOP()
    {
        base.AddPower("/Vaccination/ParameterM/BatchSetting.aspx", MyPowerEnum.刪除);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(true);

        int DefaultBatchVaccineID  = GetNumber<int>("d");

        if (DefaultBatchVaccineID == 0)
        {
            string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }

        var user = AuthServer.GetLoginUser();
        int Chk = 0;

        Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

        MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_ParameterM_xDeleteDefaultVaccineByID"
                                         , ref OutDict
                                         , new Dictionary<string, object>()
                                         {
                                                    { "@DefaultBatchVaccineID", DefaultBatchVaccineID }
                                        });

        Chk = (int)OutDict["@Chk"];

        OPVM VM = new OPVM();
        VM.chk = Chk;
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(JsonConvert.SerializeObject(VM));
        Response.End();
    }

}