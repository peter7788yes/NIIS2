using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CaseVisit_ChooseVacList : System.Web.UI.Page
{

    public string hdid = "";
    public string tbid = "";
    protected void Page_Load(object sender, EventArgs e)
    {

          hdid = Request["hdid"] ?? "";
          tbid = Request["tbid"] ?? ""; 

    }
    public static Control GetControlFromTag(string controlTag)
    {
        Page p = new Page();
        p.AppRelativeVirtualPath = "~/";
        Control control = p.ParseControl(controlTag);
        return control;
    }
}