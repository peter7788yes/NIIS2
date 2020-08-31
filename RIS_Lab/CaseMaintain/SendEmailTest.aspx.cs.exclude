using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class CaseMantain_SendEmailTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        EmailUtil.SendMail("alan.wang@hyweb.com.tw", "subject", "內容");
        EmailUtil.SendMail("NIIS@hyweb.com.tw", "alan.wang@hyweb.com.tw", "subject", "內容");
        EmailUtil.SendMail("alan.wang@hyweb.com.tw", "subject with file", "內容", Server.MapPath("~/images/content_bg.jpg"));

    }
}