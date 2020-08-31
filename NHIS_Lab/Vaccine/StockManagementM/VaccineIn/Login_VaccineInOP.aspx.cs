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

public partial class Vaccine_StockManagementM_VaccineIn_Login_VaccineInOP : BasePage
{
    public Vaccine_StockManagementM_VaccineIn_Login_VaccineInOP()
    {
        base.AddPower("/Vaccine/StockManagementM/VaccineIn/VaccineIn.aspx", MyPowerEnum.新增);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        int VaccineOutID;

        int.TryParse(Request.Form["ID"], out VaccineOutID);

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_VaccineIn_xGetLoginData", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", VaccineOutID);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }
        List<VaccineInPendingVM> list = new List<VaccineInPendingVM>();
        List<VaccineInPendDataVM> list1 = new List<VaccineInPendDataVM>();
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