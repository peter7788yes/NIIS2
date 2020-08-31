using System;

public partial class SelectSingleOrg : System.Web.UI.Page
{
    public string MyTreeData = "[]";

    protected void Page_Load(object sender, EventArgs e)
    {
        MyTreeData = SystemOrg.JsonList;
    }
}