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

public partial class Vaccine_VaccineParameterM_OrgVaccSet_OrgVaccSetOP : BasePage
{
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));

    public Vaccine_VaccineParameterM_OrgVaccSet_OrgVaccSetOP()
    {
        SearchPower = base.AddPower("/Vaccine/VaccineParameterM/OrgVaccSet/OrgVaccSet.aspx", MyPowerEnum.查詢);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        UserVM user = AuthServer.GetLoginUser();

        int Status = 0;
        
        if (SearchPower.HasPower == true)
        {
            int.TryParse(Request.Form["Status"], out Status);
        }

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_OrgVaccSet_xGetVaccine", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrgID", user.OrgID);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@CreateAccount", user.ID);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }
        List<OrgVaccSet> list = new List<OrgVaccSet>();
        AnyDataVM rtn = new AnyDataVM();

        EntityS.FillModel(list, ds.Tables[0]);

        rtn.message = list;
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}