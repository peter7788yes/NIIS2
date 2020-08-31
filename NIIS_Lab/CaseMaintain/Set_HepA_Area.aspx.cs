using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CaseMaintain_Set_HepA_Area : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            DataTable dt = (DataTable)DBUtil.DBOp("ConnDB", @"SELECT 0 id,'請選擇' AreaName  union all SELECT [id] ,[AreaName]    FROM [dbo].[S_SystemAreaCode] where AreaLevel=1 "
                , new string[] { }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);
            ddlCounty.DataTextField = "AreaName";
            ddlCounty.DataValueField = "ID";
            ddlCounty.DataSource = dt;

            ddlCounty.DataBind();

            BindData();
        }
    }


    protected void BindData()
    {
    
   DataTable dt = (DataTable)DBUtil.DBOp("ConnDB", @"SELECT [SettingID]
      ,c.[AreaName]
      ,t.[AreaName] 
  FROM [dbo].[C_CaseUserCapacity_HepASetting]
 
 left outer join [S_SystemAreaCode] c on c.[id] = [CountyID]
left outer join [S_SystemAreaCode] t on t.[id] = [TownID] 

where C_CaseUserCapacity_HepASetting.CountyID = {0}

order by SettingID desc"
            , new string[] { ddlCounty.SelectedValue }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);

        GridView1.DataSource = dt;
        GridView1.DataBind();


    }





    protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataTable dt = (DataTable)DBUtil.DBOp("ConnDB", @"SELECT [id] ,[AreaName]  FROM [dbo].[S_SystemAreaCode] where AreaLevel=2 and [AreaParent]= {0}"
            , new string[] { ddlCounty .SelectedValue }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);
        ddlTown.DataTextField = "AreaName";
        ddlTown.DataValueField = "ID";
        ddlTown.DataSource = dt;
        ddlTown.DataBind();
        BindData();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
         
        DBUtil.DBOp("ConnDB", @"INSERT INTO [dbo].[C_CaseUserCapacity_HepASetting] ([CountyID] ,[TownID]) VALUES ({0},{1})"
            , new string[] { ddlCounty .SelectedValue,ddlTown.SelectedValue }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);

        BindData();
    }
}