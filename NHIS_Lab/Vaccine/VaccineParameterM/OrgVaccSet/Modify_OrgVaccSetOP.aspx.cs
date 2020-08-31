using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vaccine_VaccineParameterM_OrgVaccSet_Modify_OrgVaccSetOP : BasePage
{
    public Vaccine_VaccineParameterM_OrgVaccSet_Modify_OrgVaccSetOP()
    {
        base.AddPower("/Vaccine/VaccineParameterM/OrgVaccSet/OrgVaccSet.aspx", MyPowerEnum.修改);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        UserVM user = AuthServer.GetLoginUser();

        int ID;
        int SafeNum;
        int AvaPeriod;
        int OrigNum = 0;
        int CheckNum =0;
        int Success = 0;

        int.TryParse(Request.Form["ID"], out ID);
        int.TryParse(Request.Form["SafeNum"], out SafeNum);
        int.TryParse(Request.Form["AvaPeriod"], out AvaPeriod);

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_OrgVaccSet_xUpdateData", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@SafeNum", SafeNum);
                cmd.Parameters.AddWithValue("@AvaPeriod", AvaPeriod);
                cmd.Parameters.AddWithValue("@ModifyAccount", user.ID);
                SqlParameter sp = cmd.Parameters.AddWithValue("@OrigNum", OrigNum);
                SqlParameter sp1 = cmd.Parameters.AddWithValue("@CheckNum", CheckNum);
                SqlParameter sp2 = cmd.Parameters.AddWithValue("@Success", Success);
                sp.Direction = ParameterDirection.Output;
                sp1.Direction = ParameterDirection.Output;
                sp2.Direction = ParameterDirection.Output;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    cmd.ExecuteNonQuery();
                    OrigNum = (int)sp.Value;
                    CheckNum = (int)sp1.Value;
                    Success = (int)sp2.Value;
                }
            }
        }
        Dictionary<string, int> dict = new Dictionary<string, int>();
        dict.Add("OrigNum", OrigNum);
        dict.Add("CheckNum", CheckNum);
        dict.Add("Success", Success);

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(dict));
        Response.End();
    }
}