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

public partial class CaseMaintain_ChooseUserContactListOP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        int pgNow=1;
        int pgSize=10; 
        string NameOrIdNo = Request.Form["NameOrIdNo"] ?? "";  
        int.TryParse(Request.Form["pgNow"], out pgNow);
        int.TryParse(Request.Form["pgSize"], out pgSize); 
        pgNow = (pgNow == 0 ? 1 : pgNow);
        pgSize = (pgSize == 0 ? 10 : pgSize);

        string SearchName = "";
        string SearchIdNo = "";
        string SearchResNo = "";

        if (NumberValidate.IsIdNo(NameOrIdNo) || (NameOrIdNo.StartsWith("99") && NameOrIdNo.Length ==10))
        {
            SearchIdNo = NameOrIdNo;

        } else 
            
            if (NumberValidate.IsResNo(NameOrIdNo))
        {
            SearchResNo = NameOrIdNo;
        }
        else
        {
            SearchName = NameOrIdNo;
        }


        DataTableCollection dtc = (DataTableCollection)DBUtil.DBOp("ConnDB"
          , " exec dbo.usp_CaseUser_xGetContactList {0},{1},{2},{3},{4}  "
          , new string[] { pgNow.ToString(), pgSize.ToString(), SearchName, SearchIdNo, SearchResNo }
          , NSDBUtil.CmdOpType.ExecuteReaderReturnDataTableCollection);
         
        List<UserProfileListVM> list = new List<UserProfileListVM>();
        PageVM rtn = new PageVM();

        EntityS.FillModel(list, dtc[0]);
        EntityS.FillModel(rtn, dtc[1]);
        rtn.message = list;

        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rtn));
        Response.End();
    }
}