<%@ WebHandler Language="C#" Class="CheckLoginOP" %>

using System.Web;
using System.Web.SessionState;

public class CheckLoginOP : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        AllowHttpMethod(context.Request.HttpMethod,"POST");

        int Rtn = 0;
        if (context.Session["LoginUser"] != null)
            Rtn = 1;
        context.Response.Write(Rtn);
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
