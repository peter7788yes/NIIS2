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

public partial class System_SystemSettingsM_SwitchAccountSet_SwitchAccountSetMonth : BasePage
{
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));

    public System_SystemSettingsM_SwitchAccountSet_SwitchAccountSetMonth()
    {
        SearchPower = base.AddPower("/System/SystemSettingsM/SwitchAccountSet/SwitchAccountSet.aspx", MyPowerEnum.查詢);
    }
    public string OrgName { get; set; }
    public string Year = "[]";
    protected void Page_Load(object sender, EventArgs e)
    {
        SearchPower = base.AddPower(SearchPower);
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(true);

        int OrgID = 0;

        int.TryParse(Request.QueryString["OrgID"], out OrgID);

        OrgName = SystemOrg.GetName(OrgID);

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_AccountSet_xGetSearchData", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrgID", OrgID);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }
        Year = JsonConvert.SerializeObject(ds.Tables[0]);
    }
}