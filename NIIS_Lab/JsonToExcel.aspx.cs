using Newtonsoft.Json;
using System;
using System.Data;

public partial class JsonToExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Request.HttpMethod.Equals("POST"))
        //{
        //    string json = Server.UrlDecode(Request.Form["json"] ?? "");
        //    DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        Response.AddHeader("Content-Disposition", string.Format("attachment; filename=ErrorWorkbook.xls"));
        //        ExcelToolT tool = new ExcelToolT();
        //        tool.RenderDataTableToExcel(dt).CopyTo(Response.OutputStream);
        //    }
        //}
        //else
        //{
        //    Response.Write("");
        //    Response.End();
        //}
    }
}