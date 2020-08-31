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

public partial class SearchCheck_ExportFileOP : BasePage
{
    int AuditID = 0; 
     
    protected void Page_Load(object sender, EventArgs e)
   { 
   int.TryParse(Request["a"], out AuditID); 

        DataTable dt = (DataTable)DBUtil.DBOp("ConnDB", "exec  [dbo].[usp_SearchCheck_xExportSearchLogDetailList] {0}  ",
              new string[] {   AuditID.ToString() }
            , NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);
         

        ExportToFile etf = new ExportToFile();

        etf.ExporttoExcel(dt, "記錄下載");
    }

 

}
 
