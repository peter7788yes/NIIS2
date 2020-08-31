<%@ WebHandler Language="C#" Class="SystemAreaCodeOP" %>

using System.Web;

public class SystemAreaCodeOP : IHttpHandler {

    public void ProcessRequest (HttpContext context)
    {
        AllowHttpMethod(context.Request.HttpMethod,"POST");

        string CountyTownData = "[]";

        string AreaCode = context.Request.Form["a"] ?? "";
        int p = 0;
        int.TryParse(context.Request.Form["p"], out p);

        if (AreaCode == "County")
        {
            CountyTownData = Newtonsoft.Json.JsonConvert.SerializeObject(SystemAreaCode.GetCountyList());
        }
        else if (AreaCode == "Town")
        {
            if (p > 0) CountyTownData = Newtonsoft.Json.JsonConvert.SerializeObject(SystemAreaCode.GetTownList(p));

        }
        else if (AreaCode == "Village")
        {
            if (p > 0) CountyTownData = Newtonsoft.Json.JsonConvert.SerializeObject(SystemAreaCode.GetVillageList(p));
        }

        context.Response.ContentType = "application/json; charset=utf-8";
        context.Response.Write(CountyTownData);
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