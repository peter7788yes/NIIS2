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

public partial class CaseMaintain_MergeCheckOP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    { 
        int pgNow;
        int pgSize=5; 
        string BirthDateS;
        string BirthDateE;
        int SearchKind;
        int OrgID=0; 
        BirthDateS = Request.Form["BirthDateS"] ?? "";
        BirthDateE = Request.Form["BirthDateE"] ?? ""; 
        if (BirthDateS!="") BirthDateS = (Convert.ToInt32(BirthDateS.Substring(0, 3)) + 1911).ToString() + "/" + BirthDateS.Substring(3, 2) + "/" + BirthDateS.Substring(5, 2);
        if (BirthDateE != "")  BirthDateE = (Convert.ToInt32(BirthDateE.Substring(0, 3)) + 1911).ToString() + "/" + BirthDateE.Substring(3, 2) + "/" + BirthDateE.Substring(5, 2);

         

        int.TryParse(Request.Form["pgNow"], out pgNow); 
        int.TryParse(Request.Form["SearchKind"], out SearchKind);
        int.TryParse(Request.Form["OrgID"], out OrgID); 

         

        DataTableCollection dtc = (DataTableCollection)DBUtil.DBOp("ConnDB"
             , "exec dbo.usp_CaseUser_xGetMergeUserList {0},{1},{2},{3},{4},{5} "
             , new string[]{   pgNow.ToString ()
                              , pgSize.ToString ()  
                              ,BirthDateS
                              ,BirthDateE 
                              ,OrgID.ToString ()     
                              ,SearchKind.ToString () 
                 }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTableCollection);
         
         

        List<MergeCheckListVM> list = new List<MergeCheckListVM>();
        PageVM rtn = new PageVM();

        EntityS.FillModel(list, dtc[0]);
        EntityS.FillModel(rtn, dtc[1]);
        rtn.message = list;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}