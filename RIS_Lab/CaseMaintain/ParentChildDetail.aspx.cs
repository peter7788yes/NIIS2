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
        string ParentID = Request.Form["ParentID"] ?? "";

        ltID.Text = ParentID;
          DataTable dt = (DataTable)DBUtil.DBOp("ConnDB", " exec [dbo].[usp_CaseUser_xGetUserChild]   {0}  "
                , new string[] { ParentID.ToString() }
                , NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);
          GridView1.DataSource = dt;
          GridView1.DataBind();

    } 
}