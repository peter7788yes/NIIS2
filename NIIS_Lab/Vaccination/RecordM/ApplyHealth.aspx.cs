using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using WayneEntity;

public partial class Vaccination_RecordM_ApplyHealth : BasePage
{
    public new MyPowerVM AddPower = new MyPowerVM("", default(MyPowerEnum));
    public int UpdateUID = 0;
    public string UpdateUserData = "{}";
    public string UserName = "";
    public string StateListAry = "[]";
    public string UserAry = "[]";
    public int CaseUserID = 0;
    public int RecordDataID = 0;
    public string SystemRecordVaccineCode = "";
    public int SystemRecordVaccineID = 0;
    public string AppointmentDate = "";
    public string nowDate = "";
    public string Agency = "";
    public int AgencyID = 0;

    public Vaccination_RecordM_ApplyHealth()
    {
        AddPower= base.AddPower("/Vaccination/RecordM/RegisterData.aspx", MyPowerEnum.新增);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);

        if (Request.HttpMethod.Equals("POST"))
        {
            nowDate = DateTime.Now.ToShortTaiwanDate();
            CaseUserID = GetNumber<int>("c");
            RecordDataID = GetNumber<int>("i");
            SystemRecordVaccineCode = GetString("r");
            SystemRecordVaccineID = GetNumber<int>("ri");
            AppointmentDate = GetString("a");
            UpdateUID = GetNumber<int>("uu");

            DateTime date = default(DateTime);
            bool success = DateTime.TryParse(AppointmentDate, out date);
            AppointmentDate = date.ToShortTaiwanDate();

            lblVC.Text = SystemRecordVaccineCode;
            lblAD.Text = success ? AppointmentDate :"";
            var user = AuthServer.GetLoginUser();

            Agency = user.OrgName;
            AgencyID = user.OrgID;

            if (UpdateUID == 0)
            {
                if (CaseUserID == 0 || RecordDataID == 0)
                {
                    string script = "<script>alert('資料取得失敗');window.close();</script>";
                    Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
                    return;
                }
            }


            if (SystemCode.dict.ContainsKey("RecordM_ApplyHealthCate"))
            {
                StateListAry = JsonConvert.SerializeObject(SystemCode.dict["RecordM_ApplyHealthCate"].OrderBy(item => item.OrderNumber));
            }

            UserName = user.UserName;

            DataTable dt = MSDB.GetDataTable("ConnUser", "dbo.usp_AccountM_xGetUserListByOrgID"
                                         , new Dictionary<string, object>()
                                         {
                                              { "@OrgID", user.OrgID }
                                           
                                        });

            List<UserNameIDVM> list = new List<UserNameIDVM>();

            EntityS.FillModel(list, dt);

            UserAry = JsonConvert.SerializeObject(list);


            if(UpdateUID>0)
            {
                dt = MSDB.GetDataTable("ConnDB", "dbo.usp_RecordM_xGetApplyHealthByID"
                                        , new Dictionary<string, object>()
                                        {
                                              { "@ID", UpdateUID }
                                        });

                ApplyHealthVM VM = new ApplyHealthVM();
                EntityS.FillModel(VM, dt);

                UpdateUserData = JsonConvert.SerializeObject(VM);
                nowDate = VM.AssessmentDate.ToShortTaiwanDate();
            }
        }
        else
        {
            Response.Write("");
            Response.End();
        }
    }

  
}