using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class CaseMaintain_UpdateReview : BasePage 
{
    public int iCaseID = 0;
    public int iApplyID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        QS();
        if (!Page.IsPostBack)
        {
            if (iApplyID > 0)
                BindData();
        }
    }

    protected void BindData()
    {
        StringBuilder sb = new StringBuilder("<table><caption>您修改的以下欄位資料，需檢附證明文件</caption><tr><th>修改欄位</th><th>修改前內容</th><th>修改後內容</th><th>檢附文件</th><th>上傳文件</th></tr>");

        string UpdateFieldTableFormat = "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td><input type=\"file\" name=\"fu_{4}\" id=\"fu_{4}\"></td></tr>";
        DataTable dt = (DataTable)DBUtil.DBOp("ConnDB", "exec dbo.usp_CaseCheck_xUpdateCaseCheckList {0}", new string[] { iApplyID.ToString () }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow r in dt.Rows)
            {
                sb.AppendFormat(UpdateFieldTableFormat, r["FieldDiscription"].ToString(), r["ValBefore"].ToString(), r["ValAfter"].ToString(), r["FileCheck"].ToString(), r["ID"].ToString());
            }

            iCaseID = Convert.ToInt32(dt.Rows[0]["ModifyCaseID"]);
            BindCaseData(iCaseID);
        }
        sb.Append("</table>");
        ltUpdateFields.Text = sb.ToString();
    }

    protected void BindCaseData(int caseid)
    {
        CaseUserProfile c = new CaseUserProfile(caseid);
            if (c.CaseID > 0)
            {
                ltName.Text = c.Name; 
                ltIdNo.Text = c.IdNo;  
                ltResAddr.Text = c.ResFullAddress;
            }

    }
    protected void QS()
    {
        if (Request["a"] != null)
            int.TryParse(Request["a"].ToString(), out iApplyID);
    
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
         
        StringBuilder sb = new StringBuilder("");

      　  DataTable dt = (DataTable)DBUtil.DBOp("ConnDB", "exec dbo.usp_CaseCheck_xUpdateCaseCheckList {0}", new string[] { iApplyID.ToString() }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);
         if (dt.Rows.Count > 0)
         {
             foreach (DataRow r in dt.Rows)
             {
                 if (Request["fu_" + r["ID"].ToString()] != null)
                 {


                 }

             }


         }


         Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "location.href='UserProfileList.aspx'", true);
      
//Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "location.href='UserProfile.aspx?i=" + iCaseID.ToString () + "'", true);
      
    }
}