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

public partial class LogManage_LogItemErrorListOP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        int pgNow;
        int pgSize = 10 ;
        int LogCheckFileID = 0;
        int.TryParse(Request.Form["LogCheckFileID"], out LogCheckFileID);
        int.TryParse(Request.Form["pgNow"], out pgNow);
        pgNow = (pgNow == 0 ? 1 : pgNow);
          

        DataTableCollection dtc = (DataTableCollection)DBUtil.DBOp("ConnDB", "exec dbo.usp_Log_xLogErrorList {0},{1},{2}  ", new string[] { pgNow.ToString(), pgSize.ToString(), LogCheckFileID.ToString() }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTableCollection);
         

         PageVM rtn = new PageVM(); 
        EntityS.FillModel(rtn, dtc[1]);
        rtn.message =  dtc[0] ;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}