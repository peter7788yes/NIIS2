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
using System.Text;

public partial class CaseVisit_VisitCaseContentOP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string action = Request.Form["action"] ?? "";
        JsonReply jr = new JsonReply();
        jr.RetCode = 0;
        try
        {
           if (action == "DelVisitFile")
            { 
                int VisitFileID;
                int.TryParse(Request.Form["VisitFileID"], out VisitFileID);
                if (Convert.ToInt32(DBUtil.DBOp("ConnDB", " UPDATE [dbo].[C_CaseVisitFile] SET [LogicDel] = 1 WHERE ID={0} ;select @@rowcount "
                        , new string[] { VisitFileID.ToString() }, NSDBUtil.CmdOpType.ExecuteScalar)) >0)
                {
                    jr.Content = "成功";
                    jr.RetCode = 1;
                } 
                

            }
        }
        catch {
            jr.RetCode = 0;
        }

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(jr));
        Response.End();
    }
}