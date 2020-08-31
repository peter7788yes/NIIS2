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

public partial class Vaccine_StockManagementM_VaccineUse_New_VaccineUseDataListOP : BasePage
{
    public Vaccine_StockManagementM_VaccineUse_New_VaccineUseDataListOP()
    {
        base.AddPower("/Vaccine/StockManagementM/VaccineUse/VaccineUse.aspx", MyPowerEnum.新增);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        int VaccineUseID;

        int.TryParse(Request.Form["ID"], out VaccineUseID);

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_VaccineUse_xGetVaccineUseDate", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", VaccineUseID);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }
        List<VaccineUseDataVM> list = new List<VaccineUseDataVM>();
        List<VaccineUseBatchDataVM> list1 = new List<VaccineUseBatchDataVM>();
        StockManagementVM rtn = new StockManagementVM();

        EntityS.FillModel(list, ds.Tables[0]);
        EntityS.FillModel(list1, ds.Tables[1]);

        rtn.DataInfo = list;
        rtn.ListInfo = list1;
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}