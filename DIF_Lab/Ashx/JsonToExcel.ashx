<%@ WebHandler Language="C#" Class="JsonToExcel" %>

using System;
using System.Web;
using Newtonsoft.Json;
using System.Data;
public class JsonToExcel : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        if (context.Request.HttpMethod.Equals("POST"))
        {
            string json = context.Server.UrlDecode(context.Request.Form["json"] ?? "");
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
            if (dt != null && dt.Rows.Count > 0)
            {
                context.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=ErrorWorkbook.xls"));
                ExcelToolT tool = new ExcelToolT();
                tool.RenderDataTableToExcel(dt).CopyTo(context.Response.OutputStream);
            }
        }
        else
        {
            context.Response.Write("");
            context.Response.End();
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}