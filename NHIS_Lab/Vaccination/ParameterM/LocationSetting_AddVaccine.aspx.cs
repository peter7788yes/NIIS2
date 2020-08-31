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

public partial class Vaccination_ParameterM_LocationSetting_AddVaccine : BasePage
{ 

    public string ListJson { get; set; }


    public Vaccination_ParameterM_LocationSetting_AddVaccine()
    {
        base.AddPower("/Vaccination/ParameterM/LocationSetting.aspx", MyPowerEnum.修改);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        base.AllowHttpMethod("GET");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";

        var user = AuthServer.GetLoginUser();

        int AgencyInfoID = 0;

        int.TryParse(Request["i"], out AgencyInfoID);
        string IDs = Request["is"] ?? "";

        bool IsValid = false;
        List<int> IDsList = new List<int>();
        try
        {
            IDsList = IDs.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .ToList<string>()
                .ConvertAll<int>(item => int.Parse(item));

            IsValid = true;
        }
        catch
        {
        }

        if (AgencyInfoID == 0 || IsValid==false)
        {
            string script = "<script>alert('資料取得失敗');history.go(-1);</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }

        DataTable dt = new DataTable();
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_ParameterM_xGetVaccineByAgencyID", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AgencyInfoID", AgencyInfoID);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(dt);
                }

            }
        }

        List<AddVaccineVM> list = new List<AddVaccineVM>();
        EntityS.FillModel(list, dt);

        if (IDsList.Count>0)
        {
            foreach(var item in list)
            {
                if(IDsList.Contains(item.ID))
                {
                    item.IsChecked = true;
                }
            }
        }
      

        ListJson=Newtonsoft.Json.JsonConvert.SerializeObject(list);

    }
}