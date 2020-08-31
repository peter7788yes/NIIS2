using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using WayneEntity;

public partial class Vaccination_RecordM_RecordDetail : BasePage
{
    public string ApplyRecordAry = "[]";
    public string ApplyHealthAry = "[]";
    public string ApplyEffectAry = "[]";
    public SimpleCaseUserVM VM = new SimpleCaseUserVM();

    public int CaseUserID = 0;
    public int RecordDataID = 0;
    public string SystemRecordVaccineCode = "";
    public string AgeEnglish = "";
    public string AppointmentDate = "";
    public string ShortTaiwanAppointmentDate = "";

    public Vaccination_RecordM_RecordDetail()
    {
        base.AddPower("/Vaccination/RecordM/RegisterData.aspx", MyPowerEnum.瀏覽);
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
            AppointmentDate = GetString("a"); 
            AgeEnglish = Server.UrlDecode(GetString("ae"));

            DateTime date = default(DateTime);
            bool success = DateTime.TryParse(AppointmentDate, out date);
            ShortTaiwanAppointmentDate = date.ToShortTaiwanDate();

            if (success == false || CaseUserID == 0 || RecordDataID == 0)
            {
                string script = "<script>alert('資料取得失敗');window.close();</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
                return;
            }

            DataTable dt = MSDB.GetDataTable("ConnDB", "dbo.usp_RecordM_xGetSimpleCaseUserByID"
                                         , new Dictionary<string, object>()
                                         {
                                              { "@CaseUserID", CaseUserID }
                                        });

            EntityS.FillModel(VM, dt);

            dt = MSDB.GetDataTable("ConnDB", "dbo.usp_RecordM_xGetApplyRecordByRecordDataID"
                                         , new Dictionary<string, object>()
                                         {
                                              { "@RecordDataID", RecordDataID }
                                        });

            List<ApplyRecordVM> listR = new List<ApplyRecordVM>();
            EntityS.FillModel(listR, dt);
            ApplyRecordAry = JsonConvert.SerializeObject(listR);

            dt = MSDB.GetDataTable("ConnDB", "dbo.usp_RecordM_xGetApplyHealthByRecordDataID"
                                         , new Dictionary<string, object>()
                                         {
                                              { "@RecordDataID", RecordDataID }
                                        });
            List<ApplyHealthVM> listH = new List<ApplyHealthVM>();
            EntityS.FillModel(listH, dt);
            ApplyHealthAry = JsonConvert.SerializeObject(listH);



            dt = MSDB.GetDataTable("ConnDB", "dbo.usp_RecordM_xGetApplyEffectDetailByRecordDataID"
                                         , new Dictionary<string, object>()
                                         {
                                              { "@RecordDataID", RecordDataID }
                                        });

            List<ApplyEffectVM> listE = new List<ApplyEffectVM>();
            EntityS.FillModel(listE, dt);
            ApplyEffectAry = JsonConvert.SerializeObject(listE);
        }
        else
        {
            Response.Write("");
            Response.End();
        }


    }

}