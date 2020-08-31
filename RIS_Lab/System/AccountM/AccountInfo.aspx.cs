﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI;
using WayneEntity;
using WayneTools;

public partial class System_AccountM_AccountInfo : BasePage
{
    UserVM user = new UserVM();

    public System_AccountM_AccountInfo()
    {
        base.AddPower("/System/AccountM/AccountInfo.aspx", MyPowerEnum.修改);
    }
    
    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);

        user = AuthServer.GetLoginUser();
      
        if (this.IsPostBack == false)
        {
            DataTable dt = MSDB.GetDataTable("ConnUser", "dbo.usp_AccountM_xGetUserInfoByID"
                                     , new Dictionary<string, object>()
                                     {
                                                { "@UserID", user.ID }
                                     });
           
            UserVM user2 = new UserVM();
            EntityS.FillModel(user2, dt);

            lblAccount.Text = user.LoginName;
            tbDept.Text = user.OrgName;
            tbIDF.Text = user2.RocID;
            tbName.Text = user2.UserName;
            tbEmail.Text = user2.Email;
            tbTitle.Text = user2.Title;
            tbTel.Text = user2.PhoneNumber;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string IDF = PureString(tbIDF.Text);
        string Email = PureString(tbEmail.Text);

        string script = "";
        string message = CheckValid(IDF, Email);

        if (message.Length > 0)
        {
            script = "<script>alert('" + message + "');</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
            return;
        }



        string UserName = PureString(tbName.Text); 
        string Title = PureString(tbTitle.Text); 
        string Tel = PureString(tbTel.Text); 
        //string PWD = PureString(tbPWD.Text); 
        //string PWD2 = PureString(tbPWD2.Text); 

        //if (PWD.Equals(PWD2)==false ||tbPWD.Enabled==false || tbPWD2.Enabled == false || btnChange.Text.Trim().Equals("變更密碼"))
        //if (PWD.Equals(PWD2) == false || tbPWD.Enabled == false || tbPWD2.Enabled == false || btnChange.Text.Trim().Equals("變更密碼"))
        //{
        //    PWD = "";
        //}
        //else
        //{
        //    EncryptT enc = new EncryptT();
        //    PWD = enc.ToSHA256(PWD);
        //}
      
        int Chk = 0;

        Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

        MSDB.ExecuteNonQuery("ConnUser", "dbo.usp_AccountM_xUpdateUserInfo"
                                         , ref OutDict
                                         , new Dictionary<string, object>()
                                         {
                                                { "@UserID", user.ID },
                                                { "@RocID", IDF },
                                                { "@UserName", UserName },
                                                { "@Email", Email },
                                                { "@Title", Title },
                                                { "@PhoneNumber", Tel },
                                                { "@LoginPassword", "" }
                                         });

       Chk = (int)OutDict["@Chk"];

       if (Chk > 0)
       {
           script = "</script><style>body{display:none;}</style><script>alert('儲存成功');location.href='';</script>";
       }
       else
       {
           script = "<script>alert('儲存失敗');</script>";
       }

       Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "alert", script, false);
    }

    //protected void btnChange_Click(object sender, EventArgs e)
    //{
    //    string script = "";
    //    if (btnChange.Text.Equals("變更密碼"))
    //    {
    //        btnChange.Text = "取消變更密碼";
    //        tbPWD.Enabled = true;
    //        tbPWD.Text = "";
    //        tbPWD.Focus();
    //        //tbPWD2.Enabled = true;
    //        script = "<script>document.querySelector('#tbPWD').value='';document.querySelector('#tbPWD2').value='';</script>";
    //    }
    //    else
    //    {
    //        btnChange.Text = "變更密碼";
    //        tbPWD.Text = "*************************";
    //        tbPWD.Enabled = false;
    //        //tbPWD2.Enabled = false;
    //        script = "<script>document.querySelector('#tbPWD').value='*************************';document.querySelector('#tbPWD2').value='';</script>";
    //    }
    //    Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", script, false);
    //}

    private string CheckValid(string IDF ,string Email)
    {
        StringBuilder sb = new StringBuilder();
        ValidT VT = new ValidT();
        if (VT.CheckRocID(IDF) == false)
        {
            sb.Append("身分證號:不合法\\n");
        }

        if (VT.CheckEmail(Email) == false)
        {
            sb.Append("電子信箱:不合法\\n");
        }

        return sb.ToString();
    }
}