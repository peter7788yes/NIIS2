using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccine_StockManagementM_VaccineOut_GetOutBDataByOrgOP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserVM user = AuthServer.GetLoginUser();

        int VaccineID = 0;
        int BatchType = 0;

        int.TryParse(Request.Form["VaccineID"], out VaccineID);
        int.TryParse(Request.Form["BatchType"], out BatchType);

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_VaccineOut_xGetSelectData1", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrgType", 2);
                cmd.Parameters.AddWithValue("@OrgID", user.OrgID);
                cmd.Parameters.AddWithValue("@VaccineID", VaccineID);
                cmd.Parameters.AddWithValue("@BatchType", BatchType);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }

        StockManagementSelectVM rtn = new StockManagementSelectVM();

        if (ds.Tables.Count > 0)
        {
            rtn.BatchIDInfo = ds.Tables[0];
        }
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}