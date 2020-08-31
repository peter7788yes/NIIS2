using System;
using ASP;

public partial class System_OrgManagement : BasePage
{
    public string MyTreeData = "[]";

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(false);

        ASP.masterpage_custom_decoratedmasterpage_master masterPage = this.Master as ASP.masterpage_custom_decoratedmasterpage_master;
        masterPage.AutoIncludeCSS = false;
        masterPage.AutoIncludeJS = false;

        MyTreeData = SystemOrg.JsonList;


    }
}