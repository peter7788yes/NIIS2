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

public partial class CaseMaintain_UserProfileModifyListOP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //base.AllowHttpMethod("POST");

        int pgNow;
        int pgSize;
        int CaseID;  
        int.TryParse(Request.Form["pgNow"], out pgNow);
        int.TryParse(Request.Form["pgSize"], out pgSize);
        int.TryParse(Request.Form["CaseID"], out CaseID); 

 
        DataTableCollection dtc = (DataTableCollection)DBUtil.DBOp("ConnDB"
        , "exec dbo.usp_CaseUser_xGetUserModifyLog {0},{1},{2} "
        , new string[]{   pgNow.ToString ()
                      , pgSize.ToString ()
                      , CaseID.ToString () 
                      }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTableCollection);

        List<UserModifyLogVM> list = new List<UserModifyLogVM>();
        PageVM rtn = new PageVM();

        EntityS.FillModel(list, dtc[0]);
        EntityS.FillModel(rtn, dtc[1]);
        rtn.message = list;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}