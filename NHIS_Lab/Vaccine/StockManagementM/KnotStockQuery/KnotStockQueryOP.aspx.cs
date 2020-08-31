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

public partial class Vaccine_StockManagementM_KnotStockQuery_KnotStockQueryOP : BasePage
{
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));
    public Vaccine_StockManagementM_KnotStockQuery_KnotStockQueryOP()
    {
        SearchPower = base.AddPower("/Vaccine/StockManagementM/KnotStockQuery/KnotStockQuery.aspx", MyPowerEnum.查詢);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        SearchPower = base.AddPower(SearchPower);
        UserVM user = AuthServer.GetLoginUser();

        int pgNow;
        int pgSize;
        string StartDeal = "";
        string EndDeal = "";
        string Vaccines = "";

        int.TryParse(Request.Form["pgNow"], out pgNow);
        int.TryParse(Request.Form["pgSize"], out pgSize);

        if (SearchPower.HasPower == true)
        {
            StartDeal = Request.Form["StartDeal"];
            EndDeal = Request.Form["EndDeal"];
            Vaccines = Request.Form["SelectVaccine"];

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
            using (SqlCommand cmd = new SqlCommand("usp_KnotStockQuery_xSearchTable", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pgNow", pgNow);
                cmd.Parameters.AddWithValue("@pgSize", pgSize);
                cmd.Parameters.AddWithValue("@OrgID", user.OrgID);
                cmd.Parameters.AddWithValue("@StartDeal", StartDeal);
                cmd.Parameters.AddWithValue("@EndDeal", EndDeal);
                cmd.Parameters.AddWithValue("@Vaccines", Vaccines);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }

        List<KnotStockQueryVM> list = new List<KnotStockQueryVM>();
        PageVM rtn = new PageVM();

        EntityS.FillModel(list, ds.Tables[0]);
        EntityS.FillModel(rtn, ds.Tables[1]);

        rtn.message = list;
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}