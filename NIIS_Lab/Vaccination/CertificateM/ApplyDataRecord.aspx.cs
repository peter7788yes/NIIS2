using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using WayneEntity;

public partial class Vaccination_CertificateM_ApplyDataRecord : BasePage
{
    public string CapacityString = "";
    public string AgeString = "";
    public string uJson = "''";
    public int CaseUserID = 0;
    public CaseUserVM VM;

    public Vaccination_CertificateM_ApplyDataRecord()
    {
        base.AddPower("/Vaccination/CertificateM/PrintCertificate.aspx", MyPowerEnum.新增);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(false);

        CaseUserID = GetNumber<int>("i");

        if (CaseUserID == 0)
        {
            string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }

        DataSet ds = MSDB.GetDataSet("ConnDB", "dbo.usp_RecordM_xGetCaseUserByID"
                                         , new Dictionary<string, object>()
                                         {
                                                    { "@CaseUserID", CaseUserID }
                                        });
        VM = new CaseUserVM();
        EntityS.FillModel(VM, ds.Tables[0]);
        uJson = JsonConvert.SerializeObject(VM);
    }
}