using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneEntity;
using Newtonsoft.Json;
using System.Text;

public partial class SearchCheck_AddAuditOP : System.Web.UI.Page
{
    int UserID = 0;
    int Year = 0;
    int Month = 0;
    int SearchKind = 0;
    int action = 0; //0 add  2 del
    protected void Page_Load(object sender, EventArgs e)
    {
        


        int.TryParse(Request.Form["UserID"], out UserID);
        int.TryParse(Request.Form["YearMonth"].ToString ().Split ('-')[0], out Year);
        int.TryParse(Request.Form["YearMonth"].ToString().Split('-')[1], out Month);
        int.TryParse(Request.Form["SearchKind"], out SearchKind);

        int.TryParse(Request.Form["action"], out action);

        JsonReply r = new JsonReply();

        if (UserID > 0 &&  Year > 0 && Month > 0 && SearchKind > 0 )
            DBUtil.DBOp("ConnDB", " exec [dbo].[usp_SearchCheck_xAddAudit]   {0},{1},{2},{3},{4} "
                , new string[] { UserID.ToString(), (Year + 1911).ToString(), Month.ToString(), SearchKind.ToString(), action.ToString () }
                , NSDBUtil.CmdOpType.ExecuteNonQuery);
       
        r.RetCode = 1;
       r.Content = "ok"  ;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(r));
        Response.End();
    }

 

}
 
