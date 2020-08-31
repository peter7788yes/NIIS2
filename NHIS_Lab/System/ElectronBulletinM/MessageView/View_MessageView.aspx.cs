using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class System_MessageViewM_View_MessageView : BasePage
{
    public System_MessageViewM_View_MessageView()
    {
        base.AddPower("/System/ElectronBulletinM/MessageView/MessageView.aspx", MyPowerEnum.瀏覽);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);
        base.BodyClass = "class='bodybg'";
    }
}