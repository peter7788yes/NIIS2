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

public partial class Vaccination_RecordM_StudentReRecord_Add : BasePage
{

    public int nowYear = DateTime.Now.Year;
    public int nextYear = DateTime.Now.Year + 1;
    public string tbAry = "[]";
    public string sAry = "[]";
    public UserVM user = new UserVM();

    public Vaccination_RecordM_StudentReRecord_Add()
    {
       base.AddPower("/Vaccination/RecordM/StudentReRecord.aspx",  MyPowerEnum.新增);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);
        base.BodyClass = "class='bodybg'";

        user = AuthServer.GetLoginUser();
        if (this.IsPostBack == false)
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
            var dict = SystemCode.GetDict("RecordM_StudentRecord_VaccineCate");

            if (dict.Count > 0)
            {
                var query = dict.Where(item => list.Contains(item.EnumValue));
                tbAry = JsonConvert.SerializeObject(query);
            }

            int nowYear = DateTime.Now.Year - 1911;
            ddlYear.Items.Add(new ListItem(nowYear.ToString() + "年", nowYear.ToString()));
            ddlYear.Items.Add(new ListItem((nowYear + 1).ToString() + "年", (nowYear + 1).ToString()));

            //if (SystemElementarySchool.list.Count > 0)
            //{
            //    ddlSchool.Items.Add(new ListItem("0", "請選擇學校名稱"));
            //    foreach(var item in SystemElementarySchool.list)
            //    {
            //        ddlSchool.Items.Add(new ListItem(item.ElementarySchoolID.ToString(), item.SchoolName));
            //    }
            //}


            if (SystemElementarySchool.list.Count > 0)
                sAry = JsonConvert.SerializeObject(SystemElementarySchool.list);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int AdmissionYear = 0;
        int SchoolID = 0;
        int StudentYear = ddlST.SelectedIndex+1;

        
        int SignUserID = user.ID;
        int OrgID = user.OrgID;


        int.TryParse(ddlYear.SelectedValue, out AdmissionYear);
        int.TryParse(Request["ss"] ?? "0", out SchoolID);

        string VaccineTypeString = Request.Form["v"] ?? "";
        string InoculationNumberString = Request.Form["i"] ?? "";
        string ShouldInoculationNumberString = Request.Form["ss"] ?? "";

        bool IsValid = false;

        try
        {
            List<int> list = VaccineTypeString.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
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

        if (IsValid == false && SchoolID>0)
        {
            string sc = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", sc, false);
            return;
        }

        string script = "";

        int Chk = 0;

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_RecordM_xAddElementaryRecord", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ElementarySchoolID", SchoolID);
                cmd.Parameters.AddWithValue("@AdmissionYear", AdmissionYear);
                cmd.Parameters.AddWithValue("@StudentNumber", 0);
                cmd.Parameters.AddWithValue("@HasYellowCardNumber", 0);
                cmd.Parameters.AddWithValue("@SignUserID", user.ID);
                //cmd.Parameters.AddWithValue("@SignDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@CreatedUserID", user.ID);
                cmd.Parameters.AddWithValue("@StudentYear", StudentYear);
                cmd.Parameters.AddWithValue("@InoculationType", 2);
                cmd.Parameters.AddWithValue("@VaccineTypeString", string.Join(",", VaccineTypeString));
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