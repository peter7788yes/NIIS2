using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneDB;
using WayneEntity;
using WayneTools;

public partial class LogOP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        int pgNow;
        int pgSize;


        int.TryParse(Request.Form["pgNow"], out pgNow);
        int.TryParse(Request.Form["pgSize"], out pgSize);

        DataSet ds = new DataSet();
        //ds = MSDB.GetDataSet("exec usp_LogM_xPageList @1,@2", pgNow, pgSize);
        
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnLog"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_LogM_xPageList", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pgNow", pgNow);
                cmd.Parameters.AddWithValue("@pgSize", pgSize);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }

        List<LogVM> list = new List<LogVM>();
        PageVM rtn = new PageVM();

        EntityS.FillModel(list, ds.Tables[0]);
        EntityS.FillModel(rtn, ds.Tables[1]);

        rtn.message = list;
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();

    }
}