using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;

public partial class Vaccination_RecordM_StudentReRecord_Detail : BasePage
{
    public int ElementaryRecordID = 0;
    public int nowYear = DateTime.Now.Year;
    public int nextYear = DateTime.Now.Year + 1;
    public string tbAry = "[]";
    public string tbAry2 = "[]";
    public UserVM user = new UserVM();
    public int StudentNumber = 0;
    public int HasYellowCardNumber = 0;


    public Vaccination_RecordM_StudentReRecord_Detail()
    {
        base.AddPower("/Vaccination/RecordM/StudentRecord.aspx", MyPowerEnum.修改);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);
        base.BodyClass = "class='bodybg'";

        int.TryParse(Request["i"], out ElementaryRecordID);

        if (ElementaryRecordID == 0)
        {
            string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }


        user = AuthServer.GetLoginUser();
        if (this.IsPostBack==false)
        {
            DataSet ds = new DataSet();

            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_RecordM_xGetElementaryRecordByID", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ElementaryRecordID", ElementaryRecordID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(ds);
                    }
                }
            }

            ElementaryRecordVM VM = new ElementaryRecordVM();
            EntityS.FillModel(VM, ds.Tables[0]);
            List<ElementaryRecordDataVM> list = new List<ElementaryRecordDataVM>();
            EntityS.FillModel(list, ds.Tables[1]);

            for (int i = VM.AdmissionYear; i <= DateTime.Now.Year - 1911; i++)
            {
                ddlYear.Items.Add(new ListItem(i.ToString() + "年", i.ToString()));
            }


            var dict = SystemCode.GetDict("RecordM_StudentRecord_VaccineCate");
            if (dict.Count > 0)
                tbAry = JsonConvert.SerializeObject(dict);
            if (list.Count > 0)
                tbAry2 = JsonConvert.SerializeObject(list);

            ddlST.SelectedValue = VM.StudentYear.ToString();

            List<SystemElementarySchoolVM> slist = SystemElementarySchool.list;
            ddlSchool.Items.Add(new ListItem("請選擇學校名稱", "0"));
            foreach (var item in slist)
            {
                ddlSchool.Items.Add(new ListItem(item.SchoolName, item.ElementarySchoolID.ToString()));
            }

            ddlSchool.SelectedValue = VM.ElementarySchoolID.ToString();
        }

        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int AdmissionYear = 0;
        int SchoolID = 0;
        int HasYellowCardNumber = 0;
        int StudentNumber = 0;

        int SignUserID = user.ID;
        int OrgID = user.OrgID;


        int.TryParse(ddlYear.SelectedValue, out AdmissionYear);
        int.TryParse(ddlSchool.SelectedValue, out SchoolID);

        string ElementaryRecordDataIDs = Request.Form["V"] ?? "";
        string InoculationNumberString = Request.Form["I"] ?? "";
        string ShouldInoculationNumberString = Request.Form["S"] ?? "";

        bool IsValid = false;

        try
        {
            List<int> list = ElementaryRecordDataIDs.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .ToList<string>()
                .ConvertAll<int>(item => int.Parse(item));

            list = InoculationNumberString.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
               .ToList<string>()
               .ConvertAll<int>(item => int.Parse(item));

            list = ShouldInoculationNumberString.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
              .ToList<string>()
              .ConvertAll<int>(item => int.Parse(item));

            IsValid = true;
        }
        catch
        {
        }

        if (IsValid == false && SchoolID > 0)
        {
            string sc = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", sc, false);
            return;
        }

        string script = "";

        int Chk = 0;

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_RecordM_xUpdateElementaryRecord", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ElementarySchoolID", SchoolID);
                cmd.Parameters.AddWithValue("@AdmissionYear", AdmissionYear);
                cmd.Parameters.AddWithValue("@StudentNumber", StudentNumber);
                cmd.Parameters.AddWithValue("@HasYellowCardNumber", HasYellowCardNumber);
                cmd.Parameters.AddWithValue("@SignUserID", user.ID);
                cmd.Parameters.AddWithValue("@StudentYear", 1);
                cmd.Parameters.AddWithValue("@InoculationType", 2);
                cmd.Parameters.AddWithValue("@ElementaryRecordID", ElementaryRecordID);
                cmd.Parameters.AddWithValue("@ElementaryRecordDataIDs", string.Join(",", ElementaryRecordDataIDs));
                cmd.Parameters.AddWithValue("@InoculationNumberString", string.Join(",", InoculationNumberString));
                cmd.Parameters.AddWithValue("@ShouldInoculationNumberString", string.Join(",", ShouldInoculationNumberString));
                cmd.Parameters.AddWithValue("@OrgID", OrgID);
                SqlParameter sp = cmd.Parameters.AddWithValue("@Chk", Chk);
                sp.Direction = ParameterDirection.Output;

                sc.Open();
                cmd.ExecuteNonQuery();

                Chk = (int)sp.Value;
            }
        }


        if (Chk > 0)
        {
            script = "<script>alert('儲存成功');location.href = '/Vaccination/RecordM/StudentReRecord.aspx';</script><style>body{display:none;}</style>";
        }
        else
        {
            script = "<script>alert('儲存失敗');</script>";
        }

        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);

    }
}