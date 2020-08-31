using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Text;

public partial class Import_EmiM_ImportList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);
    }

    protected string GetUserList()
    {
        StringBuilder sb = new StringBuilder();
        UserVM user = AuthServer.GetLoginUser();
        DataTable dt = new DataTable();
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("usp_SystemM_xGetUsersByOrgID", sc))
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

        if(dt != null)
        {
            for(int i=0;i<dt.Rows.Count;i++)
            {
                sb.Append("'" + dt.Rows[i]["UserName"].ToString() + "#" + dt.Rows[i]["ID"].ToString() + "',");
            }
        }
        if (sb.ToString().EndsWith(","))
            sb.Remove(sb.Length - 1, 1);
        return "[" + sb.ToString() + "]";
    }
}