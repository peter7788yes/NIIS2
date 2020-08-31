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

public partial class Vaccine_StockManagementM_VaccineIn_VaccineInOP : BasePage
{
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));

    public Vaccine_StockManagementM_VaccineIn_VaccineInOP()
    {
        SearchPower = base.AddPower("/Vaccine/StockManagementM/VaccineIn/VaccineIn.aspx", MyPowerEnum.查詢);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        SearchPower = base.AddPower(SearchPower);

        UserVM user = AuthServer.GetLoginUser();

        int pgNow;
        int pgSize;

        int.TryParse(Request.Form["pgNow"], out pgNow);
        int.TryParse(Request.Form["pgSize"], out pgSize);

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_VaccineIn_xSearchLogin", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pgNow", pgNow);
                cmd.Parameters.AddWithValue("@pgSize", pgSize);
                cmd.Parameters.AddWithValue("@InOrgID", user.OrgID);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }

        List<VaccineInPendingVM> list = new List<VaccineInPendingVM>();
        PageVM rtn = new PageVM();

        EntityS.FillModel(list, ds.Tables[0]);
        EntityS.FillModel(rtn, ds.Tables[1]);

        rtn.message = list;
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}