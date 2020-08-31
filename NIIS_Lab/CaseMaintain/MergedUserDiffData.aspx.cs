using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSDBUtil;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class CaseMaintain_MergedUserDiffData : System.Web.UI.Page
{
    public int iCaseID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        QS();
        if (!Page.IsPostBack)
        {
            if (iCaseID > 0)
                BindData();
        }
    }
    protected void BindData( )
    {
//欄位名稱 
//正常證號內容 
//99證號內容 
//確定內容 



        DataTableCollection dtc = (DataTableCollection)DBUtil.DBOp("ConnDB", "exec dbo.usp_CaseUser_xGetDiffContent {0}", new string[] { iCaseID.ToString() }, CmdOpType.ExecuteReaderReturnDataTableCollection);

         

        DataTable dtTitle = dtc[0];
        DataTable dtMergeUser = dtc[1];
        StringBuilder sb = new StringBuilder();
        sb.Append ("<table>");
        sb.Append("<tr><th>欄位</th><th>正常證號</th><th>99證號</th><th>確定內容</th></tr>");


        foreach (DataRow dr in dtTitle.Rows)
        {

            sb.Append("<tr>");
            sb.AppendFormat("<td>{0}</td>", dr[1].ToString());
            foreach (DataRow drc in dtMergeUser.Rows)
                sb.AppendFormat("<td>{0}</td>", drc[dr[0].ToString()].ToString());

            sb.AppendFormat("<td><input type=\"text\" value=\"\"></td>", dr[1].ToString());
           sb.Append("</tr>");
        }
        sb.Append("</table>");

        ltDiffFields.Text = sb.ToString();
    }
    protected void QS()
    {
        if (Request.Form["i"] != null)
            int.TryParse(Request.Form["i"].ToString(), out iCaseID);

    }
}