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

public partial class LogManage_LogMainListOP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //base.AllowHttpMethod("POST");
        //base.DisableTop(true);
        int pgNow;
        int pgSize = 10 ;
        int LogCheckMainID = 0;
        int.TryParse(Request.Form["LogCheckMainID"], out LogCheckMainID);
        int.TryParse(Request.Form["pgNow"], out pgNow);
        pgNow = (pgNow == 0 ? 1 : pgNow);
         
        DataSet ds = new DataSet();

        DataTableCollection dtc = (DataTableCollection)DBUtil.DBOp("ConnDB", "exec dbo.usp_Log_xLogMainList {0},{1},{2} ", new string[] { pgNow.ToString(), pgSize.ToString(), LogCheckMainID.ToString () }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTableCollection);
      
        List<LogListVM> list = new List<LogListVM>();
        PageVM rtn = new PageVM(); 
        EntityS.FillModel(list, dtc[0]);
        EntityS.FillModel(rtn, dtc[1]);
        rtn.message = list;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}