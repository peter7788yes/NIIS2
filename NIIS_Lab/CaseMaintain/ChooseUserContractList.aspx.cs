using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CaseMaintain_ChooseUserContractList : System.Web.UI.Page
{
    protected string CaseID = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        CaseID= Request.Form["i"] ?? "";
        CaseIdDiv.Controls.Add(GetControlFromTag(CaseID));

    }
    public static Control GetControlFromTag(string controlTag)
    {
        Page p = new Page();
        p.AppRelativeVirtualPath = "~/";
        Control control = p.ParseControl(controlTag);
        return control;
    }
}