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

public partial class Vaccine_StockManagementM_VaccineUse_VaccineUse_BatchListOP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserVM user = AuthServer.GetLoginUser();

        string PageType;
        int ID;
        int SelectID;

        PageType = Request.Form["Page"];
        int.TryParse(Request.Form["ID"], out ID);
        int.TryParse(Request.Form["SelectID"], out SelectID);

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_StockManagementM_xGetVaccineBatch", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PageType", PageType);
                cmd.Parameters.AddWithValue("@DataID", ID);
                cmd.Parameters.AddWithValue("@VaccID", SelectID);
                cmd.Parameters.AddWithValue("@OrgID", user.OrgID);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }
        List<ChooseVaccineBatchVM> list = new List<ChooseVaccineBatchVM>();
        AnyDataVM rtn = new AnyDataVM();

        EntityS.FillModel(list, ds.Tables[0]);

        rtn.message = list;
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}