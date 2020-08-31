using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class LogManage_LogImportSetting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    { 
        if (!Page.IsPostBack)
        {
            ddl_LogItemIni();
            
            DataTable dt = new DataTable ();
        
            ddl_FromItemColumn.Items.Clear();
            ddl_FromItemColumn.Items.Add(new ListItem("請選擇【介接項目欄位】", ""));

            dt = DB.GetDataTable("SELECT  [FieldName] ID  ,[FieldDescription]  Name  FROM [dbo].[C_CaseField] where [FieldDescription] is not null ", "ConnDB");
             
            ddl_ToCaseColumn.Items.Clear();
            ddl_ToCaseColumn.Items.Add(new ListItem("請選擇【匯入RIS欄位】", "")); 
            foreach (DataRow r in dt.Rows ) ddl_ToCaseColumn.Items.Add(new ListItem(r["Name"].ToString(), r["ID"].ToString()));




            dt = DB.GetDataTable(" SELECT     fnName ID,fnDiscript + ' (' +  fnName  + ')' Name FROM              L_LogImport_fnName ", "ConnDB");

            ddl_UseFun.Items.Clear();
            ddl_UseFun.Items.Add(new ListItem("【不使用fuction】", ""));
            foreach (DataRow r in dt.Rows) ddl_UseFun.Items.Add(new ListItem(r["Name"].ToString(), r["ID"].ToString()));

           
        }
      


    }


     protected void ddl_LogItemIni()
    {
        DataTable dt = new DataTable();
        dt = DB.GetDataTable("SELECT [LogItemID] as ID   ,[LogItemName]  +' - ' + [LogItemCName]  as Name  FROM [dbo].[L_LogItem] order by [LogItemID]", "ConnDB");
        ddl_LogItem.Items.Clear();
        ddl_LogItem.Items.Add(new ListItem("請選擇【介接項目】", ""));
        foreach (DataRow r in dt.Rows)
            ddl_LogItem.Items.Add(new ListItem(r["Name"].ToString(), r["ID"].ToString()));
     }
     
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow r = (GridViewRow)GridView1.Rows[Convert.ToInt32(e.CommandArgument)];

        string SettingID = r.Cells[1].Text;

        DBUtil.DBOp("ConnDB", "Delete from L_LogImportSetting where SettingID = {0}", new string[] { SettingID }, NSDBUtil.CmdOpType.ExecuteNonQuery);
        binddata();
    }
    protected void binddata()
    {

    

        DataTable dt = DB.GetDataTable(@" SELECT [SettingID]
      ,[LogItemName] +' - '  +[LogItemCName] '介接項目'
	    ,(select data from dbo.fn_slip_str(( SELECT [ItemFieldName]  FROM [dbo].[L_LogItem] fni where fni.LogItemID=i.LogItemID  ),',' ) where sno =s.[LogItemFieldSeq]) '項目欄位'
     ,[ImportCaseFieldName] '匯入RIS欄位'
      ,[UseFun] '使用funtion' 
	  FROM [dbo].[L_LogImportSetting]  s 
  inner join [L_LogItem] i on i.LogItemID=s.[LogItemID]  "  
        
            + (ddl_LogItem.SelectedValue=="" ? "" :"  where i.LogItemID = " +  Convert.ToInt32(ddl_LogItem.SelectedValue).ToString () )

        , "ConnDB");
        GridView1.DataSource = dt;
        GridView1.DataBind();
        ltTip.Text = Server.HtmlEncode ( "欄位介接設定 - " + (ddl_LogItem.SelectedValue == "" ? "全部" : ddl_LogItem.SelectedItem.Text) + "(" + dt.Rows.Count.ToString() + "筆)" );


        if (ddl_LogItem.SelectedValue != "")
        {
            DataTable dtItem = DB.GetDataTable(@" SELECT [LogItemID]
      ,[LogItemName]
      ,[ItemFieldLen]
      ,[ItemFieldName]
      ,[LogItemCName]
      ,[Operation]
      ,[LogItemFieldKeySeq]
      ,[ItemFieldCount]
      ,[ApplyDateSeq]
      ,[ApplyTimeSeq]
      ,[LogTableName]
      ,case when [bActive]=1 then '1' else '0' end as bActive
      ,[FileNameFormat]
,SPName
 ,case when [bMust]=1 then '1' else '0' end as bMust
,ConfirmFileNameFormat
,case when [bConfirmFileMust]=1 then '1' else '0' end as bConfirmFileMust
,ExecOrder

  FROM [dbo].[L_LogItem] where [LogItemID]=     " + Convert.ToInt32(ddl_LogItem.SelectedValue).ToString ()

          , "ConnDB");

            if (dtItem.Rows.Count > 0)
            {
                tbColLen.Text = dtItem.Rows[0]["ItemFieldLen"].ToString();
                tbColName.Text = dtItem.Rows[0]["ItemFieldName"].ToString();
                tbKey.Text = dtItem.Rows[0]["LogItemFieldKeySeq"].ToString();
                DateSeq.Text = dtItem.Rows[0]["ApplyDateSeq"].ToString();
                TimeSeq.Text = dtItem.Rows[0]["ApplyTimeSeq"].ToString();
                ddlActive.SelectedValue = dtItem.Rows[0]["bActive"].ToString();
                ddlMust.SelectedValue = dtItem.Rows[0]["bMust"].ToString();
                ddlConfirmFileMust.SelectedValue = dtItem.Rows[0]["bConfirmFileMust"].ToString();
                tbFileNameFormat.Text = dtItem.Rows[0]["FileNameFormat"].ToString();
                tbConfirmFileNameFormat.Text = dtItem.Rows[0]["ConfirmFileNameFormat"].ToString();
                ltLogTableName.Text = Server.HtmlEncode (dtItem.Rows[0]["LogTableName"].ToString());
                tbSPName.Text = dtItem.Rows[0]["SPName"].ToString();
                tbLogItemName.Text =  dtItem.Rows[0]["LogItemName"].ToString();
                tbLogItemCName.Text = dtItem.Rows[0]["LogItemCName"].ToString();
                ltID.Text = Server.HtmlEncode (dtItem.Rows[0]["LogItemID"].ToString());
                tbExecOrder.Text = dtItem.Rows[0]["ExecOrder"].ToString();

                ddlOp.SelectedValue = dtItem.Rows[0]["Operation"].ToString();
            } 

            LogItemMainSettingDIV.Visible = true;
        }
        else
        {
            LogItemMainSettingDIV.Visible = false;
        }



    }
    protected void ddl_LogItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_FromItemColumn.Items.Clear();
        ddl_FromItemColumn.Items.Add(new ListItem("請選擇【介接項目欄位】", ""));
        DataTable dt = DB.GetDataTable(" select sno as id ,data  as name from dbo.fn_slip_str((SELECT  ItemFieldName  FROM [dbo].[L_LogItem] where [LogItemID]=" + Convert.ToInt32(ddl_LogItem.SelectedValue).ToString () + "),',')", "ConnDB");
        foreach (DataRow r in dt.Rows)
            ddl_FromItemColumn.Items.Add(new ListItem(r["Name"].ToString(), r["ID"].ToString()));

        binddata();

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DBUtil.DBOp("ConnDB", " INSERT INTO [dbo].[L_LogImportSetting] ([LogItemID] ,[LogItemFieldSeq] ,[ImportCaseFieldName] ,[UseFun] ) VALUES ({0} ,{1},{2} ,{3} ) "
            , new string[] { ddl_LogItem.SelectedValue, ddl_FromItemColumn.SelectedValue, ddl_ToCaseColumn.SelectedValue, ddl_UseFun.SelectedValue }, NSDBUtil.CmdOpType.ExecuteNonQuery);
        binddata();
    }
    protected void btnSaveSetting_Click(object sender, EventArgs e)
    {
        if (tbColLen.Text.Split(',').Length != tbColName.Text.Split(',').Length)
        {
            Response.Write("<font color='red'>[欄位長度]跟[欄位名稱]個數不同 </font>");

        }
        else
        {
            DBUtil.DBOp("ConnDB", @"
   UPDATE [dbo].[L_LogItem]
   SET  [ItemFieldLen] = {1}
      ,[ItemFieldName] = {2}  
      ,[LogItemFieldKeySeq] = {3} 
      ,[ApplyDateSeq] = {4}
      ,[ApplyTimeSeq] = {5} 
      ,[bActive] = {6}
      ,[FileNameFormat] = {7}
,[bMust] = {8}
,ExecOrder = {9}
,Operation={10}
,LogItemName={11}
,LogItemCName={12}
 WHERE LogItemID ={0}
", new string[] { ddl_LogItem.SelectedValue, 
                tbColLen.Text , 
                tbColName.Text , 
                tbKey.Text , 
                DateSeq.Text , 
                TimeSeq.Text , 
                ddlActive.SelectedValue , 
                tbFileNameFormat.Text  , 
                ddlMust.SelectedValue,
                tbExecOrder.Text,
                ddlOp .SelectedValue,
                tbLogItemName.Text 
                ,tbLogItemCName.Text 
 }, NSDBUtil.CmdOpType.ExecuteNonQuery);
        }
            binddata();
        

    }
    protected void btnAlterTable_Click(object sender, EventArgs e)
    {
        if (tbColLen.Text.Split(',').Length != tbColName.Text.Split(',').Length)
        {
            Response.Write("<font color='red'>[欄位長度]跟[欄位名稱]個數不同 </font>");

        }
        else
        {   DBUtil.DBOp("ConnDB", @"
   exec dbo.usp_LogManage_AlterLogTable {0}
", new string[] { ddl_LogItem.SelectedValue 
 }, NSDBUtil.CmdOpType.ExecuteNonQuery);
        }
             
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowIndex != -1)
        {  
           ((Button)(e.Row.Cells[0].Controls[0])).OnClientClick = "if (confirm('確定要刪除')==false) {return false;}";
        }
    }
    protected void btnCreateTable_Click(object sender, EventArgs e)
    {
        DBUtil.DBOp("ConnDB", @"exec dbo. usp_LogManage_DropLogTable {0} ;
   exec dbo.usp_LogManage_CreateLogTable {0}
", new string[] { ddl_LogItem.SelectedValue 
 }, NSDBUtil.CmdOpType.ExecuteNonQuery);
    }
    protected void btnClone_Click(object sender, EventArgs e)
    {


        DBUtil.DBOp("ConnDB", @"  INSERT INTO [dbo].[L_LogItem]
           ([LogItemID]
           ,[LogItemName]
           ,[ItemFieldLen]
           ,[ItemFieldName]
           ,[LogItemCName]
           ,[Operation]
           ,[LogItemFieldKeySeq]
           ,[ItemFieldCount]
           ,[ApplyDateSeq]
           ,[ApplyTimeSeq] 
           ,[bActive]
           ,[FileNameFormat] 
           ,[bMust])
  select (select max([LogItemID])from [L_LogItem] ) + 1 ,[LogItemName]
           ,[ItemFieldLen]
           ,[ItemFieldName]
           ,[LogItemCName]
           ,[Operation]
           ,[LogItemFieldKeySeq]
           ,[ItemFieldCount]
           ,[ApplyDateSeq]
           ,[ApplyTimeSeq] 
           ,[bActive]
           ,[FileNameFormat] 
           ,[bMust] from [L_LogItem] where [LogItemID] =  {0}
", new string[] { ddl_LogItem.SelectedValue 
 }, NSDBUtil.CmdOpType.ExecuteNonQuery);
        ddl_LogItemIni(); 
        binddata();
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        DBUtil.DBOp("ConnDB", @"exec dbo. usp_LogManage_DropLogTable {0} ;
    delete from [L_LogItem] where [LogItemID] =  {0};
 delete from L_LogImportSetting where [LogItemID] =  {0};
", new string[] { ddl_LogItem.SelectedValue 
 }, NSDBUtil.CmdOpType.ExecuteNonQuery);
        ddl_LogItemIni();
        binddata();
    }
}