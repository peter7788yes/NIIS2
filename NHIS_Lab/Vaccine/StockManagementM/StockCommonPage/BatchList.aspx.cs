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

public partial class Vaccine_StockManagementM_StockCommonPage_BatchList : BasePage
{
    public Vaccine_StockManagementM_StockCommonPage_BatchList()
    {
        base.powerLogicType = PowerLogicType.OR;
        base.AddPower("/Vaccine/StockManagementM/VaccineIn/VaccineIn.aspx", MyPowerEnum.查詢);
        base.AddPower("/Vaccine/StockManagementM/VaccineOut/VaccineOut.aspx", MyPowerEnum.查詢);
        base.AddPower("/Vaccine/StockManagementM/VaccineUse/VaccineUse.aspx", MyPowerEnum.查詢);
        base.AddPower("/Vaccine/StockManagementM/VaccineDam/VaccineDam.aspx", MyPowerEnum.查詢);
        base.AddPower("/Vaccine/StockManagementM/VaccineReturn/VaccineReturn.aspx", MyPowerEnum.查詢);
    }
    public string VaccineData = "[]";
    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_StockManagementM_xGetVaccineData", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }
            }
        }
        VaccineData = JsonConvert.SerializeObject(ds.Tables[0]);
    }
}