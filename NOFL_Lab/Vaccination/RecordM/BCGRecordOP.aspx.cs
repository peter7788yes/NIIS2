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

public partial class Vaccination_RecordM_BCGRecordOP : BasePage
{
    public Vaccination_RecordM_BCGRecordOP()
    {
        base.AddPower("/Vaccination/RecordM/BCGRecord.aspx", MyPowerEnum.瀏覽);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(true);
        base.BodyClass = "class='bodybg'";

        int pgNow = 0 ;
        int pgSize = 0 ;
        int OrgID = 0 ;
        int StatisticalYearStart = 0;
        int StatisticalYearEnd = 0;

        int.TryParse(Request.Form["lid"], out OrgID);
        int.TryParse(Request.Form["s"], out StatisticalYearStart);
        int.TryParse(Request.Form["e"], out StatisticalYearEnd);
        int.TryParse(Request.Form["pgNow"], out pgNow);
        int.TryParse(Request.Form["pgSize"], out pgSize);

        DataSet ds = new DataSet();
        
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_RecordM_xGetBCGRecordListByMany", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrgID", OrgID);
                cmd.Parameters.AddWithValue("@StatisticalYearStart", StatisticalYearStart);
                cmd.Parameters.AddWithValue("@StatisticalYearEnd", StatisticalYearEnd);
                cmd.Parameters.AddWithValue("@pgNow", pgNow == 0 ? 1 : pgNow);
                cmd.Parameters.AddWithValue("@pgSize", pgSize == 0 ? 10 : pgSize);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }

        List<BCGRecordVM> list = new List<BCGRecordVM>();
        PageVM rtn = new PageVM();



        EntityS.FillModel(list, ds.Tables[0]);
        EntityS.FillModel(rtn, ds.Tables[1]);
        rtn.message = list;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}