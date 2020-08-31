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

public partial class LogManage_CheckFileLogListOP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //base.AllowHttpMethod("POST");
        //base.DisableTop(true);
        int pgNow=0;
        int pgSize=0;
        int CheckStatus = 0;  
        int.TryParse(Request.Form["pgNow"], out pgNow);
        int.TryParse(Request.Form["pgSize"], out pgSize);
        int.TryParse(Request.Form["CheckStatus"], out CheckStatus);
         pgNow = (pgNow == 0 ? 1 : pgNow);
         pgSize = (pgSize == 0 ? 10 : pgSize);


        string FileDateS = Request.Form["FileDateS"] ?? "";
        string FileDateE = Request.Form["FileDateE"] ?? "";
      
         
         
        DataTableCollection dtc = (DataTableCollection)DBUtil.DBOp("ConnDB"
      , "exec dbo.usp_Log_xCheckFileList {0},{1},{2},{3},{4}  "
      , new string[]{   pgNow.ToString ()
                              , pgSize.ToString ()  
                              ,FileDateS
                              ,FileDateE  
                              ,CheckStatus.ToString () 
                 }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTableCollection);
         

        List<CheckFileLogListVM> list = new List<CheckFileLogListVM>();
        PageVM rtn = new PageVM();

        EntityS.FillModel(list, dtc[0]);
        EntityS.FillModel(rtn, dtc[1]);
        rtn.message = list;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}