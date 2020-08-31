using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using WayneEntity;
using Newtonsoft.Json;
using System.Globalization;

public partial class Vaccination_RecordM_ApplyRecord : BasePage
{
    public int CaseUserID = 0;
    public int RecordDataID = 0;
    public string VaccineCode = "";
    public string AppointmentDate = "";
    public string tbAry = "[]";
    public string Agency = "";
    public int AgencyID = 0;
    UserVM user;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";

        if (Request.HttpMethod.Equals("POST"))
        {

            if (this.IsPostBack == false)
            {
                int.TryParse(Request.Form["c"], out CaseUserID);
                int.TryParse(Request.Form["i"], out RecordDataID);
                VaccineCode = Request.Form["r"] ?? "";
                AppointmentDate = Request.Form["a"] ?? "";
                AppointmentDate = AppointmentDate.Equals("") ? Request.Form["aa"] ?? "": AppointmentDate;
                DateTime date = default(DateTime);
                bool success = DateTime.TryParse(AppointmentDate, out date);
                AppointmentDate = date.ToShortTaiwanDate();
               

                lblVC.Text = VaccineCode;
                lblAD.Text = AppointmentDate;
                hfc.Value = CaseUserID.ToString();
                hfi.Value = RecordDataID.ToString();
                hfr.Value = VaccineCode;
                hfa.Value = AppointmentDate;

                if (success == false || CaseUserID == 0 || RecordDataID == 0)
                {
                        string script = "<script>alert('資料取得失敗');window.close();</script>";
                        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
                        return;
                }

                tbDate.Text = DateTime.Now.ToShortTaiwanDate();


                if (SystemCode.dict.ContainsKey("RecordM_ApplyRecord_ReRecordReason"))
                {
                    var codes = SystemCode.dict["RecordM_ApplyRecord_ReRecordReason"];

                    foreach (var item in codes)
                    {
                        ddlReason1.Items.Add(new ListItem(item.EnumName, item.EnumValue.ToString()));
                    }
                }

                if (SystemCode.dict.ContainsKey("RecordM_ApplyRecord_ReInoculationReason"))
                {
                    var codes = SystemCode.dict["RecordM_ApplyRecord_ReInoculationReason"];

                    foreach (var item in codes)
                    {
                        ddlReason2.Items.Add(new ListItem(item.EnumName, item.EnumValue.ToString()));
                    }
                }

                if (SystemCode.dict.ContainsKey("RecordM_ApplyRecord_EarlyLateReason"))
                {
                    var codes = SystemCode.dict["RecordM_ApplyRecord_EarlyLateReason"];

                    foreach (var item in codes)
                    {
                        ddlReason3.Items.Add(new ListItem(item.EnumName, item.EnumValue.ToString()));
                    }
                }

            }

            user = AuthServer.GetLoginUser();

            DataTable dt = new DataTable();

            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_RecordM_xGetDefaultBatchVaccineByOrgID", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrgID", user.OrgID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(dt);
                    }
                }
            }

            List<DefaultBatchVaccineVM> list = new List<DefaultBatchVaccineVM>();
            EntityS.FillModel(list, dt);

            if (list.Count > 0)
            {
                tbAry = JsonConvert.SerializeObject(list);
            }

            Agency = SystemOrg.GetName(user.OrgID);
            AgencyID = user.OrgID;

        }
        else
        {
            Response.Write("");
            Response.End();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int OrgID = 0;
        int VaccineBatchID = 0;
        
        List<string> ReasonStringList = new List<string>();
        ReasonStringList.Add(tbReason1.Text.Trim());
        ReasonStringList.Add(tbReason2.Text.Trim());
        ReasonStringList.Add(tbReason3.Text.Trim());
       

        int.TryParse(Request.Form["hfAgencyID"] ?? "0", out OrgID);
        int.TryParse(Request.Form["SelectVacc"] ?? "0", out VaccineBatchID);
        int.TryParse(hfi.Value ?? "0", out RecordDataID);
        int.TryParse(hfc.Value ?? "0", out CaseUserID);

        DateTime InoculationDate = DateTime.Now;

        //DateTime.TryParse(Request.Form["AD"], out AssessmentDate);
        DateTime.TryParseExact((tbDate.Text.Trim() ?? DateTime.Now.ToShortTaiwanDate()).RepublicToAD(),
                               "yyyyMMdd",
                               CultureInfo.InvariantCulture,
                               DateTimeStyles.None,
                               out InoculationDate);

        if(DateTime.Equals(InoculationDate, DateTime.MinValue) ==true)
            InoculationDate = DateTime.Now;

        string script = "";

        bool HasUpdate = false;
        int Chk = 0;
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_RecordM_xAddApplyRecord", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RecordDataID", RecordDataID);
                cmd.Parameters.AddWithValue("@CaseUserID", CaseUserID);
                cmd.Parameters.AddWithValue("@InoculationDate", InoculationDate);
                cmd.Parameters.AddWithValue("@OrgID", OrgID);
                cmd.Parameters.AddWithValue("@CreateType", 1);
                cmd.Parameters.AddWithValue("@VaccineBatchID", VaccineBatchID);
                cmd.Parameters.AddWithValue("@CreatedUserID", user.ID);
                cmd.Parameters.AddWithValue("@ResignReason", int.Parse(ddlReason1.SelectedValue));
                cmd.Parameters.AddWithValue("@ReinoculationReason", int.Parse(ddlReason2.SelectedValue));
                cmd.Parameters.AddWithValue("@EarlyLateReason", int.Parse(ddlReason3.SelectedValue));
                cmd.Parameters.AddWithValue("@ReasonString", string.Join(",", ReasonStringList));
                SqlParameter sp1 = cmd.Parameters.AddWithValue("@HasUpdate", HasUpdate);
                sp1.Direction = ParameterDirection.Output;
                SqlParameter sp2 = cmd.Parameters.AddWithValue("@Chk", Chk);
                sp2.Direction = ParameterDirection.Output;

                sc.Open();
                cmd.ExecuteNonQuery();

                HasUpdate = (bool)sp1.Value;
                Chk = (int)sp2.Value;
            }
        }


        if (Chk > 0)
        {
            string AddApplyRecord = " null ";
            if (HasUpdate == true)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("RID", RecordDataID);
                dict.Add("VB", VaccineBatchID);
                dict.Add("ID", InoculationDate);
                dict.Add("ON", user.OrgName);
                dict.Add("CD", DateTime.Now);
                AddApplyRecord = JsonConvert.SerializeObject(dict);
            }
            script = "<script>alert('儲存成功');window.opener.opener.AddApplyRecord(" + AddApplyRecord + ");window.close();</script><style>body{display:none;}</style>";
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
    }
}