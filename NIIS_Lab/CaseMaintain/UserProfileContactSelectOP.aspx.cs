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

public partial class CaseMaintain_UserProfileContactSelectOP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        int CaseID;
        int.TryParse(Request.Form["CaseID"], out CaseID);
         
        Response.ContentType = "application/json; charset=utf-8";
        if (CaseID != 0)
        {
            DataTable dt = (DataTable)DBUtil.DBOp("ConnDB", " exec dbo.usp_CaseUser_xGetCaseUserContactListSelect {0} "
                , new string[] { CaseID.ToString() }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);

            Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(dt));
        }
        else
        {
            //從session撈
            if (Session["NewCaseContacts"] != null)
            {
                DataTable dt = (DataTable)DBUtil.DBOp("ConnDB", " exec dbo.usp_CaseUser_xGetCaseUserContactListSelectTemp {0} "
                    , new string[] { Session["NewCaseContacts"].ToString() }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);

                Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(dt));
            }
        
        }
        //Response.Write("NewCaseContacts=" + Session["NewCaseContacts"].ToString ());
      
        Response.End();
    }

    
}
 