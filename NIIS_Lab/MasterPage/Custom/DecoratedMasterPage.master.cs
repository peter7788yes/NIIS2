using System;
using System.IO;

public partial class MasterPage_Custom_DecoratedMasterPage : System.Web.UI.MasterPage
{
    public string HeadScript = "";
    public string BodyClass = "";
    public string FileName = "";
    public bool AutoIncludeCSS = true;
    public bool AutoIncludeJS = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        FileName = Path.GetFileName(Request.PhysicalPath).Split('.')[0];

        if (File.Exists(Request.PhysicalPath.Split('.')[0] + ".min.js"))
        {
            FileName += ".min.js";
        }
        else
        {
            FileName += ".js";
        }

        BasePage basePage = (Page as BasePage);
        if(basePage != null)
        {
            HeadScript = basePage.HeadScript;
            BodyClass = basePage.BodyClass;
        }

       
    }
}
