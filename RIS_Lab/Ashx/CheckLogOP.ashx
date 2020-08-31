<%@ WebHandler Language="C#" Class="CheckLogOP" %>

using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Web.Configuration;

public class CheckLogOP : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        AllowHttpMethod(context.Request.HttpMethod,"POST");

        string jsonString = HttpContext.Current.Request.Form["o"] ?? "";
        var VM = JsonConvert.DeserializeObject<PassLogVM>(QueryStringEncryptToolS.Decrypt(jsonString));
        //var VM = JsonConvert.DeserializeObject<PassLogVM>(jsonString);

        DataTable dt = new DataTable();

        string SQL = string.Format("select CreateType,ChangeDate,ChangeUserID,ChangeOrgID from {0} where 1=1 ", VM.TableName);

        if (VM.WhereDict == null)
        {
            return;
        }

        foreach (var item in VM.WhereDict)
        {
            SQL = SQL + string.Format(" and {0}='{1}' ",item.Key.Trim('@'),item.Value.ToString());
        }

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnLog"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand(SQL, sc))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(dt);
                }

            }
        }

        List<GeneralLogVM> list = new List<GeneralLogVM>();

        WayneEntity.EntityS.FillModel(list, dt);

        Dictionary<string, object> dict = new Dictionary<string, object>();

        if (list.Count > 0)
        {
            dict["chk"] = 1;
        }
        else
        {
            dict["chk"] = 0;
        }
        var msg1 = list.Find(item => item.CreateType == 1);
        dict["msg1"] = msg1;
        list.Remove(msg1);
        dict["msg2"] = list;

        context.Response.ContentType = "application/json; charset=utf-8";
        context.Response.Write(JsonConvert.SerializeObject(dict));
        context.Response.End();

    }

    public bool IsReusable
    {
        get
        {
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
