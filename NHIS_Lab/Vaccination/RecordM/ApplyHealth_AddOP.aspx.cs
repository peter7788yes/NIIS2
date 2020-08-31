using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccination_RecordM_ApplyHealth_AddOP : BasePage
{
    int AssessmentUserID = 0;
    DateTime AssessmentDate = DateTime.Now;
    bool AllowWork = false;
    string ApplyHealthCateIDs = "";
    string OtherState = "";
    int RecordDataID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";

        int.TryParse(Request.Form["au"], out AssessmentUserID);
        //DateTime.TryParse(Request.Form["AD"], out AssessmentDate);
        DateTime.TryParseExact((Request.Form["ad"]  ?? DateTime.Now.ToShortTaiwanDate()).RepublicToAD(),
                               "yyyyMMdd",
                               CultureInfo.InvariantCulture,
                               DateTimeStyles.None,
                               out AssessmentDate);
        bool.TryParse(Request.Form["AW"], out AllowWork);
        ApplyHealthCateIDs = Request.Form["AH"] ?? "";
        bool IsValid = false;

        try
        {
            List<int> list = ApplyHealthCateIDs.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .ToList<string>()
                .ConvertAll<int>(item => int.Parse(item));

            IsValid = true;
        }
        catch
        {
        }
            

        OtherState = Request.Form["os"] ?? "";
        int.TryParse(Request.Form["rd"], out RecordDataID);

        if (IsValid==false || AssessmentUserID == 0 || AssessmentDate == default(DateTime) || RecordDataID==0)
        {
            string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }

        var user = AuthServer.GetLoginUser();
        int Chk = 0;

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_RecordM_xAddOrUpdateApplyHealth", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ApplyHealthID", 0);
                cmd.Parameters.AddWithValue("@AssessmentUserID", AssessmentUserID);
                cmd.Parameters.AddWithValue("@AssessmentDate", AssessmentDate);
                cmd.Parameters.AddWithValue("@CreatedUserID", user.ID);
                cmd.Parameters.AddWithValue("@AllowWork", AllowWork);
                cmd.Parameters.AddWithValue("@ApplyHealthCateIDs", ApplyHealthCateIDs);
                cmd.Parameters.AddWithValue("@OtherState", OtherState);
                cmd.Parameters.AddWithValue("@RecordDataID", RecordDataID);
                cmd.Parameters.AddWithValue("@OrgID", user.OrgID);
                SqlParameter sp = cmd.Parameters.AddWithValue("@Chk", Chk);
                sp.Direction = ParameterDirection.Output;

                sc.Open();
                cmd.ExecuteNonQuery();

                Chk = (int)sp.Value;
            }
        }

        OPVM VM = new OPVM();
        VM.chk = Chk;
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(VM));
        Response.End();
    }

}