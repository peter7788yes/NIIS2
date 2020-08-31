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
    int ContactCaseID = 0;
    int CaseID = 0;
    string ParentClick = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        QS();
        if (!Page.IsPostBack)
        {
            dllInit();
            BindData();
            BindMobile();
            BindEmail();
        }

    }
    protected void dllInit()
    {
        SystemCode.Update();
        if (SystemCode.dict.ContainsKey("CaseUser_ContactRelationShip"))
        {
            List<SystemCodeVM> SystemCodeList = SystemCode.dict["CaseUser_ContactRelationShip"];

            foreach (SystemCodeVM sc in SystemCodeList) ddlRS.Items.Add(new ListItem(sc.EnumName, sc.EnumValue.ToString()));

        }

    }
    

    protected void BindData()
    { 
        DataSet ds = new DataSet();

        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnDB"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.usp_CaseUser_xGetCaseUserContact", sc))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CaseID", CaseID);
                cmd.Parameters.AddWithValue("@ContactCaseID", ContactCaseID);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    sc.Open();
                    da.Fill(ds);
                }

            }
        }
        DataTable dt = ds.Tables[0];


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
        int.TryParse(Request.QueryString["c"], out ContactCaseID);
        int.TryParse(Request.QueryString["i"], out CaseID);

        ParentClick = Request.QueryString["ParentClick"] ?? "";

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
        SqlCommand cmd = new SqlCommand("dbo.usp_CaseUser_xUpdateContactRelationShip");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ContactCaseID", ContactCaseID);
        cmd.Parameters.AddWithValue("@CaseID", CaseID);
        cmd.Parameters.AddWithValue("@ContactRelationShip", ddlRS.SelectedValue);
        cmd.Parameters.AddWithValue("@IsMain", (cbMain.Checked ? "1" : "0"));
        cmd.Parameters.AddWithValue("@LogicDel", "0");
        

        DataTable dt = DB.GetDataTable(cmd, "ConnDB");
    
    
    }
    protected void UpdateContactData()
    { 
    
            SqlCommand cmd = new SqlCommand("dbo.usp_CaseUser_xModifyContact"); 
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ModifyUserID", 1);
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

        //Response.Write(Email);
        //Response.End();

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