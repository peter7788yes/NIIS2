using System;

public partial class System_OrgManagement : BasePage
{
    public string MyTreeData = "[]";

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(false);
        MyTreeData = SystemOrg.JsonList;
    }
}