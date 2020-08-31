using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WayneTools;

public partial class TopHeader : BasePage
{
    public string txtLongin1 { get; set; }
    public string txtLongin2 { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","Post");
        base.DisableTop(true);
        base.BodyClass = "class='top'";


        UserVM user = AuthServer.GetLoginUser();
        //使用者姓名(使用者帳號, 使用者單位)於YYY.MM.DD HH:MM:SS由##使用者IP##登入

        txtLongin1 = string.Format("{0}({1}, {2})"
                                   , user.UserName
                                   , user.LoginName
                                   , user.OrgName);
        txtLongin2 = string.Format("於{0} 由 ##{1}## 登入"
                                   , user.LoginDate.ToShortTaiwanDateTime()
                                   , IpAddressS.GetIP());
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        //Response.Redirect("~/Login.aspx");
        string script = "<script>window.parent.location.href='/Login.aspx';</script>";
        Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "GoLogin", script,false);
    }
}