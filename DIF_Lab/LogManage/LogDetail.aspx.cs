using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public partial class LogManage_LogDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int LogID = 0;
         int LogItem=0;
         int.TryParse(Request["LogID"], out LogID);
         int.TryParse(Request["ItemID"], out LogItem); 
         //LogID = 5;
         //LogItem = 2;
        SqlCommand cmd = new SqlCommand ("dbo.usp_Log_xLogContent");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@LogItemID", LogItem);
        cmd.Parameters.Add("@ItemLogID", LogID);

       DataSet ds = DB.GetDataSet(cmd, "ConnDB");
       DataTable dtColName = ds.Tables[0];
       DataTable dtContent = ds.Tables[1];
       StringBuilder sb = new StringBuilder("");
        
        sb.Append ("<table>");
        for (int i = 5; i < dtContent.Columns.Count; i++)   //前5欄是ID CreateDate LogID bsuc errormsg
        {
            string ColName = "";
			if ( (i-2) < dtColName.Rows.Count)
			ColName = dtColName.Rows[i-2][0].ToString();

            sb.AppendFormat("<tr/><th scope=\"row\">{0}</th><td>{1}</td></tr>", ColName, dtContent.Rows[0][i].ToString());
        }
        sb.Append("</table>");

        ltTable.Text = sb.ToString ();
         

    }
}