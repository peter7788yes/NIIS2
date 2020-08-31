using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccine_StockManagementM_VaccineIn_Delete_VaccineInBatchOP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserVM user = AuthServer.GetLoginUser();

        int BatchID = 0;
        int CheckStorage = 0;
        int Success = 0;

        int.TryParse(Request.Form["BatchID"], out BatchID);

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_VaccineIn_xDeleteVaccineInBatchData", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", BatchID);
                cmd.Parameters.AddWithValue("@Account", user.ID);
                SqlParameter sp = cmd.Parameters.AddWithValue("@CheckStorage", CheckStorage);
                SqlParameter sp1 = cmd.Parameters.AddWithValue("@Success", Success);
                sp.Direction = ParameterDirection.Output;
                sp1.Direction = ParameterDirection.Output;

                sc.Open();
                cmd.ExecuteNonQuery();
                CheckStorage = (int)sp.Value;
                Success = (int)sp1.Value;

            }
        }

        Dictionary<string, int> dict = new Dictionary<string, int>();
        dict.Add("Success", Success);
        dict.Add("CheckStorage", CheckStorage);

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(dict));
        Response.End();
    }
}