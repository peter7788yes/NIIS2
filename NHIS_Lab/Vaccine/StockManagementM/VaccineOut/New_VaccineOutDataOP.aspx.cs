using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccine_StockManagementM_VaccineOut_New_VaccineOutDataOP : BasePage
{
    public Vaccine_StockManagementM_VaccineOut_New_VaccineOutDataOP()
    {
        base.AddPower("/Vaccine/StockManagementM/VaccineOut/VaccineOut.aspx", MyPowerEnum.新增);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        UserVM user = AuthServer.GetLoginUser();

        string DealDate;
        int OrgID;
        string Remark;
        int DealType;
        int DealHospitalID;
        int NewID = 0;
        int Success = 0;

        DealDate = Request.Form["DealDate"];
        int.TryParse(Request.Form["OrgID"], out OrgID);
        Remark = Request.Form["Remark"];
        int.TryParse(Request.Form["DealType"], out DealType);
        int.TryParse(Request.Form["DealHospitalID"], out DealHospitalID);

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_VaccineOut_xAddVaccineOutData", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InOrgID", OrgID);
                cmd.Parameters.AddWithValue("@OutOrgID", user.OrgID);
                cmd.Parameters.AddWithValue("@DealDate", DealDate);
                cmd.Parameters.AddWithValue("@Remark", Remark);
                cmd.Parameters.AddWithValue("@DealType", DealType);
                cmd.Parameters.AddWithValue("@DealHospital", DealHospitalID);
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