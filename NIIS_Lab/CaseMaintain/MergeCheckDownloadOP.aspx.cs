using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class CaseMaintain_MergeCheckDownloadOP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //base.AllowHttpMethod("POST");
         
        string BirthDateS;
        string BirthDateE;
        int SearchKind=1;
        int OrgID = 0;

        BirthDateS = Request.Form["BirthDateS"] ?? "";
        BirthDateE = Request.Form["BirthDateE"] ?? "";
        if (BirthDateS != "") BirthDateS = TaiwanYear.ToDateString(BirthDateS);
        if (BirthDateE != "") BirthDateE = TaiwanYear.ToDateString(BirthDateE);
         
        int.TryParse(Request["SearchKind"], out SearchKind);
        int.TryParse(Request["OrgID"], out OrgID);

        DataTable dt = (DataTable)DBUtil.DBOp("ConnDB", "  exec dbo.usp_CaseUser_xGetMergeUserListToExport {0},{1},{2},{3} "
            , new string[] { 
            BirthDateS,BirthDateE,OrgID.ToString (),SearchKind.ToString () 
            }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);
         

      //DataTableToCSV.DownloadCSV(dt, "MyCsv.csv");
            ExcelToolT t =new ExcelToolT ();  
            Response.Clear();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/download";
            Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode("99未歸戶個案維護.csv"));
            Response.BinaryWrite(((MemoryStream)t.RenderDataTableToExcel(dt)).ToArray());
            Response.End();
    }
}