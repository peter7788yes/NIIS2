using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
using WayneEntity;

public partial class SelectSingleOrgByOrgID : System.Web.UI.Page
{
    public string MyTreeData = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        UserVM user = AuthServer.GetLoginUser();

        DataTable dt = new DataTable();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_PowerM_xGetOrgByOrgID", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrgID", user.OrgID);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(dt);
                }
            }
        }

        List<SystemOrgVM> list = new List<SystemOrgVM>();
        EntityS.FillModel(list, dt);

        if (Convert.ToInt32(WebConfigurationManager.AppSettings["OrgAreaSet"]) == 0)
        {
            list.RemoveAll(item => item.OrgLevel == 2);
            for (var i = 0; i < list.Count; i++)
            {
                if (list[i].OrgLevel == 3)
                {
                    list[i].PID = 1;
                }
            }
        }

        MyTreeData = JsonConvert.SerializeObject(list.Where(item => item.OrgCateID == Convert.ToInt32(WebConfigurationManager.AppSettings["OrgCateID"])));

    }
}