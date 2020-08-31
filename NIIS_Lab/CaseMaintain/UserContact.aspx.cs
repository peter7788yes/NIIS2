using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class CaseMaintain_UserContact : System.Web.UI.Page
{
    protected int ContactCaseID = 0;
    protected int CaseID = 0; 
    protected int ContactID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!Page.IsPostBack)
        {
            QS();
            dllInit();
            BindData();
            //BindMobile();
            //BindEmail();
        }
        else
        { 

            //postback will lose it
            if (Session["UserContract_ContactCaseID"] != null) int.TryParse(Session["UserContract_ContactCaseID"].ToString (), out ContactCaseID);
            if (Session["UserContract_CaseID"] != null) int.TryParse(Session["UserContract_CaseID"].ToString(), out CaseID);
             if (Session["UserContract_ContactID"] != null) int.TryParse(Session["UserContract_ContactID"].ToString(), out ContactID);
                
        }

    }
    protected void dllInit()
    { 
        ddlRS.Items.Add(new ListItem("請選擇", ""));
        SystemCodeControl.ServerSelect(ref ddlRS, "CaseUser_ContactRelationShip");
    }
    

    protected void BindData()
    {
        string MobileFormat = "<div id=\"MobileDetail\"><input name=\"tbMobileNo_{0}\"  type=\"text\" value=\"{1}\" class=\"text02\" /><a onclick =\"javascript:void(0);\" class=\"DelPS\"><img src=\"/images/icon_del.png\" /></a><a onclick =\"javascript:void(0);\" class=\"AddPS\"><img src=\"/images/icon_increase.png\" /></a></div>";
        string EmailFormat = "<div id=\"EmailDetail\"><input name=\"tbEmail_{0}\"  type=\"text\" value=\"{1}\" class=\"text02\" /><a onclick =\"javascript:void(0);\" class=\"DelEmail\"><img src=\"/images/icon_del.png\" /></a><a onclick =\"javascript:void(0);\" class=\"AddEmail\"><img src=\"/images/icon_increase.png\" /></a></div>";
         
        if (ContactID != 0)
        {

            UserContact uc = new UserContact(ContactID);
            ContactCaseID = uc.ContactCaseID;
            CaseID = uc.CaseID;
            Session["UserContract_ContactCaseID"] = ContactCaseID;
            Session["UserContract_CaseID"] = CaseID;
            ddlRS.SelectedValue = uc.RelationShip.ToString ();
            cbMain.Checked = uc.IsMain;


        }

        CaseUserProfile c = new CaseUserProfile(ContactCaseID);
        if (c.CaseID != 0)
        {
            ltBirthDate.Text = Server.HtmlEncode(c.BirthDate);
            ltIdNo.Text = Server.HtmlEncode (c.IdNo);
            ltName.Text = Server.HtmlEncode(c.ChName);


            tbTelDayArea.Text = c.TelDayArea;
            tbTelDayNo.Text = c.TelDayNo;
            tbTelDayExt.Text = c.TelDayExt;


            tbTelNightArea.Text = c.TelNightArea;  
            tbTelNightNo.Text = c.TelNightNo;  
            tbTelNightExt.Text = c.TelNightExt;

            foreach (UserMobile m in c.Mobiles)
                MobileDIV.Controls.Add(GetControlFromTag(string.Format(MobileFormat, m.ID, m.Mobile)));
            foreach (UserEmail m in c.Emails)
                EmailDIV.Controls.Add(GetControlFromTag(string.Format(EmailFormat, m.ID, m.Email)));


        }
         

    }
    protected void QS()
    {
        int.TryParse(Request.Form["c"], out ContactCaseID);
        int.TryParse(Request.Form["i"], out CaseID); 
        int.TryParse(Request.Form["ContactID"], out ContactID); 
          
        //postback will lose it
         Session["UserContract_ContactCaseID"] = ContactCaseID;
         Session["UserContract_CaseID"] = CaseID;
         Session["UserContract_ContactID"] = ContactID;

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
        int NewContactID = 0;
        UserContact uc = new UserContact();
        if (ContactID != 0)
        {
            uc.ContactID = ContactID;
            uc.IsMain = cbMain.Checked;
            uc.RelationShip = Convert.ToInt32(ddlRS.SelectedValue);
            uc.Update();
        }
        else
        {


            uc.CaseID = CaseID;
            uc.ContactCaseID = ContactCaseID;
            uc.IsMain = cbMain.Checked;
            uc.RelationShip = Convert.ToInt32(ddlRS.SelectedValue);
            NewContactID=  uc.Add();
            ReadSession(NewContactID);
        }
        //Response.Write(ContactID);
        //Response.Write(cbMain.Checked.ToString ());
        UpdateContactData(); 
        SaveEmailMobiles();   
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('成功!');" + (CaseID==0 && NewContactID != 0 ? "window.opener.AddContactTr(" + NewContactID.ToString() + ")" : "window.opener.LoadContactList()") + ";window.close();", true);
      
   
    }  
     protected void ReadSession(int NewContactID)
    {
        if (Session["NewCaseContacts"] != null)
        {
            Session["NewCaseContacts"] = Session["NewCaseContacts"].ToString() + "," + NewContactID.ToString();

        }
        else
            Session["NewCaseContacts"] = NewContactID;
     }

  
    protected void UpdateContactData()
    { 
    
            DBUtil.DBOp("ConnDB",
                          "exec dbo.usp_CaseUser_xModifyContact {0},{1},{2},{3},{4} ,{5} ,{6},{7}",
                    new string[] { 
                                ContactCaseID.ToString()
                                ,AuthServer .GetLoginUser().ID.ToString ()
                                , tbTelDayArea.Text 
                                , tbTelDayNo.Text 
                                , tbTelDayExt.Text 
                                , tbTelNightArea.Text 
                                , tbTelNightNo.Text 
                                , tbTelNightExt.Text 
                           },
                           NSDBUtil.CmdOpType.ExecuteScalar) ;
         
         
            
    }


    protected void SaveEmailMobiles()
    {
        CaseUserProfile u = new CaseUserProfile(ContactCaseID);
        UserEmail ueop = new UserEmail();
        foreach (UserEmail ue in u.Emails)
        {
           
            if (Request.Form["tbEmail_" + ue.ID.ToString()] != null)
            {
                string Email = Request.Form["tbEmail_" + ue.ID.ToString()].ToString();
                if (ue.Email != Email) ueop.Update(ue.ID, Email);
            }
            else
                ueop.Delete(ue.ID);


        }
        UserMobile umop = new UserMobile();
        foreach (UserMobile um in u.Mobiles)
        {

            if (Request.Form["tbMobileNo_" + um.ID.ToString()] != null)
            {
                string MobileNo = Request.Form["tbMobileNo_" + um.ID.ToString()].ToString();
                if (um.Mobile != MobileNo) umop.Update(um.ID, MobileNo);
            }
            else
                umop.Delete(um.ID);


        }

        //新增
        string tbMobileNo = Request.Form["tbMobileNo"] ?? "";
        string tbEmail = Request.Form["tbEmail"] ?? "";
        tbMobileNo = tbMobileNo.TrimEnd(',');
        tbEmail = tbEmail.TrimEnd(',');

        //Response.Write(ContactCaseID.ToString() + tbMobileNo);
        //Response.End();
        if (tbEmail != "") ueop.Add(ContactCaseID, tbEmail);
        if (tbMobileNo != "") umop.Add(ContactCaseID, tbMobileNo);

        u.UpdateMainMobileCol(ContactCaseID);


    }
   
}