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

public partial class Vaccination_ParameterM_BatchSetting_AddOP : BasePage
{
    int VaccineBatchID = 0;
    int VaccineDataID = 0;

    public Vaccination_ParameterM_BatchSetting_AddOP()
    {
        base.AddPower("/Vaccination/ParameterM/BatchSetting.aspx",MyPowerEnum.新增);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(true);
        base.BodyClass = "class='bodybg'";

        int.TryParse(Request.Form["b"], out VaccineBatchID);
        int.TryParse(Request.Form["d"], out VaccineDataID);

        if (VaccineBatchID == 0 || VaccineDataID == 0)
        {
            string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }

        var user = AuthServer.GetLoginUser();
        int Chk = 0;

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_ParameterM_xAddOrUpdateDefaultVaccine", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VaccineBatchID", VaccineBatchID);
                cmd.Parameters.AddWithValue("@VaccineDataID", VaccineDataID);
                cmd.Parameters.AddWithValue("@OrgID", user.OrgID);
                cmd.Parameters.AddWithValue("@CreatedUserID", user.ID);
                SqlParameter sp = cmd.Parameters.AddWithValue("@Chk", Chk);
                sp.Direction = ParameterDirection.Output;

                sc.Open();
                cmd.ExecuteNonQuery();

                Chk = (int)sp.Value;
            }
        }

        OPVM VM = new OPVM();
        VM.chk = Chk;
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(VM));
        Response.End();
    }

}