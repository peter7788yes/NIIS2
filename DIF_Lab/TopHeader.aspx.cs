using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Configuration;
using System.Web.UI;
using WayneTools;

public partial class TopHeader : BasePage
{
    public string txtLogin1 { get; set; }
    public string txtLogin2 { get; set; }
    public string txtLogin3 { get; set; }
    int SystemPowerCateID = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","Post");
        base.DisableTop(true);
        base.BodyClass = "class='top'";


        UserVM user = AuthServer.GetLoginUser();
        //使用者姓名(使用者帳號, 使用者單位)於YYY.MM.DD HH:MM:SS由##使用者IP##登入

        txtLogin1 = string.Format("{0}({1}, {2})"
                                   , user.UserName
                                   , user.LoginName
                                   , user.OrgName);
        txtLogin2 = string.Format("於{0} 由 {1} 登入"
                                   , user.LoginDate.ToShortTaiwanDateTime()
                                   , IpAddressS.GetIP());


     
        if (WebConfigurationManager.AppSettings["SystemPowerCateID"] != null)
        {
            SystemPowerCateID = Convert.ToInt32(WebConfigurationManager.AppSettings["SystemPowerCateID"]);
        }

        DataTable dt = GetDataTable("ConnUser", "dbo.usp_SystemM_xGetTotalOnlineUserByCateID"
                                         , new Dictionary<string, object>()
                                         {
                                                    { "@SystemPowerCateID", SystemPowerCateID }
                                        });

        if(dt.Rows.Count>0)
            txtLogin3 = "線上人數: " + dt.Rows[0][0].ToString() + " 人";
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        var user = AuthServer.GetLoginUser();

        int Chk = 0;

        Dictionary<string, object> OutDict = new Dictionary<string, object>() { { "@Chk", Chk } };

        MSDB.ExecuteNonQuery("ConnUser", "dbo.usp_SystemM_xUpdateLogoutDate"
                                         , ref OutDict
                                         , new Dictionary<string, object>()
                                         {
                                                    { "@UserID", user.ID },
                                                    { "@LoginDate", user.LoginDate },
                                                    { "@SystemPowerCateID", SystemPowerCateID }
                                        });

        Chk = (int)OutDict["@Chk"];

        string script = "";
        //if (Chk > 0)
        //{
        //    Session.Abandon();
        //    Session.Clear();

        //    script = "<script>window.parent.location.href='/Login.aspx';</script>";
        //}
        //else
        //{
        //    script = "<script>alert('登出失敗');window.parent.onbeforeunload = function () {return '請由\"登出\"按鈕，登出系統';};</script>";
        //}
        Session.Abandon();
        Session.Clear();
        script = "<script>window.parent.location.href='/Login.aspx';</script>";
        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "GoLogout", script,false);
    }
}