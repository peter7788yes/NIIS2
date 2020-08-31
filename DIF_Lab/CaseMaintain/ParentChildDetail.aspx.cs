using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class CaseMaintain_ParentChildDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SearchID"] != null)
        {
            string ParentID = Request.Form["ParentID"] ?? "";

            ltID.Text = Server.HtmlEncode ( ParentID);
            DataTable dt = (DataTable)DBUtil.DBOp("ConnDB", " exec [dbo].[usp_CaseUser_xGetUserChild]   {0}  "
                  , new string[] { ParentID.ToString() }
                  , NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);
            GridView1.DataSource = dt;
            GridView1.DataBind();

            int ParentCaseID = 0;
            try
            {
               ParentCaseID= Convert.ToInt32(DBUtil.DBOp("ConnDB", " select top 1 ID from [C_Case] where LogicDel=0   "
                      , new string[] { ParentID.ToString() }
                      , NSDBUtil.CmdOpType.ExecuteScalar));
            }
            catch { 
            }

            DBUtil.DBOp("ConnDB", " exec [dbo].[usp_CaseUser_xReCountSearchLogSearchCount] {0},{1} ", new string[] { Session["SearchID"].ToString(), ParentCaseID.ToString() }, NSDBUtil.CmdOpType.ExecuteNonQuery);
        }

    } 
}