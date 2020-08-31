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

public partial class Vaccination_ParameterM_BatchSetting_VaccineOP : BasePage
{
    int OrgID = 0;

    public Vaccination_ParameterM_BatchSetting_VaccineOP()
    {
        base.AddPower("/Vaccination/ParameterM/BatchSetting.aspx", MyPowerEnum.查詢);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("POST");
        base.DisableTop(true);
        base.BodyClass = "class='bodybg'";

        int.TryParse(Request["i"], out OrgID);

        if (OrgID == 0)
        {
            string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }



        DataTable dt = new DataTable();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_ParameterM_xGetVaccineByOrgID", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrgID", OrgID);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(dt);
                }
            }
        }

        List<VaccineByOrgVM> list = new List<VaccineByOrgVM>();
        EntityS.FillModel(list, dt);

        int VaccineID = 0;
        if (list.Count > 0)
            VaccineID = list[0].VaccineID;
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_ParameterM_xGetDefaultBatchByVaccineDataID", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VaccineDataID", VaccineID);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(dt);
                }
            }
        }

        List<BatchSettingVM> list2 = new List<BatchSettingVM>();
        EntityS.FillModel(list2, dt);
        list2 = list2.FindAll(item => item.VaccineBatchID > 0);
        Dictionary<string, object> dict = new Dictionary<string, object>();
        dict["list1"] = list;
        dict["list2"] = list2;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(dict));
        Response.End();
    }

}