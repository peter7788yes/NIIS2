using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccine_StockManagementM_VaccineUse_VaccineUse_ConfirmDataOP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserVM user = AuthServer.GetLoginUser();

        int ID = 0;
        int CheckStorage = 0;
        string LackBatch = "";
        int Success = 0;

        int.TryParse(Request.Form["ID"], out ID);

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_VaccineUse_xConfirmData", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@CreateAccount", user.ID);
                SqlParameter sp = cmd.Parameters.AddWithValue("@CheckStorage", CheckStorage);
                SqlParameter sp1 = cmd.Parameters.Add("@LackBatch", SqlDbType.VarChar, 1000);
                SqlParameter sp2 = cmd.Parameters.AddWithValue("@Success", Success);
                sp.Direction = ParameterDirection.Output;
                sp1.Direction = ParameterDirection.Output;
                sp2.Direction = ParameterDirection.Output;

                sc.Open();
                cmd.ExecuteNonQuery();
                CheckStorage = (int)sp.Value;
                LackBatch = Convert.ToString(sp1.Value);
                Success = (int)sp2.Value;

            }
        }

        Dictionary<string, string> dict = new Dictionary<string, string>();
        dict.Add("CheckStorage", CheckStorage.ToString());
        dict.Add("LackBatch", LackBatch);
        dict.Add("Success", Success.ToString());
        

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(dict));
        Response.End();
    }
}