using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class System_PowerM_Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.DisableTop(true);
        base.BodyClass = "class='content'";
    }
}