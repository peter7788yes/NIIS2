using System;
using System.Collections.Generic;
using System.Data;
using WayneEntity;
using Newtonsoft.Json;

public partial class Vaccination_CertificateM_SignSetting_GetDataOP : BasePage
{
    int OrgID = 0;

    public Vaccination_CertificateM_SignSetting_GetDataOP()
    {
        base.AddPower("/Vaccination/CertificateM/SignSetting.aspx", MyPowerEnum.新增, MyPowerEnum.修改);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(true);

        OrgID = GetNumber<int>("o");

        DataTable dt = MSDB.GetDataTable("ConnDB", "dbo.usp_CertificateM_xGetCertificateSignByOrgID"
                                         , new Dictionary<string, object>()
                                         {
                                              { "@OrgID", OrgID }
                                        });

        CertificateSignVM VM = new CertificateSignVM();
        EntityS.FillModel(VM, dt);
        VM.OrgID = OrgID;
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(JsonConvert.SerializeObject(VM));
        Response.End();
    }
}