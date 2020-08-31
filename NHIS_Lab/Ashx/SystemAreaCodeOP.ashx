<%@ WebHandler Language="C#" Class="SystemAreaCodeOP" %>

using System;
using System.Web;

public class SystemAreaCodeOP : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) { 

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

}