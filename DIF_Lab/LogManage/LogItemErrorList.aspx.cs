using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class LogManage_LogItemErrorList : System.Web.UI.Page
{ 
    protected int LogCheckFileID = 0;
    protected string tbTH = "";
    protected string tbTD = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        int.TryParse(Request.Form["f"], out LogCheckFileID);
        tbTH ="<th scope=\"col\" style=\"width :5%\">序號</th>";
        tbTD = "<td class=\"aCenter\" ng-bind='record[\"S\"]'></td>"; 
                       
        tbTH += "<th scope=\"col\" >錯誤訊息</th>";
        tbTD += "<td class=\"aCenter\" ng-bind='record[\"ErrorMsg\"]'></td>"; 

          string strTH = Convert.ToString(DBUtil.DBOp("ConnDB", "SELECT  [ItemFieldName]  FROM [dbo].[L_LogItem] where LogItemID = (SELECT  [FileItem]  FROM [dbo].[L_LogCheckItemFile] where ID={0}) ", new string[] {LogCheckFileID.ToString () }, NSDBUtil.CmdOpType.ExecuteScalar));
          string[] arrayTH=strTH.Split (',');
       int index =1;
        foreach (string th in arrayTH)
        {
            tbTH += string.Format ("<th scope=\"col\">{0}</th>",th);
            tbTD += "<td class=\"aCenter\" ng-bind='record[\"Column" + index.ToString() + "\"]'></td>";
            index ++;
        }
        
        
        //Response.Write(LogCheckMainID.ToString ());
    }
}