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

public partial class Vaccination_CertificateM_ApplyDataRecordOP : BasePage
{
    public Vaccination_CertificateM_ApplyDataRecordOP()
    {
        base.AddPower("/Vaccination/CertificateM/StudentRecord.aspx", MyPowerEnum.瀏覽);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(true);
        base.BodyClass = "class='bodybg'";

        int pgNow = 0;
        int pgSize = 0;

        int CaseUserID = 0;
        int.TryParse(Request.Form["i"], out CaseUserID);
        int.TryParse(Request.Form["pgNow"], out pgNow);
        int.TryParse(Request.Form["pgSize"], out pgSize);

        DataSet ds = new DataSet();
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_CertificateM_xGetApplyDataList", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CaseUserID", CaseUserID);
                cmd.Parameters.AddWithValue("@pgNow", pgNow == 0 ? 1 : pgNow);
                cmd.Parameters.AddWithValue("@pgSize", pgSize == 0 ? 10 : pgSize);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }

        List<ApplyDataRecordVM> list = new List<ApplyDataRecordVM>();
        List<ApplyDataRecordFileVM> FileList = new List<ApplyDataRecordFileVM>();
        PageVM rtn = new PageVM();

        EntityS.FillModel(list, ds.Tables[0]);
        EntityS.FillModel(rtn, ds.Tables[1]);
        EntityS.FillModel(FileList, ds.Tables[2]);

        rtn.message = list;
        rtn.message2 = FileList;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}