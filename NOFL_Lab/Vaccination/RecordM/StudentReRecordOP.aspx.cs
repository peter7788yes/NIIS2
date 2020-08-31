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

public partial class Vaccination_RecordM_StudentRecordOP : BasePage
{
    public Vaccination_RecordM_StudentRecordOP()
    {
        base.AddPower("/Vaccination/RecordM/StudentReRecord.aspx", MyPowerEnum.瀏覽);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(true);
        base.BodyClass = "class='bodybg'";

        int pgNow = 0;
        int pgSize = 0;
        int OrgID = 0;
        int AdmissionYearStart = 0;
        int AdmissionYearEnd = 0;
        int ElementarySchoolID = 0;
        int StudentYear = 0;

        int.TryParse(Request.Form["LID"], out OrgID);
        int.TryParse(Request.Form["S"], out AdmissionYearStart);
        int.TryParse(Request.Form["E"], out AdmissionYearEnd);
        int.TryParse(Request.Form["pgNow"], out pgNow);
        int.TryParse(Request.Form["pgSize"], out pgSize);
        int.TryParse(Request.Form["EI"], out ElementarySchoolID);
        int.TryParse(Request.Form["SY"], out StudentYear);

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_RecordM_xGetElementRecordListByMany", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrgID", OrgID);
                cmd.Parameters.AddWithValue("@AdmissionYearStart", AdmissionYearStart);
                cmd.Parameters.AddWithValue("@AdmissionYearEnd", AdmissionYearEnd);
                cmd.Parameters.AddWithValue("@InoculationType",2);
                cmd.Parameters.AddWithValue("@ElementarySchoolID", ElementarySchoolID);
                cmd.Parameters.AddWithValue("@StudentYear", StudentYear);
                cmd.Parameters.AddWithValue("@pgNow", pgNow == 0 ? 1 : pgNow);
                cmd.Parameters.AddWithValue("@pgSize", pgSize == 0 ? 10 : pgSize);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }

        List<ElementaryRecordVM> list = new List<ElementaryRecordVM>();
        PageVM rtn = new PageVM();



        EntityS.FillModel(list, ds.Tables[0]);
        EntityS.FillModel(rtn, ds.Tables[1]);
        rtn.message = list;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}