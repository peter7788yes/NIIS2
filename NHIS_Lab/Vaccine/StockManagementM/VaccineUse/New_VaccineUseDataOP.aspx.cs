using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccine_StockManagementM_VaccineUse_New_VaccineUseDataOP : BasePage
{
    public Vaccine_StockManagementM_VaccineUse_New_VaccineUseDataOP()
    {
        base.AddPower("/Vaccine/StockManagementM/VaccineUse/VaccineUse.aspx", MyPowerEnum.新增);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        UserVM user = AuthServer.GetLoginUser();

        string DealDate;
        string Remark;
        int NewID = 0;
        int Success = 0;

        DealDate = Request.Form["DealDate"];
        Remark = Request.Form["Remark"];

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_VaccineUse_xAddVaccineUseData", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UseOrgID", user.OrgID);
                cmd.Parameters.AddWithValue("@DealDate", DealDate);
                cmd.Parameters.AddWithValue("@Remark", Remark);
                cmd.Parameters.AddWithValue("@CreateAccount", user.ID);
                SqlParameter sp = cmd.Parameters.AddWithValue("@NewID", NewID);
                SqlParameter sp1 = cmd.Parameters.AddWithValue("@Success", Success);
                sp.Direction = ParameterDirection.Output;
                sp1.Direction = ParameterDirection.Output;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    cmd.ExecuteNonQuery();
                    NewID = (int)sp.Value;
                    Success = (int)sp1.Value;
                }
            }
        }
        Dictionary<string, int> dict = new Dictionary<string, int>();
        dict.Add("NewID", NewID);
        dict.Add("Success", Success);

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(dict));
        Response.End();
    }
}