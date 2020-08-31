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

public partial class System_SystemSettingsM_SwitchAccountSet_SwitchAccountSetOP : BasePage
{
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));

    public System_SystemSettingsM_SwitchAccountSet_SwitchAccountSetOP()
    {
        SearchPower = base.AddPower("/System/SystemSettingsM/SwitchAccountSet/SwitchAccountSet.aspx", MyPowerEnum.查詢);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        SearchPower = base.AddPower(SearchPower);
        int pgNow;
        int pgSize;
        int OrgID = 0;

        int.TryParse(Request.Form["pgNow"], out pgNow);
        int.TryParse(Request.Form["pgSize"], out pgSize);

        if (SearchPower.HasPower == true)
        {
            int.TryParse(Request.Form["OrgID"],out OrgID);
        }

        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_AccountSet_xSearchTable", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pgNow", pgNow);
                cmd.Parameters.AddWithValue("@pgSize", pgSize);
                cmd.Parameters.AddWithValue("@OrgID", OrgID);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }
        List<SwitchAccountSetVM> list = new List<SwitchAccountSetVM>();
        PageVM rtn = new PageVM();

        EntityS.FillModel(list, ds.Tables[0]);

        rtn.message = list;
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}