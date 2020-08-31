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

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["AO"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("select @@servername", sc))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(dt);
                }
            }
        }

        Response.Write(JsonConvert.SerializeObject(dt));
        Response.End();
    }
}