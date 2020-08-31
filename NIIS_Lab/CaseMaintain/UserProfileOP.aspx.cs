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

public partial class CaseMaintain_UserProfileOP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string action = Request.Form["action"] ?? "";
        JsonReply jr = new JsonReply();
        try
        {
           if (action == "IsRepeatNo")
            {
                jr.Content = "repeat";
                jr.RetCode = 0;
                int CaseID;
                int.TryParse(Request.Form["CaseID"], out CaseID);
                string IdNo = Request.Form["IdNo"] ?? "";
                if (Convert.ToInt32(DBUtil.DBOp("ConnDB", " SELECT  count([CaseID])  FROM [dbo].[C_CaseUser] where [CaseID]!={0} and IdNo={1}"
                        , new string[] { CaseID.ToString(), IdNo }, NSDBUtil.CmdOpType.ExecuteScalar)) == 0)
                {
                    jr.Content = " not repeat";
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