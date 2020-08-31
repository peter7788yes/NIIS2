using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class CaseMaintain_UserContract : System.Web.UI.Page
{
    protected int ContactCaseID = 0;
    protected int CaseID = 0;
    protected string ParentClick = "";
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!Page.IsPostBack)
        {
            QS();
            dllInit();
            BindData();
            BindMobile();
            BindEmail();
        }
        else
        { 

            //postback will lose it
            if (Session["UserContract_ContactCaseID"] != null) int.TryParse(Session["UserContract_ContactCaseID"].ToString (), out ContactCaseID);
            if (Session["UserContract_CaseID"] != null) int.TryParse(Session["UserContract_CaseID"].ToString(), out CaseID);
            if (Session["UserContract_ParentClick"] != null) ParentClick = Session["UserContract_ParentClick"].ToString ();
                    
        }

    }
    protected void dllInit()
    { 
        ddlRS.Items.Add(new ListItem("請選擇", ""));
        SystemCodeControl.ServerSelect(ref ddlRS, "CaseUser_ContactRelationShip");
    }
    

    protected void BindData()
    { 
        DataTable dt = (DataTable)DBUtil.DBOp("ConnDB", " exec dbo.usp_CaseUser_xGetCaseUserContact {0},{1}", new string[] { CaseID.ToString(), ContactCaseID.ToString () }, NSDBUtil.CmdOpType.ExecuteReaderReturnDataTable);
      

        if (dt.Rows.Count > 0)
        {

            ltBirthDate.Text = dt.Rows[0]["BirthDate"].ToString();
            ltIdNo.Text = dt.Rows[0]["IdNo"].ToString();
            ltName.Text = dt.Rows[0]["ChName"].ToString();
            

            tbTelDayArea.Text = dt.Rows[0]["TelDayArea"].ToString();
            tbTelDayNo.Text = dt.Rows[0]["TelDayNo"].ToString();
            tbTelDayExt.Text = dt.Rows[0]["TelDayExt"].ToString();


            tbTelNightArea.Text = dt.Rows[0]["TelNightArea"].ToString();
            tbTelNightNo.Text = dt.Rows[0]["TelNightNo"].ToString();
            tbTelNightExt.Text = dt.Rows[0]["TelNightExt"].ToString();

            ddlRS.SelectedValue = dt.Rows[0]["ContactRelationShip"].ToString();
            if ( dt.Rows[0]["IsMain"].ToString ()=="1")
                cbMain.Checked = true;
        }






    }
    protected void QS()
    {
        int.TryParse(Request.Form["c"], out ContactCaseID);
        int.TryParse(Request.Form["i"], out CaseID);
        ParentClick = Request.Form["ParentClick"] ?? "";

        //postback will lose it
         Session["UserContract_ContactCaseID"] = ContactCaseID;
         Session["UserContract_CaseID"] = CaseID;
         Session["UserContract_ParentClick"] = ParentClick;
    }
    protected void BindMobile()
    {
        string CommentAreaFormat = "<div id=\"MobileDetail\"><input name=\"tbMobileNo_{0}\"  type=\"text\" value=\"{1}\" class=\"text02\" /><a onclick =\"javascript:void(0);\" class=\"DelPS\"><img src=\"/images/icon_del.png\" /></a><a onclick =\"javascript:void(0);\" class=\"AddPS\"><img src=\"/images/icon_increase.png\" /></a></div>";

        SqlCommand cmd = new SqlCommand("SELECT MobileID,[MobileNo]  FROM [C_CaseUserMobile] where [LogicDel]=0 and [CaseID]=@CaseID order by [MobileID] ");
        cmd.Parameters.AddWithValue("@CaseID", ContactCaseID);
        DataTable dt = DB.GetDataTable(cmd, "ConnDB");

        foreach (DataRow r in dt.Rows)
        {
            MobileDIV.Controls.Add(GetControlFromTag(string.Format(CommentAreaFormat, r["MobileID"], r["MobileNo"])));

        }


    }
    protected void BindEmail()
    {
        string CommentAreaFormat = "<div id=\"EmailDetail\"><input name=\"tbEmail_{0}\"  type=\"text\" value=\"{1}\" class=\"text02\" /><a onclick =\"javascript:void(0);\" class=\"DelEmail\"><img src=\"/images/icon_del.png\" /></a><a onclick =\"javascript:void(0);\" class=\"AddEmail\"><img src=\"/images/icon_increase.png\" /></a></div>";

        SqlCommand cmd = new SqlCommand("SELECT [EmailID],[Email]  FROM [C_CaseUserEmail] where [LogicDel]=0 and [CaseID]=@CaseID order by [EmailID] ");
        cmd.Parameters.AddWithValue("@CaseID", ContactCaseID);
        DataTable dt = DB.GetDataTable(cmd, "ConnDB");

        foreach (DataRow r in dt.Rows)
        {
            EmailDIV.Controls.Add(GetControlFromTag(string.Format(CommentAreaFormat, r["EmailID"], r["Email"])));

        }


    }
    public static Control GetControlFromTag(string controlTag)
    {
        Page p = new Page();
        p.AppRelativeVirtualPath = "~/";
        Control control = p.ParseControl(controlTag);
        return control;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {


        UpdateRelationShip();
        UpdateContactData();
        HandleMobile();
        HandleEmail();

        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('成功!');" + (ParentClick !="" ? string.Format ("window.opener.document.getElementById('{0}').click();",ParentClick):"")+"window.close();", true);
      
   
    }
    protected void HandleEmail()
    {
        QSOldEmail();
        SaveEmail();
        BindEmail();
    }
    protected void HandleMobile()
    { 
        QSOldMobile(); 
        SaveMobile();
        BindMobile();
    }
    protected void UpdateRelationShip()
    {
         
            
             DBUtil.DBOp("ConnDB",
                       "exec dbo.usp_CaseUser_xUpdateContactRelationShip {0},{1},{2},{3},{4} ",
                       new string[] { 
                                CaseID.ToString()
                                ,ddlRS.SelectedValue              
                                ,ContactCaseID.ToString() 
                                ,"0"
                                ,(cbMain.Checked ? "1" : "0")
                           },
                        NSDBUtil.CmdOpType.ExecuteNonQuery);
           
    
    }
    protected void UpdateContactData()
    { 
    
            SqlCommand cmd = new SqlCommand("dbo.usp_CaseUser_xModifyContact"); 
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ModifyUserID", AuthServer .GetLoginUser ().ID );
            cmd.Parameters.AddWithValue("@CaseID", ContactCaseID);
            cmd.Parameters.AddWithValue("@TelDayArea", tbTelDayArea.Text );
            cmd.Parameters.AddWithValue("@TelDayNo", tbTelDayNo.Text);
           cmd.Parameters.AddWithValue("@TelDayExt", tbTelDayExt.Text);
        cmd.Parameters.AddWithValue("@TelNightArea", tbTelNightArea.Text);
        cmd.Parameters.AddWithValue("@TelNightNo", tbTelNightNo.Text);
        cmd.Parameters.AddWithValue("@TelNightExt", tbTelNightExt.Text);
          
            DataTable dt =  DB.GetDataTable (cmd,"ConnDB");
         
            
    }

    protected void QSOldMobile()
    {


        SqlCommand cmd = new SqlCommand("SELECT [MobileID] as ID  FROM [dbo].[C_CaseUserMobile] where [CaseID]=@CaseID and [LogicDel]=0  ");
        cmd.Parameters.AddWithValue("@CaseID", ContactCaseID );
        DataTable dt = DB.GetDataTable(cmd, "ConnDB");

        foreach (DataRow r in dt.Rows)
        {
            string MobileNo = "";
            if (Request.Form["tbMobileNo_" + r["ID"]] != null)
            {
                MobileNo = Request.Form["tbMobileNo_" + r["ID"].ToString()].ToString();

                SqlCommand cmdr = new SqlCommand(" Update  [C_CaseUserMobile] set  [MobileNo]=@MobileNo  where MobileID=@MobileID  ");
                cmdr.Parameters.AddWithValue("@MobileID", r["ID"]);
                cmdr.Parameters.AddWithValue("@MobileNo", MobileNo); 

                DB.ExecuteNonQuery(cmdr, "ConnDB");

            }
            else
            {
                SqlCommand cmdr = new SqlCommand(" Update  [C_CaseUserMobile] set LogicDel=1 where MobileID=@MobileID  ");
                cmdr.Parameters.AddWithValue("@MobileID", r["ID"]);
                DB.ExecuteNonQuery(cmdr, "ConnDB");

            }

        }


    }
    protected void QSOldEmail()
    {


        SqlCommand cmd = new SqlCommand("SELECT [EmailID] as ID  FROM [dbo].[C_CaseUserEmail] where [CaseID]=@CaseID and [LogicDel]=0  ");
        cmd.Parameters.AddWithValue("@CaseID", ContactCaseID);
        DataTable dt = DB.GetDataTable(cmd, "ConnDB");

        foreach (DataRow r in dt.Rows)
        {
            string Email  = "";
            if (Request.Form["tbEmail_" + r["ID"]] != null)
            {
                Email = Request.Form["tbEmail_" + r["ID"].ToString()].ToString(); 
                SqlCommand cmdr = new SqlCommand(" Update  [C_CaseUserEmail] set  [EmailID]=@Email  where EmailID=@EmailID  ");
                cmdr.Parameters.AddWithValue("@EmailID", r["ID"]);
                cmdr.Parameters.AddWithValue("@Email", Email); 
                DB.ExecuteNonQuery(cmdr, "ConnDB");

            }
            else
            {
                SqlCommand cmdr = new SqlCommand(" Update  [C_CaseUserEmail] set LogicDel=1 where EmailID=@EmailID  ");
                cmdr.Parameters.AddWithValue("@EmailID", r["ID"]);
                DB.ExecuteNonQuery(cmdr, "ConnDB");

            }

        }


    }


    protected void SaveEmail()
    {

        string tbEmail = "";
        if (Request.Form["tbEmail"] != null) tbEmail = Request.Form["tbEmail"].ToString();

        string[] EmailArray = tbEmail.Split(',');

        string Email = "";
        for (int i = 0; i < EmailArray.Length; i++)
        {
            if (EmailArray[i].ToString().Trim() != "") Email = Email + EmailArray[i].Trim() + ",";
        }
         

        if (Email != "")
        {
            SqlCommand cmd = new SqlCommand("dbo.usp_CaseUser_xAddCaseUserEmail");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CaseID", ContactCaseID);
            cmd.Parameters.AddWithValue("@Emails", Email.TrimEnd(','));
            cmd.Parameters.AddWithValue("@CreatedUserID", 1);
            DB.ExecuteNonQuery(cmd, "ConnDB");
        }


    }
    protected void SaveMobile()
    {

        string  MobileNo = "";
        if (Request.Form["tbMobileNo"] != null) MobileNo = Request.Form["tbMobileNo"].ToString();

        string[] MobileNoArray = MobileNo.Split(',');

        string Mobile ="";
        for (int i = 0; i < MobileNoArray.Length; i++)
        {
            if (MobileNoArray[i].ToString().Trim() != "") Mobile = Mobile + MobileNoArray[i].Trim() + ",";
        }
          

        if (Mobile != "")
        {
            SqlCommand cmd = new SqlCommand("dbo.usp_CaseUser_xAddCaseUserMobile");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CaseID", ContactCaseID);
            cmd.Parameters.AddWithValue("@MobileNos", Mobile.TrimEnd(','));
            cmd.Parameters.AddWithValue("@CreatedUserID", 1);
            DB.ExecuteNonQuery(cmd, "ConnDB");
        }

 
    }
}