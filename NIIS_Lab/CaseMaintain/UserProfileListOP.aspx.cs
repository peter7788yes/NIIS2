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

//public partial class CaseMaintain_UserProfileListOP : BasePage
public partial class CaseMaintain_UserProfileListOP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //base.AllowHttpMethod("POST");
        //base.DisableTop(true);
        int pgNow = 1, pgSize = 10;
        int OrderCol = 0, OrderAsc = 1;//0出生日期1身份證號    //1ASC 0 DESC
        int OrgID = 0;
        int AddrKind=0 ,CountyID = 0,TownID= 0;
        int SearchNumberType = 0;
        string BirthDateS ="",BirthDateE = ""; 
        string CaseName = "",CaseIdNo = "", HouseNo = "";
        string ContactName = "",ContactIdNo = "", ContactBirthDate = "";
        #region QS()
        BirthDateS = Request.Form["BirthDateS"] ?? "";
        BirthDateE = Request.Form["BirthDateE"] ?? "";
        ContactBirthDate = Request.Form["ContactBirthDate"] ?? "";
        if (BirthDateS!="") BirthDateS =  TaiwanYear.ToDateString(BirthDateS);
        if (BirthDateE != "") BirthDateE = TaiwanYear.ToDateString(BirthDateE);  
        if (ContactBirthDate != "") ContactBirthDate = TaiwanYear.ToDateString(ContactBirthDate);  
       
        CaseName = Request.Form["CaseName"] ?? "";
        CaseIdNo = Request.Form["CaseIdNo"] ?? "";
        HouseNo = Request.Form["HouseNo"] ?? "";
        ContactName = Request.Form["ContactName"] ?? "";
        ContactIdNo = Request.Form["ContactIdNo"] ?? "";

        int.TryParse(Request.Form["pgNow"], out pgNow);
        int.TryParse(Request.Form["pgSize"], out pgSize);
        int.TryParse(Request.Form["AddrKind"], out AddrKind);
        int.TryParse(Request.Form["OrgID"], out OrgID);
        int.TryParse(Request.Form["CountyID"], out CountyID);
        int.TryParse(Request.Form["TownID"], out TownID);
        int.TryParse(Request.Form["NumberType"], out SearchNumberType);

        int.TryParse(Request.Form["OrderCol"], out OrderCol);
        int.TryParse(Request.Form["OrderAsc"], out OrderAsc);


        #endregion

        OrgID = AuthServer.GetLoginUser().OrgID;


       DataTableCollection dtc = (DataTableCollection)DBUtil.DBOp("ConnDB"
            , "exec dbo.usp_CaseUser_xGetUserList {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}"
            , new string[]{   pgNow.ToString ()
                      , pgSize.ToString ()
                      , OrderCol.ToString ()
                      , OrderAsc.ToString ()
                      , CaseName
                      , CaseIdNo
                      ,BirthDateS
                      ,BirthDateE
                      ,HouseNo
                      ,AddrKind.ToString ()
                      ,ContactName
                      ,ContactIdNo
                      ,ContactBirthDate
                      ,OrgID.ToString ()     //請帶入想查看的ORG   //若沒得選 請帶入目前登入user.org
                      ,CountyID.ToString ()
                      ,TownID.ToString ()
                      ,SearchNumberType.ToString ()
     }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTableCollection);

       

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