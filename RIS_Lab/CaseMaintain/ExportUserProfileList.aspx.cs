using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Data;

public partial class CaseMaintain_ExportUserProfileList : BasePage
{
    //protected string  CountyData = "[]";
    //protected string  TownData = "[]";
    protected void Page_Load(object sender, EventArgs e)
    {
        base.DisableTop(true);  
          // CountyData = JsonConvert.SerializeObject(SystemAreaCode.GetCountyList());
         // TownData = JsonConvert.SerializeObject(SystemAreaCode.GetCountyList());

        if (!Page.IsPostBack)
        {
            cblReportFields.Items.Clear();
            DataTable dt = DB.GetDataTable("SELECT [ID] ,[FieldDescription] text  FROM [dbo].[C_CaseField] where [bReportCol]=1 ", "ConnDB");
            foreach (DataRow r in dt.Rows)
                cblReportFields.Items.Add(new ListItem(r["text"].ToString(), r["ID"].ToString()));
        }

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
         
        int CountyID = 0;
        int TownID = 0;
        string BirthDateS;
        string BirthDateE;
        string CaseName;
        string CaseIdNo;
        string SearchReason;
        string SearchConditions = "";
        string ReportFields = "";
        int SearcResultCount = 0;
        int IsSearch = 0;
        int SearchKind = 3;

        BirthDateS = Request.Form["BirthDateS"] ?? "";
        BirthDateE = Request.Form["BirthDateE"] ?? "";
        if (BirthDateS != "")
        {
            try
            {
                BirthDateS = TaiwanYear.ToDateTime(BirthDateS).ToString("yyyyMMdd");

            }
            catch
            {
                BirthDateS = "";
            }
        }
        if (BirthDateE != "")
        {
            try
            {
                BirthDateE = TaiwanYear.ToDateTime(BirthDateE).ToString("yyyyMMdd"); 
            }
            catch
            {
                BirthDateE = "";
            }
        }
 
        CaseName = Request.Form["CaseName"] ?? "";
        CaseIdNo = Request.Form["CaseIdNo"] ?? ""; 
        int.TryParse(Request.Form["CountyID"], out CountyID);
        int.TryParse(Request.Form["TownID"], out TownID);
        int.TryParse(Request.Form["IsSearch"], out IsSearch);
        SearchReason = Request.Form["SearchReason"] ?? ""; 
             foreach (ListItem item in cblReportFields.Items) {if (item.Selected)ReportFields+= item.Value + ",";}
             ReportFields = ReportFields.TrimEnd(',');

           // Response.Write(string.Format("exec dbo.usp_CaseUser_xGetUserListExport {0},{1},{2},{3},{4},{5},{6} ", CaseName, CaseIdNo, BirthDateS, BirthDateE, CountyID.ToString(), TownID.ToString(), ReportFields));
           //  Response.Write(ReportFields);
             DataTable dt = new DataTable();
             try
             {
                 dt = (DataTable)DBUtil.DBOp("ConnDB", "exec dbo.usp_CaseUser_xGetUserListExport {0},{1},{2},{3},{4},{5},{6} ", new string[] { CaseName, CaseIdNo, BirthDateS, BirthDateE, CountyID.ToString(), TownID.ToString(), ReportFields }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);

                 SearcResultCount = dt.Rows.Count;
             }
             catch
             {
            }
             finally { 
                   DBUtil.DBOp("ConnDB", " exec [dbo].[usp_CaseUser_xAddSearchLog] {0}, {1}, {2} ,{3} ,{4}", new string[] { AuthServer.GetLoginUser().ID.ToString(), SearchConditions, SearchReason, SearcResultCount.ToString(), SearchKind.ToString() }, NSDBUtil.CmdOpType.ExecuteNonQuery);
            
             }
             ExportToFile etf = new ExportToFile();

            etf.ExporttoExcel(dt, "批次資料勾稽");

    }
}