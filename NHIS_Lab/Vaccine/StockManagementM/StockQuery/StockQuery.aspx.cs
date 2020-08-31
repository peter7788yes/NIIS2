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

public partial class Vaccine_StockManagementM_StockQuery_StockQuery : BasePage
{
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM PrintPower = new MyPowerVM("", default(MyPowerEnum));

    List<MyPowerVM> list = new List<MyPowerVM>();

    public Vaccine_StockManagementM_StockQuery_StockQuery()
    {
        list = base.AddPower("/Vaccine/StockManagementM/StockQuery/StockQuery.aspx", MyPowerEnum.查詢, MyPowerEnum.列印);
    }
    public string OrgName { get; set; }
    public string Vaccine = "[]";
    protected void Page_Load(object sender, EventArgs e)
    {
        SearchPower = base.AddPower(list[0]);
        PrintPower = base.AddPower(list[1]);
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);

        UserVM user = AuthServer.GetLoginUser();

        OrgName = string.Format("{0}", user.OrgName);

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_StockQuery_xGetSearchData", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }
        Vaccine = JsonConvert.SerializeObject(ds.Tables[0]);
    }
}