using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class SearchCheck_DoAudit : System.Web.UI.Page
{
    int AuditID = 0;
    String savePath="" ;
    protected void Page_Load(object sender, EventArgs e)
    {savePath= Server.MapPath("~/UploadFile/SearchCheck/");
        
        if (!Page.IsPostBack)
        {     Session["DoAuditID"] = null;
              Session["DoAuditFileLoc"]  = null;
            int.TryParse(Request["a"], out AuditID); 
            
            if (AuditID != 0)
            {
                Session["DoAuditID"] = AuditID;
                DataTable dt = (DataTable)DBUtil.DBOp("ConnDB", "SELECT FileRealName,FileName, convert(varchar,year([AuditYearMonth]))+ '-' + convert(varchar,month([AuditYearMonth])) YearMonth , isnull([AuditResult],0) AuditResult ,sk.KindName as SearchKindName  FROM [dbo].[C_CaseSearchAudit]  inner join  (select 1 as ID ,'批次資料查詢' as KindName  union all select 2,'單筆資料查詢'    union all select 3,'批次資料勾稽'    union all select 4,'親子資料勾稽' ) as sk   on sk.ID = C_CaseSearchAudit.SearchKind where C_CaseSearchAudit.[id]={0}", new string[] { AuditID.ToString() }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);

                if (dt.Rows.Count > 0)
                {
                    ltYearMonth.Text = Server.HtmlEncode( dt.Rows[0]["YearMonth"].ToString());
                    ltSearchKindName.Text = Server.HtmlEncode(dt.Rows[0]["SearchKindName"].ToString());
                    FileLinkButton.Text = Server.HtmlEncode(dt.Rows[0]["FileName"].ToString());
                    ddl_AuditStatus.SelectedValue = dt.Rows[0]["AuditResult"].ToString();
                    if (dt.Rows[0]["FileRealName"].ToString()!="")
                    {   Session["DoAuditFileName"]= dt.Rows[0]["FileName"].ToString();
                        Session["DoAuditFileLoc"] = savePath + AuditID.ToString ()+"\\"+  dt.Rows[0]["FileRealName"].ToString();
                    }

                }
            }
        }
    }
    protected void btnAudit_Click(object sender, EventArgs e)
    {

        if (Session["DoAuditID"] != null)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    String FileName = FileUpload1.FileName;
                    String ext = FileName.Split('.').Last();
                    String FileRealName = DateTime.Now.ToString("yyyyMMddhhmmssfff") + "." + ext;
                   String FileSavePath =savePath + Session["DoAuditID"].ToString() + "/" ;

                    if (!Directory.Exists(FileSavePath)) Directory.CreateDirectory(FileSavePath);
                    String saveResult = FileSavePath + FileRealName;
                    FileUpload1.SaveAs(saveResult);
                    DBUtil.DBOp("ConnDB", "UPDATE [dbo].[C_CaseSearchAudit]  SET AuditDate =getdate(),AuditResult={3}, [FileName] = {1} ,[FileRealName] = {2}  WHERE ID={0}", new string[] { Session["DoAuditID"].ToString(), FileName, FileRealName,ddl_AuditStatus .SelectedValue }, NSDBUtil.CmdOpType.ExecuteNonQuery);

                }
            }
            catch { 
            
            }
        }
        Session["DoAuditID"] = null;

        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('成功!');location.href='AuditSearchLogList.aspx'", true);
   

    }
    protected void FileLinkButton_Click(object sender, EventArgs e)
    {
      
        if (Session["DoAuditFileLoc"] != null && File.Exists(Session["DoAuditFileLoc"].ToString()))
        {
            try
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + Session["DoAuditFileName"].ToString() + "\"");
                Response.WriteFile(Session["DoAuditFileLoc"].ToString());
                Response.End(); 

            }
            catch (Exception)
            { }
        }
    }
}