using System;

public partial class System_CodeM_CodeSetting : BasePage
{
    public string tbData = "";

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(true);
    }
}