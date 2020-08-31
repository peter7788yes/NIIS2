using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CaseMaintain_CapacityHistory : System.Web.UI.Page
{
    protected int CaseID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {  
        int.TryParse(Request["i"], out CaseID);


        
             //身份別有4種
            DataTable CapDt = (DataTable)DBUtil.DBOp("ConnDB", "SELECT [ID] , [TypeName]   FROM [dbo].[C_CaseUserCapacity_Type] order by [CapacityCate] ,[ID]", new string[] { }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);

            foreach (DataRow r in CapDt.Rows)
            {
                 ListItem li = new ListItem(r["TypeName"].ToString(), r["ID"].ToString());
                 ddlCapacity.Items.Add(li); 
            }

    }

}