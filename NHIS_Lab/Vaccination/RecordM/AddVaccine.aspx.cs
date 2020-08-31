using Newtonsoft.Json;
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

public partial class Vaccination_RecordM_AddVaccine : BasePage
{
    public int CaseUserID = 0;
    public string tbAry = "[]";
    public Vaccination_RecordM_AddVaccine()
    {
        base.AddPower("/Vaccination/RecordM/RegisterData.aspx", MyPowerEnum.新增);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";

       

        if (Request.HttpMethod.Equals("POST"))
        {
            int.TryParse(Request.Form["c"], out CaseUserID);

            DataTable dt = new DataTable();
            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_RecordM_xGetVaccineList", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        sc.Open();
                        da.Fill(dt);
                    }
                }
            }

            List<string> VaccineList = (Request.Form["a"] ?? "").Split(',').ToList();

            List<AddVaccineVM> list = new List<AddVaccineVM>();
            EntityS.FillModel(list, dt);

            //var listLookup = list.ToLookup(item => item.VaccineID);
            var listDiff = list.Where(item => (VaccineList.Contains(item.VaccineID) == false));
            if (list.Count > 0)
            {
                tbAry = JsonConvert.SerializeObject(listDiff);
            }
        }
        else
        {
            Response.Write("");
            Response.End();
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
    }
}