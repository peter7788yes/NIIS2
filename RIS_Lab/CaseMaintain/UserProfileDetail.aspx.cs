using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public partial class CaseMaintain_UserProfileDetail : System.Web.UI.Page
{
    protected int CaseID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
    if (Session["SearchID"] != null)
        {
        int.TryParse(Request.Form ["i"], out CaseID);

        
            //DBUtil.DBOp("ConnDB", " exec [dbo].[usp_CaseUser_xReCountSearchLogSearchCount] {0},{1} ", new string[] { Session["SearchID"].ToString(), CaseID.ToString() }, NSDBUtil.CmdOpType.ExecuteNonQuery);



            DataTable dtContent = (DataTable)DBUtil.DBOp("ConnDB", " exec [dbo].[usp_CaseUser_xUserContent] {0} ", new string[] { CaseID.ToString() }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);
            StringBuilder sb = new StringBuilder("");

            sb.Append("<table>");

            for (int c = 0; c < dtContent.Columns.Count; c++)
            {
                for (int r = 0; r < dtContent.Rows.Count; r++)
                {
                    sb.AppendFormat("<tr/><th scope=\"row\">{0}</th><td>{1}</td></tr>", dtContent.Columns[c].ColumnName, dtContent.Rows[r][c].ToString());
                }
            }
            sb.Append("</table>");

            ltTable.Text = sb.ToString();
        }

    }
}