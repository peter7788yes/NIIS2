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

public partial class Vaccine_StockManagementM_VaccineUse_VaccineUseOP : BasePage
{
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));

    public Vaccine_StockManagementM_VaccineUse_VaccineUseOP()
    {
        SearchPower = base.AddPower("/Vaccine/StockManagementM/VaccineUse/VaccineUse.aspx", MyPowerEnum.查詢);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        SearchPower = base.AddPower(SearchPower);
        UserVM user = AuthServer.GetLoginUser();

        int pgNow;
        int pgSize;
        string StartDeal = "";
        string EndDeal = "";
        int Staff = 0;
        int VaccineID = 0;
        int UseType = 0;
        int BatchID = 0;
        int Sort = 0;

        int.TryParse(Request.Form["pgNow"], out pgNow);
        int.TryParse(Request.Form["pgSize"], out pgSize);
        if (SearchPower.HasPower == true)
        {
            StartDeal = Request.Form["StartDeal"];
            EndDeal = Request.Form["EndDeal"];
            int.TryParse(Request.Form["Staff"], out Staff);
            int.TryParse(Request.Form["VaccineID"], out VaccineID);
            int.TryParse(Request.Form["UseType"], out UseType);
            int.TryParse(Request.Form["BatchID"], out BatchID);
            int.TryParse(Request.Form["Sort"], out Sort);

            if (StartDeal == "NaN")
            {
                StartDeal = "";
            }
            if (EndDeal == "NaN")
            {
                EndDeal = "";
            }
        }
        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_VaccineUse_xSearchTable", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pgNow", pgNow);
                cmd.Parameters.AddWithValue("@pgSize", pgSize);
                cmd.Parameters.AddWithValue("@StartDeal", StartDeal);
                cmd.Parameters.AddWithValue("@EndDeal", EndDeal);
                cmd.Parameters.AddWithValue("@OrgType", 2);
                cmd.Parameters.AddWithValue("@OrgID", user.OrgID);
                cmd.Parameters.AddWithValue("@Staff", Staff);
                cmd.Parameters.AddWithValue("@VaccineID", VaccineID);
                cmd.Parameters.AddWithValue("@UseType", UseType);
                cmd.Parameters.AddWithValue("@BatchID", BatchID);
                cmd.Parameters.AddWithValue("@Sort", Sort);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }

        List<VaccineUseDataBatchVM> list = new List<VaccineUseDataBatchVM>();
        PageVM rtn = new PageVM();

        EntityS.FillModel(list, ds.Tables[0]);
        EntityS.FillModel(rtn, ds.Tables[1]);

        rtn.message = list;
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}