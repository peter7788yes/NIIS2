using System;
using System.Collections.Generic;
using System.Web.UI;
using Newtonsoft.Json;

public partial class Vaccination_RecordM_ApplyHealth_RemoveOP : BasePage
{
    public Vaccination_RecordM_ApplyHealth_RemoveOP()
    {
        base.AddPower("/Vaccination/RecordM/RegisterData.aspx", MyPowerEnum.刪除);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");

        int ApplyHealthID = GetNumber<int>("i");

        if (ApplyHealthID == 0)
        {
            string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }

        var user = AuthServer.GetLoginUser();
        int Chk = 0;

        Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

        MSDB.ExecuteNonQuery("ConnDB", "dbo.usp_RecordM_xDeleteApplyHealthByID"
                                         , ref OutDict
                                         , new Dictionary<string, object>()
                                         {
                                                    { "@ApplyHealthID", ApplyHealthID }
                                        });

        Chk = (int)OutDict["@Chk"];

        OPVM VM = new OPVM();
        VM.chk = Chk;
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(JsonConvert.SerializeObject(VM));
        Response.End();
    }

}