using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls; 
using System.Text;

public partial class CaseMaintain_ucCaseRemarkOP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //base.AllowHttpMethod("POST");
        string action = Request.Form["action"] ?? "";
        JsonReply jr = new JsonReply();

        try
        {
            if (action == "GetList")
            {
                int CaseID = 0;
                int.TryParse(Request.Form["c"], out CaseID);

                StringBuilder sb = new StringBuilder("");
                sb.Append("<table id=\"Reamrk_TB\">");
                sb.Append("<tr><th style=\"width:200px\">類別</td><th>內容</th><th style=\"width:1%\">維護</th><th  style=\"width:1%\">刪除</th></tr>");

                if (CaseID != 0)
                {
                    DataTable dt = (DataTable)DBUtil.DBOp("ConnDB", "EXECUTE  [dbo].[usp_CaseUser_xGetRemarkList]  {0} ", new string[] { CaseID.ToString() }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);
                    foreach (DataRow r in dt.Rows) sb.AppendFormat("<tr><td style=\"width:200px\">{0}</td><td>{1}</td><td><a href=\"javascript:void(0);\" id=\"ModifyRemark_{2}\" class=\"ModifyRemark\" ><img src=\"/images/icon_maintain.png\"></a></td><td> <a  href=\"javascript:void(0);\"  id=\"DeleteRemark_{2}\" class=\"DeleteRemark\"><img src=\"/images/icon_del01.gif\"></a></td></tr>", r["RemarkType"].ToString(), (Convert.ToInt32(r["FileID"]) > 0 ? "<a href=\"DownloadFileOP.aspx?i=" + r["FileID"].ToString() + "\">" + r["CaseRemark"].ToString() + "</a>" : r["CaseRemark"].ToString()), r["ID"].ToString());
                }
                sb.Append("</table>");
                jr.Content = sb.ToString();
                jr.RetCode = 1;
            }
            else if (action == "Delete")
            {
                int RemarkID = 0;
                int.TryParse(Request.Form["RemarkID"], out RemarkID);
                if (Convert.ToInt32(DBUtil.DBOp("ConnDB", " Update [C_CaseUserRemark] set LogicDel =1 where ID={0} ;select @@rowcount; ", new string[] { RemarkID.ToString() }, NSDBUtil.CmdOpType.ExecuteScalar)) > 0) ;
                {
                    jr.Content = "刪除成功";
                    jr.RetCode = 1;
                }
            }
            else if (action == "GetRemarkTr")
            {
                int RemarkID = 0;
                int.TryParse(Request.Form["RemarkID"], out RemarkID);
                UserRemark ur = new UserRemark(RemarkID);
                if (ur.RemarkID > 0)
                {
                    jr.Content = string.Format("<tr><td style=\"width:200px\">{0}</td><td>{1}</td><td><a href=\"javascript:void(0);\" id=\"ModifyRemark_{2}\" class=\"ModifyRemark\" ><img src=\"/images/icon_maintain.png\"></a></td><td> <a  href=\"javascript:void(0);\"  id=\"DeleteRemark_{2}\" class=\"DeleteRemark\"><img src=\"/images/icon_del01.gif\"></a></td></tr>", ur.RemarkTypeName, (ur.FileID > 0 ? "<a href=\"DownloadFileOP.aspx?i=" + ur.FileID.ToString() + "\">" + ur.RemarkContent.ToString() + "</a>" : ur.RemarkContent.ToString()), ur.RemarkID.ToString());
                }
                else
                    jr.Content = "";
                jr.RetCode = 1;


            }
        }
        catch (Exception ex)
        {
            jr.Content = "失敗" + ex.Message ;
            jr.RetCode = 0;
        }
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(jr));
     
        Response.End();
    }
}