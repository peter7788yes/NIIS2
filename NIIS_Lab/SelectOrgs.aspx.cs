using System;
using System.Web.Configuration;

public partial class SelectOrgs : System.Web.UI.Page
{
    public string MyTreeData = "";
    public bool OrgArea = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToInt32(WebConfigurationManager.AppSettings["OrgAreaSet"]) == 0)
        {
            OrgArea = false;
        }
        MyTreeData = SystemOrg.JsonList;
        
    }
}