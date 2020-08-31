using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CaseMaintain_MergeCheck : System.Web.UI.Page
{
    protected string OrgID = "";
    protected string OrgName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        UserVM u = AuthServer.GetLoginUser();
        OrgID = u.OrgID.ToString();
        OrgName = u.OrgName;
    }
}