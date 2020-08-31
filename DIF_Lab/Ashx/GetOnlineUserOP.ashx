<%@ WebHandler Language="C#" Class="GetOnlineUserOP" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using WayneEntity;
using System.Web.Configuration;

public class GetOnlineUserOP : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        DataTable dt = new DataTable();

         int SystemPowerCateID = 1;
        if (WebConfigurationManager.AppSettings["SystemPowerCateID"] != null)
        {
            SystemPowerCateID = Convert.ToInt32(WebConfigurationManager.AppSettings["SystemPowerCateID"]) ;
        }

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnUser"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_SystemM_xGetTotalOnlineUserByCateID", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SystemPowerCateID", SystemPowerCateID);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(dt);
                }

            }
        }


        context.Response.ContentType = "application/json; charset=utf-8";
        context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(dt));
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}