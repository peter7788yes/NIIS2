using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    //public int BanSingleUsed { set; get; }

    //public string  RedirectPath { set; get; }

    //public string BodyCssClass { set; get; }

    protected void Page_Load(object sender, EventArgs e)
    {
        //BodyCssClass = "";
        //BanSingleUsed = 0;
        //RedirectPath = "login.aspx";
        //base.OnInit(e);
        //Page.InitComplete += Page_InitComplete;
    }

    void Page_InitComplete(object sender, EventArgs e)
    {
        //initialization complete
        //take necessary action
        //BodyCssClass = "";
        //BanSingleUsed = 1;
        //RedirectPath = "login.aspx";
    }
 
}
