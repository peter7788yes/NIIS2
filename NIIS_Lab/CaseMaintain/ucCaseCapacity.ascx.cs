using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CaseMaintain_ucCaseCapacity : System.Web.UI.UserControl
{
    /// <summary>
    /// 勾選
    /// </summary>
    public   string CheckedCapacitys { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (!Page.IsPostBack)
        {
            string[] CheckedCapacity = new string[0]{} ;

            if (CheckedCapacitys != null) CheckedCapacity = CheckedCapacitys.Split(',');

            //身份別有4種
            DataTable CapDt = (DataTable)DBUtil.DBOp("ConnDB", "SELECT [ID] ,[CapacityCate] ,[TypeName] ,[bCanEdit]  FROM [dbo].[C_CaseUserCapacity_Type] order by [CapacityCate] ,[ID]", new string[] { }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);

            foreach (DataRow r in CapDt.Rows)
            {
                CheckBoxList cblist = (CheckBoxList)tbCapacity.FindControl("cblCapacity_" + r["CapacityCate"].ToString());
                ListItem li = new ListItem(r["TypeName"].ToString(), r["ID"].ToString());
                if (CheckedCapacity.Contains(r["ID"].ToString()))
                    li.Selected = true;
                cblist.Items.Add(li);

            }
        }
    }







}