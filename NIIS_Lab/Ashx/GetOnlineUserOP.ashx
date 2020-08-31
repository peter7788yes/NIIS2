<%@ WebHandler Language="C#" Class="GetOnlineUserOP" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public class GetOnlineUserOP : IHttpHandler {

    public void ProcessRequest (HttpContext context)
    {
        AllowHttpMethod(context.Request.HttpMethod,"POST");

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

    protected void AllowHttpMethod(string myMethod,params string[] methods)
    {
        bool HasPower = false;

        System.Collections.Generic.List<string> list = new  System.Collections.Generic.List<string>(methods);

        for (int i = 0; i <= methods.Length - 1; i++)
        {
            if (methods[i].Trim().ToUpper().Equals(myMethod))
            {
                HasPower = true;
                break;
            }
        }


        if (HasPower == false)
        {
            throw new HttpException(404, "Not found");
            //Response.Redirect("~/html/ErrorPage/NoPower.html");
            //string myScript = "<script>alert('您無權操作此頁面');history.go(-1);</script>";
            //Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "DisableTop", myScript, false);
        }
    }

}