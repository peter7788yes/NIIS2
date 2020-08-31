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

public partial class CaseMaintain_CapacityHistoryOP : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //base.AllowHttpMethod("POST");

        int pgNow;
        int pgSize;

        int CapacityID = 1;
        int CaseID = 0;

        int.TryParse(Request.Form["pgNow"], out pgNow);
        int.TryParse(Request.Form["pgSize"], out pgSize);
        int.TryParse(Request.Form["CapacityID"], out CapacityID);
        int.TryParse(Request.Form["CaseID"], out CaseID); 
       pgNow = ( pgNow == 0 ? 1 : pgNow);
       pgSize = ( pgSize == 0 ? 10 : pgSize);


       //Response.Write(pgNow.ToString());
       //Response.Write(pgSize.ToString());
       //Response.Write(CaseID.ToString());
       //Response.Write(CapacityID.ToString());
       DataTableCollection dtc = (DataTableCollection)DBUtil.DBOp("ConnDB"
            ," exec dbo.usp_CaseUser_xGetCapacityModifyLog {0},{1},{2},{3}"
            , new string[]{pgNow.ToString (),pgSize .ToString (),CaseID.ToString (),CapacityID .ToString ()}
            ,NSDBUtil.CmdOpType .ExecuteReaderReturnDataTableCollection);


       List<CapacityModifyLogVM> list = new List<CapacityModifyLogVM>();
        PageVM rtn = new PageVM();

        EntityS.FillModel(list, dtc[0]);
        EntityS.FillModel(rtn, dtc[1]);
        rtn.message = list;
 
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
     
        Response.End();
    }
}